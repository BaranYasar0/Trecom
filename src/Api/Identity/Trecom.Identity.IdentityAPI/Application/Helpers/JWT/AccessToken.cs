namespace Trecom.Api.Identity.Application.Helpers.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}