using System.Diagnostics;
using System.Reflection;
using QuadAutomotiveGroupSite.Properties;

namespace QuadAutomotiveGroupSite.Services
{
    public class DotNetPropertyService : IPropertyService
    {
        private readonly string _propertyPrefix;

        public DotNetPropertyService()
        {
            _propertyPrefix = Assembly.GetExecutingAssembly().GetName().Name + ".Properties.Settings.";
        }

        public T Get<T>(string key)
        {
            var prop = Settings.Default.Properties[key + (Debugger.IsAttached ? "Debug" : "")];
            return prop == null ? default(T) : (T) prop.DefaultValue;
        }

        public string GetKey(string key)
        {
            return _propertyPrefix + key + (Debugger.IsAttached ? "Debug" : "");
        }
    }
}