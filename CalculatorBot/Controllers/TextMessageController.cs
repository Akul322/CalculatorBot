using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CalculatorBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {


            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Подсчет символов", "Sum"), 
                        InlineKeyboardButton.WithCallbackData($" Сложение чисел", "calculate")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Бот-калькулятор .</b> {Environment.NewLine}" +
                       $"{Environment.NewLine}Этот бот Счтаает количество символов в сообщении, если выбрать (Подсчет символов)," +
                       $" либо складывает числа, если выбрать (Сложение чисел) к примеру, если написать (11 5 1 3), бот выведет на экран 20.{Environment.NewLine}",


                       cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:            


                    Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "(;", cancellationToken: ct);
                break;

            }

        
        }
    }
}
