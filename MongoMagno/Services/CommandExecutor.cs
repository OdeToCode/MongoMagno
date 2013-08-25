using MongoDB.Driver;

namespace MongoMagno.Services
{
    public interface CommandExecutor
    {       
    }
    
    [CommandMatch("^db.(?<collection>\\w*).find\\(")]
    public class FindExecutor : CommandExecutor
    {        
    }
}