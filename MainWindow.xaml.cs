using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string path = null, st = null, mainKey = "скорпион";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string alp = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string upAlp = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            string alp2;
            
            string key = mainKey;
            
            string[] nAlp = new string[key.Length];
            int n;
            for (int i = 0; i < key.Length; i++)
            {
                alp2 = null; ;
                n = alp.IndexOf(key[i]);
                alp2 = alp.Substring(n);
                alp2 = alp2 + alp.Substring(0, n);
                nAlp[i] = alp2;
            }
            string nS = null;
            string s = " ";
            try 
            { 
            s = File.ReadAllText(@path);
            }
            catch (System.IO.FileNotFoundException)
            {
                System.Windows.MessageBox.Show("В данном пути файл не обнаружен");
                nS = "Здесь будет выведен исходный текст дешифровки";
            }
            char c;
            n = 0;
            for (int i = 0; i < s.Length; i++)
            {
                c = s[i];
                if (Char.IsLetter(c) && (!Char.IsUpper(c)))
                {
                    if (!(nAlp[n].IndexOf(c) == -1)) 
                    { 
                        c = alp[nAlp[n].IndexOf(c)];
                        n++;
                        if (n == key.Length) { n = 0; }
                    }
                }
                if (Char.IsUpper(c))
                {
                    if (!(upAlp.IndexOf(c) == -1)) 
                    {
                        c = Char.ToLower(c);
                        c = alp[nAlp[n].IndexOf(c)];
                        c = Char.ToUpper(c);
                        n++;
                        if (n == key.Length) { n = 0; }
                    }
                }
                nS += c;
            }
            TextOnMain1.Text = nS;
            st = nS;
        }

        private void KeyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mainKey = KeyBox.Text;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            path = TextBox.Text;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (st == null)
            {
                System.Windows.MessageBox.Show("Расшифровка не происходила, сохранение в файле невозможно");
            }
            else
            {
                File.WriteAllText(@path, st);
                System.Windows.MessageBox.Show("Файл успешно сохранён");
            }
        }

    }
}
