using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using VetClinicManager.Command;
using VetClinicManager.DataServices;
using VetClinicManager.Model;
using VetClinicManager.View.Windows;

namespace VetClinicManager.ViewModel
{
    internal class PatientsViewModel : INotifyPropertyChanged
    {
        private readonly IPatientService _service;

        // INotifyPropertyChanged is an interface that allows us to notify the UI that a property has changed

        public ObservableCollection<Patient> Patients { get; set; }
        private Patient? _selectedPatient = null;

        public Patient? SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                if (value == null)
                {
                    _selectedPatient = null;
                    OnPropertyChanged(nameof(SelectedPatient));
                    return;
                }

                _selectedPatient ??= new Patient();

                _selectedPatient.Name = value.Name;
                _selectedPatient.Breed = value.Breed;
                _selectedPatient.ClientId = value.ClientId;
                _selectedPatient.DateOfBirth = value.DateOfBirth;
                _selectedPatient.PatientId = value.PatientId;
                _selectedPatient.Species = value.Species;

                OnPropertyChanged(nameof(SelectedPatient));
            }
        }

        public ICommand DeletePatientCommand { get; }
        public ICommand UpdatePatientCommand { get; }
        public ICommand CreatePatientCommand { get; }

        public ICommand ShowCreatePatientWindowCommand { get; }

        public PatientsViewModel()
        {
            _service = new PatientService(VetClinicManager.Properties.Settings.Default.apiPath);

            Patients = new ObservableCollection<Patient>();

            DeletePatientCommand = new RelayCommand(DeletePatient);
            UpdatePatientCommand = new RelayCommand(UpdatePatient, CanExecute);
            CreatePatientCommand = new RelayCommand(CreatePatient);

            ShowCreatePatientWindowCommand = new RelayCommand(ShowCreatePatientWindow);

            LoadData();
        }

        private async void DeletePatient(object param)
        {
            if (param is int id)
            {
                await _service.DeletePatientAsync(id);
                LoadData();
            }
        }

        private async void UpdatePatient(object param)
        {
            if (param is Patient patient)
            {
                await _service.UpdatePatientAsync(patient);
                LoadData();
            }
        }

        private async void CreatePatient(object param)
        {
            if (param is Patient patient)
            {
                await _service.CreatePatientAsync(patient);
                LoadData();
            }
        }

        private void ShowCreatePatientWindow(object param)
        {
            var window = new PatientCreateWindow();
            window.ShowDialog();
            LoadData();
        }

        private bool CanExecute(object parameter)
        {
            return SelectedPatient != null;
        }

        private async Task LoadData()
        {

            Patients = new ObservableCollection<Patient>(await _service.GetPatientsAsync());
            OnPropertyChanged(nameof(Patients));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
