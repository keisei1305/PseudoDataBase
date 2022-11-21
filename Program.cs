using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Hello
{
    class Program
    {
        class student
        {
            public string surname;
            public string name;
            public string middle_name;
            public string phone;
            public string mail;
            public string login;
            public string password;

            public student()
            {
                surname = "";
                name = "";
                middle_name = "";
                phone = "";
                mail = "";
                login = "";
                password = "";
            }
            public student(string str)
            {
                string[] Arr;
                Arr = str.Split(' ');
                for(int i=0;i<7; i++) eq(Arr[i], i);
            }
            public bool IsEmpty()
            {
                if (name == "" && surname == "" && middle_name == "" && login == "" && phone == "" && password == "" && mail == "") return true;
                return false;
            }
            public bool TruePassword()
            {
                Console.WriteLine("Введите пароль");
                string maybepassword = Console.ReadLine();
                if (password == GetHash(maybepassword)) return true;
                return false;
            }
            public bool Check()
            {
                string mpassword = password;
                bool flag = false;
                if (Regex.IsMatch(mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == false)
                {
                    Console.WriteLine("Неверно введён mail");
                    flag = true;
                }
                if (Regex.IsMatch(phone, @"^((8|\+7)[\- ]?)(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$") == false)
                {
                    Console.WriteLine("Неверно введён телефон");
                    flag = true;
                }
                if (Regex.IsMatch(name, @"^([А-Я]{1})([а-я]{1,})$") == false)
                {
                    Console.WriteLine("Неверно введено имя");
                    flag = true;
                }
                if (Regex.IsMatch(surname, @"^([А-Я]{1})([-а-я]{1,})$") == false)
                {
                    Console.WriteLine("Неверно введена фамилия");
                    flag = true;
                }
                if (Regex.IsMatch(middle_name, @"^([А-Я]{1})([а-я]{1,})$") == false)
                {
                    Console.WriteLine("Неверно введено отчество");
                    flag = true;
                }
                if (Regex.IsMatch(login, @"^([!@#$%^&_*a-zA-z0-9]{1,})$") == false)
                {
                    Console.WriteLine("Неверно введён логин");
                    flag = true;
                }
                if (Regex.IsMatch(mpassword, @"^(\S)*([!@#$%^&_*])(\S)*$") == false
                    || Regex.IsMatch(mpassword, @"^(\S)*([0-9])(\S)*$") == false
                    || Regex.IsMatch(mpassword, @"^(\S)*([a-z])(\S)*$") == false
                    || Regex.IsMatch(mpassword, @"^(\S)*([A-Z])(\S)*$") == false
                    || Regex.IsMatch(mpassword, @"^([!@#$%^&_*a-zA-z0-9]{8,})$") == false)
                {
                    Console.WriteLine("Неверно введён пароль");
                    flag = true;
                }
                return flag;
            }
            public bool CheckWithoutPassword()
            {
                bool flag = false;
                if (Regex.IsMatch(mail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == false)
                {
                    Console.WriteLine("Неверно введён mail");
                    flag = true;
                }
                if (Regex.IsMatch(phone, @"^((8|\+7)[\- ]?)(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$") == false)
                {
                    Console.WriteLine("Неверно введён телефон");
                    flag = true;
                }
                if (Regex.IsMatch(name, @"^([А-Я]{1})([а-я]{1,})$") == false)
                {
                    Console.WriteLine("Неверно введено имя");
                    flag = true;
                }
                if (Regex.IsMatch(surname, @"^([А-Я]{1})([-а-я]{1,})$") == false)
                {
                    Console.WriteLine("Неверно введена фамилия");
                    flag = true;
                }
                if (Regex.IsMatch(middle_name, @"^([А-Я]{1})([а-я]{1,})$") == false)
                {
                    Console.WriteLine("Неверно введено отчество");
                    flag = true;
                }
                if (Regex.IsMatch(login, @"^([!@#$%^&_*a-zA-z0-9]{1,})$") == false)
                {
                    Console.WriteLine("Неверно введён логин");
                    flag = true;
                }
                return flag;
            }

            public void refresh()
            {
                phone = (Regex.Replace(phone, @"(\+7|8)*(\-|\(| )?(?<111>\d{3})(\-|\)| )?(?<222>\d{3})(\-| )?(?<33>\d{2})(\-| )?(?<44>\d{2})", "+7-(${111})-${222}-${33}-${44}"));
            }
            public void create()
            {
                Console.WriteLine("\nВведите имя пользователя  ");
                name = Console.ReadLine();
                Console.WriteLine("\nВведите фамилию  ");
                surname = Console.ReadLine();
                Console.WriteLine("\nВведите отчество  ");
                middle_name = Console.ReadLine();
                Console.WriteLine("\nВведите номер телефона  ");
                phone = Console.ReadLine();
                Console.WriteLine("\nВведите e-mail  ");
                mail = Console.ReadLine();
                Console.WriteLine("\nВведите логин  ");
                login = Console.ReadLine();
                Console.WriteLine("\nВведите пароль  ");
                password = Console.ReadLine();
            }  
            private void eq(string str, int n)
            {
                switch (n)
                {
                    case 0:
                        surname = str;
                        break;
                    case 1:
                        name = str;
                        break;
                    case 2:
                        middle_name = str;
                        break;
                    case 3:
                        phone = str;
                        break;
                    case 4:
                        mail = str;
                        break;
                    case 5:
                        login = str;
                        break;
                    case 6:
                        password = str;
                        break;
                    default:
                        break;
                }
            }
        };
        static void Print(student solo)
        {
            Console.WriteLine($"{solo.surname} {solo.name} {solo.middle_name} {solo.login}");
        }

        static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }

        static void Copy(student first, student second)
        {
            first.name = second.name; first.surname = second.surname; first.middle_name = second.middle_name; first.phone = second.phone; first.mail = second.mail; first.login = second.login; first.password = second.password;
        }


        static student student_find(List<student> students, int method)
        {
            student emptystudent = new student();
            switch (method)
            {
                case 1:
                    {
                        Console.WriteLine("Введите Фамилию Имя пользователь в виде (Иванов Иван)");
                        string solo = Console.ReadLine();
                        foreach (student astudent in students)
                        {
                            if (astudent.surname + " " + astudent.name == solo)
                            {
                                return astudent;
                            }
                        }
                        Console.WriteLine("Пользователь не найден");
                        return emptystudent;
                    }
                case 2:
                    {
                        Console.WriteLine("Введите логин пользователя");
                        string solo = Console.ReadLine();
                        foreach (student astudent in students)
                        {
                            if (astudent.login== solo)
                            {
                                return astudent;
                            }
                        }
                        Console.WriteLine("Пользователь не найден");
                        return emptystudent;
                    }
                case 3:
                    {
                        Console.WriteLine("Введите номер телефона пользователя");
                        string solo = Console.ReadLine();
                        solo = (Regex.Replace(solo, @"(\+7|8)*(\-|\(| )?(?<111>\d{3})(\-|\)| )?(?<222>\d{3})(\-| )?(?<33>\d{2})(\-| )?(?<44>\d{2})", "+7-(${111})-${222}-${33}-${44}"));
                        foreach (student astudent in students)
                        {
                            if (astudent.phone == solo)
                            {
                                return astudent;
                            }
                        }
                        Console.WriteLine("Пользователь не найден");
                        return emptystudent;
                    }
                default:
                    {
                        Console.WriteLine("Введите существующий метод поиска");
                        return emptystudent;
                    }
            }
        }
        static string ToString(student solo)
        {
            string str;
            str = solo.surname + " " + solo.name + " " + solo.middle_name + " " + solo.phone + " " + solo.mail + " " + solo.login+" "+solo.password;
            return str;
        }

        static void Sort(string[] mas, List<student> students)
        {
            for(int i=0; i<mas.Length; i++)
            {
                for(int j=i%2; j<mas.Length-1; j+=2)
                {
                    if (!More(mas[j+1],mas[j]))
                    {
                        string temp = mas[j + 1];
                        mas[j + 1] = mas[j];
                        mas[j] = temp;
                        student temp1 = students[j + 1];
                        students[j + 1] = students[j];
                        students[j] = temp1;
                    }
                }
            }
        }

        static bool More (string a,string b)
        {
            if (a != b)
            {
                int lena = a.Length, lenb = b.Length;
                int len;
                if (lena > lenb) len = lenb;
                else len = lena;
                for (int i = 0; i < len; i++)
                {
                    if (a[i] > b[i]) return true;                   
                    else if (a[i] < b[i]) return false;
                }
            }
            return false;
        }

        static void Main()
        {
            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = Encoding.GetEncoding(1251);
            Console.InputEncoding = Encoding.GetEncoding(1251);

            List<student> students = new List<student>();
            StreamReader FileOpen = new StreamReader("C:\\Users\\admin\\source\\repos\\Пам\\Пам\\TextFile1.txt", System.Text.Encoding.GetEncoding(1251));
            string s;
            while (FileOpen.EndOfStream!= true)
            {
                s = FileOpen.ReadLine();
                student man = new student(s);
                students.Add(man);              
            }
            FileOpen.Close();

            int n = 0;
            while (n != 8)
            {
                Console.WriteLine("Выберите одну из 8 команд:\n1.Посмотреть список пользователей\n2.Добавить пользователя\n3.Удалить пользователя\n4.Изменить пользователя\n5.Сохранить изменения в файл\n6.Отправить сообщение на e-mail пользователя\n7.Отсортировать по выбранному полю\n8.Выход");
                n = Convert.ToInt32(Console.ReadLine()[0]);
                n -= 48;
                switch (n)
                {
                    case 1:
                        {
                            Console.Write("\n");
                            foreach (student astudent in students)
                            {
                                Print(astudent);
                            }
                            Console.Write("\n");
                            break;
                        }

                    case 2:
                        {
                            while (true)
                            {
                                student new_student = new student();
                                new_student.create();
                                if (new_student.Check() == true)
                                {
                                    Console.WriteLine("Желаете ли вы исправить ошибку Да/Нет\n");
                                    string a;
                                    a = Console.ReadLine();
                                    if (a == "Да" || a == "да") continue;
                                    else if (a == "Нет" || a == "нет") break;
                                    else Console.WriteLine("Введите Да/Нет без пробелов\n");
                                }
                                else
                                {
                                    new_student.password = GetHash(new_student.password);
                                    new_student.refresh();
                                    students.Add(new_student);
                                }
                                break;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Выберите способ нахождения пользователя\n1.По фамилии-имени\n2.По логину\n3.По номеру телефона\n");
                            int nn;
                            nn = Convert.ToInt32(Console.ReadLine()[0]);
                            nn -= 48;
                            student foundstudent;
                            foundstudent = student_find(students, nn);
                            if (foundstudent.IsEmpty() == false)
                            {
                                if (foundstudent.TruePassword() == true) students.Remove(foundstudent);
                                else Console.WriteLine("Пароль неверный");
                            }
                            Console.WriteLine();
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Выберите способ нахождения пользователя\n1.По фамилии-имени\n2.По логину\n3.По номеру телефона\n");
                            int nn;
                            nn = Convert.ToInt32(Console.ReadLine()[0]);
                            nn -= 48;
                            student foundstudent;
                            foundstudent = student_find(students, nn);
                            if (foundstudent.IsEmpty() == false)
                            {
                                if (foundstudent.TruePassword() == true)
                                {
                                    while (true)
                                    {
                                        Console.WriteLine("Выберите, что хотите изменить: \n1.Имя \n2.Фамилия \n3.Отчество \n4.Номер телефона \n5.Mail \n6.Логин \n7.Пароль");
                                        int nnn;
                                        nnn= Convert.ToInt32(Console.ReadLine()[0]); ;
                                        nnn -= 48;
                                        student foundstudent1 = new student();
                                        Copy(foundstudent1, foundstudent);
                                        switch(nnn) 
                                        {
                                            case 1:
                                                Console.WriteLine("Введите имя");
                                                foundstudent1.name = Console.ReadLine();
                                                break;
                                            case 2:
                                                Console.WriteLine("Введите фамилию");
                                                foundstudent1.surname = Console.ReadLine();
                                                break;
                                            case 3:
                                                Console.WriteLine("Введите отчество");
                                                foundstudent1.middle_name = Console.ReadLine();
                                                break;
                                            case 4:
                                                Console.WriteLine("Введите номер телефона");
                                                foundstudent1.phone = Console.ReadLine();
                                                foundstudent1.refresh();
                                                break;
                                            case 5:
                                                Console.WriteLine("Введите мэйл");
                                                foundstudent1.mail = Console.ReadLine();
                                                break;
                                            case 6:
                                                Console.WriteLine("Введите логин");
                                                foundstudent1.login = Console.ReadLine();
                                                break;
                                            case 7:
                                                Console.WriteLine("Введите пароль");
                                                foundstudent1.password = Console.ReadLine();
                                                break;
                                            default:
                                                Console.WriteLine("Такого способа изменения нет");
                                                break;
                                        }
                                        if (foundstudent1.CheckWithoutPassword() == true || (nn==7 && foundstudent1.Check()==true)) Console.WriteLine("Такие изменения недопустимы");
                                        else Copy(foundstudent, foundstudent1);
   
                                        Console.WriteLine("Желаете еще что-то изменить Да/Нет");
                                        string a;
                                        a = Console.ReadLine();
                                        if (a == "Да" || a == "да") continue;
                                        else if (a == "Нет" || a == "нет") break;
                                        else Console.WriteLine("Введите Да/Нет без пробелов\n");
                                    }
                                }
                                else Console.WriteLine("Пароль неверный");
                            }
                            Console.WriteLine();
                            break;
                        }
                    case 5:
                        {
                            StreamWriter FileWriter = new StreamWriter("C:/Users/admin/source/repos/Пам/Пам/TextFile1.txt", false, System.Text.Encoding.GetEncoding(1251));
                            foreach (student astudent in students)
                            {
                                string str = ToString(astudent);
                                FileWriter.WriteLine(str);
                            }
                            FileWriter.Close();
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("Выберите способ нахождения пользователя\n1.По фамилии-имени\n2.По логину\n3.По номеру телефона\n");
                            int nn;
                            nn = Convert.ToInt32(Console.ReadLine()[0]);
                            nn -= 48;
                            student foundstudent = student_find(students, nn);
                            if (foundstudent.IsEmpty() == false)
                            {
                                if (foundstudent.TruePassword() == true)
                                {
                                    Console.WriteLine("Введите почту, на которую хотите отправить сообщение");
                                    string Tomail = Console.ReadLine();
                                    MailAddress from = new MailAddress(foundstudent.mail, foundstudent.login);
                                    MailAddress to = new MailAddress(Tomail);
                                    MailMessage message = new MailMessage(from, to);
                                    Console.WriteLine("Введите сообщение, которое хотите отправить");
                                    string messagetext = Console.ReadLine();
                                    message.Body = messagetext;
                                    Console.WriteLine("Введите тему для сообщения");
                                    string messagesubject = Console.ReadLine();
                                    message.Subject = messagesubject;
                                    Console.WriteLine("Введите пароль приложения от почты");
                                    string messagepassword = Console.ReadLine();

                                    if (Regex.IsMatch(Tomail, @"^(\S)+@gmail.com$") == true)
                                    {
                                        SmtpClient smtpclient = new SmtpClient();
                                        smtpclient.Host = "smtp.gmail.com";
                                        smtpclient.Port = 587;
                                        smtpclient.EnableSsl = true;
                                        smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtpclient.UseDefaultCredentials = false;
                                        smtpclient.Credentials = new NetworkCredential(from.Address, messagepassword);
                                        smtpclient.Send(message);
                                        Console.WriteLine("Сообщение отправлено \n");
                                    }
                                    else if (Regex.IsMatch(Tomail, @"^(\S)+@mail.ru$") == true)
                                    {
                                        SmtpClient smtpclient = new SmtpClient();
                                        smtpclient.Host = "smtp.mail.ru";
                                        smtpclient.Port = 465;
                                        smtpclient.EnableSsl = true;
                                        smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                        smtpclient.UseDefaultCredentials = false;
                                        smtpclient.Credentials = new NetworkCredential(from.Address, messagepassword);
                                        smtpclient.Send(message);
                                        Console.WriteLine("Сообщение отправлено \n");
                                    }
                                    else Console.WriteLine("Такая почта не обработана на нашем сервере");
                                }
                                else Console.WriteLine("Пароль неверный");
                            }
                                break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Выберите поле для сортировки:\n1.Фамилия\n2.Имя\n3.Отчество\n4.Телефон\n5.Mail\n6.Логин\n7.Пароль");
                            int nnn;
                            nnn = Convert.ToInt32(Console.ReadLine()[0]); ;
                            nnn -= 48;
                            switch (nnn)
                            {
                                case 1:
                                    {
                                        string[] mas = new string[students.Count];
                                        for (int i = 0; i < students.Count; i++)
                                        {
                                            mas[i] = students[i].surname;
                                        }
                                        Sort(mas, students);
                                        break;
                                    }
                                case 2:
                                    {
                                        string[] mas = new string[students.Count];
                                        for (int i = 0; i < students.Count; i++)
                                        {
                                            mas[i] = students[i].name;
                                        }
                                        Sort(mas, students);
                                        break;
                                    }
                                case 3:
                                    {
                                        string[] mas = new string[students.Count];
                                        for (int i = 0; i < students.Count; i++)
                                        {
                                            mas[i] = students[i].middle_name;
                                        }
                                        Sort(mas, students);
                                    }
                                    break;
                                case 4:
                                    {
                                        string[] mas = new string[students.Count];
                                        for (int i = 0; i < students.Count; i++)
                                        {
                                            mas[i] = students[i].phone;
                                        }
                                        Sort(mas, students);
                                    }
                                    break;
                                case 5:
                                    {
                                        string[] mas = new string[students.Count];
                                        for (int i = 0; i < students.Count; i++)
                                        {
                                            mas[i] = students[i].mail;
                                        }
                                        Sort(mas, students);
                                    }
                                    break;
                                case 6:
                                    {
                                        string[] mas = new string[students.Count];
                                        for (int i = 0; i < students.Count; i++)
                                        {
                                            mas[i] = students[i].login;
                                        }
                                        Sort(mas, students);
                                    }
                                    break;
                                case 7:
                                    {
                                        string[] mas = new string[students.Count];
                                        for (int i = 0; i < students.Count; i++)
                                        {
                                            mas[i] = students[i].phone;
                                        }
                                        Sort(mas, students);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("Выберите цифру от 1 до 7");
                                    break;
                            }                           
                            break;
                        }
                    case 8:
                        break;
                    default:
                        Console.WriteLine("Введите цифру от 1 до 8\n");
                        break;
                }
            }
        }
    }
}
