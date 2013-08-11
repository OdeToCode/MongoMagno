using System.Collections.Generic;
using System.Web.Http;
using AttributeRouting.Web.Http;
using MongoMagno.Services;

namespace MongoMagno.Controllers
{
    public class ServerController : ApiController
    {     
        [GET("api/server/{server}")]
        public IEnumerable<string> Get(string server, string username = null, string password = null)
        {
            var db = new MongoDb(server, username, password);
            return db.GetDatabaseNames();
        }       
    }
}