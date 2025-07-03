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
using Microsoft.Extensions.Logging;

namespace Lunqis.Telegram.Bot.Framework;
public static partial class Extensions
{
    /// <summary>
    /// Configures the application to use console-based logging with a minimum log level of Information.
    /// </summary>
    /// <remarks>This module integrates console logging into the application's logging infrastructure. It
    /// registers the necessary services in the provided <see cref="IServiceCollection"/>.</remarks>
    private class UseConsoleLogModule : ITelegramModule
    {
        /// <summary>
        /// Configures logging services for the application.
        /// </summary>
        /// <remarks>This method adds console logging to the provided service collection and sets the
        /// minimum logging level to <see cref="LogLevel.Information"/>.</remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to which logging services will be added.</param>
        /// <param name="builderService">The <see cref="IServiceProvider"/> used to resolve dependencies during the configuration process.</param>
        public void Build(IServiceCollection services, IServiceProvider builderService)
        {
            _ = services.AddLogging(loggingBuilder =>
            {
                _ = loggingBuilder.AddConsole();
                _ = loggingBuilder.SetMinimumLevel(LogLevel.Information);
            });
        }
    }
}