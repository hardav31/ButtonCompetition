using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ButtonCare
{
    public delegate void HelperToCall(Button btn);
    
    public partial class Form1 : Form
    {
        Thread t1;
        Thread t2;
        Thread t3;
        HelperToCall helper;

        Random r;
        ButtonCompare[] button;
        public Form1()
        {
            InitializeComponent();
            helper = new HelperToCall(Motion);
            r = new Random();
            button = new ButtonCompare[] {first_btn,second_btn,third_btn };
             
        }
        void Motion(Button button) 
        {
            button.Location = new Point(button.Location.X+r.Next(3),button.Location.Y);
            Lider();
            if (button.Location.X > pictureBox1.Location.X - button.Width) 
            {
                pause_btn_Click(new object(), new EventArgs());
                MessageBox.Show("Won "+button.Text);
                start_btn.Enabled = false;
            }
        }
        void Lider() 
        { 
            Array.Sort(button);
            button[0].BackColor = Color.Yellow;
            button[1].BackColor = button[2].BackColor = SystemColors.Control;
        }

         void Run1()
        {
            while (true) 
            {
                Thread.Sleep(r.Next(5,40));
                Invoke(helper,first_btn);
            }  
        }
         void Run2() 
         {
             while (true) 
             {
                 Thread.Sleep(r.Next(5,40));
                 Invoke(helper, second_btn);
             }
         }
         void Run3() 
         {
             while (true) 
             {
                 Thread.Sleep(r.Next(5,40));
                 Invoke(helper, third_btn);
             }
         }

        private void start_btn_Click(object sender, EventArgs e)
        {
            start_btn.Enabled = false;
            pause_btn.Enabled = stop_btn.Enabled = true;
            if (t1 != null) 
            {
                t1.Resume();
                t2.Resume();
                t3.Resume();
                return;
            }
            
            t1 = new Thread(Run1);
            t2 = new Thread(Run2);
            t3 = new Thread(Run3);

            t1.IsBackground = t2.IsBackground = t3.IsBackground = true;

            t1.Start();
            t2.Start();
            t3.Start();
        }

        private void pause_btn_Click(object sender, EventArgs e)
        {
            start_btn.Enabled = true;
            if (t1 != null) 
            {
                t1.Suspend();
                t2.Suspend();
                t3.Suspend();
            }
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            pause_btn_Click(sender, e);
            Reset();
            for (int i = 0; i < button.Length; i++) 
            {
                button[i].BackColor = SystemColors.Control;
            }
        }
        void Reset() 
        {
            first_btn.Location = new Point(56, first_btn.Location.Y);
            second_btn.Location = new Point(56, second_btn.Location.Y);
            third_btn.Location = new Point(56, third_btn.Location.Y);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop_btn_Click(sender, new EventArgs());
        }
    }
}
