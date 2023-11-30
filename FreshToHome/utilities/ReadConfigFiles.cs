using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshToHome.utilities
{
    internal class ReadConfigFiles
    {
      public static  Dictionary<string, string>? properties;
        public static void ReadConfigProperty()
        {
            properties = new Dictionary<string, string>();
            string curDir = Directory.GetParent(@"../../../").FullName;
            string file = curDir + "/configSettings/config.properties";

            string[] lines = File.ReadAllLines(file);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    properties[key] = value;

                }
            }

        }
    }
}
