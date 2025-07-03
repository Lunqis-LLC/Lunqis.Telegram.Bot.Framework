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

namespace Lunqis.Telegram.Bot.Framework;
public static partial class Extensions
{
    /// <summary>
    /// Initializes the Telegram bot by adding the required module to the builder.
    /// </summary>
    /// <param name="telegramModuleBuilder">The <see cref="ITelegramModuleBuilder"/> instance to which the initialization module will be added.</param>
    /// <returns>The updated <see cref="ITelegramModuleBuilder"/> instance with the initialization module added.</returns>
    internal static ITelegramModuleBuilder InitTelegramBot(this ITelegramModuleBuilder telegramModuleBuilder)
    {
        /// Adds the initialization module to the Telegram module builder.
        return telegramModuleBuilder.AddModule(new InitTelegramBotModule());
    }

    /// <summary>
    /// Configures the Telegram module builder to use a proxy for network communication.
    /// </summary>
    /// <remarks>Use this method to configure a proxy for the Telegram module builder, enabling network
    /// requests to be routed through the specified proxy server. Ensure that the provided URI and port correspond to a
    /// valid proxy server. If authentication is required, supply the appropriate username and password.</remarks>
    /// <param name="telegramModuleBuilder">The <see cref="ITelegramModuleBuilder"/> instance to which the proxy configuration will be applied.</param>
    /// <param name="uri">The URI of the proxy server. This parameter cannot be null or empty.</param>
    /// <param name="port">The port number of the proxy server. If not specified, the default port for the proxy protocol will be used.</param>
    /// <param name="username">The username for proxy authentication, if required. Pass <see langword="null"/> if authentication is not needed.</param>
    /// <param name="password">The password for proxy authentication, if required. Pass <see langword="null"/> if authentication is not needed.</param>
    /// <returns>The <see cref="ITelegramModuleBuilder"/> instance with the proxy module configured.</returns>
    public static ITelegramModuleBuilder UseProxy(this ITelegramModuleBuilder telegramModuleBuilder, string uri, int? port = null, string? username = null, string? password = null)
    {
        /// Adds the proxy module to the Telegram module builder.
        return telegramModuleBuilder.AddModule(new UseProxyModule(uri, port, username, password));
    }

    /// <summary>
    /// Configures the Telegram module builder to use an in-memory session for managing user state.
    /// </summary>
    /// <remarks>This method adds an in-memory session module to the Telegram module builder, enabling session
    /// management without requiring external storage. It is suitable for scenarios where lightweight, transient state
    /// management is sufficient.</remarks>
    /// <param name="telegramModuleBuilder">The <see cref="ITelegramModuleBuilder"/> instance to configure.</param>
    /// <returns>The configured <see cref="ITelegramModuleBuilder"/> instance, allowing for further chaining of module
    /// configurations.</returns>
    public static ITelegramModuleBuilder UseMemorySession(this ITelegramModuleBuilder telegramModuleBuilder)
    {
        /// Adds the session options module to the Telegram module builder.
        return telegramModuleBuilder.AddModule(new UseMemorySessionModule());
    }

    /// <summary>
    /// Adds a dictionary-based session management module to the Telegram module builder.
    /// </summary>
    /// <remarks>This method enables session management using an in-memory dictionary. It is suitable for
    /// scenarios where session data does not need to persist beyond the application's lifetime or where a lightweight
    /// session management solution is sufficient.</remarks>
    /// <param name="telegramModuleBuilder">The <see cref="ITelegramModuleBuilder"/> instance to which the dictionary session module will be added.</param>
    /// <returns>The same <see cref="ITelegramModuleBuilder"/> instance, allowing for method chaining.</returns>
    public static ITelegramModuleBuilder UseDictionarySession(this ITelegramModuleBuilder telegramModuleBuilder)
    {
        /// Adds the dictionary session module to the Telegram module builder.
        return telegramModuleBuilder.AddModule(new UseDictionarySessionModule());
    }

    /// <summary>
    /// Adds a token module to the Telegram module builder.
    /// </summary>
    /// <param name="telegramModuleBuilder">The <see cref="ITelegramModuleBuilder"/> instance to which the token module will be added.</param>
    /// <param name="token">The bot token used to authenticate with the Telegram API. Cannot be <see langword="null"/> or empty.</param>
    /// <returns>The same <see cref="ITelegramModuleBuilder"/> instance, allowing for method chaining.</returns>
    public static ITelegramModuleBuilder UseToken(this ITelegramModuleBuilder telegramModuleBuilder, string token)
    {
        /// Adds the token module to the Telegram module builder.
        return telegramModuleBuilder.AddModule(new UseTokenModule(token));
    }
}
