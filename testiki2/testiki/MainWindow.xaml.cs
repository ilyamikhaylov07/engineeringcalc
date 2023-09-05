using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace calculator

{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();
            foreach (UIElement el in GroupButton.Children)
            {
                if(el is Button)
                {
                    ((Button)el).Click +=ButtonClick;
                }
            }
        }
        (double last,string str) funk(string str)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            double a = 0;
            string str1= str;
            string jk = "";
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] == '+' || str[i] == '*' || str[i] == '/' || str[i] == '^' || str[i] == '!' || str[i] == '(' || str[i] == '|' || str[i] == '^')
                {
                    a = double.Parse(str.Substring(i + 1));
                    str = str.Remove(i + 1);
                    return (a, str);
                }
                else if (str[i] == '-')
                {
                    if (str[i] == '-' && (i==0 || str[i - 1] == '+' || str[i - 1] == '-' || str[i - 1] == '*' || str[i - 1] == '/' || str[i - 1] == '^' || str[i - 1] == '(' || str[i - 1] == '|' 
                        ))
                    {
                        a = -(double.Parse(str.Substring(i + 1)));
                        str = str.Remove(i);
                        return (a, str);
                    }
                    a = double.Parse(str.Substring(i + 1));
                    str = str.Remove(i + 1);
                    return (a, str);
                }
                else
                {
                    jk =str[i]+jk;
                }
            }
            a = double.Parse(jk);
            if (str1.Length == jk.Length)
            {
                jk = jk.Remove(0);
            }
            return (a, jk);
        }
        double fact(int N)
        {
            if (N < 0) 
                return 0; 
            if (N == 0) 
                return 1; 
            else 
                return N * fact(N - 1);
        }
        string rev(string str)
        {
            string str1="";
            for (int i = str.Length - 1; i >= 0; i--)
            {
                str1 += str[i];
            }
            return str1;
        }
        string str = "";
        int coun = 0;
        int coun1 = 0;
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var last_num = (1.0, "");
            string TextButton = ((Button)e.OriginalSource).Content.ToString();
            if (TextButton == "C")
            {
                Text.Clear();
                str = "";
            }
            else if (TextButton == "Back")
            {
                if (Text.Text.Length > 0)
                {
                    Text.Text = Text.Text.Remove(Text.Text.Length - 1);
                    str = str.Remove(str.Length - 1);
                }
            }
            else if (TextButton == "1")
            {
                Text.Text += "1";
                str += "1";
            }
            else if (TextButton == "2")
            {
                Text.Text += "2";
                str += "2";
            }
            else if (TextButton == "3")
            {
                Text.Text += "3";
                str += "3";
            }
            else if (TextButton == "4")
            {
                Text.Text += "4";
                str += "4";
            }
            else if (TextButton == "5")
            {
                Text.Text += "5";
                str += "5";
            }
            else if (TextButton == "6")
            {
                Text.Text += "6";
                str += "6";
            }
            else if (TextButton == "7")
            {
                Text.Text += "7";
                str += "7";
            }
            else if (TextButton == "8")
            {
                Text.Text += "8";
                str += "8";
            }
            else if (TextButton == "9")
            {
                Text.Text += "9";
                str += "9";
            }
            else if (TextButton == "0")
            {
                Text.Text += "0";
                str += "0";
            }
            else if (TextButton == "-")
            {
                Text.Text += "-";
                str += "-";
            }
            else if (TextButton == "+")
            {
                Text.Text += "+";
                str += "+";
            }
            else if (TextButton == "*")
            {
                Text.Text += "*";
                str += "*";
            }
            else if (TextButton == "/")
            {
                Text.Text += "/";
                str += "/";
            }
            else if (TextButton == "=")
            {
                string str1 = "";
                string str2 = "";
                var last_num1 = (1.0, "");
                var last_num2 = (1.0, "");
                double b = 0;
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == '^')
                    {
                        str1 = str.Substring(0, i);
                        last_num1 = funk(str1);
                        str2 = str.Substring(i + 1);
                        if (str2[0] == '-')
                        {
                            str2 = str2.Substring(1);
                            str2 = rev(str2);
                            last_num2 = funk(str2);
                            b = Math.Pow(last_num1.Item1, -last_num2.Item1);
                            str2 = rev(last_num2.Item2);
                            str = last_num1.Item2 + b.ToString() + str2;
                        }
                        else
                        {
                            str2 = rev(str2);
                            last_num2 = funk(str2);
                            b = Math.Pow(last_num1.Item1, last_num2.Item1);
                            str2 = rev(last_num2.Item2);
                            str = last_num1.Item2 + b.ToString() + str2;
                        }
                    }
                }
                Jor.Text = Jor.Text + Text.Text + '=';
                str = new DataTable().Compute(str, null).ToString();
                Text.Text = str;
                coun++;
                Jor.Text = Jor.Text + str + '\n';
                Jor.Text = Jor.Text.Replace(',', '.');
                Text.Text = Text.Text.Replace(',', '.');
                str = str.Replace(',', '.');
            }
            else if (TextButton == "(")
            {
                Text.Text += "(";
                str += "(";
            }
            else if (TextButton == ")")
            {
                string str1 = "";
                string str2 = "";
                var last_num1 = (1.0, "");
                var last_num2 = (1.0, "");
                double b = 0;
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == '^')
                    {
                        str1 = str.Substring(0, i);
                        last_num1 = funk(str1);
                        str2 = str.Substring(i + 1);
                        if (str2[0] == '-')
                        {
                            str2 = str2.Substring(1);
                            str2 = rev(str2);
                            last_num2 = funk(str2);
                            b = Math.Pow(last_num1.Item1, -last_num2.Item1);
                            str2 = rev(last_num2.Item2);
                            str = last_num1.Item2 + b.ToString() + str2;
                        }
                        else
                        {
                            str2 = rev(str2);
                            last_num2 = funk(str2);
                            b = Math.Pow(last_num1.Item1, last_num2.Item1);
                            str2 = rev(last_num2.Item2);
                            str = last_num1.Item2 + b.ToString() + str2;
                        }
                    }
                }
                Text.Text += ")";
                str += ")";
                str1 = "";
                str2 = str;
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    str2 = str2.Remove(i, 1);
                    str1 = str[i] + str1;
                    if (str[i] == '(')
                    {
                        str1 = new DataTable().Compute(str1, null).ToString();
                        break;
                    }
                }
                str = str2 + str1;
            }
            else if (TextButton == "^")
            {
                Text.Text += "^";
                str += "^";
            }
            else if (TextButton == "n!")
            {
                last_num = funk(str);
                double ba1 = last_num.Item1;
                if (last_num.Item1 % 1 == 0)
                {
                    ba1 = fact(Convert.ToInt32(ba1));
                    if (last_num.Item2.Length != str.Length)
                    {
                        Text.Text = last_num.Item2 + last_num.Item1.ToString() + "!";
                        str = last_num.Item2 + (ba1).ToString();
                    }
                    else
                    {
                        Text.Text = last_num.Item1.ToString() + "!";
                        str = (ba1).ToString();
                    }
                }
            }
            else if (TextButton == ".")
            {
                Text.Text += ".";
                str += ".";
            }
            else if (TextButton == "|n")
            {
                Text.Text += "|";
                str += "|";
            }
            else if (TextButton == "n|")
            {
                string str1 = "";
                string str2 = "";
                var last_num1 = (1.0, "");
                var last_num2 = (1.0, "");
                double b = 0;
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] == '^')
                    {
                        str1 = str.Substring(0, i);
                        last_num1 = funk(str1);
                        str2 = str.Substring(i + 1);
                        if (str2[0] == '-')
                        {
                            str2 = str2.Substring(1);
                            str2 = rev(str2);
                            last_num2 = funk(str2);
                            b = Math.Pow(last_num1.Item1, -last_num2.Item1);
                            str2 = rev(last_num2.Item2);
                            str = last_num1.Item2 + b.ToString() + str2;
                        }
                        else
                        {
                            str2 = rev(str2);
                            last_num2 = funk(str2);
                            b = Math.Pow(last_num1.Item1, last_num2.Item1);
                            str2 = rev(last_num2.Item2);
                            str = last_num1.Item2 + b.ToString() + str2;
                        }
                    }
                }
                Text.Text += "|";
                str1 = "";
                str2 = str;
                double ba = 0;
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    str2 = str2.Remove(i, 1);
                    str1 = str[i] + str1;
                    if (str[i] == '|')
                    {
                        str1 = str1.Remove(0, 1);
                        str1 = new DataTable().Compute(str1, null).ToString();
                        ba = Math.Abs(Convert.ToDouble(str1));
                        str1 = ba.ToString();
                        break;
                    }
                }
                str = str2 + str1;
            }
            else if (TextButton == "√")
            {
                last_num = funk(str);
                Text.Text = last_num.Item2 + "√" + last_num.Item1.ToString();
                str = last_num.Item2 + (Math.Pow(last_num.Item1, 0.5)).ToString();
            }
            else if (TextButton == "Sin")
            {
                last_num = funk(str);
                if (RadBT.IsChecked == true)
                {
                    Text.Text = last_num.Item2 + "sin" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Sin(last_num.Item1)).ToString();
                }
                else
                {
                    Text.Text = last_num.Item2 + "sin" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Sin((last_num.Item1 * Math.PI)/180)).ToString();
                }
            }
            else if (TextButton == "Cos")
            {
                last_num = funk(str);
                if (RadBT.IsChecked == true)
                {
                    Text.Text = last_num.Item2 + "cos" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Cos(last_num.Item1)).ToString();
                }
                else
                {
                    Text.Text = last_num.Item2 + "cos" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Cos((last_num.Item1 * Math.PI)/180)).ToString();
                }
            }
            else if (TextButton == "Tg")
            {
                last_num = funk(str);
                if (RadBT.IsChecked == true)
                {
                    Text.Text = last_num.Item2 + "tg" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Tan(last_num.Item1)).ToString();
                }
                else
                {
                    Text.Text = last_num.Item2 + "tg" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Tan((last_num.Item1 * Math.PI)/180)).ToString();
                }
            }
            else if (TextButton == "ArcSin")
            {
                last_num = funk(str);
                if (RadBT.IsChecked == true)
                {
                    Text.Text = last_num.Item2 + "arcsin" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Asin(last_num.Item1)).ToString();
                }
                else
                {
                    Text.Text = last_num.Item2 + "arcsin" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Asin(last_num.Item1) * 180 / Math.PI).ToString();
                }
            }
            else if (TextButton == "ArcCos")
            {
                last_num = funk(str);
                if (RadBT.IsChecked == true)
                {
                    Text.Text = last_num.Item2 + "arccos" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Acos(last_num.Item1)).ToString();
                }
                else
                {
                    Text.Text = last_num.Item2 + "arccos" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Acos(last_num.Item1) * 180 / Math.PI).ToString();
                }
            }
            else if (TextButton == "ArcTg")
            {
                last_num = funk(str);
                if (RadBT.IsChecked == true)
                {
                    Text.Text = last_num.Item2 + "arctg" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Atan(last_num.Item1)).ToString();
                }
                else
                {
                    Text.Text = last_num.Item2 + "arctg" + last_num.Item1.ToString();
                    str = last_num.Item2 + (Math.Atan(last_num.Item1) * 180 / Math.PI).ToString();
                }
            }
            else if (TextButton == "lg")
            {
                last_num = funk(str);
                Text.Text = last_num.Item2 + "lg" + last_num.Item1.ToString();
                str = last_num.Item2 + (Math.Log10(last_num.Item1)).ToString();
            }
            else if (TextButton == "ln")
            {
                last_num = funk(str);
                Text.Text = last_num.Item2 + "ln" + last_num.Item1.ToString();
                str = last_num.Item2 + (Math.Log(last_num.Item1)).ToString();

            }
            else if (TextButton == "e")
            {
                Text.Text = Text.Text + Math.Exp(1).ToString();
                str = str + Math.Exp(1).ToString();

            }
            else if (TextButton == "π")
            {
                Text.Text = Text.Text + Math.PI.ToString();
                str = str + Math.PI.ToString();

            }
            else if (TextButton == "+/-")
            {
                last_num = funk(str);
                if (last_num.Item1.ToString()[0] != '-')
                {
                    Text.Text = last_num.Item2 + "(-" + last_num.Item1.ToString() + ")";
                    str = last_num.Item2 + (-last_num.Item1).ToString();
                }
                else
                {
                    Text.Text = last_num.Item2 + (-last_num.Item1).ToString();
                    str = last_num.Item2 + (-last_num.Item1).ToString();
                }

            }
            else if (TextButton == "MS")
            {
                last_num = funk(str);
                Mem.Text += last_num.Item1.ToString() + '\n';
                coun1++;
            }
            else if (TextButton == "MC")
            {
                Mem.Clear();
                coun1=0;
            }
            else if (TextButton == "M+")
            {
                int c = 0;
                string str1;
                double Memory = 0;
                Mem.Text = Mem.Text.TrimEnd();
                if (coun1 > 0)
                {
                    for (int i = Mem.Text.Length - 1; i >= 0; i--)
                    {
                        if (Mem.Text[i] == '\n')
                        {
                            c = i; break;
                        }
                    }
                    str1 = Mem.Text.Substring(c);
                    Memory = double.Parse(str1);
                    Mem.Text = Mem.Text.Substring(0, c);
                    last_num = funk(str);
                    Memory += last_num.Item1;
                    if (coun1 == 1)
                    {
                        Mem.Text += Memory.ToString() + '\n';
                    }
                    else
                    {
                        Mem.Text += '\n' + Memory.ToString() + '\n';
                    }

                }

            }
            else if (TextButton == "MR")
            {
                int c = 0;
                string str1;
                Mem.Text = Mem.Text.TrimEnd();
                if (coun1 > 0)
                {
                    for (int i = Mem.Text.Length - 1; i >= 0; i--)
                    {
                        if (Mem.Text[i] == '\n')
                        {
                            c = i; break;
                        }
                    }
                    str1 = Mem.Text.Substring(c);
                    last_num = funk(str1);
                    if (str1[0] == '-')
                    {
                        Text.Text += '('+last_num.Item1.ToString()+')';
                        str += last_num.Item1.ToString();
                    }
                    else
                    {
                        Text.Text += last_num.Item1.ToString();
                        str += last_num.Item1.ToString();
                    }
                }

            }
            else if (TextButton == "M-")
            {
                int c = 0;
                string str1;
                double Memory = 0;
                Mem.Text = Mem.Text.TrimEnd();
                if (coun1 > 0)
                {
                    for (int i = Mem.Text.Length - 1; i >= 0; i--)
                    {
                        if (Mem.Text[i] == '\n')
                        {
                            c = i; break;
                        }
                    }
                    str1 = Mem.Text.Substring(c);
                    Memory = double.Parse(str1);
                    Mem.Text = Mem.Text.Substring(0, c);
                    last_num = funk(str);
                    Memory -= last_num.Item1;
                    if (coun1 == 1)
                    {
                        Mem.Text += Memory.ToString() + '\n';
                    }
                    else
                    {
                        Mem.Text += '\n' + Memory.ToString() + '\n';
                    }

                }

            }
        }
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Jor.Text !="") e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            {
                string logFilePath = Directory.GetCurrentDirectory() + @"\" + "Session_" + DateTime.Now.ToString("dd.MM.yyyy") + ' ' + DateTime.Now.ToString("hh.mm.ss") + ".txt";
                using (StreamWriter streamWriter = new StreamWriter(logFilePath))
                {
                        streamWriter.WriteLine(Jor.Text);
                        streamWriter.Close();                   
                    MessageBox.Show("History of computations has been saved in the file: " + logFilePath);
                }
            }



        }
        private void JourCL_Click(object sender, RoutedEventArgs e)
        {
            int c = 0;
            Jor.Text = Jor.Text.TrimEnd();
            if (coun > 0)
            {
                for (int i = Jor.Text.Length - 1; i >= 0; i--)
                {
                    if (Jor.Text[i] == '\n')
                    {
                        c = i; break;
                    }
                }
                Jor.Text=Jor.Text.Substring(0, c);
            }
        }

        private void JorC_Click(object sender, RoutedEventArgs e)
        {
            Jor.Clear();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            Jor.Visibility= Visibility.Visible;
            Mem.Visibility= Visibility.Hidden;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            Jor.Visibility = Visibility.Hidden;
            Mem.Visibility = Visibility.Visible;
        }
        
        
        
        
        
        
        
        
        
        private void number_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Module_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
        
    }
}