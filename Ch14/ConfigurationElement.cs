using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CSharpPhrase.CustomSection
{
    public class TraceOption : ConfigurationElement
    {
        internal class ConfigurationElement
        {
        }

        [ConfigurationProperty("enabled)")]
        public bool Enabled {
            get { return (bool)this["enabled"]; }
        }

        [ConfigurationProperty("filePath")]
        public string FilePath
        {
            get { return (string)this["filePath"]; }
        }

        [ConfigurationProperty("bufferSize")]
        public int BufferSize
        {
            get { return (int)this["bufferSize"]; }
        }

    }

    public class MyAppSettings : ConfigurationSection
    {
        [ConfigurationProperty("traceOption")]
        public TraceOption TraceOption
        {
            get { return (TraceOption)this["traceOption"]; }
            set { this["traceOption"] = value; }
        }
    }


}
