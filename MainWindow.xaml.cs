using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace Szyfrowanie
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Encrypts data using Caesar cipher. Author: Wiktor Lutowski
        /// </summary>
        /// <param name="key">Key used in cipher</param>
        /// <param name="text">Text to encrypt</param>
        /// <returns>Encrypted text</returns>
        private string Encrpyt(int key, string text)
        {
            string letters = "abcdefghijklmnopqrstuvwxyz";

            string result = "";

            foreach (var textChar in text)
            {
                bool isUpper = char.IsUpper(textChar);

                int charIndex = letters.IndexOf(char.ToLower(textChar));

                // Skip other chars than letters
                if (charIndex == -1)
                {
                    result += textChar;
                    continue;
                }

                for (int i = 0; i < Math.Abs(key % letters.Length); i++)
                {
                    if (key > 0)
                        charIndex++;
                    else
                        charIndex--;
                }


                // Edit index when it is too big or small. Modulo makes it, so it's enough to do this once
                if (charIndex < 0)
                    charIndex += letters.Length;

                if (charIndex > letters.Length - 1)
                    charIndex -= letters.Length;

                // Check if an orginal char was in upper or lower case and back it to orginal state
                if (char.ToLower(textChar) != textChar)
                    result += char.ToUpper(letters[charIndex]);
                else
                    result += letters[charIndex];
            }

            return result;
        }

        private void Button_Click_Encrypt(object sender, RoutedEventArgs e)
        {
            // Ensure that the key is a inteager number
            if (!int.TryParse(keyTxtBox.Text, out int key))
                key = 0;

            resultTxtBlock.Text = Encrpyt(key, textTxtBox.Text);
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(resultTxtBlock.Text))
                return;

            SaveFileDialog dialog = new SaveFileDialog();

            // Show save file dialog and if it passed, save the file
            if ((bool)dialog.ShowDialog())
                File.WriteAllText(dialog.FileName, resultTxtBlock.Text);
        }
    }
}
