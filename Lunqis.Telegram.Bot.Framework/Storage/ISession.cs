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
namespace Lunqis.Telegram.Bot.Framework.Storage;

/// <summary>
/// 表示用于管理用户特定数据的会话存储接口。
/// </summary>
/// <remarks>
/// <para>
/// <see cref="ISession"/> 接口提供了一种机制，用于存储、检索和管理与特定用户或上下文相关的键值对。
/// 它旨在支持用户交互跨多个请求或命令的状态管理。
/// </para>
/// <para>
/// 此接口是框架存储系统的核心组件，开发者可以基于此接口实现自定义的会话存储解决方案，
/// 如内存存储、分布式存储或持久化存储。
/// </para>
/// <para>
/// <b>历史：</b> 在框架的 1.0 版本中引入，用于标准化会话管理。在后续版本中增强，
/// 增加了如 <see cref="AddOrUpdate"/> 等操作支持。
/// </para>
/// </remarks>
public interface ISession : IDisposable
{
    /// <summary>
    /// 获取会话的唯一标识符。
    /// </summary>
    /// <value>
    /// 表示会话唯一 ID 的字符串。
    /// </value>
    public string ID { get; }

    /// <summary>
    /// 获取会话中存储的键值对总数。
    /// </summary>
    /// <value>
    /// 表示存储项数量的整数。
    /// </value>
    public int Count { get; }

    /// <summary>
    /// 向会话中添加新的键值对。
    /// </summary>
    /// <param name="key">要添加的键。</param>
    /// <param name="value">要存储的值。</param>
    /// <exception cref="ArgumentNullException">当 <paramref name="key"/> 为 null 时抛出。</exception>
    /// <exception cref="InvalidOperationException">当键已存在于会话中时抛出。</exception>
    public void Add(object key, object value);

    /// <summary>
    /// 从会话中移除指定键的值。
    /// </summary>
    /// <param name="key">要移除的键。</param>
    /// <exception cref="ArgumentNullException">当 <paramref name="key"/> 为 null 时抛出。</exception>
    /// <exception cref="KeyNotFoundException">当键不存在于会话中时抛出。</exception>
    public void Remove(object key);

    /// <summary>
    /// 清除会话中的所有键值对。
    /// </summary>
    /// <remarks>
    /// 此操作会移除会话中的所有数据，相当于重置会话。
    /// </remarks>
    public void Clear();

    /// <summary>
    /// 检索与指定键相关联的值。
    /// </summary>
    /// <param name="key">要检索的键。</param>
    /// <returns>
    /// 与指定键相关联的值，如果键不存在，则返回 <c>null</c>。
    /// </returns>
    /// <exception cref="ArgumentNullException">当 <paramref name="key"/> 为 null 时抛出。</exception>
    public object Get(object key);

    /// <summary>
    /// 向会话中添加新的键值对，或更新已存在键的值。
    /// </summary>
    /// <param name="key">要添加或更新的键。</param>
    /// <param name="value">要添加或更新的值。</param>
    /// <exception cref="ArgumentNullException">当 <paramref name="key"/> 为 null 时抛出。</exception>
    public void AddOrUpdate(object key, object value);
}
