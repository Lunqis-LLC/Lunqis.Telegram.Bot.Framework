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
namespace Lunqis.Telegram.Bot.Framework;

/// <summary>
/// Represents internal configuration settings for the application.
/// </summary>
/// <remarks>This class contains settings used internally by the application, such as API tokens, environment
/// flags,  server URLs, and HTTP client configurations. It is not intended for external use and should only be accessed
/// within the application's internal logic.</remarks>
internal class InternalSettings
{
    /// <summary>
    /// Gets or sets the authentication token used for securing requests.
    /// </summary>
    internal string? Token { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the application should use the test environment.
    /// </summary>
    internal bool UseTestEnvironment { get; set; }

    /// <summary>
    /// Gets or sets the URL of the local server.
    /// </summary>
    internal string? LocalServerURL { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="System.Net.Http.HttpClient"/> instance used for making HTTP requests.
    /// </summary>
    internal HttpClient? HttpClient { get; set; }
}
