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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunqis.Telegram.Bot.Framework.Bot;
/// <summary>
/// 表示一个 Telegram 模块构建器接口，用于定义模块的添加和构建逻辑。
/// </summary>
/// <remarks>
/// <para>
/// 该接口的主要作用是为 Telegram Bot 框架提供模块化的构建能力。通过实现该接口，
/// 开发者可以动态添加模块并构建 Telegram Bot 实例。
/// </para>
/// <para>
/// 意义：模块化设计可以提高代码的可维护性和可扩展性，便于团队协作开发。
/// </para>
/// <para>
/// 历史：该接口设计于 2022 年，目的是为 Telegram Bot 框架提供标准化的模块构建机制。
/// </para>
/// <para>
/// 使用示例：
/// <code>
/// var builder = new TelegramModuleBuilder();
/// builder.AddModule(new MyTelegramModule())
///        .AddModule&lt;AnotherModule&gt;(param1, param2)
///        .Build();
/// </code>
/// </para>
/// </remarks>
public interface ITelegramModuleBuilder
{
    /// <summary>
    /// 添加模块。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 该方法用于向构建器中添加一个模块实例。模块实例需要实现 <see cref="ITelegramModule"/> 接口。
    /// </para>
    /// <para>
    /// 参数意义：
    /// <list type="bullet">
    /// <item>
    /// <term>module</term>
    /// <description>需要添加的模块实例。</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <param name="module">需要添加的模块实例</param>
    /// <returns>返回当前构建器实例以支持链式调用。</returns>
    public ITelegramModuleBuilder AddModule(ITelegramModule module);

    /// <summary>
    /// 添加模块。
    /// </summary>
    /// <typeparam name="T">模块的类型，必须实现 <see cref="ITelegramModule"/> 接口。</typeparam>
    /// <param name="objects">模块的构造函数参数。</param>
    /// <remarks>
    /// <para>
    /// 该方法用于通过类型和构造函数参数动态创建并添加模块。
    /// </para>
    /// <para>
    /// 参数意义：
    /// <list type="bullet">
    /// <item>
    /// <term>objects</term>
    /// <description>模块的构造函数参数。</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <returns>返回当前构建器实例以支持链式调用。</returns>
    public ITelegramModuleBuilder AddModule<T>(params object[] objects) where T : ITelegramModule;

    /// <summary>
    /// 开始构建 Telegram Bot 实例。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 该方法用于构建 Telegram Bot 实例。构建过程中会初始化所有已添加的模块。
    /// </para>
    /// </remarks>
    /// <returns>返回构建完成的 <see cref="ITelegramBot"/> 实例。</returns>
    public ITelegramBot Build();
}

