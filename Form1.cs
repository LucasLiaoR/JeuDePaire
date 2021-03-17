using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Boolean carteDecouverte = false;

        public void creer_button(int x, int y, String number)
        {
            Label btn = new Label();

            panel1.Controls.Add(btn);

            btn.Left = x;
            btn.Top = y;
            btn.Tag = number;
            btn.Text = "?";
            btn.Size = new Size(34, 34);
            btn.AllowDrop = true;
            btn.BackColor = Color.White;
            btn.BorderStyle = BorderStyle.FixedSingle;
            btn.TextAlign = ContentAlignment.MiddleCenter;

            btn.MouseDown += new MouseEventHandler(button_MouseDown);
            btn.DragEnter += new DragEventHandler(button_DragEnter);
            btn.DragDrop += new DragEventHandler(button_DragDrop);

        }

        String value_drag;
        int index_btn_depart;


        public void button_MouseDown(object sender, MouseEventArgs e)
        {
            Label butn = (Label)sender;

            value_drag = (String)butn.Tag;
            index_btn_depart = panel1.Controls.IndexOf(butn);
            butn.DoDragDrop("bidon", DragDropEffects.All);
        }

        public void button_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        public void button_DragDrop(object sender, DragEventArgs e)
        {
            Label btn = (Label)sender;

            if (value_drag == (String)btn.Tag && index_btn_depart != panel1.Controls.IndexOf(btn))
            {
                panel1.Controls.RemoveAt(index_btn_depart);
                panel1.Controls.Remove(btn);
                MessageBox.Show("Bien joué!", "Trouvé!", MessageBoxButtons.OK);
                if (panel1.Controls.Count == 0)
                {
                    MessageBox.Show("Félicitations!", "Gagné!", MessageBoxButtons.OK);
                    button1.Enabled = true;
                }
            }
            else if (index_btn_depart == panel1.Controls.IndexOf(btn))
            {
                if (!carteDecouverte)
                {
                    btn.Text = (String)btn.Tag;
                    carteDecouverte = true;
                }
                
            }
            else
            {
                btn.Text = (String)btn.Tag;
                carteDecouverte = false;
                MessageBox.Show("Raté!", "Pas la bonne paire!", MessageBoxButtons.OK);
                btn.Text = "?";
                panel1.Controls[index_btn_depart].Text = "?";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var rand = new Random();

            int x = 0;
            int y = 0;
            String number ="";

            if (textBox1.Text != "")
            {
                button1.Enabled = false;
                for (int i = 1; i <= Int16.Parse(textBox1.Text); i++)
                {
                    number = i.ToString();
                    x = rand.Next(700);
                    y = rand.Next(300);
                    creer_button(x,y, number);
                    x = rand.Next(700);
                    y = rand.Next(300);
                    creer_button(x, y, number);
                }
            }
            else
            {
                MessageBox.Show("Vous n'avez pas saisi de nombre de paire !", "ERREUR !", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
            }
            
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            panel1.Controls[index_btn_depart].Top = e.Y - this.Top - 150;
            panel1.Controls[index_btn_depart].Left = e.X - this.Left - 50;
        }
    }
}
 