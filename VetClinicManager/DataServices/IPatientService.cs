using VetClinicManager.Model;

namespace VetClinicManager.DataServices;

internal interface IPatientService
{
    Task<List<Patient>> GetPatientsAsync();
    Task<Patient> CreatePatientAsync(Patient patient);
    Task UpdatePatientAsync(Patient updatedPatient);
    Task DeletePatientAsync(int patientId);
}