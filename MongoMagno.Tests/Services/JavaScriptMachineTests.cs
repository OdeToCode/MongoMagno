﻿using MongoMagno.Services.JsVm;
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
                vm.Execute("db = new Database();");
                Assert.NotNull(vm.Script.db);
            }
        }

        [Fact]
        public void Can_Create_Db_Collections()
        {
            using (var vm = new JavaScriptMachine())
            {
                vm.CreateEnvironment(new[] {"collection_a", "collection_b"});

                Assert.NotNull(vm.Script.db.collection_a);
            }
        }
    }
}