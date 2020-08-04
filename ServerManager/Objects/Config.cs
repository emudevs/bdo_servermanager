using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.Objects
{
    public class Config
    {
        public string SourcePath;
        public string ServerPath;
        public bool Exists;

        public void Save(string path)
        {
            Exists = true;

            if (!Directory.Exists(path.Replace("\\config.json", ""))) Directory.CreateDirectory(path.Replace("\\config.json", ""));
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
