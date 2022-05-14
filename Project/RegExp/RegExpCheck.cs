using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Project.RegExp
{
    static class RegExpCheck
    {
       static public bool CheckLogin(string str)
        {
            string pattern = @"\w{4,16}";
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Логин должен быть от 4 символов!");
                
                return false;
            }
        }

        static public bool CheckPassword(string str)
        {
            string pattern = @"\w{4,16}";
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Пароль должен быть от 4 символов!");
                return false;
            }
        }

        static public bool CheckName(string str)
        {
            string pattern = @"\w";
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введенное имя введено неправильно");
                return false;
            }
        }

        static public bool CheckSurname(string str)
        {
            string pattern = @"\w";
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введенная фамилия введена неправильно");
                return false;
            }
        }

        static public bool CheckPhone(string str)
        {
            string pattern = @"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$";
            string target = "";
            Regex regex = new Regex(pattern);
           
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Введенный номер телефона неправильный");
                return false;
               
            }

        }

        static public bool CheckEmail(string str)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            Regex regex = new Regex(pattern);
            if (Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase)) return true;
            else
            {
                MessageBox.Show("Введенный email неправильный");
                return false;
            }
        }

        
    }
}
