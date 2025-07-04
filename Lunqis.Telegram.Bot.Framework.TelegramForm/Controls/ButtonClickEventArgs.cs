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
using Lunqis.Telegram.Bot.Framework.Users;

namespace Lunqis.Telegram.Bot.Framework.TelegramForm.Controls;

/// <summary>
/// Provides data for a button click event in a Telegram context.
/// </summary>
/// <remarks>This class encapsulates information about the button click, including the associated Telegram context
/// and the identifier of the clicked button. It is typically used in event handlers to process button click
/// actions.</remarks>
public class ButtonClickEventArgs : EventArgs
{
    /// <summary>
    /// Gets the required context for interacting with the Telegram API.
    /// </summary>
    public required TelegramContext Context { get; init; }

    /// <summary>
    /// Gets the unique identifier for the button.
    /// </summary>
    public required string ButtonID { get; init; }
}