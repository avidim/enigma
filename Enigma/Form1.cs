using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enigma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string reflector = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        Rotors rotor1 = new Rotors(1);
        Rotors rotor2 = new Rotors(2);
        Rotors rotor3 = new Rotors(3);
        char key;
        int id;
        bool isStep;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                if (textBox1.Text.Length != 0)
                    MessageBox.Show("Choose step for Rotor I, Rotor II and Rotor III", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                return;
            }

            //Empty
            if (textBox1.TextLength == 0)
            {
                textBox2.Clear();
                return;
            }

            //Space button
            if (textBox1.Text[textBox1.TextLength - 1] == ' ')
            {
                textBox2.Text += ' ';
                return;
            }

            //Rotors step
            //Rotor 1
            if (comboBox1.SelectedIndex == 25)
                comboBox1.SelectedIndex = 0;
            else
                comboBox1.SelectedIndex = comboBox1.SelectedIndex + 1;
            isStep = false;
            //Rotor 2
            if (comboBox1.SelectedIndex == 17)
            {
                if (comboBox2.SelectedIndex == 25)
                    comboBox2.SelectedIndex = 0;
                else
                    comboBox2.SelectedIndex = comboBox2.SelectedIndex + 1;
                isStep = true;
            }
            //Rotor 3
            if (comboBox2.SelectedIndex == 5)
            {
                if (isStep == true)
                {
                    if (comboBox3.SelectedIndex == 25)
                        comboBox3.SelectedIndex = 0;
                    else
                        comboBox3.SelectedIndex = comboBox3.SelectedIndex + 1;
                    isStep = false;
                }
            }

            //Rotor 1 | Forward
            key = textBox1.Text[textBox1.TextLength - 1];
            id = getCharacterID(key);
            id = calculatePlus(id, comboBox1.SelectedIndex);
            key = rotor1.getCharacter(id);

            //Rotor 2 | Forward
            id = getCharacterID(key);
            id = calculatePlus(id, calculateMinus(comboBox2.SelectedIndex, comboBox1.SelectedIndex));
            key = rotor2.getCharacter(id);

            //Rotor 3 | Forward
            id = getCharacterID(key);
            id = calculatePlus(id, calculateMinus(comboBox3.SelectedIndex, comboBox2.SelectedIndex));
            key = rotor3.getCharacter(id);

            //Reflector
            id = getCharacterID(key);
            id = calculateMinus(id, comboBox3.SelectedIndex);
            key = reflector[id];

            //Rotor 3 | Back
            id = getCharacterID(key);
            id = calculatePlus(id, comboBox3.SelectedIndex);
            key = getCharacter(id);
            id = rotor3.getCharacterID(key);

            //Rotor 2 | Back
            id = calculateMinus(id, calculateMinus(comboBox3.SelectedIndex, comboBox2.SelectedIndex));
            key = getCharacter(id);
            id = rotor2.getCharacterID(key);
            key = getCharacter(id);

            //Rotor 1 | Back
            id = calculateMinus(id, calculateMinus(comboBox2.SelectedIndex, comboBox1.SelectedIndex));
            key = getCharacter(id);
            id = rotor1.getCharacterID(key);
            key = getCharacter(id);

            //Final conversion
            id = getCharacterID(key);
            id = calculateMinus(id, comboBox1.SelectedIndex);
            textBox2.Text += getCharacter(id);
        }

        private int getCharacterID(char letter)
        {
            return alphabet.IndexOf(letter);
        }

        public char getCharacter(int id)
        {
            return alphabet[id];
        }

        private int calculateMinus(int start, int end)
        {
            int delta;
            if (start >= end)
            {
                delta = start - end;
            }
            else
            {
                delta = Math.Abs(start - end);
                delta = 26 - delta;
            }
            return delta;
        }

        private int calculatePlus(int start, int end)
        {
            int delta;
            if (start + end > 25)
            {
                delta = start + end;
                delta = delta - 26;
            }
            else
            {
                delta = start + end;
            }
            return delta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox4.SelectedIndex = 0;
        }
    }
}