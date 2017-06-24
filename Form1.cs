using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet.Net;

namespace allTovarBike18
{
    public partial class Form1 : Form
    {
        Thread forms;
        CookieDictionary cookieNethouse = new CookieDictionary();
        int countTovars = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbLogin.Text = Properties.Settings.Default.login;
            tbPassword.Text = Properties.Settings.Default.password;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            #region Сохранение паролей
            Properties.Settings.Default.login = tbLogin.Text;
            Properties.Settings.Default.password = tbPassword.Text;
            Properties.Settings.Default.Save();
            #endregion

            countTovars = 0;

            Thread tabl = new Thread(() => Actual());
            forms = tabl;
            forms.IsBackground = true;
            forms.Start();
        }

        private void Actual()
        {
            string otv = GetRequest("https://bike18.ru/products");
            MatchCollection razdel = new Regex("(?<=<div class=\"category-capt-txt -text-center\"><a href=\").*?(?=\" class=\"blue\">)").Matches(otv);
            MatchCollection tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*?(?=\" >)").Matches(otv);
            if (tovar.Count != 0)
                countTovars += tovar.Count;
            else
            {
                for (int i = 0; razdel.Count > i; i++)
                {
                    string urlRazdel = razdel[i].ToString();
                    otv = GetRequest("https://bike18.ru" + urlRazdel + "?page=all");
                    MatchCollection razdel2 = new Regex("(?<=<div class=\"category-capt-txt -text-center\"><a href=\").*?(?=\" class=\"blue\">)").Matches(otv);
                    tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*?(?=\" >)").Matches(otv);
                    if (tovar.Count != 0)
                        countTovars += tovar.Count;
                    else
                    {
                        for (int i2 = 0; razdel2.Count > i2; i2++)
                        {
                            string urlRazdel2 = razdel2[i2].ToString();
                            otv = GetRequest("https://bike18.ru" + urlRazdel2 + "?page=all");
                            MatchCollection razdel3 = new Regex("(?<=<div class=\"category-capt-txt -text-center\"><a href=\").*?(?=\" class=\"blue\">)").Matches(otv);
                            tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*?(?=\" >)").Matches(otv);
                            if (tovar.Count != 0)
                                countTovars += tovar.Count;
                            else
                            {
                                for (int i3 = 0; razdel3.Count > i3; i3++)
                                {
                                    string urlRazdel3 = razdel3[i3].ToString();
                                    otv = GetRequest("https://bike18.ru" + urlRazdel3 + "?page=all");
                                    MatchCollection razdel4 = new Regex("(?<=<div class=\"category-capt-txt -text-center\"><a href=\").*?(?=\" class=\"blue\">)").Matches(otv);
                                    tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*?(?=\" >)").Matches(otv);
                                    if (tovar.Count != 0)
                                        countTovars += tovar.Count;
                                    else
                                    {
                                        for (int i4 = 0; razdel4.Count > i4; i4++)
                                        {
                                            string urlRazdel4 = razdel4[i4].ToString();
                                            otv = GetRequest("https://bike18.ru" + urlRazdel4 + "?page=all");
                                            MatchCollection razdel5 = new Regex("(?<=<div class=\"category-capt-txt -text-center\"><a href=\").*?(?=\" class=\"blue\">)").Matches(otv);
                                            tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*?(?=\" >)").Matches(otv);
                                            if (tovar.Count != 0)
                                                countTovars += tovar.Count;
                                            else
                                            {
                                                for (int i5 = 0; razdel5.Count > i5; i5++)
                                                {
                                                    string urlRazdel5 = razdel5[i5].ToString();
                                                    otv = GetRequest("https://bike18.ru" + urlRazdel5 + "?page=all");
                                                    MatchCollection razdel6 = new Regex("(?<=<div class=\"category-capt-txt -text-center\"><a href=\").*?(?=\" class=\"blue\">)").Matches(otv);
                                                    tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*?(?=\" >)").Matches(otv);
                                                    if (tovar.Count != 0)
                                                        countTovars += tovar.Count;
                                                    else
                                                    {
                                                        for (int i6 = 0; razdel6.Count > i6; i6++)
                                                        {
                                                            string urlRazdel6 = razdel6[i6].ToString();
                                                            otv = GetRequest("https://bike18.ru" + urlRazdel6 + "?page=all");
                                                            MatchCollection razdel7 = new Regex("(?<=<div class=\"category-capt-txt -text-center\"><a href=\").*?(?=\" class=\"blue\">)").Matches(otv);
                                                            tovar = new Regex("(?<=<div class=\"product-link -text-center\"><a href=\").*?(?=\" >)").Matches(otv);
                                                            if (tovar.Count != 0)
                                                                countTovars += tovar.Count;
                                                            else
                                                            {

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Количество товаров = " + countTovars);


        }
        private string GetRequest(string v)
        {
            var request2 = new HttpRequest();
            request2.UserAgent = HttpHelper.RandomChromeUserAgent();
            request2.Proxy = HttpProxyClient.Parse("127.0.0.1:8888");
            // Отправляем запрос.
            HttpResponse response = request2.Get(v);
            // Принимаем тело сообщения в виде строки.
            string content = response.ToText();
            return content;
        }
    }
}
