namespace ODS.HelperModels
{
    // Email required for password change
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}