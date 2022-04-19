using Bot.Data;
using Bot.DTOs;
using Bot.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

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

        public static string DrawGraph()
        {
            var chart = new Chart();
            chart.Size = new Size(640, 320);
            chart.ChartAreas.Add("ChartArea1");
            chart.Legends.Add("legend1");
            chart.Series.Add("line");
            chart.Series["line"].LegendText = "Graph";
            chart.Series["line"].ChartType = SeriesChartType.Line;
            //Bu hisse bele yox API dnn data gelecek bunu islede bilmirem d
            //deye APIden datani cekib vermirem Yoxlama meqsedli bele x,y
            //verirem.
            for (double x = 0; x < 3 * Math.PI; x += 0.01)
            {
                chart.Series["line"].Points.AddXY(x, x + 4);
            }
            Guid path_url = Guid.NewGuid();
            chart.SaveImage(path_url.ToString()+".png", System.Drawing.Imaging.ImageFormat.Png);
            return "/"+path_url.ToString() +".png";
        }

        public static  string BalanceAddress(long id)
        {
            string price = "";
            using(SqliteDbContext db = new SqliteDbContext())
            {
                var addresses = from cust in db.Trackers
                              where cust.UserId == id
                              select cust;
                foreach(var address in addresses)
                {
                    price += BotCommand.GetBalance(address.Address) + "\r\n";
                }
            }
            return price;
        }
    }
}
