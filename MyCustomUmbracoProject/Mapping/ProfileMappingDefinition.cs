using MyCustomUmbracoProject.Models.ViewModels;
using MyCustomUmbracoProject.Services.Account.Models.Response;
using Umbraco.Cms.Core.Mapping;

namespace MyCustomUmbracoProject.Mapping;

public class ProfileMappingDefinition : IMapDefinition
{
    public void DefineMaps(IUmbracoMapper mapper)
    {
        mapper.Define<GetUserProfileResponseModel, ProfileViewModel>((_, _) => new ProfileViewModel(), MapProfile);
    }

    private void MapProfile(GetUserProfileResponseModel source, ProfileViewModel target, MapperContext context)
    {
        target.Email = source.Email;
        target.FirstName = source.FirstName;
        target.LastName = source.LastName;
        target.Company = source.Company;
        target.Address = source.Address;
        target.City = source.City;
        target.Country = source.Country;
        target.Zip = source.Zip;
        target.Phone = source.Phone;
        target.Mobile = source.Mobile;
        target.EmailConfirm = source.EmailConfirm;
        target.MobileConfirm = source.MobileConfirm;
        target.CountryID = source.CountryID;
        target.Fax = source.Fax;
        target.Birthday = source.Birthday;
        target.State = source.State;
        target.PassportConfirmed = source.PassportConfirmed;
        target.CardConfirmed = source.CardConfirmed;
        target.aID = source.aID;
    }
}