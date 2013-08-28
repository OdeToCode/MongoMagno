using System.IO;
using System.Reflection;
using Microsoft.ClearScript.Windows;

namespace MongoMagno.Services.JsVm
{
    public class JavaScriptMachine : JScriptEngine
    {
        public JavaScriptMachine()
        {
            LoadDefaultScripts();
        }

        void LoadDefaultScripts()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var baseName in _scripts)
            {
                var fullName = _scriptNamePrefix + baseName;
                using (var stream = assembly.GetManifestResourceStream(fullName))
                using(var reader = new StreamReader(stream))
                {
                    var contents = reader.ReadToEnd();
                    Execute(contents);
                }
            }
        }

        string _scriptNamePrefix = "MongoMagno.Services.ExecutorScripts.";
        string[] _scripts = new[]
            {
                "Database.js", "Collection.js"
            };
    }
}