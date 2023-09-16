using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Application.Models.Dtos;

public class UserForRegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public EGender Gender { get; set; }
    public string BirthYear { get; set; }
    public string BirthMonth { get; set; }
    public string BirthDay { get; set; }


}