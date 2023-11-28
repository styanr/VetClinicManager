using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VetClinicManager.Command;
using VetClinicManager.DataServices;
using VetClinicManager.Model;
using VetClinicManager.View.Windows;

namespace VetClinicManager.ViewModel
{
    internal class ClientsViewModel : INotifyPropertyChanged
    {
        private readonly IClientService _service;

        public ObservableCollection<Client> Clients { get; set; }
        private Client? _selectedClient = null;

        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                if (value == null)
                {
                    _selectedClient = null;
                    OnPropertyChanged(nameof(SelectedClient));
                    return;
                }

                _selectedClient ??= new Client();

                _selectedClient.FirstName = value.FirstName;
                _selectedClient.LastName = value.LastName;
                _selectedClient.ClientId = value.ClientId;
                _selectedClient.Email = value.Email;
                _selectedClient.PhoneNumber = value.PhoneNumber;
                _selectedClient.PostalCode = value.PostalCode;
                _selectedClient.Address = value.Address;

                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public ICommand DeleteClientCommand { get; }
        public ICommand UpdateClientCommand { get; }
        public ICommand CreateClientCommand { get; }

        public ICommand ShowCreateClientWindowCommand { get; }

        public ClientsViewModel()
        {
            _service = new ClientService(VetClinicManager.Properties.Settings.Default.apiPath);

            Clients = new ObservableCollection<Client>();

            DeleteClientCommand = new RelayCommand(DeleteClient);
            UpdateClientCommand = new RelayCommand(UpdateClient, CanExecute);
            CreateClientCommand = new RelayCommand(CreateClient);

            ShowCreateClientWindowCommand = new RelayCommand(ShowCreateClientWindow);

            LoadData();
        }

        private async void DeleteClient(object param)
        {
            if (param is int id)
            {
                await _service.DeleteClientAsync(id);
                LoadData();
            }
        }

        private async void UpdateClient(object param)
        {
            if (param is Client client)
            {
                await _service.UpdateClientAsync(client);
                LoadData();
            }
        }

        private async void CreateClient(object param)
        {
            if (param is Client client)
            {
                await _service.CreateClientAsync(client);
                LoadData();
            }
        }

        private void ShowCreateClientWindow(object param)
        {
            var window = new ClientCreateView();
            window.ShowDialog();
            LoadData();
        }

        private bool CanExecute(object parameter)
        {
            return SelectedClient != null;
        }

        private async Task LoadData()
        {
            Clients = new ObservableCollection<Client>(await _service.GetClientsAsync());
            OnPropertyChanged(nameof(Clients));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
