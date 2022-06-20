using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        Vector vector1;
        Vector vector2;

        public Form1()
        {
            InitializeComponent();
            textBox1.TextChanged += textBox1_TextChanged;
            textBox2.TextChanged += textBox2_TextChanged;            
        }

        private void InitializeVectorAndProps(TextBox xTextBox, TextBox yTextBox)
        {
            int x, y;
            if (Int32.TryParse(xTextBox.Text, out x) && Int32.TryParse(yTextBox.Text, out y))
            {
                vector1 = new Vector(Int32.Parse(xTextBox.Text), Int32.Parse(yTextBox.Text));
                label5.Text = String.Format("{0:0.#####}", vector1.GetLength());
                CalculateAllFunctions();
            }
        }

        private void CalculateAllFunctions()
        {
            if (vector1 != null && vector2 != null)
            {
                var newVector = vector1.Add(vector2);
                label12.Text = String.Format("{0:0.#####}", newVector.X);
                label13.Text = String.Format("{0:0.#####}", newVector.Y);
                newVector = vector1.Substract(vector2);
                label16.Text = String.Format("{0:0.#####}", newVector.X);
                label17.Text = String.Format("{0:0.#####}", newVector.Y);
                label21.Text = String.Format("{0:0.#####}", vector1.ScalarMultiplication(vector2));
                label23.Text = String.Format("{0:0.#####}", vector1.CosBetweenVectors(vector2));
            }
        }

        private void InitializeVectorAndProps2(TextBox xTextBox, TextBox yTextBox)
        {
            int x, y;
            if (Int32.TryParse(xTextBox.Text, out x) && Int32.TryParse(yTextBox.Text, out y))
            {
                vector2 = new Vector(Int32.Parse(xTextBox.Text), Int32.Parse(yTextBox.Text));
                label10.Text = String.Format("{0:0.#####}", vector2.GetLength());
                CalculateAllFunctions();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InitializeVectorAndProps(textBox1, textBox2);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            InitializeVectorAndProps(textBox1, textBox2);

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            InitializeVectorAndProps2(textBox3, textBox4);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        { 
            InitializeVectorAndProps2(textBox3, textBox4);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label5(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
    }
}
