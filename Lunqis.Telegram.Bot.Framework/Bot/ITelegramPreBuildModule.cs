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
/// ITelegramPreBuildModule 接口用于在 Telegram Bot 构建过程中执行预处理操作。
/// </summary>
/// <remarks>
/// 该接口的主要作用是为 Telegram Bot 的构建过程提供扩展点，允许开发者在服务注册和构建器服务初始化之前执行自定义逻辑。
/// 
/// <para>意义：</para>
/// <list type="bullet">
/// <item>提供灵活的扩展机制，支持自定义服务注册。</item>
/// <item>允许在构建器服务初始化之前注入依赖或执行初始化逻辑。</item>
/// </list>
/// 
/// <para>历史：</para>
/// <list type="bullet">
/// <item>2022-2025: 由 Azumo Lab 开发并维护。</item>
/// </list>
/// 
/// <para>使用方法：</para>
/// 实现该接口并在实现中定义 PreBuild 方法的逻辑，然后将实现类注册到 Telegram Bot 的构建流程中。
/// </remarks>
public interface ITelegramPreBuildModule
{
    /// <summary>
    /// 在 Telegram Bot 构建过程中执行预处理操作。
    /// </summary>
    /// <param name="services">服务集合，用于注册依赖项。</param>
    /// <param name="builderService">构建器服务，提供对构建过程的上下文访问。</param>
    /// <remarks>
    /// <para>参数说明：</para>
    /// <list type="bullet">
    /// <item><c>services</c>: 表示依赖注入容器的服务集合，允许注册自定义服务。</item>
    /// <item><c>builderService</c>: 表示构建器服务的实例，提供对当前构建上下文的访问。</item>
    /// </list>
    /// 
    /// <para>使用场景：</para>
    /// <list type="bullet">
    /// <item>在服务集合中注册自定义服务。</item>
    /// <item>初始化或配置构建器服务。</item>
    /// </list>
    /// </remarks>
    public void PreBuild(IServiceCollection services, IServiceProvider builderService);
}

