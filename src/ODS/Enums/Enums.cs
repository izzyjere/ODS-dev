namespace ODS.Enums
{
    public enum PaymentMethod
    {
        [Description("MTN")]
        MTNMomo = 1,
        [Description("Airtel")]
        AirtelMoney,
        [Description("Zamtel")]
        ZamtelKwacha
    }
    public enum DonationType
    {
        None,
        Money,
        Item
    }
    public enum AuditType
    {
        None,
        Create,
        Update,
        Delete
    }
}
