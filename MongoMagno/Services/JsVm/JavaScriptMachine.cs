﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.ClearScript.Windows;

namespace MongoMagno.Services.JsVm
{
    public interface IJavaScriptMachine : IDisposable
    {
        void CreateEnvironment(string[] collectionNames);
        dynamic Evaluate(string expression);
    }

    public class JavaScriptMachine : JScriptEngine, IJavaScriptMachine
    {      
        public JavaScriptMachine()
            :base(WindowsScriptEngineFlags.EnableDebugging)
        {
            LoadDefaultScripts();                
        }
        
        public void CreateEnvironment(string[] collectionNames)
        {
            Script.environment.createDatabase();
            Script.environment.createCollections(collectionNames);
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

        private const string _scriptNamePrefix = "MongoMagno.Services.ExecutorScripts.";
        readonly string[] _scripts = new[]
            {
                "Database.js", "Collection.js", "Environment.js"
            };       
    }
}