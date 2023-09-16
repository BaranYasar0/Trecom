using Trecom.Client.MvcClient.Models.Enums;

namespace Trecom.Client.MvcClient.Models.InputModels;

public class SignUpInputModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BirthYear { get; set; }
    public string BirthMonth { get; set; }
    public string BirthDay { get; set; }
    public EGender Gender { get; set; }

}