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
using Microsoft.Extensions.Caching.Memory;

namespace Lunqis.Telegram.Bot.Framework.Storage;

/// <summary>
/// Provides an in-memory implementation of the <see cref="ISession"/> interface for storing and managing session data.
/// </summary>
/// <remarks>This class uses <see cref="IMemoryCache"/> to store session data and maintains a queue of keys for
/// efficient management. It is suitable for scenarios where session data needs to be stored temporarily in memory. Note
/// that the session data will only persist for the lifetime of the application and is not shared across
/// instances.</remarks>
internal class MemorySessionStorage : ISession
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemorySessionStorage"/> class using the specified memory cache.
    /// </summary>
    /// <param name="memoryCache">The memory cache instance used to store session data. Cannot be <see langword="null"/>.</param>
    public MemorySessionStorage(IMemoryCache memoryCache) =>
        this.memoryCache = memoryCache;

    /// <summary>
    /// Represents a memory cache used for storing and retrieving data in memory.
    /// </summary>
    /// <remarks>This field is intended to provide caching functionality for the application. It is a readonly
    /// instance of <see cref="IMemoryCache"/>, ensuring that the cache implementation cannot be replaced after
    /// initialization.</remarks>
    private readonly IMemoryCache memoryCache;

    /// <summary>
    /// Represents a collection of unique objects used internally to manage queued items.
    /// </summary>
    /// <remarks>This field is a read-only <see cref="HashSet{T}"/> that ensures each object in the queue is
    /// unique. It is intended for internal use and is not exposed to external callers.</remarks>
    private readonly HashSet<object> queue = [];

    /// <summary>
    /// Gets the unique identifier for this instance.
    /// </summary>
    public string ID { get; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets the number of elements currently in the queue.
    /// </summary>
    public int Count => queue.Count;

    /// <summary>
    /// Adds a key-value pair to the internal cache and queue.
    /// </summary>
    /// <remarks>This method stores the key in an internal queue and associates the key with the value in the
    /// cache. Ensure that both <paramref name="key"/> and <paramref name="value"/> are non-null before calling this
    /// method.</remarks>
    /// <param name="key">The key associated with the value to be added. Cannot be <see langword="null"/>.</param>
    /// <param name="value">The value to be stored in the cache. Cannot be <see langword="null"/>.</param>
    public void Add(object key, object value)
    {
        _ = queue.Add(key);
        _ = memoryCache.Set(key, value);
    }

    /// <summary>
    /// Adds a new key-value pair to the collection or updates the value of an existing key.
    /// </summary>
    /// <remarks>If the specified <paramref name="key"/> already exists in the collection, its associated
    /// value is updated to <paramref name="value"/>. If the <paramref name="key"/> does not exist, a new key-value pair
    /// is added.</remarks>
    /// <param name="key">The key to add or update. Cannot be <see langword="null"/>.</param>
    /// <param name="value">The value to associate with the key. Cannot be <see langword="null"/>.</param>
    public void AddOrUpdate(object key, object value) => Add(key, value);

    /// <summary>
    /// Clears all items from the queue and removes their associated entries from the memory cache.
    /// </summary>
    /// <remarks>This method ensures that both the queue and the memory cache are emptied.  Use this method to
    /// reset the state when cached items are no longer needed.</remarks>
    public void Clear()
    {
        foreach (var item in queue)
            memoryCache.Remove(item);

        queue.Clear();
    }

    /// <summary>
    /// Releases all resources used by the current instance of the class.
    /// </summary>
    /// <remarks>This method should be called when the instance is no longer needed to ensure proper cleanup
    /// of resources. After calling <see cref="Dispose"/>, the instance should not be used further.</remarks>
    public void Dispose() => Clear();

    /// <summary>
    /// Retrieves an object from the cache using the specified key.
    /// </summary>
    /// <param name="key">The key used to locate the cached object. Cannot be <see langword="null"/>.</param>
    /// <returns>The cached object associated with the specified key, or <see langword="null"/> if the key does not exist in the
    /// cache.</returns>
    public object Get(object key) => memoryCache.Get(key)!;

    /// <summary>
    /// Removes the specified entry from the cache.
    /// </summary>
    /// <remarks>If the specified key does not exist in the cache, no action is taken.</remarks>
    /// <param name="key">The key of the cache entry to remove. Cannot be <see langword="null"/>.</param>
    public void Remove(object key) => memoryCache.Remove(key);
}
