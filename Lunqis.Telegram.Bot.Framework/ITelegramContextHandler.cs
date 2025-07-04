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

namespace Lunqis.Telegram.Bot.Framework;

/// <summary>
/// Defines a contract for handling Telegram context operations.
/// </summary>
/// <remarks>Implementations of this interface process a <see cref="TelegramContext"/> object,  which represents
/// the context of a Telegram interaction. This may include handling  incoming messages, commands, or other
/// Telegram-specific data.</remarks>
public interface ITelegramContextHandler
{
    /// <summary>
    /// Processes the provided Telegram context and handles the associated operations asynchronously.
    /// </summary>
    /// <param name="telegramContext">The context containing information about the Telegram message or event to be processed.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation will be canceled if the token is triggered.</param>
    /// <returns>A task representing the asynchronous operation. The task completes when the Telegram context has been fully
    /// processed.</returns>
    public Task HandleTelegramContext(TelegramContext telegramContext, CancellationToken cancellationToken = default);
}
