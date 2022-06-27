using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Timers;

namespace auto_bitcoin
{
    public partial class Form1 : Form
    {

        public static string url = "https://www.binance.com/en";
        string max = "";
        string min = "";
        string amount = "";
        public static System.Timers.Timer goTimer;
        public Form1()
        {
            InitializeComponent();

            max = Properties.Settings.Default.max;
            min = Properties.Settings.Default.min;
            amount = Properties.Settings.Default.amount;

            textBox1.Text = max;
            textBox2.Text = min;
            textBox3.Text = amount;

            goTimer = new System.Timers.Timer(500);
            goTimer.Elapsed += onTimerOn;

            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);

            chromiumWebBrowser1.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            chromiumWebBrowser1.JavascriptObjectRepository.Register("bound", new BoundObject(), false, BindingOptions.DefaultBinder);

            chromiumWebBrowser1.Load(url+ "/my/dashboard");
        }

        public static bool loginSuccess = false;
        public static bool ready = false;
        private void chromiumWebBrowser1_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading) {
                chromiumWebBrowser1.ExecuteScriptAsync(script.checkSite());
                

            }

        }

        private void onTimerOn(object source, ElapsedEventArgs e) {
            if (ready) {
                chromiumWebBrowser1.ExecuteScriptAsync("goTrade("+max+ "," + min + "," + amount + "); console.log()");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            max = textBox1.Text;
            Properties.Settings.Default.max = max;
            Properties.Settings.Default.Save();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            min = textBox2.Text;
            Properties.Settings.Default.min = min;
            Properties.Settings.Default.Save();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            amount = textBox3.Text;
            Properties.Settings.Default.amount = amount;
            Properties.Settings.Default.Save();
        }

        bool start = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!start)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
                {
                    start = true;
                    ready = true;
                    button1.Text = "정지";
                    chromiumWebBrowser1.ExecuteScriptAsync(script.go());
                    goTimer.Start();
                }
                else
                {
                    MessageBox.Show("값을 설정해 주세요.");
                }
            }
            else {

                button1.Text = "시작";
                start = false;
                ready = false;
            }
        }
    }

    public class BoundObject {

        public void Message(string msg) {
            MessageBox.Show(msg);
        }

        public void checkLogin(string rst) {
            if (rst == "true") {
                Form1.loginSuccess = true;
            } else {
                Form1.loginSuccess = false;
            }
        }

        public void checkReady(string rst)
        {
            if (rst == "true")
            {
                Form1.ready = true;
            }
            else
            {
                Form1.ready = false;
            }
        }

    }
}
