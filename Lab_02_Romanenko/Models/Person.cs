using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Lab_02_Romanenko.Tools.Exceptions;

namespace Lab_02_Romanenko.Models
{
    public class Person
    {
        private static readonly string[] Chinese =
            {"Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"};

        private static readonly string[] Western =
        {
            "Capricorn", "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra",
            "Scorpio", "Sagittarius"
        };
        
        private static readonly Regex EMail = new Regex("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$:");
        private static readonly int MAX_AGE = 135;


        public readonly DateTime _birthDate;
        public string _eMail;
        public string _firstName;
        public string _lastName;

        public Person(string firstName, string lastName, string eMail, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _eMail = eMail;
            _birthDate = birthDate;
            ThrowExceptionsIfNecessary();
        }

        public Person(string firstName, string lastName, string eMail)
        {
            _firstName = firstName;
            _lastName = lastName;
            _eMail = eMail;
            ThrowExceptionsIfNecessary();
        }

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthDate = birthDate;
            ThrowExceptionsIfNecessary();
        }

        public string ChineseSign => CalculateChinese();

        public string WesternSign => CalculateWestern();

        public int Age => CalculateAge();

        public bool IsBirthday => Birthday();

        public bool IsAdult => Adult();

        private int CalculateAge()
        {
            var y = _birthDate.Year;
            var m = _birthDate.Month;
            var d = _birthDate.Day;
            var todayM = DateTime.Today.Month;
            var todayD = DateTime.Today.Day;
            var age = DateTime.Today.Year - y;
            if (m > todayM || m == todayM && d > todayD)
                --age;
            return age;
        }

        private bool IsEMailValid()
        {
            try
            {
                MailAddress m = new MailAddress(_eMail);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
        }
        
        private bool IsUserNotDead()
        {
            return  _birthDate.Year > DateTime.Today.Year - MAX_AGE;;
        }

        private bool IsUserBorn()
        {
            return  _birthDate.CompareTo(DateTime.Today) < 0;
        }
        
        private void ThrowExceptionsIfNecessary()
        {
            if (!IsUserBorn())
            {
                throw new TimeContinuumException("Birth date in future: " + _birthDate.Date);
            }

            if (!IsUserNotDead())
            {
                throw new UserIsProbablyDeadException("Birth date is 135 or more years away: " + _birthDate.Date.Year);
            }

            if (!IsEMailValid())
            {
                throw new InvalidEMailException("E-Mail does not match e-mail standards: "+ _eMail);
            }
            Console.Write("Checks are correct in ThrowExceptionsIfNecessary()");
        }
        private string CalculateChinese()
        {
            return Chinese[_birthDate.Year % 12];
        }

        private string CalculateWestern()
        {
            var m = _birthDate.Month;
            var d = _birthDate.Day;
            var thresh = Threshold(m);
            if (d <= thresh) m = (m + 11) % 12;
            return Western[m];
        }

        private static int Threshold(int m)
        {
            var thresh = -1;
            if (m >= 1 && m <= 6)
                thresh = 21;
            else if (m == 2)
                thresh = 20;
            else
                thresh = 22;
            return thresh;
        }

        private bool Adult()
        {
            return Age >= 18;
        }

        private bool Birthday()
        {
            return _birthDate.Month == DateTime.Today.Month && _birthDate.Day == DateTime.Today.Day;
        }
    }
}