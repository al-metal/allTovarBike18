using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

            Thread tabl = new Thread(() => Actual());
            forms = tabl;
            forms.IsBackground = true;
            forms.Start();
        }

        private void Actual()
        {
            using (var request = new HttpRequest())
            {
                request.UserAgent = HttpHelper.RandomChromeUserAgent();
                request.Cookies = cookieNethouse;
                request.Proxy = HttpProxyClient.Parse("127.0.0.1:8888");
                string post_data = "login=" + tbLogin.Text + "&password=" + tbPassword.Text + "&quick_expire=0&submit=%D0%92%D0%BE%D0%B9%D1%82%D0%B8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
                byte[] dataStream = Encoding.UTF8.GetBytes(String.Format(post_data));
                request.Post("https://nethouse.ru/signin", dataStream).ToString();
            }
            if (cookieNethouse.Count == 1)
            {
                MessageBox.Show("Логин или пароль для сайта Nethouse введены не верно", "Ошибка логина/пароля");
                return;
            }

            string otv = GetRequest("");


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
