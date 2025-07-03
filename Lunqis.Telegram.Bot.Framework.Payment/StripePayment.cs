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
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace Lunqis.Telegram.Bot.Framework.Payment;

/// <summary>
/// Provides functionality for processing payments using the Stripe payment provider.
/// </summary>
/// <remarks>This class implements the <see cref="IPaymentService"/> interface to handle payment operations such
/// as creating orders and querying order statuses. It uses the Stripe API to process payments and requires
/// configuration settings for the Stripe API key.</remarks>
internal class StripePayment : IPaymentService
{
    /// <summary>
    /// The name of the payment provider used for processing transactions.
    /// </summary>
    private const string PaymentProviderName = "Stripe";

    /// <summary>
    /// Occurs when the state of a payment order changes.
    /// </summary>
    /// <remarks>Subscribe to this event to be notified whenever a payment order is updated.  The event
    /// provides details about the change through the <see cref="PaymentOrderArgs"/> parameter.</remarks>
    public event EventHandler<PaymentOrderArgs> OnOrderChange = (obj, e) => { };

    /// <summary>
    /// Initializes a new instance of the <see cref="StripePayment"/> class and configures the Stripe API key.
    /// </summary>
    /// <remarks>This constructor retrieves the Stripe API key from the application's configuration and sets
    /// it in the  <see cref="StripeConfiguration.ApiKey"/> property. Ensure that the configuration contains the
    /// necessary  settings before calling this constructor.</remarks>
    /// <param name="configuration">The application configuration object containing the Stripe payment provider settings. The configuration must
    /// include a section named <see cref="PaymentProviderName"/> with a valid API key.</param>
    public StripePayment(IConfiguration configuration)
    {
        var stripeConfig = configuration.GetSection(PaymentProviderName);
        var apiKey = stripeConfig.GetSection(nameof(StripeConfiguration.ApiKey)).Value;

        StripeConfiguration.ApiKey = apiKey;
    }

    /// <summary>
    /// Creates a new payment order asynchronously and returns the result containing the order ID and payment URL.
    /// </summary>
    /// <remarks>This method initiates a payment session using the Stripe API and monitors the payment status
    /// in the background. The payment status is periodically checked, and the <see cref="OnOrderChange"/> event is
    /// raised when the status changes. Note that the QR code is not provided directly by Stripe and must be generated
    /// separately if required.</remarks>
    /// <param name="userId">The unique identifier of the user creating the order. Must be a valid user ID.</param>
    /// <param name="amount">The total amount for the payment order, specified in the smallest currency unit (e.g., cents). Must be greater
    /// than zero.</param>
    /// <param name="description">A description of the payment order, typically used to identify the product or service being purchased. Cannot be
    /// null or empty.</param>
    /// <returns>A <see cref="PaymentOrderResult"/> containing the order ID, payment URL, and additional details. The payment URL
    /// can be used to redirect the user to complete the payment.</returns>
    public async Task<PaymentOrderResult> CreateOrderAsync(long userId, decimal amount, string description)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = ["card"],
            LineItems =
            [
                new() {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)amount,
                        Currency = "JPY",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = description,
                        },
                    },
                    Quantity = 1,
                },
            ],
            Mode = "payment",
        };

        var service = new SessionService();
        var session = service.Create(options);

        ThreadPool.QueueUserWorkItem(async (obj) =>
        {
            var paymentStatus = PaymentStatus.Pending;
            while (paymentStatus == PaymentStatus.Pending)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                if (obj is string orderID && (paymentStatus = await QueryOrderStatusAsync(orderID)) != PaymentStatus.Pending)
                    OnOrderChange?.Invoke(this, new PaymentOrderArgs
                    {
                        OrderId = orderID,
                        UserID = userId,
                        Status = paymentStatus
                    });
            }
        }, session.Id);

        return await Task.FromResult(new PaymentOrderResult
        {
            OrderId = session.Id,
            PaymentUrl = session.Url,
            QrCode = null // Stripe does not provide a QR code directly, you may need to generate it separately if required
        });
    }

    /// <summary>
    /// Asynchronously queries the payment status of an order based on its unique identifier.
    /// </summary>
    /// <remarks>This method interacts with an external session service to retrieve the payment status. Ensure
    /// that the provided <paramref name="orderId"/> corresponds to a valid session.</remarks>
    /// <param name="orderId">The unique identifier of the order whose payment status is being queried. Must not be null or empty.</param>
    /// <returns>A <see cref="PaymentStatus"/> value indicating the current payment status of the order. Returns <see
    /// cref="PaymentStatus.Success"/> if the payment is complete; otherwise,  returns <see
    /// cref="PaymentStatus.Pending"/>.</returns>
    public async Task<PaymentStatus> QueryOrderStatusAsync(string orderId)
    {
        var service = new SessionService();
        var session = service.Get(orderId);
        return session.Status switch
        {
            "complete" => await Task.FromResult(PaymentStatus.Success),
            _ => await Task.FromResult(PaymentStatus.Pending),
        };
    }
}
