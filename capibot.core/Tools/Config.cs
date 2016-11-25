using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Capibot.Core.Tools
{
    public static class Config
    {
        public static string GetToken(string tokenName)
        {
            if (tokenName.IsNullOrEmpty()) {
                return '';
            }
            
            var section = ConfigurationManager.GetSection("APIToken") as NameValueCollection;
            return section[tokenName];
        }

    }
}
