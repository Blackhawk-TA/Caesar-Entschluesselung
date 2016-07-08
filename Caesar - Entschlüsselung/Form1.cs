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
    public partial class Caesar : Form
    {
        //Globale Variablen
        char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray(); //Array des Alphabets für spätere Analyse
        double accuracy = 0.01; //Übereinstimmung Value (Max. Abweichungstoleranz zwischen rel Häufigkeiten für Übereinstimmung)
        double accuracy_modification = 0;

        public Caesar()
        {
            InitializeComponent();
        }

        //Gibt die relative Häufigkeit von Buchstaben in einem Text als Array zurück
        public double[] analyseText(string text)
        {
            double[] Buchstaben_AbsoluteH = new double[26]; //Array für absolute Häufigkeit der Buchstaben
            double[] Buchstaben_RelH = new double[26]; //Array für relative Häufigkeit der Buchstaben

            for(int i=0; i<alphabet.Length; i++)
            {
                for (int k = 0; k < text.Length; k++) //Zähle wie oft welcher Buchstabe im Text vorkommt & berechne die relative Häufigkeit
                {
                    char curChar = text[k];
                    if (curChar == alphabet[i])
                    {
                        Buchstaben_AbsoluteH[i]++; //Berechnung der absoluten Häufigkeit
                        Buchstaben_RelH[i] = Buchstaben_AbsoluteH[i] / text.Length; //Berechnung der relativen Häufigkeit
                    }
                }
            }
            return Buchstaben_RelH; //Gib relative Häufigkeit zurück
        }

        public int[] relH_abgleichen(Double[] example_relH, Double[] encrypted_relH)
        {
            int[] erkannteVerschiebungen = new int[26];

            for (int i = 0; i<alphabet.Length; i++)
            {
                for (int j = 0; j<alphabet.Length; j++)
                {
                    //Bei geringer Abweichung der relativen Häufigkeiten -> Übereinstimmung -> Verschiebung wird in Array gespeichert
                    if (Math.Abs(example_relH[i] - encrypted_relH[j]) < (accuracy + accuracy_modification)) //0.01 default
                    {
                        //Debugging: 
                        //Console.WriteLine(alphabet[i] + ": " + " ( "+ Convert.ToInt32(j-i) +" )" + example_relH[i] + " - " + encrypted_relH[j]);
                        erkannteVerschiebungen[i] = j - i; //erkannte Verschiebung pro Array Eintrag                       
                    }
                }
            }
            return erkannteVerschiebungen;
        }
        
        //Vergleicht wie oft welche Verschiebung erkannt wurde und gibt die am Häufigsten auftretende als Verschiebung zurück
        public int verschErkennen(int[] erkannteVerschiebungen)
        {
            int[] erlaubteVerschiebungen = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }; //mögliche Verschiebungen die auftreten können
            int[] counter = new int[26]; //Array das pro erkannte Verschiebung zählt wie oft die selbe Verschiebung erkannt wurde
            int verschiebung = 0; //Endgültige Verschiebung des Textes
            
            for (int i = 0; i < erkannteVerschiebungen.Length; i++)
            {
                for (int j = 0; j < erlaubteVerschiebungen.Length; j++)
                {
                    if (erkannteVerschiebungen[i] == erlaubteVerschiebungen[j]) 
                    {
                        counter[j]++; //Verschiebung erkannt -> wird mitgezählt wie oft die selbe Verschiebung erkannt wurde
                    }
                }
            }
            
            verschiebung = Array.IndexOf(counter, counter.Max()); //Finde Index der höchsten Zahl im Array

            return verschiebung;
        }

        //Text wird mit erkannter Verschiebung entschlüsselt
        public string decrypt(string text_encrypted, int verschiebung)
        {
            string text_decrypted = ""; //Variabel für entschlüsselten Text
            decimal newCharNum = 0; //Variable für Char Nummber die Verschlüsselt bzw Entschlüsselt wurde
            char[] text_chars = text_encrypted.ToCharArray(); //Wandle 'Text Eingabe String'(text_input) in Char Array (Array das aus den Zeichen des Strings besteht) um

            //Text wird mit erkannter Verschiebung entschlüsselt
            //Für jedes Zeichen in text_chars wird das Zeichen entsprechend des Schlüssels verschoben
            for (int i = 0; i < text_chars.Length; i++)
            {
                char curChar = text_chars[i];
                bool charInArray = Array.Exists(alphabet, element => element == curChar); //Bool ob char im Alphabet ist oder nicht

                if (charInArray) //Char ist im Alphabet vorhanden und wird verschlüsselt
                {
                    int curChar_Index = Array.IndexOf(alphabet, curChar); //Wandle Zeichen aus dem Char Array in Alphabet Nummer um (A=0, B=1, C=2, ...)

                    //Verschiebe Buchstabe im Alphabet um den Faktor 'key' (A+2=C, wird hier jedoch in Zahlen gemacht also: 0+2 = 2 -> C)
                    //Text wird Entschlüsselt
                    newCharNum = (26 + (curChar_Index - verschiebung)) % 26; //Verschiebung des Buchstabens um 'key' nach links

                    text_decrypted += alphabet[Convert.ToInt32(newCharNum)]; //text_decrypted String wird um das verschlüsselte Zeichen verlängert
                }
                else //Wenn keine Übereinstimmung gefunden -> Zeichen einfach übernehmen also '!' bleibt '!'
                {
                    text_decrypted += curChar; //text_output String wird um das unverschlüsselte Sonderzeichen verlängert
                }
            }
            return text_decrypted;
        }

        //Starte Entschlüsselung
        public void initDecryption(string text_encrypted, string text_example, double[] example_relH, double[] encrypted_relH)
        {
            //Gibt Array mit allen erkannten Verschiebungen zurück
            int[] erkannteVerschiebungen = relH_abgleichen(example_relH, encrypted_relH); //erneut durchlaufen lassen

            //Vergleicht wie oft welche Verschiebung erkannt wurde und gibt die am Häufigsten auftretende als Verschiebung zurück
            int verschiebung = verschErkennen(erkannteVerschiebungen);

            //Wenn Verschiebung 0 solange Übereinstimmungs-Value anpassen bis Verschiebung != 0 oder accuracy >= 0.1
            if (verschiebung == 0 && (accuracy + accuracy_modification) < 0.1)
            {
                accuracy += 0.005;  //Übereinstimmungs-Value erhöhen, Toleranz für Abweichungen erhöhen
                initDecryption(text_encrypted, text_example, example_relH, encrypted_relH);
            }
            else if (verschiebung == 0 && (accuracy+accuracy_modification) >= 0.1)
            {
                MessageBox.Show("Entweder ist der Text nicht verschlüsselt oder er ist zu kurz um entschlüsselt zu werden. Bitte passen Sie die Übereinstimmungs Value oder den Text an.",
                "Fehler",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                //Info über die Verschiebung im UI
                lbl_key.Text = "Erkannte Verschiebung: " + verschiebung;

                //Text mit erkannter Verschlüsselung entschlüsseln
                box_decrypted.Text = decrypt(text_encrypted, verschiebung); //Gib entschlüsselten Text in Ausgabe Textbox aus  
            }
            //Debugging: Console.WriteLine(accuracy.ToString() + ", " + accuracy_modification.ToString());
            lbl_accuracy.Text = "Übereinstimmungs Value: " + (accuracy + accuracy_modification);
        }

        //Entschlüsseln Button wird geklickt
        private void btn_decrypt_Click(object sender, EventArgs e)
        {
            string text_encrypted = box_encrypted.Text.ToLower(); //Auslesen und in Kleinbuchstaben setzen des verschlüsselten Textes  
            string text_example = box_exampleText.Text.ToLower(); //Auslesen und in Kleinbuchstaben setzen Beispieltextes

            //Analyse des Beispieltextes und des verschlüsselten Textes
            double[] example_relH = analyseText(text_example); //Gibt relative Häufigkeit der Buchstaben des Beispieltextes zurück
            double[] encrypted_relH = analyseText(text_encrypted); //Gibt relative Häufigkeit der Buchstaben des verschlüsselten Textes zurück

            var verschiebungKeyWord = keyWordDecryption(text_encrypted); //Erkenne Verschiebung durch KeyWords wie der, die, das, und

            //Wenn Erfolgreich -> Entschlüsseln
            if (verschiebungKeyWord > 0)
            {
                decrypt(text_encrypted, keyWordDecryption(text_encrypted));

                //Info über die Verschiebung im UI
                lbl_key.Text = "Erkannte Verschiebung: " + verschiebungKeyWord;

                //Text mit erkannter Verschlüsselung entschlüsseln
                box_decrypted.Text = decrypt(text_encrypted, verschiebungKeyWord); //Gib entschlüsselten Text in Ausgabe Textbox aus  
            }
            //Wenn Erfolglos -> Rel Häufigkeit verwenden
            else
            {
                //Starte Entschlüsselung durch relative Häufigkeit
                accuracy = 0.01;
                initDecryption(text_encrypted, text_example, example_relH, encrypted_relH);
                Console.WriteLine("Relative Häufigkeit verwendet");
            }
        }

        public int keyWordDecryption(string text_encrypted)
        {
            //Schlüssel Wörter für Suche
            string suche_der = " der ";
            string suche_die = " die ";
            string suche_und = " und ";
            string suche_das = " das ";
            int verschiebung = 0;

            for (int i = 0; i < alphabet.Length; i++)
            {
                string text_decryptedTry = decrypt(text_encrypted, i); //Text mit Verschiebung i entschlüsseln

                //Überprüfen ob mind 2 der Schlüsselwörter vorhanden sind
                if (text_decryptedTry.Contains(suche_der) && text_decryptedTry.Contains(suche_die))
                {
                    verschiebung = i;
                    break;
                }
                else if (text_decryptedTry.Contains(suche_der) && text_decryptedTry.Contains(suche_und))
                {
                    verschiebung = i;
                    break;
                }
                else if (text_decryptedTry.Contains(suche_die) && text_decryptedTry.Contains(suche_und))
                {
                    verschiebung = i;
                    break;
                }
                else if (text_decryptedTry.Contains(suche_das) && text_decryptedTry.Contains(suche_und))
                {
                    verschiebung = i;
                    break;
                }
                else if (text_decryptedTry.Contains(suche_das) && text_decryptedTry.Contains(suche_der))
                {
                    verschiebung = i;
                    break;
                }
                else if (text_decryptedTry.Contains(suche_das) && text_decryptedTry.Contains(suche_die))
                {
                    verschiebung = i;
                    break;
                }
            }
            return verschiebung;
        }

        //Zeige veränderung am Übereinstimmungs-Slider & passe Übereinstimmungs-Value an
        private void slider_accuracy_Scroll(object sender, EventArgs e)
        {
            accuracy_modification = Convert.ToDouble(slider_accuracy.Value) / 250.0;

            lbl_accuracy.Text = "Übereinstimmungs Value: " +(accuracy + accuracy_modification);
            lbl_accuracy_adj.Text = "Übereinstimmungs Value Anpassung: " + accuracy_modification;
        }
    }
}