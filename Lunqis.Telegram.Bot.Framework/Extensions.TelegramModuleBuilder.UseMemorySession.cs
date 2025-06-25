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
using Lunqis.Telegram.Bot.Framework.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lunqis.Telegram.Bot.Framework;
public static partial class Extensions
{
    /// <summary>
    /// Configures the application to use in-memory session storage for managing session data.
    /// </summary>
    /// <remarks>This module replaces any existing session storage implementation with an in-memory session
    /// storage. It is intended for scenarios where session data does not need to persist beyond the application's
    /// lifetime.</remarks>
    private class UseMemorySessionModule : ITelegramModule
    {
        /// <summary>
        /// Configures the service collection by replacing the existing session implementation  with a memory-based
        /// session storage.
        /// </summary>
        /// <remarks>This method removes all existing registrations of the <see cref="ISession"/>
        /// interface  and adds a singleton implementation using <see cref="MemorySessionStorage"/>.</remarks>
        /// <param name="services">The service collection to configure. Cannot be null.</param>
        /// <param name="builderService">The service provider used during the configuration process. Cannot be null.</param>
        public void Build(IServiceCollection services, IServiceProvider builderService)
        {
            services.RemoveAll<ISession>();
            services.AddSingleton<ISession, MemorySessionStorage>();
        }
    }
}
