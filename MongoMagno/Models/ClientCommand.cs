namespace MongoMagno.Models
{
    public class ClientCommand
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string CommandText { get; set; }
    }
}