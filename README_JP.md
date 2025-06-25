# Lunqis.Telegram.Bot.Framework

### 強力な Telegram Bot フレームワーク

| [中文](README_CN.md) | [English](README.md) | [日本語](README_JP.md) |

これは強力な Telegram フレームワークで、少ないコードで素早くBotを開発できます。より詳細なAPIの使い方や機能例については、WIKI機能をご参照ください。

## クイックスタート

このフレームワークの使用は非常に簡単で、ASP.NET Coreでコントローラーコードを書くのと同じような感覚で使えます。

まず最初に、Main関数でBotの動作に必要な各種サービスを設定します。

```csharp
public static void Main(string[] args)
{

}
```

設定が完了すると、このBotは正常に実行できるようになります。設定によって、Botの実行時のデフォルトモードも異なります。`UseController`を設定すると、ASP.NET Coreのような方法でコマンド開発が可能です。

```csharp
[BotCommand("hello")]
public Task<IActionResult> SayHello(string str)
{

}
```

このように、簡単な「Hello」と返すボットが作成できます。

あとは、Telegramのボットチャットウィンドウでコマンド `/hello` を入力するだけで、ボットが応答します。