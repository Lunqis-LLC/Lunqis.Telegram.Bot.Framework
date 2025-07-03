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
namespace Lunqis.Telegram.Bot.Framework.Payment;

/// <summary>
/// Represents the status of a payment operation.
/// </summary>
/// <remarks>This enumeration is typically used to indicate the outcome of a payment process. It can represent a
/// pending state, a successful completion, or a failure.</remarks>
public enum PaymentStatus
{
    /// <summary>
    /// Represents a pending state or operation that has not yet been completed or resolved.
    /// </summary>
    /// <remarks>This type or member is used to indicate that an action, process, or state is currently
    /// awaiting completion.  It can be utilized in scenarios where deferred execution or asynchronous operations are
    /// involved.</remarks>
    Pending,

    /// <summary>
    /// Represents the success status of an operation.
    /// </summary>
    /// <remarks>This enumeration value is typically used to indicate that an operation completed
    /// successfully.</remarks>
    Success,

    /// <summary>
    /// Represents a state indicating that an operation or process has failed.
    /// </summary>
    /// <remarks>This value is typically used to signify that an error or issue occurred during execution. It
    /// may be returned or set by methods, properties, or events to indicate failure.</remarks>
    Failed
}