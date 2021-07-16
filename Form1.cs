using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace Ping {
    public partial class Form1 : Form {
        private int count;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            comboBox1.SelectedIndex = 0;
            if((File.Exists(@"ping.txt") && new FileInfo(@"ping.txt").Length > 0)) {
                StreamReader sr = new StreamReader(@"ping.txt");
                string item = sr.ReadLine();
                int index = comboBox1.FindString(item);
                if((index != -1 & !string.IsNullOrEmpty(item))) {
                    comboBox1.SelectedIndex = index;
                }
                sr.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            StreamWriter wr = new StreamWriter(@"ping.txt");
            wr.WriteLine(comboBox1.Text);
            wr.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e) {
            if(checkBox1.Checked) {
                timer1.Start();
                return;
            }
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 10000;
            PingReply r;
            string s;
            s = textBox1.Text;
            if(s == "")
                s = comboBox1.GetItemText(comboBox1.SelectedItem);
            r = p.Send(s, timeout, buffer);
            if(r.Status == IPStatus.Success) {
                richTextBox1.AppendText(Environment.NewLine + " Successful" + " Response delay = " + r.RoundtripTime.ToString() + " ms");
                richTextBox1.ScrollToCaret();
                count++;
            } else if(r.Status == IPStatus.TimedOut) {
                richTextBox1.AppendText(Environment.NewLine + "TIMED OUT");
                richTextBox1.ScrollToCaret();
            }
            if(r.RoundtripTime > int.Parse(label6.Text)) {
                label6.Text = r.RoundtripTime.ToString();
            }
            if(r.RoundtripTime < int.Parse(label5.Text)) {
                label5.Text = r.RoundtripTime.ToString();
            }
        }


        private void button3_Click(object sender, EventArgs e) {
            if(textBox1.Text == string.Empty) {
                richTextBox1.Text = "Nothing Added";
            } else {
                comboBox1.Items.Add(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Interval = int.Parse(textBox2.Text);
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 10000;
            PingReply r;
            string s;
            s = textBox1.Text;
            if(s == "")
                s = comboBox1.GetItemText(comboBox1.SelectedItem);
            r = p.Send(s, timeout, buffer);
            if(r.Status == IPStatus.Success) {
                richTextBox1.AppendText(Environment.NewLine + " Successful" + " Response delay = " + r.RoundtripTime.ToString() + " ms");
                richTextBox1.ScrollToCaret();
                count++;
            } else if(r.Status == IPStatus.TimedOut) {
                richTextBox1.AppendText(Environment.NewLine + "TIMED OUT");
                richTextBox1.ScrollToCaret();
            }
            if(r.RoundtripTime > int.Parse(label6.Text)) {
                label6.Text = r.RoundtripTime.ToString();
            }
            if(r.RoundtripTime < int.Parse(label5.Text)) {
                label5.Text = r.RoundtripTime.ToString();
            }

        }

        private void button4_Click(object sender, EventArgs e) {
            richTextBox1.Text = string.Empty;
            label6.Text = "0";
            label5.Text = "999";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenURL("https://ping.canbeuseful.com/");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenURL("https://www.nperf.com/en/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            OpenURL("https://www.speedtest.net/");

        }

        private void OpenURL(string url) {
            string key = @"htmlfile\shell\open\command";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);
            string Default_Browser_Path = ((string)registryKey.GetValue(null, null)).Split('"')[1];
            Process p = new Process();
            p.StartInfo.FileName = Default_Browser_Path;
            p.StartInfo.Arguments = url;
            p.Start();
        }
    }
}
