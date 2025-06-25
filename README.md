# Lunqis.Telegram.Bot.Framework

## A powerful Telegram Bot framework

| [中文](README_CN.md) | [English](README.md) | [日本語](README_JP.md) |

This is a powerful Telegram framework that allows you to quickly develop bots with very little code. For more detailed API usage and feature examples, please refer to the WIKI for further information.

## Quick Start

Using this framework is very simple, and the process is similar to writing controller code in ASP.NET Core.

The first step is to configure the various services that the Bot will use during operation in the Main function.

```csharp
public static void Main(string[] args)
{

}
```

After the configuration is complete, the Bot can run successfully. Depending on the configuration, the Bot will have different default modes during execution. When you configure `UseController`, you can develop commands in a way similar to ASP.NET Core.

```csharp
[BotCommand("hello")]
public Task<IActionResult> SayHello(string str)
{

}
```

In this way, a simple bot that says Hello is created.

Now, just enter the command `/hello` in the Telegram bot chat window, and the bot will respond.

## Advanced Configuration

## Framework Development

## Support Services

This open-source product is developed by Lunqis. You are free to use it under the terms of the MIT LICENSE. If you want to customize your own Telegram Bot with features not provided by this framework, or if you are seeking paid support services, please contact us and we will provide you with satisfactory service.

Our website: https://lunqis.com

Contact us: info@lunqis.com