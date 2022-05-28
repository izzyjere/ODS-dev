namespace ODS.Domain.HelperModels
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public Guid TokenKey { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}