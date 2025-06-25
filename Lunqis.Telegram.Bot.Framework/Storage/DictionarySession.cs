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
/// Represents a session that stores key-value pairs in memory, providing functionality for adding, updating,
/// retrieving, and removing items.
/// </summary>
/// <remarks>This class implements <see cref="ISession"/> and extends <see cref="Dictionary{object, object}"/> to
/// provide session-specific behavior. It generates a unique session identifier upon instantiation and allows for
/// flexible management of session data.</remarks>
internal class DictionarySession : Dictionary<object, object>, ISession
{
    /// <summary>
    /// Gets the unique identifier for this instance.
    /// </summary>
    public string ID { get; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Adds a key-value pair to the collection.
    /// </summary>
    /// <param name="key">The key of the element to add. Cannot be <see langword="null"/>.</param>
    /// <param name="value">The value of the element to add. Can be <see langword="null"/>.</param>
    public new void Add(object key, object value) =>
        base.Add(key, value);

    /// <summary>
    /// Adds a new key-value pair to the collection or updates the value of an existing key.
    /// </summary>
    /// <remarks>If the specified <paramref name="key"/> already exists in the collection, its associated
    /// value is updated to <paramref name="value"/>. Otherwise, a new key-value pair is added to the
    /// collection.</remarks>
    /// <param name="key">The key to add or update. Cannot be <see langword="null"/>.</param>
    /// <param name="value">The value to associate with the specified key. Can be <see langword="null"/>.</param>
    public void AddOrUpdate(object key, object value)
    {
        if (ContainsKey(key))
            this[key] = value;
        else
            Add(key, value);
    }

    /// <summary>
    /// Releases all resources used by the current instance of the class.
    /// </summary>
    /// <remarks>Call this method when you are finished using the object to free up resources.  After calling
    /// <see cref="Dispose"/>, the object is in an unusable state and  should not be accessed further.</remarks>
    public void Dispose() => Clear();

    /// <summary>
    /// Retrieves the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose associated value is to be retrieved. Cannot be <see langword="null"/>.</param>
    /// <returns>The value associated with the specified key if found; otherwise, <see langword="null"/>.</returns>
    public object Get(object key) =>
        TryGetValue(key, out var value) ? value : null!;

    /// <summary>
    /// Removes the element with the specified key from the collection.
    /// </summary>
    /// <remarks>If the specified key does not exist in the collection, no action is taken.</remarks>
    /// <param name="key">The key of the element to remove. Cannot be <see langword="null"/>.</param>
    public new void Remove(object key) =>
        base.Remove(key);
}
