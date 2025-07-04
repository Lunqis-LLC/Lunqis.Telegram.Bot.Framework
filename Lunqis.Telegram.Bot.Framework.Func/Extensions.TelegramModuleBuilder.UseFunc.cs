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
using Telegram.Bot.Polling;

namespace Lunqis.Telegram.Bot.Framework.Func;
public static partial class Extensions
{
    /// <summary>
    /// Provides functionality to configure services for the controller module in a Telegram application.
    /// </summary>
    /// <remarks>This module integrates controller-related services into the application's dependency
    /// injection system. Use the <see cref="Build"/> method to register required services and dependencies.</remarks>
    private class UseFuncModule : ITelegramModule
    {
        /// <summary>
        /// Configures the service collection to use the controller module.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        /// <param name="builderService">The service provider used during the configuration process.</param>
        public void Build(IServiceCollection services, IServiceProvider builderService)
        {
            services.AddSingleton<IUpdateHandler, TelegramControllerUpdateHandler>();
            services.AddSingleton<IFuncManager>(new FuncManager(AddFuncModule._actions));
        }
    }
}
