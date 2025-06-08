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
using Microsoft.Extensions.DependencyInjection;

namespace Lunqis.Telegram.Bot.Framework.Bot;
/// <summary>
/// 表示一个 Telegram 模块接口，用于定义模块的构建和运行时服务的添加逻辑。
/// </summary>
/// <remarks>
/// <para>
/// 该接口的主要作用是为 Telegram Bot 框架提供模块化的扩展能力。通过实现该接口，
/// 开发者可以定义模块的创建时服务和运行时服务，从而实现功能的解耦和扩展。
/// </para>
/// <para>
/// 意义：模块化设计可以提高代码的可维护性和可扩展性，便于团队协作开发。
/// </para>
/// <para>
/// 历史：该接口设计于 2022 年，目的是为 Telegram Bot 框架提供标准化的模块扩展机制。
/// </para>
/// <para>
/// 使用示例：
/// <code>
/// public class MyTelegramModule : ITelegramModule
/// {
///     public void AddBuildService(IServiceCollection services)
///     {
///         services.AddSingleton&lt;MyService&gt;();
///     }
/// 
///     public void Build(IServiceCollection services, IServiceProvider builderService)
///     {
///         var myService = builderService.GetService&lt;MyService&gt;();
///         services.AddSingleton(new RuntimeService(myService));
///     }
/// }
/// </code>
/// </para>
/// </remarks>
public interface ITelegramModule
{
    /// <summary>
    /// 创建运行时服务。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 运行时服务是指在模块运行阶段需要注册的服务。可以通过 <paramref name="builderService"/> 参数
    /// 获取创建时服务，并基于这些服务注册运行时服务。
    /// </para>
    /// <para>
    /// 参数意义：
    /// <list type="bullet">
    /// <item>
    /// <term>services</term>
    /// <description>运行时服务集合，用于注册运行时服务。</description>
    /// </item>
    /// <item>
    /// <term>builderService</term>
    /// <description>创建时服务的提供者，用于获取已注册的创建时服务。</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <param name="services">运行时服务集合</param>
    /// <param name="builderService">创建时服务</param>
    public void Build(IServiceCollection services, IServiceProvider builderService);
}
