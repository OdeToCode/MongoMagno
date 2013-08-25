using MongoDB.Driver;

namespace MongoMagno.Services
{
    public abstract class CommandExecutor
    {       
    }
    
    [CommandMatch("^db.(?<collection>\\w*).find\\(")]
    public class FindExecutor : CommandExecutor
    {        
    }
}