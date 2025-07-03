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
using Lunqis.CoreLib.Extensions.StateMatch;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lunqis.Telegram.Bot.Framework.Users;
/// <summary>
/// Telegram请求
/// </summary>
public sealed class TelegramRequest : Update
{
    /// <summary>
    /// 发送的消息内容
    /// </summary>
    public string MessageText { get; } = string.Empty;

    /// <summary>
    /// 机器人指令
    /// </summary>
    /// <remarks>
    /// 机器人指令可以附带参数，例如：<br></br>
    /// <code>
    /// /start abc 123
    /// </code>
    /// 可以获取到的指令为：
    /// <code>
    /// /start
    /// </code>
    /// 如果没有指令，则返回空字符串
    /// </remarks>
    public string BotCommand
    {
        get
        {
            return Message?.Text ?? string.Empty;
        }
    }

    /// <summary>
    /// 获取机器人指令的参数
    /// </summary>
    /// <remarks>
    /// 机器人指令可以附带参数，例如：<br></br>
    /// <code>
    /// /start abc 123
    /// </code>
    /// 可以获取到的参数为：
    /// <code>
    /// abc 123
    /// </code>
    /// </remarks>
    public Dictionary<string, string> Params
    {
        get
        {
            if (!string.IsNullOrEmpty(BotCommand))
            {
                var message = Message?.Text ?? string.Empty;
                string? args;
                if (message.Length == BotCommand.Length)
                    return [];
                else
                    args = message[BotCommand.Length..];

                return ArgsParse.Instance.StartArgsParse(args);
            }
            return [];
        }
    }

    /// <summary>
    /// 用户的聊天信息，或者群组的聊天信息
    /// </summary>
    public Chat? Chat { get; }

    /// <summary>
    /// Chat ID
    /// </summary>
    /// <remarks>
    /// Chat ID，这个ID在 <see cref="ChatType.Private"/> 时候，为发送用户的User ID <br></br>
    /// 在 <see cref="ChatType.Channel"/> <see cref="ChatType.Group"/> <see cref="ChatType.Supergroup"/> 时，则为群组的ID
    /// </remarks>
    public ChatId? ChatId { get; }

    /// <summary>
    /// 发送用户的ID
    /// </summary>
    public User? RequestUser { get; }

    /// <summary>
    /// 请求用户的Chat ID
    /// </summary>
    public ChatId? RequestUserChatID =>
        RequestUser == null ? null : new ChatId(RequestUser.Id);

    /// <summary>
    /// 
    /// </summary>
    public ITelegramBotClient TelegramBotClient { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="update"></param>
    /// <param name="telegramBotClient"></param>
    internal TelegramRequest(Update update, ITelegramBotClient telegramBotClient)
    {
        Message = update.Message;
        Id = update.Id;
        EditedMessage = update.EditedMessage;
        ChannelPost = update.ChannelPost;
        CallbackQuery = update.CallbackQuery;
        ChatJoinRequest = update.ChatJoinRequest;
        ChatMember = update.ChatMember;
        ChosenInlineResult = update.ChosenInlineResult;
        MyChatMember = update.MyChatMember;
        PollAnswer = update.PollAnswer;
        Poll = update.Poll;
        PreCheckoutQuery = update.PreCheckoutQuery;
        ShippingQuery = update.ShippingQuery;
        InlineQuery = update.InlineQuery;
        EditedChannelPost = update.EditedChannelPost;

        TelegramBotClient = telegramBotClient;

        string? messageText = null;
        Chat? chat = null;
        ChatId? chatId = null;
        User? requestUser = null;

        MessageText = messageText ?? string.Empty;
        Chat = chat;
        ChatId = chatId;
        RequestUser = requestUser;
    }
}
