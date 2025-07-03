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
/// Represents the result of a payment order operation, including details necessary for completing the payment.
/// </summary>
/// <remarks>This class encapsulates information such as the unique identifier for the order, the URL for payment
/// processing,  and a QR code for alternative payment methods. It is typically used to provide the client with the
/// necessary  details to complete a payment transaction.</remarks>
public class PaymentOrderResult
{
    /// <summary>
    /// Gets or sets the unique identifier for the order.
    /// </summary>
    public required string OrderId { get; set; }

    /// <summary>
    /// Gets or sets the URL used to initiate a payment transaction.
    /// </summary>
    public required string PaymentUrl { get; set; }

    /// <summary>
    /// Gets or sets the QR code value as a string.
    /// </summary>
    public string? QrCode { get; set; }
}
