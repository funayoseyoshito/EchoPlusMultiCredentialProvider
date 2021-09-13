using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;

namespace EchoBot
{
    public class MultiCredentialProvider : ICredentialProvider
    {
        public Dictionary<string, string> Credentials = new Dictionary<string, string>
        {
            { "{MicrosoftAppId}", "{MicrosoftAppPassword}" }, // bot1
            { "{MicrosoftAppId}", "{MicrosoftAppPassword}" }  // bot2
        };

        public Task<bool> IsValidAppIdAsync(string appId)
        {
            return Task.FromResult(this.Credentials.ContainsKey(appId));
        }

        public Task<string> GetAppPasswordAsync(string appId)
        {
            return Task.FromResult(this.Credentials.ContainsKey(appId) ? this.Credentials[appId] : null);
        }

        public Task<bool> IsAuthenticationDisabledAsync()
        {
            return Task.FromResult(!this.Credentials.Any());
        }
    }
}
