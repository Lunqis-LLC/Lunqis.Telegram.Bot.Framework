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

namespace Lunqis.Telegram.Bot.Framework.Func;

/// <summary>
/// Defines a contract for managing and retrieving functions associated with bot commands.
/// </summary>
/// <remarks>This interface is designed to provide a mechanism for mapping bot commands to their corresponding
/// actions. Implementations of this interface should ensure that the returned action is appropriate for the specified
/// bot command.</remarks>
internal interface IFuncManager
{
    /// <summary>
    /// Retrieves an action associated with the specified bot command.
    /// </summary>
    /// <param name="botCommand">The bot command for which the corresponding action is requested. Cannot be null or empty.</param>
    /// <returns>An <see cref="Action{TelegramContext}"/> that represents the operation to be performed for the given bot
    /// command. Returns <see langword="null"/> if no action is associated with the specified command.</returns>
    public Action<TelegramContext> GetFunc(string botCommand);
}
