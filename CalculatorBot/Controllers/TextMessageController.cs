using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using CalculatorBot.Services;

namespace CalculatorBot.Controllers
{
    public class TextMessageController
{
    private readonly ITelegramBotClient _telegramClient;
    private readonly TextFunctions _textFunctions;

    public TextMessageController(ITelegramBotClient telegramClient, 
        TextFunctions textFunctions)
    {
        _telegramClient= telegramClient;
        _textFunctions = textFunctions;
    }
    public async Task Handle(Message message, string? command, CancellationToken ct)
    {
        switch (message.Text)
        {
            case "/start":
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($"Длина", $"len"),
                        InlineKeyboardButton.WithCallbackData($"Сумма", $"sum")
                    }); ;
                await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                    $"<b>Наш бот считает длину строки</b> {Environment.NewLine}" +
                    $"или {Environment.NewLine} " +
                    $"<b>Суммирует числа</b> {Environment.NewLine}"
                    , cancellationToken: ct
                    , parseMode: ParseMode.Html
                    , replyMarkup: new InlineKeyboardMarkup(buttons));
                break;
            default:

                switch (command)
                {
                    case "len":
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                            $"В вашем сообщении {_textFunctions.Len(message.Text)} символов.", cancellationToken: ct);
                        break;
                    case "sum":
                        int? sum = _textFunctions.Sum(message.Text);
                        if (sum != null) 
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                                $"Сумма чисел: {sum}", cancellationToken: ct);
                        else
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                                $"Невозможно вычислить сумму!", cancellationToken: ct);
                        break;
                }
                break;
        }
    }
}
}
