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
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Lunqis.Telegram.Bot.Framework.Users;

/// <summary>
/// Defines a factory for creating Telegram requests based on incoming updates.
/// </summary>
/// <remarks>Implementations of this interface are responsible for generating instances of <see
/// cref="TelegramRequest"> that encapsulate the details of a specific interaction with the Telegram API. This allows
/// for decoupling request creation logic from the rest of the application.</remarks>
public interface IRequestFactory
{
    /// <summary>
    /// Creates a new Telegram request based on the provided parameters.
    /// </summary>
    /// <param name="botClient">The Telegram bot client used to interact with the Telegram API.</param>
    /// <param name="update">The update received from the Telegram API.</param>
    /// <param name="cancellationToken">The cancellation token to monitor for cancellation requests.</param>
    /// <returns>A new instance of <see cref="TelegramRequest"/>.</returns>
    public TelegramRequest Create(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}