using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using VetClinicManager.Command;
using VetClinicManager.DataServices;
using VetClinicManager.Model;

namespace VetClinicManager.ViewModel
{
    internal class PatientCreateViewModel : INotifyPropertyChanged
    {
        private readonly IPatientService _service;

        public Patient NewPatient { get; set; }

        public ICommand SavePatientCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public PatientCreateViewModel()
        {
            _service = new PatientService(Properties.Settings.Default.apiPath);

            NewPatient = new Patient() {DateOfBirth = DateTime.Now};

            SavePatientCommand = new RelayCommand(SavePatient);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

        private void CloseWindow(object param)
        {
            if (param is Window window) window.Close();
        }

        private async void SavePatient(object param)
        {
            if (param is not Window window) return;
            try
            {
                await _service.CreatePatientAsync(NewPatient);
                CloseWindow(param);
                OnPropertyChanged(nameof(NewPatient));
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to add the patient. " +
                                "Please check if all the fields entered are correct.", "Create Patient",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
