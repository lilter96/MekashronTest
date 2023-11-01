using MyCustomUmbracoProject.Models.ViewModels;
using MyCustomUmbracoProject.Services.Account.Models.Request;
using Umbraco.Cms.Core.Mapping;

namespace MyCustomUmbracoProject.Mapping;

public class AuthMappingDefinition : IMapDefinition
{
    public void DefineMaps(IUmbracoMapper mapper)
    {
        mapper.Define<RegisterViewModel, RegisterRequestModel>((_, _) => new RegisterRequestModel(), MapRegister);
        mapper.Define<RegisterViewModel, LoginRequestModel>((_, _) => new LoginRequestModel(), MapLogin);
    }

    private void MapRegister(RegisterViewModel source, RegisterRequestModel target, MapperContext context)
    {
        target.FirstName = source.FirstName;
        target.LastName = source.LastName;
        target.Password = source.Password;
        target.Mobile = source.Mobile;
        target.CountryId = source.CountryId;
        target.Email = source.Email;
        target.AId = source.AId;
    }
    
    private void MapLogin(RegisterViewModel source, LoginRequestModel target, MapperContext context)
    {
        target.Username = source.Email;
        target.Password = source.Password;
    }
}
