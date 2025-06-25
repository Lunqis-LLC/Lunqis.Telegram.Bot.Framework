# Lunqis.Telegram.Bot.Framework

### 一款功能强大的 Telegram Bot 框架

| [中文](README_CN.md) | [English](README.md) | [日本語](README_JP.md) |

这是一款功能强大的 Telegram 框架，只要很少的代码就可以实现Bot的快速编写。如果需要更详细的API使用方法，功能示例，请使用 WIKI 功能进行更详细的查阅。

## 快速开始

使用这个框架非常简单，使用过程就像是在使用 ASP.NET Core 编写控制器代码。

首先第一步是在 Main 函数中配置各类 Bot 运行中会用到的各项服务。

```csharp
public static void Main(string[] args)
{

}
```

配置完成之后，这个 Bot 就可以成功顺利的执行了。根据配置不同，Bot 在执行的时候也会有不同的默认模式。当我们配置使用 `UseController` 之后，就可以使用类似 ASP.NET Core 的方式进行指令的开发了。

```csharp
[BotCommand("hello")]
public Task<IActionResult> SayHello(string str)
{

}
```

这样，一个简单的会说 Hello 的机器人就创建好了。

现在，只需要在 Telegram 的机器人聊天窗口中，输入指令 `/hello` 机器人就会有回应了。