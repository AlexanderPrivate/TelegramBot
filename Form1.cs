using System;
using System.Text;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegrambot
{
    public partial class Form1 : Form
    {
        TelegramBotClient client;
        string lastMessage = "";

        [Obsolete]
        public Form1()
        {
            InitializeComponent();
            client = new TelegramBotClient("6729845188:AAF3ZRwGiWlCryMa8kmWJXFbXma9gXPq4qs");
            client.StartReceiving();

            

            client.OnMessage += Client_OnMessage;
            client.OnCallbackQuery += Client_OnCallbackQuery;
        }
        [Obsolete]
        private void Client_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var ChatID = e.CallbackQuery.Message.Chat.Id;
            var data = e.CallbackQuery.Data;

            switch(data){
                case "time":
                client.SendTextMessageAsync(ChatID, DateTime.UtcNow.ToLongDateString());
                break;
            }

        }

        [Obsolete]
        private void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var ChatId = e.Message.Chat.Id;
            var Message = e.Message.Text;

            var Keys = new InlineKeyboardButton[]
            {
                InlineKeyboardButton.WithCallbackData("تاریخ", "time")
            };

            var markup = new InlineKeyboardMarkup(Keys);

            StringBuilder builder = new StringBuilder();

            builder.Append("سلام دستورات من :");
            builder.AppendLine();
            builder.Append("/time");

            if(lastMessage != Message && !Message.Equals("/time"))
            {
                client.SendTextMessageAsync(ChatId, builder.ToString());
                lastMessage = Message;
            }else if (Message.Equals("/time"))
            {
                client.SendTextMessageAsync(ChatId, "برای دریافت تاریخ بر روی دکمه کلیک کنید",
                    replyMarkup: markup);
            }
        }
    }
}

