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
public abstract class TelegramMessage
{
    /// <summary>
    /// 
    /// </summary>
    protected enum EscapeMode
    {
        /// <summary>
        /// HTML格式
        /// </summary>
        Html,
        /// <summary>
        /// 第2代MD格式
        /// </summary>
        MarkdownV2,
        /// <summary>
        /// URL格式
        /// </summary>
        URL,
        /// <summary>
        /// 不转义
        /// </summary>
        Raw
    }

    /// <summary>
    /// 解析模式
    /// </summary>
    public ParseMode ParseMode { get; protected set; }

    /// <summary>
    /// 字符拼接
    /// </summary>
    private readonly StringBuilder stringBuilder = new();

    /// <summary>
    /// 第二代MD格式
    /// </summary>
    public static TelegramMessage MarkdownV2 => new MarkdownV2MessageBuilder();

    /// <summary>
    /// HTML格式
    /// </summary>
    public static TelegramMessage Html => new HtmlMessageBuilder();

    /// <summary>
    /// 第一代MD格式
    /// </summary>
    public static TelegramMessage Markdown => new MarkdownMessageBuilder();

    /// <summary>
    /// 默认的实现
    /// </summary>
    public static TelegramMessage Default => Html;

    /// <summary>
    /// HTML消息创建器
    /// </summary>
    private class HtmlMessageBuilder : TelegramMessage
    {
        public HtmlMessageBuilder() : base() =>
            ParseMode = ParseMode.Html;

        public override TelegramMessage Blockquote(string text, bool expandable) =>
            AppendRaw($"<blockquote{IIF(expandable, $" {nameof(expandable)}", string.Empty)}>{EscapeString(text, EscapeMode.Html)}</blockquote>");
        public override TelegramMessage Bold(string text) =>
            AppendRaw($"<b>{EscapeString(text, EscapeMode.Html)}</b>");
        public override TelegramMessage Code(string text) =>
            AppendRaw($"<code>{EscapeString(text, EscapeMode.Html)}</code>");
        public override TelegramMessage HashTag(string text) =>
            AppendRaw($"<a>{EscapeString(text, EscapeMode.Html)}</a>");
        public override TelegramMessage Italic(string text) =>
            AppendRaw($"<i>{EscapeString(text, EscapeMode.Html)}</i>");
        public override TelegramMessage Link(string text, string url) =>
            AppendRaw($"<a href=\"{EscapeString(text, EscapeMode.URL)}\">{EscapeString(text, EscapeMode.Html)}</a>");
        public override TelegramMessage LinkUser(string text, long userid) =>
            AppendRaw($"<a href=\"tg://user?id={userid}\">{EscapeString(text, EscapeMode.Html)}</a>");
        public override TelegramMessage Pre(string text) =>
            AppendRaw($"<pre>{EscapeString(text, EscapeMode.Html)}</pre>");
        public override TelegramMessage PreCode(string text, string language) =>
            AppendRaw($"<pre><code class=\"language-{language}\">{EscapeString(text, EscapeMode.Html)}</code></pre>");
        public override TelegramMessage Spoiler(string text) =>
            AppendRaw($"<tg-spoiler>{EscapeString(text, EscapeMode.Html)}</tg-spoiler>");
        public override TelegramMessage Strikethrough(string text) =>
            AppendRaw($"<s>{EscapeString(text, EscapeMode.Html)}</s>");
        public override TelegramMessage Text(string text) =>
            AppendRaw(EscapeString(text, EscapeMode.Html));
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
    /// MD1代消息创建器
    /// </summary>
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
    /// 
    /// </summary>
    /// <param name="text"></param>
    public static implicit operator TelegramMessage(string text) =>
        Default.AppendRaw(text);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        stringBuilder.ToString();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    protected virtual TelegramMessage AppendRaw(string text)
    {
        _ = stringBuilder.Append(text);
        return this;
    }

    /// <summary>
    /// 转义文本
    /// </summary>
    /// <returns></returns>
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
    /// 添加新行
    /// </summary>
    /// <returns>处理后的文本</returns>
    public virtual TelegramMessage NewLine()
    {
        _ = stringBuilder.AppendLine();
        return this;
    }

    /// <summary>
    /// 创建一个带有连接的文本
    /// </summary>
    /// <param name="text">显示的文本</param>
    /// <param name="url">链接</param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Link(string text, string url);

    /// <summary>
    /// 创建一个带有连接的文本
    /// </summary>
    /// <param name="text">显示的文本</param>
    /// <param name="userid">链接</param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage LinkUser(string text, long userid);

    /// <summary>
    /// 加粗文字
    /// </summary>
    /// <param name="text">想要加粗的文字</param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Bold(string text);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Italic(string text);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Underline(string text);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Strikethrough(string text);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Spoiler(string text);

    /// <summary>
    /// 代码文字块
    /// </summary>
    /// <param name="text">代码文字</param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Code(string text);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Pre(string text);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public abstract TelegramMessage PreCode(string text, string language);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="expandable"></param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Blockquote(string text, bool expandable);

    /// <summary>
    /// Tag标签
    /// </summary>
    /// <param name="text">标签内容</param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage HashTag(string text);

    /// <summary>
    /// 写入文本
    /// </summary>
    /// <param name="text">文本内容</param>
    /// <returns>处理后的文本</returns>
    public abstract TelegramMessage Text(string text);

    #region 内部的工具方法，继承可见
    /// <summary>
    /// 用于判断条件的工具方法
    /// </summary>
    /// <remarks>
    /// 可以在字符串拼接时使用。例如如下的字符串拼接：
    /// <code>
    /// $"{IIF(true, trueValue, falseValue)}"
    /// </code>
    /// </remarks>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="condition">布尔表达式</param>
    /// <param name="trueValue">表达式的返回值为 True 时的返回值</param>
    /// <param name="falseValue">表达式的返回值为 False 时的返回值</param>
    /// <returns>输入的表达式值</returns>
    protected static T IIF<T>(bool condition, T trueValue, T falseValue) =>
        condition ? trueValue : falseValue;

    #endregion
}
