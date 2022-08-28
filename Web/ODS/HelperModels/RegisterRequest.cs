namespace ODS.HelperModels
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare(nameof(Password), ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
        public string? PhoneNumber { get; set; }

        public bool ActivateUser { get; set; } = false;
        public bool AutoConfirmEmail { get; set; } = false;
    }
}