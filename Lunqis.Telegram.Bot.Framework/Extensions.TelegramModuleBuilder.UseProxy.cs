//MIT License

//Copyright (c) 2025-2025 Lunqis LLC

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using Lunqis.Telegram.Bot.Framework.Bot;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Lunqis.Telegram.Bot.Framework;
public static partial class Extensions
{
    /// <summary>
    /// Configures a proxy for HTTP client communication in a Telegram bot module.
    /// </summary>
    /// <remarks>This class implements <see cref="ITelegramModule"/> and <see
    /// cref="ITelegramBotBuildService"/> to  provide proxy configuration for HTTP requests made by the Telegram bot. It
    /// allows specifying  the proxy URI, port, and optional credentials (username and password).</remarks>
    private class UseProxyModule : ITelegramModule, ITelegramBotBuildService
    {
        /// <summary>
        /// Represents the URI associated with the current instance.
        /// </summary>
        /// <remarks>This field is read-only and is intended to store the URI value used internally by the
        /// class.</remarks>
        private readonly string _uri;

        /// <summary>
        /// Represents the port number used for network communication.  This value is optional and may be null if no
        /// specific port is configured.
        /// </summary>
        private readonly int? _port;

        /// <summary>
        /// Represents the username associated with the current instance.
        /// </summary>
        /// <remarks>This field is read-only and may be null if no username is provided.</remarks>
        private readonly string? _username;

        /// <summary>
        /// Represents the password associated with the current instance.
        /// </summary>
        /// <remarks>This field is read-only and may be null if no password is set. It is intended for
        /// internal use only and should not be exposed directly.</remarks>
        private readonly string? _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="UseProxyModule"/> class with the specified proxy settings.
        /// </summary>
        /// <remarks>Use this constructor to configure a proxy server for network requests. Ensure that
        /// the <paramref name="uri"/> is a valid URI. If authentication is required, provide both <paramref
        /// name="username"/> and <paramref name="password"/>.</remarks>
        /// <param name="uri">The URI of the proxy server. This parameter cannot be null or empty.</param>
        /// <param name="port">The port number of the proxy server. If null, the default port for the protocol will be used.</param>
        /// <param name="username">The username for proxy authentication. If null, no authentication will be performed.</param>
        /// <param name="password">The password for proxy authentication. If null, no authentication will be performed.</param>
        public UseProxyModule(string uri, int? port = null, string? username = null, string? password = null)
        {
            _uri = uri;
            _port = port;
            _username = username;
            _password = password;
        }

        /// <summary>
        /// Configures and registers a build service in the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <remarks>This method registers a singleton delegate that configures an <see
        /// cref="HttpClient"/> instance with a proxy based on the internal settings. The proxy configuration includes
        /// optional credentials if a username and password are provided.</remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the build service will be added.</param>
        public void AddBuildService(IServiceCollection services)
        {
            services.AddSingleton<Action<InternalSettings>>((setting) =>
            {
                UriBuilder uriBuilder = new(_uri);
                if (_port.HasValue)
                {
                    uriBuilder.Port = _port.Value;
                }

                setting.HttpClient = new HttpClient(new HttpClientHandler
                {
                    Proxy = new WebProxy(uriBuilder.Uri)
                    {
                        Credentials = string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password)
                            ? null
                            : new NetworkCredential(_username, _password)
                    },
                    UseProxy = true,
                    AllowAutoRedirect = true,
                    UseCookies = true
                });
            });
        }

        public void Build(IServiceCollection services, IServiceProvider builderService)
        {

        }
    }
}
