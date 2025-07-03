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
using Lunqis.Telegram.Bot.Framework.Storage;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Lunqis.Telegram.Bot.Framework.Users;

/// <summary>
/// Represents the context for handling a Telegram bot request, providing access to the request data,  services, and
/// other resources required to process the request.
/// </summary>
/// <remarks>This class encapsulates the resources and dependencies needed to handle a Telegram bot request, 
/// including the bot client, chat information, session state, and scoped services. It is designed to  be used within
/// the scope of a single request and should be disposed of when no longer needed.</remarks>
public sealed class TelegramContext : IDisposable
{
    /// <summary>
    /// Gets the request details for the Telegram API operation.
    /// </summary>
    public TelegramRequest TelegramRequest { get; internal set; }

    #region TelegramRequest 快速属性

    /// <summary>
    /// Gets the <see cref="ITelegramBotClient"/> instance used to interact with the Telegram Bot API.
    /// </summary>
    public ITelegramBotClient TelegramBotClient => TelegramRequest.TelegramBotClient;

    /// <summary>
    /// Gets the identifier of the chat associated with the current Telegram request.
    /// </summary>
    public ChatId? ChatId => TelegramRequest.ChatId;
    #endregion

    /// <summary>
    /// Gets the current session associated with the application.
    /// </summary>
    public ISession Session { get; }

    /// <summary>
    /// Gets the current <see cref="IServiceScope"/> associated with the service.
    /// </summary>
    public IServiceScope ServiceScope { get; }

    /// <summary>
    /// Gets the service provider associated with the current scope.
    /// </summary>
    public IServiceProvider ScopeServiceProvider => ServiceScope.ServiceProvider;

    /// <summary>
    /// Gets or sets the <see cref="CancellationToken"/> used to signal cancellation for the associated operation.
    /// </summary>
    public CancellationToken CancellationToken { get; set; } = default!;

    /// <summary>
    /// Initializes a new instance of the <see cref="TelegramContext"/> class using the specified service provider and
    /// Telegram request.
    /// </summary>
    /// <remarks>This constructor creates a scoped service provider to manage the lifetime of dependencies
    /// during the operation.</remarks>
    /// <param name="serviceProvider">The service provider used to resolve dependencies for the context.</param>
    /// <param name="telegramRequest">The Telegram request containing the data and metadata for the current operation.</param>
    internal TelegramContext(IServiceProvider serviceProvider, TelegramRequest telegramRequest) : this(serviceProvider.CreateScope(), telegramRequest) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelegramContext"/> class, providing access to the current service
    /// scope, session, and Telegram request.
    /// </summary>
    /// <remarks>This constructor initializes the session by resolving the <see cref="ISession"/> service from
    /// the provided service scope. Ensure that the <paramref name="serviceScope"/> contains all required services for
    /// proper operation.</remarks>
    /// <param name="serviceScope">The service scope used to resolve dependencies within the current context. Cannot be null.</param>
    /// <param name="telegramRequest">The incoming Telegram request associated with this context. Cannot be null.</param>
    internal TelegramContext(IServiceScope serviceScope, TelegramRequest telegramRequest)
    {
        ServiceScope = serviceScope;

        Session = ScopeServiceProvider.GetRequiredService<ISession>();
        TelegramRequest = telegramRequest;
    }

    /// <summary>
    /// Releases all resources used by the current instance of the class.
    /// </summary>
    /// <remarks>This method disposes the underlying service scope, freeing any resources it holds.  After
    /// calling this method, the instance should not be used further.</remarks>
    public void Dispose() =>
        ServiceScope.Dispose();
}
