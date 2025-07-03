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
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

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
    /// Gets the service provider used to resolve application services.
    /// </summary>
    private IServiceProvider ServiceProvider { get; } = null!;

    /// <summary>
    /// Gets the <see cref="CancellationTokenSource"/> used to manage cancellation tokens for asynchronous operations.
    /// </summary>
    private CancellationTokenSource CancellationTokenSource { get; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="TelegramBot"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the Telegram bot by invoking the initialization method.  It is
    /// intended for internal use and should not be called directly by external code.</remarks>
    private TelegramBot()
    {
        _ = this.InitTelegramBot();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelegramBot"/> class with the specified service provider.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve dependencies for the bot.  This parameter cannot be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="serviceProvider"/> is <see langword="null"/>.</exception>
    private TelegramBot(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        CancellationTokenSource = new CancellationTokenSource();
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

    /// <summary>
    /// Represents a builder for configuring and managing a collection of Telegram modules.
    /// </summary>
    /// <remarks>This interface provides methods for adding, configuring, and retrieving Telegram modules.
    /// Implementations of this interface are responsible for managing the lifecycle and dependencies of the
    /// modules.</remarks>
    private readonly List<ITelegramModule> _modules = [];

    /// <summary>
    /// Adds a module to the current builder for configuration and processing.
    /// </summary>
    /// <param name="module">The module to add. Cannot be <see langword="null"/>.</param>
    /// <returns>The current instance of <see cref="ITelegramModuleBuilder"/> to allow method chaining.</returns>
    public ITelegramModuleBuilder AddModule(ITelegramModule module)
    {
        ArgumentNullException.ThrowIfNull(module, nameof(module));
        _modules.Add(module);
        return this;
    }

    /// <summary>
    /// Adds a module of the specified type to the current module builder.
    /// </summary>
    /// <typeparam name="T">The type of the module to add. Must implement <see cref="ITelegramModule"/>.</typeparam>
    /// <param name="objects">An array of parameters to pass to the constructor of the module.</param>
    /// <returns>An <see cref="ITelegramModuleBuilder"/> instance for chaining additional module configurations.</returns>
    /// <exception cref="InvalidOperationException">Thrown if an instance of the specified module type <typeparamref name="T"/> cannot be created  using the
    /// provided parameters.</exception>
    public ITelegramModuleBuilder AddModule<T>(params object[] objects) where T : ITelegramModule
    {
        return Activator.CreateInstance(typeof(T), objects) is ITelegramModule module
            ? AddModule(module)
            : throw new InvalidOperationException($"Cannot create an instance of {typeof(T).FullName} with the provided parameters.");
    }

    /// <summary>
    /// Builds and returns an instance of <see cref="ITelegramBot"/> configured with the registered modules and
    /// services.
    /// </summary>
    /// <remarks>This method initializes the necessary services and modules, invoking their build logic to
    /// configure the bot. Modules implementing <see cref="ITelegramBotBuildService"/> can add additional build services
    /// to the configuration.</remarks>
    /// <returns>An instance of <see cref="ITelegramBot"/> with all registered modules and services applied.</returns>
    public ITelegramBot Build()
    {
        ServiceCollection services = new();
        ServiceCollection BuildServices = new();
        foreach (var module in _modules)
        {
            if (module is ITelegramBotBuildService telegramBotBuildService)
            {
                telegramBotBuildService.AddBuildService(BuildServices);
            }

            module.Build(services, BuildServices.BuildServiceProvider());
        }
        return new TelegramBot(services.BuildServiceProvider());
    }
    #endregion

    #region ITelegramBot 接口部分

    /// <summary>
    /// Releases the resources used by the current instance of the class.
    /// </summary>
    /// <remarks>This method stops any ongoing asynchronous operations, disposes of the service provider if
    /// applicable,  and suppresses finalization to optimize garbage collection. It should be called when the instance
    /// is no  longer needed to ensure proper cleanup of resources.</remarks>
    public void Dispose()
    {
        StopAsync().Wait(CancellationTokenSource.Token);
        (ServiceProvider as IDisposable)?.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Starts the Telegram bot client to begin receiving updates asynchronously.
    /// </summary>
    /// <remarks>This method initializes the bot client and begins receiving updates using the specified
    /// update handler. If <paramref name="wait"/> is set to <see langword="true"/>, the method will block execution
    /// until the cancellation token is triggered.</remarks>
    /// <param name="wait">A boolean value indicating whether to block execution until cancellation is requested.  If <see
    /// langword="true"/>, the method will continuously wait; otherwise, it returns immediately after starting the bot
    /// client.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task StartAsync(bool wait = false)
    {
        var telegramBotClient = ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var updateHandler = ServiceProvider.GetRequiredService<IUpdateHandler>();

        telegramBotClient.StartReceiving(updateHandler, new ReceiverOptions
        {
            AllowedUpdates = [],
        }, CancellationTokenSource.Token);

        if (wait)
        {
            while (!CancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(1000, CancellationTokenSource.Token);
            }
        }
    }

    /// <summary>
    /// Stops the Telegram bot client asynchronously, closing any active connections.
    /// </summary>
    /// <remarks>This method retrieves the required <see cref="ITelegramBotClient"/> instance from the service
    /// provider  and initiates the closing process using the provided cancellation token. Ensure that the service
    /// provider  is properly configured and the bot client is initialized before calling this method.</remarks>
    /// <returns>A task that represents the asynchronous operation of stopping the bot client.</returns>
    public async Task StopAsync()
    {
        var telegramBotClient = ServiceProvider.GetRequiredService<ITelegramBotClient>();
        await telegramBotClient.Close(CancellationTokenSource.Token);
    }
    #endregion
}
