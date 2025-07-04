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
using Microsoft.Extensions.DependencyInjection;

namespace Lunqis.Telegram.Bot.Framework.Users;

/// <summary>
/// Defines a factory for creating and managing user contexts in the application.
/// </summary>
/// <remarks>This interface provides methods to retrieve or create user contexts based on incoming requests. It
/// supports both persistent and one-time contexts, depending on the method used.</remarks>
public interface IContextFactory
{
    /// <summary>
    /// Retrieves the existing user context for the specified Telegram request, or creates a new one if none exists.
    /// </summary>
    /// <remarks>This method ensures that a user context is available for processing the given Telegram
    /// request.  If no existing context is found, a new one may be created using the provided service
    /// provider.</remarks>
    /// <param name="botServiceProvider">The service provider used to resolve dependencies required for creating or retrieving the user context.</param>
    /// <param name="request">The Telegram request containing user-specific information used to identify or create the context.</param>
    /// <returns>A <see cref="TelegramContext"/> instance representing the user context associated with the request,  or <see
    /// langword="null"/> if the context could not be created or retrieved.</returns>
    TelegramContext? GetOrCreateUserContext(IServiceProvider botServiceProvider, TelegramRequest request);

    /// <summary>
    /// Retrieves a one-time user context for processing a Telegram request.
    /// </summary>
    /// <remarks>This method is intended for scenarios where a user context is required for a single operation
    /// and will not be reused. Ensure proper disposal of the returned <see cref="TelegramContext"/> if
    /// applicable.</remarks>
    /// <param name="serviceScope">The service scope used to resolve dependencies required for creating the user context. Cannot be null.</param>
    /// <param name="telegramRequest">The Telegram request containing the data necessary to build the user context. Cannot be null.</param>
    /// <returns>A <see cref="TelegramContext"/> instance representing the user context for the given Telegram request.</returns>
    TelegramContext GetOneTimeUserContext(IServiceScope serviceScope, TelegramRequest telegramRequest);
}
