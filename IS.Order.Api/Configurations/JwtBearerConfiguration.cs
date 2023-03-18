using IS.Order.Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace IS.Order.Api.Configurations
{
    public class JwtBearerConfiguration : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly AuthSettings _authSettings;

        public JwtBearerConfiguration(IOptions<AuthSettings> authSettingsOptions)
        {
            _authSettings = authSettingsOptions.Value;
        }

        public void Configure(string name, JwtBearerOptions options)
        {
            options.Authority = _authSettings.Authority;
            // Todo: create token params provider
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateAudience = false
            };
            options.Events = new JwtBearerEvents
            {
                // Todo: Put logic into a service/event handler
                OnAuthenticationFailed = _ => Task.CompletedTask,
                OnTokenValidated = _ => Task.CompletedTask,
                OnChallenge = _ => Task.CompletedTask,
                OnForbidden = _ => Task.CompletedTask,
                OnMessageReceived = _ => Task.CompletedTask
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(string.Empty, options);
        }
    }
}
