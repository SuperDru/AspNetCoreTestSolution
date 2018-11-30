namespace WebApp
{
    public class Account
    {
        public string UserName { get; set; }
        public string ExternalId { get; set; }
        public long InternalId { get; set; }
        public int Counter { get; set; }
        public string Role { get; set; }

        public Account Clone() => (Account) MemberwiseClone();

        public override bool Equals(object obj)
        {
            var acc = obj as Account;

            bool result = 
                acc?.ExternalId == ExternalId &&
                acc?.InternalId == InternalId &&
                acc?.UserName == UserName &&
                acc?.Counter == Counter &&
                acc?.Role == Role;

            return result;
        }
    }
}