# EchoPlusMultiCredentialProvider

This is an example of code for use multiple Azure bot services.

This bot has been created using [Bot Framework](https://dev.botframework.com), it shows how to create a simple bot that accepts input from the user and echoes it back.

## I created this code by following the steps below.

* Download template

```
dotnet new echobot -lang C#
```

* Add MultiCredentialProvider

```CSharp
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector.Authentication;

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

```

* Add Startup

```CSharp
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddSingleton<ICredentialProvider, MultiCredentialProvider>(); <= added

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();
```

* Change AdapterWithErrorHandler
```CSharp
        public AdapterWithErrorHandler(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger)
            : base(configuration, logger)
```
↓

```CSharp
        public AdapterWithErrorHandler(ICredentialProvider credentialProvider, ILogger<BotFrameworkHttpAdapter> logger)
            : base(credentialProvider)
```
