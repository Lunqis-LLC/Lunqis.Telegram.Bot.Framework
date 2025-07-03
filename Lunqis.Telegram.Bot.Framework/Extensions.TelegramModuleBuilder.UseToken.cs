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

namespace Lunqis.Telegram.Bot.Framework;
public static partial class Extensions
{
    /// <summary>
    /// Represents a module that configures a Telegram bot using a specified token.
    /// </summary>
    /// <remarks>This module implements <see cref="ITelegramModule"/> and <see
    /// cref="ITelegramBotBuildService"/>  to provide functionality for adding and building services required for a
    /// Telegram bot.</remarks>
    /// <param name="token"></param>
    private class UseTokenModule(string token) : ITelegramModule, ITelegramBotBuildService
    {
        /// <summary>
        /// Represents the authentication token used for secure communication.
        /// </summary>
        /// <remarks>This field is read-only and initialized with a value provided at construction.  It is
        /// used internally to authenticate requests and should not be modified.</remarks>
        private readonly string _token = token;

        /// <summary>
        /// Configures the build service by adding a singleton action to the specified service collection.
        /// </summary>
        /// <remarks>The added singleton action configures an instance of <c>InternalSettings</c> by
        /// setting its <c>Token</c> property. The <c>_token</c> field must not be null or empty; otherwise, an <see
        /// cref="ArgumentException"/> is thrown.</remarks>
        /// <param name="services">The service collection to which the build service configuration will be added. Cannot be null.</param>
        public void AddBuildService(IServiceCollection services)
        {
            services.AddSingleton<Action<InternalSettings>>(settings =>
            {
                ArgumentException.ThrowIfNullOrEmpty(_token, nameof(_token));
                settings.Token = _token;
            });
        }

        public void Build(IServiceCollection services, IServiceProvider builderService)
        {

        }
    }
}
