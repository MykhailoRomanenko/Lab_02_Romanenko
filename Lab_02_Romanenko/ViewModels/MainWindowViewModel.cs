using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Lab_02_Romanenko.Models;
using Lab_02_Romanenko.Tools;
using Lab_02_Romanenko.Tools.Exceptions;

namespace Lab_02_Romanenko.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged, ILoaderOwner
    {
        private static readonly int DAFULT_YEAR = 2000;
        private Person _person;

        public MainWindowViewModel()
        {
            ProceedCommand = new ProceedCommand(this);
            DateValue = new DateTime(2000, 1, 1);
            LoaderManager.Instance.Initialize(this);
            
        }

        public ProceedCommand ProceedCommand { get; set; }

        public string FirstNameValue { get; set; }

        public string LastNameValue { get; set; }

        public string EMailValue { get; set; }

        public DateTime DateValue { get; set; }

       

        public bool CanProceed()
        {
            return FirstNameValue != null && !FirstNameValue.Equals("") &&
                   LastNameValue != null && !LastNameValue.Equals("")
                   && EMailValue != null && !EMailValue.Equals("");
        }

        public async void Proceed()
        {
            try
            {
                LoaderManager.Instance.ShowLoader();
               await Task.Run(ShowInfo);

            }
            catch (TimeContinuumException e)
            {
                Console.Write(e.Message);
                MessageBox.Show("This... can't... be... The consistency of time was disrupted! Please try another date.");
            }
            catch (InvalidEMailException e)
            {
                Console.Write(e.Message);
                MessageBox.Show("Your e-mail is of an incorrect format. Please check if it's typed correctly.");
            }
            catch (UserIsProbablyDeadException e)
            {
                Console.Write(e.Message);
                MessageBox.Show("Ain't no service for dead people.");
            }
        }

        private void ShowInfo()
        {
            _person = new Person(FirstNameValue, LastNameValue, EMailValue, DateValue);
            if (_person.IsBirthday)
                MessageBox.Show("First of all, happy Birthday!");
            Thread.Sleep(1000);
            MessageBox.Show("First Name: " + _person._firstName + "\nLast Name: " + _person._lastName
                            + "\nE-Mail: " + _person._eMail + "\nBirth Date: " 
                            + _person._birthDate.Day+'.'+_person._birthDate.Month+'.'+_person._birthDate.Year
                            + "\nIsAdult: " + _person.IsAdult + "\nChinese: " + _person.ChineseSign + "\nWestern: " +
                            _person.WesternSign
                            + "\nIsBirthday: " + _person.IsBirthday);
            LoaderManager.Instance.HideLoader();

        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        
        #region Loader
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        #endregion


        

        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged(null);
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged(null);
            }
        } 
    }
}