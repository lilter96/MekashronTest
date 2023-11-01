using MyCustomUmbracoProject.Mapping;
using MyCustomUmbracoProject.Security;
using MyCustomUmbracoProject.Services.Account;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Mapping;

namespace MyCustomUmbracoProject.Composing;

public class RegisterServicesComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddScoped<CurrentUserContext>();
        builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>()
            .Add<AuthMappingDefinition>()
            .Add<ProfileMappingDefinition>();
    }
}