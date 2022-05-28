namespace ODS.Domain.HelperModels
{
    public class ChangePasswordRequest
    {
        // change password requirements.

        [Required]
        public string Password { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}