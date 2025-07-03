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
using Telegram.Bot;

namespace Lunqis.Telegram.Bot.Framework;
public static partial class Extensions
{
    /// <summary>
    /// Provides functionality to initialize and configure services required for a Telegram bot.
    /// </summary>
    /// <remarks>This class implements <see cref="ITelegramModule"/> and <see
    /// cref="ITelegramBotBuildService"/> to  facilitate the setup of necessary dependencies for a Telegram bot,
    /// including internal settings  and the bot client. It ensures that required configurations, such as the bot token,
    /// are validated  and applied during the build process.</remarks>
    private class InitTelegramBotModule : ITelegramModule, ITelegramBotBuildService
    {
        /// <summary>
        /// Adds the build service dependencies to the specified service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the dependencies will be added. Cannot be null.</param>
        public void AddBuildService(IServiceCollection services)
        {
            services.AddSingleton<InternalSettings>();
        }

        /// <summary>
        /// Configures and registers the required services for the Telegram bot client.
        /// </summary>
        /// <remarks>This method retrieves and configures internal settings for the Telegram bot client.
        /// It ensures that the required token is provided and initializes a <see cref="TelegramBotClient"/> instance,
        /// which is then registered as a singleton service.</remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the Telegram bot client will be added.</param>
        /// <param name="builderService">The <see cref="IServiceProvider"/> used to resolve dependencies required for configuration.</param>
        public void Build(IServiceCollection services, IServiceProvider builderService)
        {
            InternalSettings internalSettings = builderService.GetRequiredService<InternalSettings>();
            builderService.GetServices<Action<InternalSettings>>()
                .ToList()
                .ForEach(setting => setting.Invoke(internalSettings));

            ArgumentException.ThrowIfNullOrEmpty(internalSettings.Token, nameof(internalSettings.Token));

            TelegramBotClient botClient = new(internalSettings.Token, internalSettings.HttpClient);

            services.AddSingleton<ITelegramBotClient>(botClient);
        }
    }
}
