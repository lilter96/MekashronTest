using System.Security.Claims;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Security;
using EncryptionHelper = MyCustomUmbracoProject.Helpers.EncryptionHelper;

namespace MyCustomUmbracoProject.Security;

public class CurrentUserContext
{
    private const string EncryptedPasswordAlias = "encryptedPassword";
    private const string EntityIdAlias = "entityId";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMemberManager _memberManager;
    private readonly IMemberService _memberService;
    private readonly IMemberSignInManager _memberSignInManager;
    private readonly ILogger<CurrentUserContext> _logger;

    public CurrentUserContext(
        IHttpContextAccessor httpContextAccessor,
        IMemberManager memberManager,
        IMemberService memberService,
        ILogger<CurrentUserContext> logger,
        IMemberSignInManager memberSignInManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _memberManager = memberManager;
        _memberService = memberService;
        _logger = logger;
        _memberSignInManager = memberSignInManager;
    }

    private CurrentUserInfo CurrentUser { get; set; }

    public async Task SignInAsync(string email, string entityId, string password, string fullName = "")
    {
        var member = _memberService.GetByEmail(email);

        var identityUser = await _memberManager.FindByEmailAsync(email);

        if (member == null)
        {
            identityUser = MemberIdentityUser.CreateNew(email, email, Constants.Security.DefaultMemberTypeAlias, true,
                fullName);
            var identityResult = await _memberManager.CreateAsync(identityUser, password);

            if (!identityResult.Succeeded)
            {
                _logger.LogError("Register: Can not register new user");
                return;
            }

            member = _memberService.GetByEmail(email);
        }

        member.SetValue(EncryptedPasswordAlias, EncryptionHelper.Encrypt(password));
        member.SetValue(EntityIdAlias, entityId);
        _memberService.Save(member);

        var isValid = identityUser != null && await _memberManager.CheckPasswordAsync(identityUser, password);

        if (isValid)
        {
            await _memberSignInManager.SignInAsync(identityUser, true);
        }
    }

    public async Task SignOutAsync()
    {
        var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrWhiteSpace(email))
        {
            _logger.LogWarning("Attempt to sign out a non-authenticated user");
            return;
        }

        var member = _memberService.GetByEmail(email);
        if (member != null)
        {
            member.SetValue(EncryptedPasswordAlias, string.Empty);
            _memberService.Save(member);
        }

        CurrentUser = null;
        
        await _memberSignInManager.SignOutAsync();
    }

    public CurrentUserInfo GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated && CurrentUser != null)
        {
            return CurrentUser;
        }

        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            return null;
        }
        
        var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        var member = _memberService.GetByEmail(email);

        if (member == null)
        {
            _logger.LogWarning($"User with email {email} was not found in the system.");
            return null;
        }
        
        CurrentUser =  new CurrentUserInfo
        {
            Email = email,
            Password = EncryptionHelper.Decrypt(member.GetValue<string>(EncryptedPasswordAlias)),
            EntityId = int.Parse(member.GetValue<string>(EntityIdAlias))
        };

        return CurrentUser;
    }
}