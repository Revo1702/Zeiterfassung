using System;
using System.IO;
using System.Windows.Forms;

namespace Zeiterfassung
{
    public partial class Form1 : Form
    {
        DateTime startZeit;
        DateTime endZeit;
        int gesamtZeit;
        int savedTime;
        int origTime = 26400; //26400 sec for 7h and 20min
        int newTime = 0;

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            startZeit = DateTime.Now;
            Console.WriteLine(startZeit);
            string savedTimeString = ReadTxt();
            if (savedTimeString != "0")
            {
                savedTime = Convert.ToInt32(savedTimeString);
                if (savedTime != gesamtZeit)
                {
                    gesamtZeit += savedTime;
                    origTime -= gesamtZeit;
                    WriteText(Convert.ToString(gesamtZeit));
                }

            }
            timer2.Start();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            endZeit = DateTime.Now;
            int dauer = (int)(endZeit - startZeit).TotalSeconds + gesamtZeit;
            gesamtZeit = dauer;
            WriteText(Convert.ToString(gesamtZeit));
            string savedTimeString = ReadTxt();
            savedTime = Convert.ToInt32(savedTimeString);
            Console.WriteLine(gesamtZeit);


            if (gesamtZeit > 3600)
            {
                int seconds = (int)gesamtZeit;
                int minutes = seconds / 60;
                label1.Text = String.Format("{0} Stunde/n {1} Minute/n {2} Sekunde/n", minutes / 60, minutes % 60, seconds % 60);
            }
            else if (gesamtZeit > 60)
            {
                label1.Text = String.Format("{0} Minute/n {1} Sekunde/n", gesamtZeit / 60, gesamtZeit % 60);
            }
            else
            {
                label1.Text = String.Format("{0} Sekunde/n", gesamtZeit);
            }
            timer2.Stop();
            timer3.Stop();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            label4.Text = DateTime.Now.ToString();
            timer2.Stop();
            timer3.Stop();
            if (origTime > 3600)
            {
                int seconds2 = (int)origTime;
                int minutes2 = seconds2 / 60;
                label6.Text = String.Format("{0} Stunde/n {1} Minute/n {2} Sekunde/n", minutes2 / 60, minutes2 % 60, seconds2 % 60);
            }
            else
            {
                label6.Text = origTime / 60 + " Minuten";
            }
            if (origTime <= 0)
            {
                timer3.Start();
                int seconds = (int)newTime;
                int minutes = seconds / 60;
                int hours = minutes / 60;
                label5.Text = "Überstunden: ";
                label6.Text = String.Format("{0} Stunde/n {1} Minute/n {2} Sekunden", hours, minutes, seconds);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString();
            timer1.Start();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            origTime--;
            if (origTime > 3600)
            {
                int seconds2 = (int)origTime;
                int minutes2 = seconds2 / 60;
                label6.Text = String.Format("{0} Stunde/n {1} Minute/n {2} Sekunde/n", minutes2 / 60, minutes2 % 60, seconds2 % 60);
                //Testcode um die Sekunden zu sehen
                //label6.Text = Convert.ToString(origTime);
            }
            else
            {
                label6.Text = origTime / 60 + " Minuten";
            }
            if (origTime <= 0)
            {
                timer2.Stop();
                timer3.Start();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            newTime++;
            if (origTime <= 0)
            {
                timer3.Start();
                int seconds = (int)newTime;
                int minutes = seconds / 60;
                int hours = minutes / 60;
                label5.Text = "Überstunden: ";
                label6.Text = String.Format("{0} Stunde/n {1} Minute/n {2} Sekunden", hours, minutes % 60, seconds % 60);
            }
            timer3.Start();
        }
        static void WriteText(string input)
        {
            Console.WriteLine("Writing...");
            StreamWriter sw = new StreamWriter("../../../log.txt");
            sw.WriteLine(input);
            sw.Close();
        }
        static string ReadTxt()
        {
            Console.WriteLine("Reading..");
            StreamReader sr = new StreamReader("../../../log.txt");
            string line = sr.ReadLine();
            sr.Close();
            return line;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WriteText("0");
            gesamtZeit = 0;
            origTime = 26400;
            string reset = ReadTxt();
            Console.WriteLine(reset);
        }
    }
}