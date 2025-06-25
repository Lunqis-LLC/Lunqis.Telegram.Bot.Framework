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
/// 
/// </summary>
public interface ITelegramBotBuildService
{
    /// <summary>
    /// 添加创建时服务。
    /// </summary>
    /// <remarks>
    /// <para>
    /// 创建时服务是指在模块构建阶段需要注册的服务。这些服务通常是模块运行时服务的依赖项。
    /// </para>
    /// <para>
    /// 参数意义：
    /// <list type="bullet">
    /// <item>
    /// <term>services</term>
    /// <description>服务集合，用于注册创建时服务。</description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <param name="services">服务集合</param>
    public void AddBuildService(IServiceCollection services);
}
