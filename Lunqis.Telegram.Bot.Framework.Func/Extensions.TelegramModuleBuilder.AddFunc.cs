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
using Lunqis.Telegram.Bot.Framework.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Lunqis.Telegram.Bot.Framework.Func;
public static partial class Extensions
{
    /// <summary>
    /// Represents a module for adding functionality to a Telegram-based application.
    /// </summary>
    /// <remarks>This module is designed to integrate with the application's dependency injection system.
    /// Implementations of <see cref="ITelegramModule"/> are used to configure services and dependencies required for
    /// Telegram-related functionality.</remarks>
    private class AddFuncModule(string botCommand, Action<TelegramContext> action) : ITelegramModule, ITelegramBotBuildService
    {
        internal static readonly Dictionary<string, Action<TelegramContext>> _actions = [];

        public void AddBuildService(IServiceCollection services)
        {
            _actions.Add(botCommand, action);
        }

        public void Build(IServiceCollection services, IServiceProvider builderService)
        {
            
        }
    }
}
