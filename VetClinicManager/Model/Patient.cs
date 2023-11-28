namespace VetClinicManager.Model
{
    internal class Patient
    {
        public int PatientId { get; set; }
        public int ClientId { get; set; }

        public string Species { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
