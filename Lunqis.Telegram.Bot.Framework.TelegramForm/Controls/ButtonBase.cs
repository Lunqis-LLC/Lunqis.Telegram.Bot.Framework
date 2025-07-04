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
namespace Lunqis.Telegram.Bot.Framework.TelegramForm.Controls;

/// <summary>
/// Represents the base class for button controls, providing common functionality for derived button types.
/// </summary>
/// <remarks>This class serves as a foundation for button controls, offering shared properties and behaviors.
/// Derived classes can extend this functionality to implement specific button types.</remarks>
public abstract class ButtonBase : IDisposable
{
    /// <summary>
    /// Gets or sets the text content associated with this instance.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Occurs when the button is clicked.
    /// </summary>
    /// <remarks>Subscribe to this event to handle button click actions. The event provides  <see
    /// cref="ButtonClickEventArgs"/> containing additional information about the click event.</remarks>
    public event EventHandler<ButtonClickEventArgs>? OnClick;

    /// <summary>
    /// Releases all resources used by the current instance of the class.
    /// </summary>
    /// <remarks>This method should be called when the instance is no longer needed to ensure proper cleanup
    /// of resources. It suppresses finalization to optimize garbage collection.</remarks>
    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
