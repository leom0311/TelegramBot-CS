using Bot.Data;
using Bot.DTOs;
using Bot.Model;
using Bot.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Commands
{  
    class BotOperation
    {

        public static ReplyKeyboardMarkup OrderCrypto()
        {
            CryptoData crypto = BotCommand.AllCrypto();
            var rkm = new ReplyKeyboardMarkup();
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            foreach (Crypto p in crypto.Data)
            {
                string name = p.slug;
                cols.Add(new KeyboardButton("" + name));
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();
            }
            rkm.Keyboard = rows.ToArray();
            return rkm;
        }

        public static string SendPrice(string slug)
        {
            string price = BotCommand.GetPrice(slug);
            if (price == null || price == "USD")
            {
                return "CryptoValue doesn't found.Please try again!";
            }
            else
            {
                return price;
            }
        }

        public static async Task<string> ReminderPrice(long chatId,string text)
        {
            string[] words = text.Split(' ');
            if(words.Length != 3)
            {
                return "Please write correct";
            }
            string slug = words[1];
            decimal price;
            try
            {
               price = Convert.ToDecimal(words[2]);
            }
            catch (Exception)
            {
                return "Please write correct price";
            }
            using (SqliteDbContext database = new SqliteDbContext())
            {
                await database.AddAsync(new Reminder() { UserId = chatId, CryptoName = slug, ExceptionPrice = price, CompliteStatus = false });
                await database.SaveChangesAsync();
                return "Your reminder setted.";
            }
        }

        public static async Task<string> TrackAddress(long chatId,string text)
        {
            string[] words = text.Split(' ');
            if (words.Length != 2)
            {
                return "Please write correct";
            }
            string address = words[1];
            using(SqliteDbContext database = new SqliteDbContext())
            {
                await database.AddAsync(new Tracker() { UserId = chatId,Address = address,UpdateTime= DateTime.Now });
                await database.SaveChangesAsync();
            }
            return "Your address watching...";
           
        }

        public static  string BalanceAddress(string text)
        {
            string[] words = text.Split(' ');
            if (words.Length != 3)
            {
                return "Please write correct";
            }
            string type = words[1];
            string address = words[2];
            using(SqliteDbContext db = new SqliteDbContext())
            {
            }
            string price = Convert.ToString(BotCommand.GetBalance(type,address));
            return price + " " + type;
        }
    }
}
