public class Constants
{
    public const string RowKey = "RowKey";
    public const string PartitionKey = "PartitionKey";

    public static class Role
    {
        public const string Authenticated = "authenticated";
        public const string Administrator = "administrator";
    }
    
    public class StaticWebAppRole
    {
        public static readonly string Anonymous = "anonymous";
        public static readonly string Authenticated = "authenticated";
        public static readonly string Administrator = "administrator";
    }
}
