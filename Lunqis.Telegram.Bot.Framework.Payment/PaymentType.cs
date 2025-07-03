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
/// Represents the type of payment method used in a transaction.
/// </summary>
/// <remarks>This enumeration is used to specify the payment provider or method for processing payments.
/// Currently, it includes <see cref="Stripe"/> as a supported payment type.</remarks>
public enum PaymentType
{
    /// <summary>
    /// Represents a stripe in a graphical or visual layout.
    /// </summary>
    /// <remarks>A stripe is typically used to define a visual element, such as a band or line, in a layout or
    /// design. This class can be used to represent and manipulate stripes in various contexts, such as UI components or
    /// graphical rendering.</remarks>
    Stripe = 1,
}
