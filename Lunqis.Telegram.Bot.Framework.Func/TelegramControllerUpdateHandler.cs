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
using Lunqis.Telegram.Bot.Framework.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Lunqis.Telegram.Bot.Framework.Func;

/// <summary>
/// Handles updates and errors received from a Telegram bot client.
/// </summary>
/// <remarks>This class implements the <see cref="IUpdateHandler"/> interface to process incoming updates and
/// errors from the Telegram bot client. It uses dependency injection to resolve required services, including logging,
/// context management, request creation, and function execution.</remarks>
/// <param name="serviceProvider"></param>
internal class TelegramControllerUpdateHandler(IServiceProvider serviceProvider) : IUpdateHandler
{
    /// <summary>
    /// Represents the logger instance used to log messages related to the <see
    /// cref="TelegramControllerUpdateHandler"/>.
    /// </summary>
    /// <remarks>This logger is initialized using the provided <see cref="IServiceProvider"/> and may be null
    /// if the service provider does not contain a registered <see cref="ILogger{T}"/> for <see
    /// cref="TelegramControllerUpdateHandler"/>.</remarks>
    private readonly ILogger<TelegramControllerUpdateHandler>? _logger = serviceProvider.GetService<ILogger<TelegramControllerUpdateHandler>>();

    /// <summary>
    /// Provides access to the factory responsible for creating context instances.
    /// </summary>
    /// <remarks>This field is initialized using the required service provider to ensure that the context
    /// factory is properly resolved. It is intended for internal use and should not be exposed directly.</remarks>
    private readonly IContextFactory _contextFactory = serviceProvider.GetRequiredService<IContextFactory>();

    /// <summary>
    /// Represents the factory used to create HTTP requests.
    /// </summary>
    /// <remarks>This field is initialized using the service provider to ensure dependency injection. It is
    /// intended for internal use and should not be accessed directly outside of this class.</remarks>
    private readonly IRequestFactory _requestFactory = serviceProvider.GetRequiredService<IRequestFactory>();

    /// <summary>
    /// Provides access to the function management service used for handling operations related to functions.
    /// </summary>
    /// <remarks>This field is initialized using the service provider to ensure dependency injection of the
    /// required <see cref="IFuncManager"/> implementation. It is intended for internal use within the class and should
    /// not be accessed directly by external code.</remarks>
    private readonly IFuncManager _funcManager = serviceProvider.GetRequiredService<IFuncManager>();

    /// <summary>
    /// Handles errors that occur during the processing of updates from the Telegram bot client.
    /// </summary>
    /// <remarks>This method logs the error details, including the exception and its source, for diagnostic
    /// purposes. It does not attempt to recover from the error or modify the bot client's state.</remarks>
    /// <param name="botClient">The Telegram bot client instance that encountered the error.</param>
    /// <param name="exception">The exception that was thrown during the update handling process.</param>
    /// <param name="source">The source of the error, indicating the context in which the error occurred.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        _logger?.LogError(exception, "An error occurred while handling an update from the Telegram bot client. Source: {Source}", source);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Handles an incoming update from the Telegram Bot API asynchronously.
    /// </summary>
    /// <remarks>This method processes the incoming update by creating a request and user context, determining
    /// the appropriate action based on the bot command,  and executing the action. If no context or action is found,
    /// the method exits without performing any operations.</remarks>
    /// <param name="botClient">The Telegram bot client instance used to process the update.</param>
    /// <param name="update">The update received from the Telegram Bot API, containing information such as messages, commands, or callback
    /// queries.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests, allowing the operation to be canceled if needed.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var request = _requestFactory.Create(botClient, update, cancellationToken);
        var context = _contextFactory.GetOrCreateUserContext(serviceProvider, request);

        if (context == null)
            return;

        var action = _funcManager.GetFunc(context.TelegramRequest.BotCommand);

        if (action == null)
            return;

        action(context);

        await Task.CompletedTask;
    }
}
