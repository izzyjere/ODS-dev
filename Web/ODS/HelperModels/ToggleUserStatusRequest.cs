namespace ODS.HelperModels
{
    public class ToggleUserStatusRequest
    {
        public bool ActivateUser { get; set; }
        public int UserId { get; set; }
    }
}