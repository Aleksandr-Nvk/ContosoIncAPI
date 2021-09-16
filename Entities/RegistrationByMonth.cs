namespace ContosoIncAPI.Entities
{
    public record RegistrationByMonth
    {
        public uint Year {get; set;}
        public string Month {get; set;}
        public uint UserNumber {get; set;}
    }
}