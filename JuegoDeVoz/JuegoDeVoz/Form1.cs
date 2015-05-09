using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace JuegoDeVoz
{
    public partial class Form1 : Form
    {
        static Random _random = new Random();
        char a, b, c, d;
        private SpeechRecognitionEngine escucha = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();
            Choices palabros = new Choices();
            palabros.Add(new string[] { "google", "facebook", "twitter", "intro" });
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(palabros);
            try
            {
                escucha.SetInputToDefaultAudioDevice();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("error");
            }
            escucha.LoadGrammar(new DictationGrammar());
            escucha.LoadGrammar(new Grammar(gb));
            escucha.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(lector);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            escucha.RecognizeAsync(RecognizeMode.Multiple);          
            /*
            string texto = textBox1.Text;
            if (texto.Contains(a) && texto.Contains(b) && texto.Contains(c))
            {
                button1.Text = "Correcto";
            }
            else
            {
                button1.Text = "Error";
            
              */
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = true;
                timer1.Interval = 10000;
            }

            catch
            {
                timer1.Enabled = false;
                MessageBox.Show("No input text/interval", "Woops");
            }

            a = GetLetter();
            b = GetLetter();
            c = GetLetter();
            
            label1.Text = a + " - " + b ;
        }

        public void lector(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (RecognizedWordUnit palabra in e.Result.Words)
            {
                textBox1.Text = palabra.Text;
                if (palabra.Text.Contains(a) && palabra.Text.Contains(b) )
                {
                    button1.Text = "Correcto";
                }
                else
                {
                    button1.Text = "Error";
                }
            }

        }

        public static char GetLetter()
        {
            int num = _random.Next(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1.Text = "Error";
            a = GetLetter();
            b = GetLetter();
            c = GetLetter();
           
            label1.Text = a + " - " + b ;
        }
    }
}
