using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caesar___Entschlüsselung
{
    public partial class Form1 : Form
    {
        //Globale Variablen
        char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray(); //Array des Alphabets für spätere Analyse

        public Form1()
        {
            InitializeComponent();
        }

        public double[] analyseText(String text)
        {
            double[] Buchstaben_AbsoluteH = new double[26]; //Array für absolute Häufigkeit der Buchstaben
            double[] Buchstaben_RelH = new double[26]; //Array für relative Häufigkeit der Buchstaben

            for(int i=0; i<alphabet.Length; i++)
            {
                for (int k = 0; k < text.Length; k++) //Zähle wie oft welcher Buchstabe im Text vorkommt & berechne die relative Häufigkeit
                {
                    var curChar = text[k];
                    if (curChar == alphabet[i])
                    {
                        Buchstaben_AbsoluteH[i]++; //Berechnung der absoluten Häufigkeit
                        Buchstaben_RelH[i] = Buchstaben_AbsoluteH[i] / text.Length; //Berechnung der relativen Häufigkeit
                    }
                }
            }
            return Buchstaben_RelH; //Gib relative Häufigkeit zurück
        } 
        
        public int verschErkennen(int[] verschiebungen)
        {
            int[] erlaubteVerschiebungen = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }; //mögliche Verschiebungen die auftreten können
            int[] counter = new int[26]; //Array das pro erkannte Verschiebung zählt wie oft die selbe Verschiebung erkannt wurde
            var verschiebung = 0; //Endgültige Verschiebung des Textes
            
            for (int i = 0; i < verschiebungen.Length; i++)
            {
                for (int j = 0; j < erlaubteVerschiebungen.Length; j++)
                {
                    if (verschiebungen[i] == erlaubteVerschiebungen[j]) 
                    {
                        counter[j]++; //Verschiebung erkannt -> wird mitgezählt wie oft die selbe Verschiebung erkannt wurde
                    }
                }
            }
            
            verschiebung = Array.IndexOf(counter, counter.Max()); //Finde Index der höchsten Zahl im Array

            return verschiebung;
        }

        private void btn_decrypt_Click(object sender, EventArgs e)
        {
            var text_encrypted = box_encrypted.Text.ToLower(); //Auslesen und in Kleinbuchstaben setzen des verschlüsselten Textes  
            var text_example = box_exampleText.Text.ToLower(); //Auslesen und in Kleinbuchstaben setzen Beispieltextes
            var text_decrypted = ""; //Variabel für entschlüsselten Text
            decimal newCharNum = 0; //Variable für Char Nummber die Verschlüsselt bzw Entschlüsselt wurde
            char[] text_chars = text_encrypted.ToCharArray(); //Wandle 'Text Eingabe String'(text_input) in Char Array (Array das aus den Zeichen des Strings besteht) um


            //Analyse des Beispieltextes und des verschlüsselten Textes
            var example_relH = analyseText(text_example); //Gibt relative Häufigkeit der Buchstaben des Beispieltextes zurück
            var encrypted_relH = analyseText(text_encrypted); //Gibt relative Häufigkeit der Buchstaben des verschlüsselten Textes zurück
            int[] erkannteVersch = new int[26];

            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (Math.Abs(example_relH[i] - encrypted_relH[j]) < 0.01) //Bei geringer Abweichung der relativen Häufigkeiten -> Übereinstimmung
                    {
                        Console.WriteLine(alphabet[i] + ": " + " ( "+ Convert.ToInt32(j-i) +" )" + example_relH[i] + " - " + encrypted_relH[j]);

                        erkannteVersch[i] = j - i; //erkannte Verschiebung pro Array Eintrag
                    }
                }
            }
            
            var verschiebung = verschErkennen(erkannteVersch); //Erkenne die Verschiebung anhand der Übereinstimmungen
            lbl_key.Text = "Erkannte Verschiebung: " + verschiebung; //Info über die Verschiebung im UI

            //Fehlermeldung bei ungültiger Verschiebung
            if (verschiebung == 0)
            {
                MessageBox.Show("Entweder ist der Text nicht verschlüsselt oder er ist zu kurz um entschlüsselt zu werden.", 
                "Fehler",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }

            //Text wird mit erkannter Verschiebung entschlüsselt
            //Für jedes Zeichen in text_chars wird das Zeichen entsprechend des Schlüssels verschoben
            for (int i = 0; i < text_chars.Length; i++)
            {
                //Wandle Zeichen aus dem Char Array in Alphabet Nummer um (A=0, B=1, C=2, ...)
                char curChar = text_chars[i];

                //Überprüfe für jeden Buchstaben im Alphabet ob er mit der Nummer übereinstimmt
                for (int k = 0; k < alphabet.Length; k++)
                {
                    //Wenn Übereinstimmung gefunden -> entsprechendes Zeichen zurückgeben und Ver- / Entschlüsseln
                    if (curChar == alphabet[k])
                    {
                        //Übereinstimmung gefunden -> Variable festlegen für einfachere Lesbarkeit
                        decimal plainCharNumber = k;

                        //Verschiebe Buchstabe im Alphabet um den Faktor 'key' (A+2=C, wird hier jedoch in Zahlen gemacht also: 0+2 = 2 -> C)
                        //Text wird Entschlüsselt
                        newCharNum = (26 + (plainCharNumber - verschiebung)) % 26; //Verschiebung des Buchstabens um 'key' nach links


                        text_decrypted += alphabet[Convert.ToInt32(newCharNum)]; //text_output String wird um das verschlüsselte Zeichen verlängert
                        break; //Abbrechen des for loops
                    }
                    //Wenn keine Übereinstimmung gefunden -> Zeichen einfach übernehmen also '!' bleibt '!'
                    else if (k == alphabet.Length - 1)
                    {
                        text_decrypted += curChar; //text_decrypted String wird um das unverschlüsselte Sonderzeichen verlängert
                    }
                }
            }
            box_decrypted.Text = text_decrypted; //Gib entschlüsselten Text in Ausgabe Textbox aus 
        }
    }
}