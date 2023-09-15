using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zip_codes
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            readfile();
        }
        char[] letters = new char[26] {'a','b','c','d','e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y','z' };
        List<int> indexes = new List<int>();
        Random numbergenerator = new Random();

        class ZipCodeMasks
        {
        public  ZipCodeMasks(string country, string mask)
            {
                Country = country;
                Mask = mask;
            }
          public  string Country { get; set; }
          public  string Mask { get; set; }
        }
        List<string> countries = new List<string>();
        List<string> masks = new List<string>();

        void readfile()
        {
            string file = @"C:\Users\1\Downloads\zipcodemasks.txt";
            StreamReader fileReader = new StreamReader(file);
            string line1;
            string line2;
            while ((line1 = fileReader.ReadLine()) != null)
            {
                line2 = fileReader.ReadLine();
                ZipCodeMasks data = new ZipCodeMasks(line1, line2);
                countries.Add(data.Country);
                masks.Add(data.Mask);
            }

            comboBox1.DataSource = countries.Distinct().ToList();
        }

        void addNewZipCode(string zipCode, string zipMask)
        {
            if (zipCode.Length != zipMask.Length)
            {
                MessageBox.Show("please enter code correctly");
                return;
            }
            for (int i = 0; i < zipCode.Length; i++)
            {
                if (zipMask[i] != '9' && zipMask[i] != 'A' && zipCode[i] != zipMask[i])
                {
                    MessageBox.Show("please enter code correctly");
                    return;
                }
                if (zipMask[i] == '9' && !char.IsDigit(zipCode[i]))
                {
                    MessageBox.Show("please enter code correctly");
                    return;
                }

                if (zipMask[i] == 'A' && !char.IsLetter(zipCode[i]))
                {
                    MessageBox.Show("please enter code correctly");
                    return;
                }
            }
            string result = comboBox1.SelectedItem.ToString() + " " + zipCode.ToUpper();
            listBox1.Items.Add(result);
        }

        string RandomZipCode (string zipMask)
        {
            string result = "";
            for (int i = 0; i < zipMask.Length; i++)
            {
                if (zipMask[i] == '9')
                {
                    result += numbergenerator.Next(0, 10);
                }
                

                if (zipMask[i] == 'A')
                {
                    result += letters[numbergenerator.Next(0, 26)];
                }
                if (zipMask[i] != '9' && zipMask[i] != 'A')
                {
                    result += zipMask[i];
                }    
            }
            return result;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            
            string zipCode = textBox1.Text;
            textBox1.Clear();
            string zipMask = comboBox2.SelectedItem.ToString();
            addNewZipCode(zipCode, zipMask);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            for (int i = 0; i < countries.Count(); i++)
            {
                if (comboBox1.SelectedItem.ToString() == countries[i])
                {
                    indexes.Add(i);
                }
            }
            
            for (int j = 0; j < indexes.Count() ; j++)
            {
                comboBox2.Items.Add(masks[indexes[j]]);
                comboBox2.SelectedIndex = 0;
            }
            indexes.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string zipMask = comboBox2.SelectedItem.ToString();
            string result = comboBox1.SelectedItem.ToString() + " " + RandomZipCode(zipMask).ToUpper();
           listBox1.Items.Add(result);
        }
    }
}
