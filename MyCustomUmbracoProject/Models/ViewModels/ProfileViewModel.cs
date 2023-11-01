using Umbraco.Cms.Core.Models;

namespace MyCustomUmbracoProject.Models.ViewModels;

public class ProfileViewModel
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
    
    public string Company { get; set; }
    
    public string Address { get; set; }
    
    public string City { get; set; }
    
    public string Country { get; set; }
    
    public string Zip { get; set; }
    
    public string Phone { get; set; }
    
    public string Mobile { get; set; }
    
    public string Email { get; set; }
    
    public int EmailConfirm { get; set; }
    
    public int MobileConfirm { get; set; }
    
    public int CountryID { get; set; }
    
    public string Fax { get; set; }
    
    public string Birthday { get; set; }
    
    public string State { get; set; }
    
    public int PassportConfirmed { get; set; }
    
    public int CardConfirmed { get; set; }
    
    public int aID { get; set; }
}