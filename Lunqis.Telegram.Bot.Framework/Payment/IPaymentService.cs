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
public interface IPaymentService
{
    /// <summary>
    /// Occurs when the state of a payment order changes.
    /// </summary>
    /// <remarks>Subscribe to this event to handle changes in payment order status, such as updates or
    /// cancellations. The event provides a <see cref="PaymentOrderArgs"/> instance containing details about the order
    /// change.</remarks>
    public event EventHandler<PaymentOrderArgs> OnOrderChange;

    /// <summary>
    /// Creates a new payment order for the specified user with the given amount and description.
    /// </summary>
    /// <remarks>This method initiates the creation of a payment order and returns the result of the
    /// operation. Ensure that the user ID is valid and the amount is positive before calling this method.</remarks>
    /// <param name="userId">The unique identifier of the user for whom the payment order is created.</param>
    /// <param name="amount">The monetary amount for the payment order. Must be greater than zero.</param>
    /// <param name="description">A brief description of the payment order. Cannot be null or empty.</param>
    /// <returns>A <see cref="PaymentOrderResult"/> object containing the details of the created payment order,  including its
    /// status and any relevant metadata.</returns>
    Task<PaymentOrderResult> CreateOrderAsync(long userId, decimal amount, string description);

    /// <summary>
    /// Asynchronously retrieves the payment status of a specified order.
    /// </summary>
    /// <remarks>Use this method to check the payment status of an order, such as whether it has been paid, is
    /// pending, or has failed. Ensure the <paramref name="orderId"/> is valid and corresponds to an existing
    /// order.</remarks>
    /// <param name="orderId">The unique identifier of the order whose payment status is being queried. Must not be <see langword="null"/> or
    /// empty.</param>
    /// <returns>A <see cref="PaymentStatus"/> value representing the current payment status of the order.</returns>
    Task<PaymentStatus> QueryOrderStatusAsync(string orderId);
}
