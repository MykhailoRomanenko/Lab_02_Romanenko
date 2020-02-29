using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab_02_Romanenko.Models;
using Lab_02_Romanenko.Tools;

namespace Lab_02_Romanenko.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static readonly int MAX_AGE = 135;
        private static readonly int DAFULT_YEAR = 2000;
        private Person _person;

        public MainWindowViewModel()
        {
            ProceedCommand = new ProceedCommand(this);
            DateValue = new DateTime(2000, 1, 1);
        }

        public ProceedCommand ProceedCommand { get; set; }

        public string FirstNameValue { get; set; }

        public string LastNameValue { get; set; }

        public string EMailValue { get; set; }

        public DateTime DateValue { get; set; }

        private bool IsDateValid(DateTime dt)
        {
            return dt.CompareTo(DateTime.Today) < 0 && dt.Year > DateTime.Today.Year - MAX_AGE;
        }

        public bool CanProceed()
        {
            return FirstNameValue != null && !FirstNameValue.Equals("") &&
                   LastNameValue != null && !LastNameValue.Equals("")
                   && EMailValue != null && !EMailValue.Equals("") && IsDateValid(DateValue);
        }

        public void Proceed()
        {
            _person = new Person(FirstNameValue, LastNameValue, EMailValue, DateValue);
            if (_person.IsBirthday)
                MessageBox.Show("First of all, happy Birthday!");
            Task.Run(ShowInfo);
        }

        private void ShowInfo()
        {
            Thread.Sleep(1000);
            MessageBox.Show("First Name: " + _person._firstName + "\nLast Name: " + _person._lastName
                            + "\nE-Mail: " + _person._eMail + "\nBirth Date: " 
                            + _person._birthDate.Day+'.'+_person._birthDate.Month+'.'+_person._birthDate.Year
                            + "\nIsAdult: " + _person.IsAdult + "\nChinese: " + _person.ChineseSign + "\nWestern: " +
                            _person.WesternSign
                            + "\nIsBirthday: " + _person.IsBirthday);
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}