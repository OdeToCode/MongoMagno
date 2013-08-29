using MongoMagno.Services.JsVm;
using Xunit;

namespace MongoMagno.Tests.Services
{
    public class JavaScriptMachineTests
    {
        [Fact]
        public void Can_Execute_Script_With_Defaults()
        {
            using (var vm = new JavaScriptMachine())
            {
                vm.Execute("db = new Database('test');");

                Assert.Equal("test", vm.Script.db.name);
            }
        }
    }
}