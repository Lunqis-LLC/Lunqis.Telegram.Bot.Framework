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

namespace Lunqis.Telegram.Bot.Framework.Controllers;
public static partial class Extensions
{
    /// <summary>
    /// Configures the specified <see cref="ITelegramModuleBuilder"/> to use the controller module.
    /// </summary>
    /// <param name="builder">The <see cref="ITelegramModuleBuilder"/> instance to configure. Cannot be <see langword="null"/>.</param>
    /// <returns>The configured <see cref="ITelegramModuleBuilder"/> instance, allowing for further chaining of module
    /// configurations.</returns>
    public static ITelegramModuleBuilder UseController(this ITelegramModuleBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        builder.AddModule(new UseControllerModule());
        return builder;
    }
}
