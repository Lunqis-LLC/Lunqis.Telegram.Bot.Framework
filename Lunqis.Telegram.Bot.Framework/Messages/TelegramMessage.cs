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
using System.Text;
using Telegram.Bot.Types.Enums;

namespace Lunqis.Telegram.Bot.Framework.Messages;

/// <summary>
/// Represents a base class for constructing and formatting Telegram messages.
/// </summary>
/// <remarks>The <see cref="TelegramMessage"/> class provides an abstract foundation for creating and formatting
/// messages in various styles and formats supported by Telegram, such as HTML, Markdown, and MarkdownV2. It includes
/// methods for applying text formatting (e.g., bold, italic, underline) and creating links, hashtags, and code blocks.
/// Subclasses implement specific formatting rules for each supported format.</remarks>
public abstract class TelegramMessage
{
    /// <summary>
    /// Specifies the modes of escaping text for different formats.
    /// </summary>
    /// <remarks>This enumeration defines the available text escaping modes, which determine how special
    /// characters  are handled in the output. Use the appropriate mode based on the target format or context.</remarks>
    protected enum EscapeMode
    {
        /// <summary>
        /// Represents an HTML document or fragment.
        /// </summary>
        /// <remarks>This class provides functionality for working with HTML content, such as parsing,
        /// manipulating,  and rendering HTML elements. It can be used to create or modify HTML structures
        /// programmatically.</remarks>
        Html,
        
        /// <summary>
        /// Represents the MarkdownV2 formatting style used in text processing.
        /// </summary>
        /// <remarks>MarkdownV2 is a specific version of the Markdown syntax that supports advanced
        /// formatting features,  such as bold, italic, inline code, and hyperlinks. This class or member is typically
        /// used to handle  text content that requires MarkdownV2-compatible formatting.</remarks>
        MarkdownV2,
        
        /// <summary>
        /// Represents a Uniform Resource Locator (URL) used to identify and access resources on the web.
        /// </summary>
        /// <remarks>A URL is a string that specifies the location of a resource on the internet. It
        /// typically includes components such as the protocol (e.g., "http" or "https"), domain name, path, and query
        /// parameters. This class or member may be used to store, manipulate, or validate URLs.</remarks>
        URL,
        
        /// <summary>
        /// Represents raw data or content that has not been processed or transformed.
        /// </summary>
        /// <remarks>This type or member is intended to handle unprocessed data. It can be used in
        /// scenarios where raw input needs to be stored, transmitted, or further processed.</remarks>
        Raw
    }

    /// <summary>
    /// Gets or sets the mode used for parsing input data.
    /// </summary>
    public ParseMode ParseMode { get; protected set; }

    /// <summary>
    /// A <see cref="StringBuilder"/> instance used for constructing or manipulating strings.
    /// </summary>
    /// <remarks>This field is read-only and initialized when the containing class is instantiated. It is
    /// intended for internal use only.</remarks>
    private readonly StringBuilder stringBuilder = new();

    /// <summary>
    /// Gets a preconfigured instance of a Telegram message formatted using MarkdownV2.
    /// </summary>
    /// <remarks>MarkdownV2 is a specific formatting syntax supported by Telegram for rich text styling. Use
    /// this property to create messages with MarkdownV2 formatting.</remarks>
    public static TelegramMessage MarkdownV2 => new MarkdownV2MessageBuilder();

    /// <summary>
    /// Gets a preconfigured instance of a <see cref="TelegramMessage"/> that supports HTML formatting.
    /// </summary>
    public static TelegramMessage Html => new HtmlMessageBuilder();

    /// <summary>
    /// Gets a preconfigured instance of a Telegram message formatted using Markdown.
    /// </summary>
    public static TelegramMessage Markdown => new MarkdownMessageBuilder();

    /// <summary>
    /// Gets the default message format for Telegram messages.
    /// </summary>
    public static TelegramMessage Default => Html;

    /// <summary>
    /// Provides functionality for building HTML-formatted messages for Telegram.
    /// </summary>
    /// <remarks>This class extends <see cref="TelegramMessage"/> and enables the creation of messages using
    /// HTML formatting tags. It supports various formatting options such as bold, italic, links, and code blocks, among
    /// others. The <see cref="ParseMode"/> is set to <see cref="ParseMode.Html"/> by default.</remarks>
    private class HtmlMessageBuilder : TelegramMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlMessageBuilder"/> class.
        /// </summary>
        /// <remarks>This class is designed to build messages with HTML formatting. The <see
        /// cref="ParseMode"/> property  is automatically set to <see cref="ParseMode.Html"/> upon
        /// initialization.</remarks>
        public HtmlMessageBuilder() : base() =>
            ParseMode = ParseMode.Html;

        /// <summary>
        /// Creates a blockquote element with the specified text and an optional expandable attribute.
        /// </summary>
        /// <remarks>The method escapes the provided text to ensure it is safe for HTML rendering. If
        /// <paramref name="expandable"/> is <see langword="true"/>, the blockquote element will include the expandable
        /// attribute.</remarks>
        /// <param name="text">The text content to be enclosed within the blockquote element. Cannot be null or empty.</param>
        /// <param name="expandable">A value indicating whether the blockquote should include the expandable attribute. <see langword="true"/> to
        /// include the attribute; otherwise, <see langword="false"/>.</param>
        /// <returns>A <see cref="TelegramMessage"/> containing the formatted blockquote element.</returns>
        public override TelegramMessage Blockquote(string text, bool expandable) =>
            AppendRaw($"<blockquote{IIF(expandable, $" {nameof(expandable)}", string.Empty)}>{EscapeString(text, EscapeMode.Html)}</blockquote>");
        
        /// <summary>
        /// Formats the specified text as bold using HTML tags.
        /// </summary>
        /// <param name="text">The text to format as bold. Cannot be null.</param>
        /// <returns>A <see cref="TelegramMessage"/> instance containing the formatted text.</returns>
        public override TelegramMessage Bold(string text) =>
            AppendRaw($"<b>{EscapeString(text, EscapeMode.Html)}</b>");

        /// <summary>
        /// Wraps the specified text in HTML <c>&lt;code&gt;</c> tags and escapes it for safe rendering.
        /// </summary>
        /// <param name="text">The text to be wrapped in <c>&lt;code&gt;</c> tags. Cannot be <see langword="null"/>.</param>
        /// <returns>A <see cref="TelegramMessage"/> containing the escaped text wrapped in <c>&lt;code&gt;</c> tags.</returns>
        public override TelegramMessage Code(string text) =>
            AppendRaw($"<code>{EscapeString(text, EscapeMode.Html)}</code>");

        /// <summary>
        /// Formats the specified text as a hashtag in HTML.
        /// </summary>
        /// <remarks>The method escapes the input text to ensure it is safe for use in HTML.</remarks>
        /// <param name="text">The text to be formatted as a hashtag. Cannot be null or empty.</param>
        /// <returns>A string containing the formatted HTML representation of the hashtag.</returns>
        public override TelegramMessage HashTag(string text) =>
            AppendRaw($"<a>{EscapeString(text, EscapeMode.Html)}</a>");

        /// <summary>
        /// Formats the specified text with italic HTML tags.
        /// </summary>
        /// <remarks>This method escapes the input text to ensure it is safe for use in HTML.</remarks>
        /// <param name="text">The text to be formatted. Cannot be null or empty.</param>
        /// <returns>A <see cref="TelegramMessage"/> containing the text wrapped in italic HTML tags.</returns>
        public override TelegramMessage Italic(string text) =>
            AppendRaw($"<i>{EscapeString(text, EscapeMode.Html)}</i>");

        /// <summary>
        /// Creates a hyperlink in the format supported by Telegram messages.
        /// </summary>
        /// <remarks>The method escapes the provided text and URL to ensure proper formatting and
        /// security.</remarks>
        /// <param name="text">The display text for the hyperlink. Cannot be null or empty.</param>
        /// <param name="url">The URL to link to. Must be a valid URL and cannot be null or empty.</param>
        /// <returns>A <see cref="TelegramMessage"/> containing the formatted hyperlink.</returns>
        public override TelegramMessage Link(string text, string url) =>
            AppendRaw($"<a href=\"{EscapeString(text, EscapeMode.URL)}\">{EscapeString(text, EscapeMode.Html)}</a>");
        
        /// <summary>
        /// Creates a hyperlink to a Telegram user based on their user ID.
        /// </summary>
        /// <remarks>The method generates an HTML anchor tag with the `tg://user?id=` scheme, allowing the
        /// hyperlink to open the Telegram app and navigate to the specified user.</remarks>
        /// <param name="text">The display text for the hyperlink. This text will be escaped for HTML.</param>
        /// <param name="userid">The Telegram user ID to link to. Must be a valid numeric identifier.</param>
        /// <returns>A <see cref="TelegramMessage"/> containing the formatted hyperlink to the specified user.</returns>
        public override TelegramMessage LinkUser(string text, long userid) =>
            AppendRaw($"<a href=\"tg://user?id={userid}\">{EscapeString(text, EscapeMode.Html)}</a>");
        
        /// <summary>
        /// Wraps the specified text in HTML <pre> tags, escaping it for safe HTML rendering.
        /// </summary>
        /// <param name="text">The text to be wrapped in <pre> tags. Cannot be null.</param>
        /// <returns>A <see cref="TelegramMessage"/> containing the escaped text wrapped in <pre> tags.</returns>
        public override TelegramMessage Pre(string text) =>
            AppendRaw($"<pre>{EscapeString(text, EscapeMode.Html)}</pre>");
        
        /// <summary>
        /// Formats the specified text as a preformatted code block with syntax highlighting.
        /// </summary>
        /// <remarks>The method escapes the provided text to ensure it is safe for HTML rendering. The
        /// <paramref name="language"/> parameter is used to specify the syntax highlighting class.</remarks>
        /// <param name="text">The text to be included within the code block. Must not be null or empty.</param>
        /// <param name="language">The programming language used for syntax highlighting. This value is used as the class name for the code
        /// block.</param>
        /// <returns>A <see cref="TelegramMessage"/> containing the formatted preformatted code block.</returns>
        public override TelegramMessage PreCode(string text, string language) =>
            AppendRaw($"<pre><code class=\"language-{language}\">{EscapeString(text, EscapeMode.Html)}</code></pre>");
        
        /// <summary>
        /// Formats the specified text as a spoiler in Telegram's message format.
        /// </summary>
        /// <remarks>Spoiler formatting hides the text in Telegram until the user chooses to reveal it.
        /// Ensure the input text does not contain invalid characters for HTML escaping.</remarks>
        /// <param name="text">The text to be formatted as a spoiler. Cannot be null.</param>
        /// <returns>A <see cref="TelegramMessage"/> instance containing the formatted spoiler text.</returns>
        public override TelegramMessage Spoiler(string text) =>
            AppendRaw($"<tg-spoiler>{EscapeString(text, EscapeMode.Html)}</tg-spoiler>");
        
        /// <summary>
        /// Applies strikethrough formatting to the specified text.
        /// </summary>
        /// <remarks>The method wraps the provided text in HTML strikethrough tags (<c>&lt;s&gt;</c> and
        /// <c>&lt;/s&gt;</c>)  and escapes any special characters to ensure valid HTML formatting.</remarks>
        /// <param name="text">The text to format with strikethrough. Cannot be null.</param>
        /// <returns>A <see cref="TelegramMessage"/> instance containing the formatted text.</returns>
        public override TelegramMessage Strikethrough(string text) =>
            AppendRaw($"<s>{EscapeString(text, EscapeMode.Html)}</s>");
        
        /// <summary>
        /// Appends the specified text to the message, escaping it for HTML formatting.
        /// </summary>
        /// <param name="text">The text to append. Cannot be null or empty.</param>
        /// <returns>A <see cref="TelegramMessage"/> instance with the appended and escaped text.</returns>
        public override TelegramMessage Text(string text) =>
            AppendRaw(EscapeString(text, EscapeMode.Html));
        
        /// <summary>
        /// Formats the specified text with an underline HTML tag.
        /// </summary>
        /// <remarks>The method escapes the input text to ensure it is safe for use in HTML.</remarks>
        /// <param name="text">The text to be underlined. Cannot be null.</param>
        /// <returns>A <see cref="TelegramMessage"/> instance containing the underlined text.</returns>
        public override TelegramMessage Underline(string text) =>
            AppendRaw($"<u>{EscapeString(text, EscapeMode.Html)}</u>");
    }

    /// <summary>
    /// MD2代消息创建器
    /// </summary>
    private class MarkdownV2MessageBuilder : TelegramMessage
    {
        public MarkdownV2MessageBuilder() : base() =>
            ParseMode = ParseMode.MarkdownV2;

        public override TelegramMessage Blockquote(string text, bool expandable) =>
            throw new System.NotImplementedException();
        public override TelegramMessage Bold(string text) =>
            throw new System.NotImplementedException();
        public override TelegramMessage Code(string text) =>
            throw new System.NotImplementedException();
        public override TelegramMessage HashTag(string text) =>
            throw new System.NotImplementedException();
        public override TelegramMessage Italic(string text) =>
            throw new System.NotImplementedException();
        public override TelegramMessage Link(string text, string url) =>
            throw new System.NotImplementedException();
        public override TelegramMessage LinkUser(string text, long userid) =>
            throw new System.NotImplementedException();
        public override TelegramMessage Pre(string text) =>
            throw new System.NotImplementedException();
        public override TelegramMessage PreCode(string text, string language) =>
            throw new System.NotImplementedException();
        public override TelegramMessage Spoiler(string text) =>
            throw new System.NotImplementedException();
        public override TelegramMessage Strikethrough(string text) =>
            throw new System.NotImplementedException();
        public override TelegramMessage Text(string text) =>
            throw new NotImplementedException();
        public override TelegramMessage Underline(string text) =>
            throw new System.NotImplementedException();
    }

    /// <summary>
    /// Provides functionality for building Telegram messages using Markdown formatting.
    /// </summary>
    /// <remarks>This class extends <see cref="TelegramMessage"/> and enables the creation of messages with
    /// Markdown-specific formatting options. It supports various Markdown elements such as bold, italic, code blocks,
    /// links, and more. Use the methods provided to construct messages with the desired formatting.</remarks>
    private class MarkdownMessageBuilder : TelegramMessage
    {
        public MarkdownMessageBuilder() : base() =>
            ParseMode = ParseMode.Markdown;

        public override TelegramMessage Blockquote(string text, bool expandable) =>
            AppendRaw(string.Empty);
        public override TelegramMessage Bold(string text) =>
            AppendRaw($"*{text}*");
        public override TelegramMessage Code(string text) =>
            AppendRaw($"`{text}`");
        public override TelegramMessage HashTag(string text) =>
            AppendRaw(string.Empty);
        public override TelegramMessage Italic(string text) =>
            AppendRaw($"_{text}_");
        public override TelegramMessage Link(string text, string url) =>
            AppendRaw($"[{text}]({url})");
        public override TelegramMessage LinkUser(string text, long userid) =>
            AppendRaw($"[{text}](tg://user?id={userid})");
        public override TelegramMessage Pre(string text) =>
            AppendRaw($"```").NewLine().AppendRaw(text).NewLine().AppendRaw("```");
        public override TelegramMessage PreCode(string text, string language) =>
            AppendRaw($"```{language.ToString().ToLower()}").NewLine().AppendRaw(text).NewLine().AppendRaw("```");
        public override TelegramMessage Spoiler(string text) =>
            AppendRaw($"||{EscapeString(text, EscapeMode.MarkdownV2)}||");
        public override TelegramMessage Strikethrough(string text) =>
            AppendRaw($"~{EscapeString(text, EscapeMode.MarkdownV2)}~");
        public override TelegramMessage Text(string text) =>
            AppendRaw(EscapeString(text));
        public override TelegramMessage Underline(string text) =>
            AppendRaw($"__{EscapeString(text, EscapeMode.MarkdownV2)}__");
    }

    /// <summary>
    /// Converts a string to a <see cref="TelegramMessage"/> instance.
    /// </summary>
    /// <param name="text">The text to be converted into a <see cref="TelegramMessage"/>.</param>
    public static implicit operator TelegramMessage(string text) =>
        Default.AppendRaw(text);

    /// <summary>
    /// Returns the string representation of the current object.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current object.</returns>
    public override string ToString() =>
        stringBuilder.ToString();

    /// <summary>
    /// Appends the specified raw text to the current message.
    /// </summary>
    /// <remarks>This method appends the provided text directly to the internal message representation without
    /// any formatting or validation. Ensure that the input text is properly sanitized if required, as this method does
    /// not perform any checks.</remarks>
    /// <param name="text">The text to append to the message. Cannot be null.</param>
    /// <returns>The current <see cref="TelegramMessage"/> instance, allowing for method chaining.</returns>
    protected virtual TelegramMessage AppendRaw(string text)
    {
        _ = stringBuilder.Append(text);
        return this;
    }

    /// <summary>
    /// Escapes the specified text according to the provided escape mode.
    /// </summary>
    /// <remarks>This method is useful for preparing text for specific contexts, such as HTML rendering,
    /// Markdown formatting, or URL encoding.</remarks>
    /// <param name="text">The text to be escaped. If <paramref name="text"/> is <see langword="null"/> or empty, an empty string is
    /// returned.</param>
    /// <param name="escapeMode">The mode of escaping to apply. Valid values are: <list type="bullet"> <item><term><see
    /// cref="EscapeMode.Html"/></term><description>Escapes special HTML characters such as <c>&lt;</c>, <c>&gt;</c>,
    /// and <c>&amp;</c>.</description></item> <item><term><see
    /// cref="EscapeMode.MarkdownV2"/></term><description>Escapes special Markdown V2 characters such as <c>*</c>,
    /// <c>_</c>, and <c>[</c>.</description></item> <item><term><see cref="EscapeMode.URL"/></term><description>Encodes
    /// the text for safe use in a URL using percent-encoding.</description></item> <item><term><see
    /// cref="EscapeMode.Raw"/></term><description>Returns the text unchanged.</description></item> </list></param>
    /// <returns>A string containing the escaped text based on the specified <paramref name="escapeMode"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="escapeMode"/> is not a valid <see cref="EscapeMode"/> value.</exception>
    protected virtual string EscapeString(string text, EscapeMode escapeMode = EscapeMode.Raw)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        switch (escapeMode)
        {
            case EscapeMode.Html:
                text = text.Replace("&", "&amp;")
                           .Replace("<", "&lt;")
                           .Replace(">", "&gt;")
                           .Replace("\"", "&quot;")
                           .Replace("'", "&apos;");
                return text;
            case EscapeMode.MarkdownV2:
                text = text.Replace("_", "\\_")
                           .Replace("*", "\\*")
                           .Replace("[", "\\[")
                           .Replace("]", "\\]")
                           .Replace("(", "\\(")
                           .Replace(")", "\\)")
                           .Replace("~", "\\~")
                           .Replace("`", "\\`")
                           .Replace(">", "\\>")
                           .Replace("#", "\\#")
                           .Replace("+", "\\+")
                           .Replace("-", "\\-")
                           .Replace("=", "\\=")
                           .Replace("|", "\\|")
                           .Replace("{", "\\{")
                           .Replace("}", "\\}")
                           .Replace(".", "\\.")
                           .Replace("!", "\\!");
                return text;
            case EscapeMode.URL:
                text = Uri.EscapeDataString(text);
                return text;
            case EscapeMode.Raw:
                return text;
            default:
                throw new ArgumentOutOfRangeException(nameof(escapeMode), escapeMode, null);
        }
    }

    /// <summary>
    /// Appends a newline character to the current message content.
    /// </summary>
    /// <remarks>This method adds a newline to the internal message being constructed and returns the current
    /// instance  to allow method chaining. The newline is appended using the default line terminator for the
    /// environment.</remarks>
    /// <returns>The current <see cref="TelegramMessage"/> instance, allowing for method chaining.</returns>
    public virtual TelegramMessage NewLine()
    {
        _ = stringBuilder.AppendLine();
        return this;
    }

    /// <summary>
    /// Creates a hyperlink in a Telegram message with the specified text and URL.
    /// </summary>
    /// <param name="text">The display text for the hyperlink. Cannot be null or empty.</param>
    /// <param name="url">The URL to link to. Must be a valid, well-formed URL.</param>
    /// <returns>A <see cref="TelegramMessage"/> object containing the formatted hyperlink.</returns>
    public abstract TelegramMessage Link(string text, string url);

    /// <summary>
    /// Links a user to a specified text message in the Telegram system.
    /// </summary>
    /// <param name="text">The text message to associate with the user. Cannot be null or empty.</param>
    /// <param name="userid">The unique identifier of the user to link. Must be a valid user ID.</param>
    /// <returns>A <see cref="TelegramMessage"/> object representing the linked message and user association.</returns>
    public abstract TelegramMessage LinkUser(string text, long userid);

    /// <summary>
    /// Formats the specified text as bold in a Telegram message.
    /// </summary>
    /// <param name="text">The text to be formatted as bold. Cannot be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> instance containing the bold-formatted text.</returns>
    public abstract TelegramMessage Bold(string text);

    /// <summary>
    /// Formats the specified text with italic styling for use in a Telegram message.
    /// </summary>
    /// <param name="text">The text to be formatted. Cannot be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> object containing the italicized text.</returns>
    public abstract TelegramMessage Italic(string text);

    /// <summary>
    /// Formats the specified text with an underline style and returns it as a <see cref="TelegramMessage"/>.
    /// </summary>
    /// <param name="text">The text to be formatted with an underline style. Cannot be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> instance containing the underlined text.</returns>
    public abstract TelegramMessage Underline(string text);

    /// <summary>
    /// Applies strikethrough formatting to the specified text and returns a new TelegramMessage instance.
    /// </summary>
    /// <param name="text">The text to format with strikethrough. Cannot be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> instance containing the formatted text.</returns>
    public abstract TelegramMessage Strikethrough(string text);

    /// <summary>
    /// Creates a spoiler message with the specified text, marking it as hidden until revealed by the user.
    /// </summary>
    /// <param name="text">The text to be included in the spoiler message. Cannot be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> instance representing the spoiler message.</returns>
    public abstract TelegramMessage Spoiler(string text);

    /// <summary>
    /// Creates a new Telegram message containing the specified text.
    /// </summary>
    /// <param name="text">The text content of the Telegram message. Cannot be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> instance representing the message with the specified text.</returns>
    public abstract TelegramMessage Code(string text);

    /// <summary>
    /// Prepares a <see cref="TelegramMessage"/> instance based on the provided text.
    /// </summary>
    /// <param name="text">The text content to include in the message. Cannot be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> instance containing the specified text.</returns>
    public abstract TelegramMessage Pre(string text);

    /// <summary>
    /// Prepares a Telegram message by encoding the provided text according to the specified language.
    /// </summary>
    /// <param name="text">The text to be encoded. Cannot be null or empty.</param>
    /// <param name="language">The language used for encoding the text. Must be a valid language identifier.</param>
    /// <returns>A <see cref="TelegramMessage"/> object containing the encoded text.</returns>
    public abstract TelegramMessage PreCode(string text, string language);

    /// <summary>
    /// Creates a blockquote message with the specified text and optional expandability.
    /// </summary>
    /// <param name="text">The text to display within the blockquote. Cannot be null or empty.</param>
    /// <param name="expandable">A value indicating whether the blockquote can be expanded to show additional content.  <see langword="true"/> if
    /// expandable; otherwise, <see langword="false"/>.</param>
    /// <returns>A <see cref="TelegramMessage"/> representing the blockquote with the specified text and expandability.</returns>
    public abstract TelegramMessage Blockquote(string text, bool expandable);

    /// <summary>
    /// Creates a new <see cref="TelegramMessage"/> instance containing the specified hashtag text.
    /// </summary>
    /// <param name="text">The hashtag text to include in the message. Must not be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> object representing the hashtag.</returns>
    public abstract TelegramMessage HashTag(string text);

    /// <summary>
    /// Creates a new text-based Telegram message with the specified content.
    /// </summary>
    /// <param name="text">The content of the message. Cannot be null or empty.</param>
    /// <returns>A <see cref="TelegramMessage"/> instance representing the text message.</returns>
    public abstract TelegramMessage Text(string text);

    #region 内部的工具方法，继承可见
    /// <summary>
    /// Returns one of two values based on a specified condition.
    /// </summary>
    /// <remarks>
    /// you can use this method to conditionally select between two values in a concise manner.
    /// <code>
    /// string str = $"{IIF(true, trueValue, falseValue)}"
    /// </code>
    /// </remarks>
    /// <typeparam name="T">The type of the values to return.</typeparam>
    /// <param name="condition">The condition to evaluate. If <see langword="true"/>, <paramref name="trueValue"/> is returned; otherwise,
    /// <paramref name="falseValue"/> is returned.</param>
    /// <param name="trueValue">The value to return if <paramref name="condition"/> is <see langword="true"/>.</param>
    /// <param name="falseValue">The value to return if <paramref name="condition"/> is <see langword="false"/>.</param>
    /// <returns>The value of <paramref name="trueValue"/> if <paramref name="condition"/> is <see langword="true"/>; otherwise,
    /// the value of <paramref name="falseValue"/>.</returns>
    protected static T IIF<T>(bool condition, T trueValue, T falseValue) =>
        condition ? trueValue : falseValue;

    #endregion
}
