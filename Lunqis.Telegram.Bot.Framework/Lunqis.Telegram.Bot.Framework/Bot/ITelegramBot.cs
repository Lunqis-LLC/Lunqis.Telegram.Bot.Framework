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
namespace Lunqis.Telegram.Bot.Framework.Bot;

/// <summary>
/// 表示 Telegram Bot 的接口。
/// </summary>
/// <remarks>
/// <para>
/// <see cref="ITelegramBot"/> 接口定义了 Telegram Bot 的基本行为，包括启动和停止操作。
/// 它是框架中用于构建和管理 Telegram Bot 的核心接口。
/// </para>
/// <para>
/// 此接口的实现类可以扩展以支持自定义的 Bot 功能，例如消息处理、命令解析和模块化扩展。
/// </para>
/// <para>
/// <b>历史：</b> 在框架的初始版本中引入，旨在提供一个标准化的 Bot 操作接口。
/// 后续版本可能会扩展更多功能以支持复杂的 Bot 场景。
/// </para>
/// </remarks>
public interface ITelegramBot : IDisposable
{
    /// <summary>
    /// 启动 Telegram Bot。
    /// </summary>
    /// <param name="wait">是否等待启动完成。</param>
    /// <returns>表示异步操作的 <see cref="Task"/>。</returns>
    /// <remarks>
    /// 此方法用于启动 Bot 的核心功能，例如连接到 Telegram API 和初始化必要的资源。
    /// </remarks>
    public Task StartAsync(bool wait = false);

    /// <summary>
    /// 停止 Telegram Bot。
    /// </summary>
    /// <returns>表示异步操作的 <see cref="Task"/>。</returns>
    /// <remarks>
    /// 此方法用于释放 Bot 的资源并断开与 Telegram API 的连接。
    /// </remarks>
    public Task StopAsync();
}
