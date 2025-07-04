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
/// Provides functionality to manage and retrieve actions associated with bot commands.
/// </summary>
/// <remarks>This class maintains a collection of bot commands mapped to their corresponding actions. It allows
/// retrieval of actions based on a specified bot command.</remarks>
/// <param name="data"></param>
internal class FuncManager(Dictionary<string, Action<TelegramContext>> data) : IFuncManager
{
    /// <summary>
    /// Represents a collection of actions mapped to string keys for processing <see cref="TelegramContext"/> instances.
    /// </summary>
    /// <remarks>This dictionary is used to associate specific string keys with corresponding actions that
    /// operate on  <see cref="TelegramContext"/> objects. It is intended for internal use to facilitate efficient
    /// lookup and execution  of context-specific operations.</remarks>
    private readonly Dictionary<string, Action<TelegramContext>> _data = new(data);

    /// <summary>
    /// Retrieves the action associated with the specified bot command.
    /// </summary>
    /// <param name="botCommand">The bot command for which the associated action is to be retrieved. Cannot be null or empty.</param>
    /// <returns>An <see cref="Action{TelegramContext}"/> representing the action associated with the specified bot command. If
    /// the command is not found, returns a default non-null action.</returns>
    public Action<TelegramContext> GetFunc(string botCommand)
    {
        _data.TryGetValue(botCommand, out var action);
        return action!;
    }
}
