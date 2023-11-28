using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VetClinicManager.Model;

namespace VetClinicManager.DataServices
{
    internal class PatientService(string baseAddress) : IPatientService
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri(baseAddress) };

        public async Task<List<Patient>> GetPatientsAsync()
        {
            var patients = await _httpClient.GetFromJsonAsync<List<Patient>>("api/patients");

            return patients;
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            var response = await _httpClient.PostAsJsonAsync("api/patients", patient);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Patient>();
        }

        public async Task UpdatePatientAsync(Patient updatedPatient)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/patients/", updatedPatient);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePatientAsync(int patientId)
        {
            var response = await _httpClient.DeleteAsync($"api/patients/{patientId}");

            response.EnsureSuccessStatusCode();
        }
    }
}
