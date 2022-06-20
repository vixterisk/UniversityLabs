using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORM2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeDgvCaseFormFactor();
            InitializeDgvCaseInformation();
        }
        private string WhitespaceFormat(string text)
        {
            text = Regex.Replace(text, @"[\s]+", " ");
            return text.Trim(new char[] { ' ' });
        }

        private string Formatted(string str)
        {
            return WhitespaceFormat(str).ToLower();
        }
    }
}
