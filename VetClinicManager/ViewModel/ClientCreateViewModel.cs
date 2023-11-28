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
    internal class ClientCreateViewModel : INotifyPropertyChanged
    {
        private readonly IClientService _service;

        public Client NewClient { get; set; }

        public ICommand SaveClientCommand { get; }
        public ICommand CloseWindowCommand { get; }

        public ClientCreateViewModel()
        {
            _service = new ClientService(Properties.Settings.Default.apiPath);

            NewClient = new Client();

            SaveClientCommand = new RelayCommand(SaveClient);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

        private void CloseWindow(object param)
        {
            if (param is Window window) window.Close();
        }

        private async void SaveClient(object param)
        {
            if (param is not Window window) return;
            try
            {
                await _service.CreateClientAsync(NewClient);
                CloseWindow(param);
                OnPropertyChanged(nameof(NewClient));
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to add the client. " +
                                "Please check if all the fields entered are correct.", "Create Client",
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
