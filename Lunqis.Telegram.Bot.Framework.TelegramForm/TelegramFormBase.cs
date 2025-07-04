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

namespace Lunqis.Telegram.Bot.Framework.TelegramForm;

/// <summary>
/// Represents the base class for creating interactive Telegram forms. Provides lifecycle methods for handling user
/// interactions and form events.
/// </summary>
/// <remarks>This abstract class serves as a foundation for implementing Telegram forms.  Derived classes can
/// override the provided virtual methods to customize behavior for specific events,  such as form loading, submission,
/// cancellation, and user input handling.</remarks>
public abstract class TelegramFormBase
{
    public string Title { get; set; } = string.Empty;

    protected int MessageId { get; set; } = 0;

    protected virtual async Task OnLoad(TelegramContext context)
    {
        await Task.CompletedTask;
    }

    protected virtual async Task OnError(TelegramContext context, Exception exception)
    {
        await Task.CompletedTask;
    }

    protected virtual async Task OnClose(TelegramContext context)
    {
        await Task.CompletedTask;
    }

    protected virtual async Task OnButtonClick(TelegramContext context, string buttonId)
    {
        await Task.CompletedTask;
    }

    protected virtual async Task OnTextInput(TelegramContext context, string input)
    {
        await Task.CompletedTask;
    }

    public async Task LoadAsync(TelegramContext context)
    {
        try
        {
            await OnLoad(context);
        }
        catch (Exception ex)
        {
            await OnError(context, ex);
        }
    }

    public async Task CloseAsync(TelegramContext context)
    {
        try
        {
            await OnClose(context);
        }
        catch (Exception ex)
        {
            await OnError(context, ex);
        }
    }
}
