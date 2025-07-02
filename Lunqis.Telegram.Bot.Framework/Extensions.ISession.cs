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
using Lunqis.Telegram.Bot.Framework.Storage;

namespace Lunqis.Telegram.Bot.Framework;
public static partial class Extensions
{
    /// <summary>
    /// Retrieves an object from the session associated with the specified key, or returns a default value if the key is
    /// not found or the value is not of the expected type.
    /// </summary>
    /// <typeparam name="T">The expected type of the object to retrieve from the session.</typeparam>
    /// <param name="session">The session from which to retrieve the object. Cannot be <see langword="null"/>.</param>
    /// <param name="key">The key associated with the object to retrieve.</param>
    /// <param name="defaultValue">The value to return if the key is not found or the value is not of the expected type.</param>
    /// <returns>The object associated with the specified key if it exists and is of type <typeparamref name="T"/>; otherwise,
    /// <paramref name="defaultValue"/>.</returns>
    public static T GetOrDefault<T>(this ISession session, object key, T defaultValue)
    {
        ArgumentNullException.ThrowIfNull(session);

        object result = session.Get(key);
        if (result is not null and T value)
            return value;
        return defaultValue;
    }
}
