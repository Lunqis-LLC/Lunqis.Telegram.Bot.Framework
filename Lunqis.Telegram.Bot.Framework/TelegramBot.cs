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

namespace Lunqis.Telegram.Bot.Framework;

/// <summary>
/// Represents a Telegram bot that can be configured, managed, and started using a modular approach.
/// </summary>
/// <remarks>The <see cref="TelegramBot"/> class provides functionality for building and managing a Telegram bot
/// through the <see cref="ITelegramModuleBuilder"/> interface. It supports adding modules to extend functionality and
/// offers methods to start and stop the bot asynchronously.</remarks>
public partial class TelegramBot : ITelegramBot, ITelegramModuleBuilder
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TelegramBot"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the Telegram bot by invoking the initialization method.  It is
    /// intended for internal use and should not be called directly by external code.</remarks>
    private TelegramBot()
    {
        this.InitTelegramBot();
    }

    /// <summary>
    /// Creates and returns a new instance of a builder for configuring and managing a Telegram bot module.
    /// </summary>
    /// <returns>An object implementing <see cref="ITelegramModuleBuilder"/> that can be used to configure and manage a Telegram
    /// bot module.</returns>
    public static ITelegramModuleBuilder Create()
    {
        return new TelegramBot();
    }

    #region ITelegramModuleBuilder 接口部分

    private List<ITelegramModule> _modules = [];

    public ITelegramModuleBuilder AddModule(ITelegramModule module)
    {
        ArgumentNullException.ThrowIfNull(module, nameof(module));
        _modules.Add(module);
        return this;
    }

    public ITelegramModuleBuilder AddModule<T>(params object[] objects) where T : ITelegramModule
    {
        throw new NotImplementedException();
    }

    public ITelegramBot Build()
    {
        foreach (var module in _modules)
        {
            module.Build(new ServiceCollection(), null);
        }
    }
    #endregion

    #region ITelegramBot 接口部分
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task StartAsync(bool wait = false)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync()
    {
        throw new NotImplementedException();
    }
    #endregion
}
