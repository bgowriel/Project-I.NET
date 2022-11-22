namespace DoctorAppointment.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IAppointmentRepository AppointmentRepository { get; }

        public IBillRepository BillRepository { get; }

        //public IDoctorRepository Doctors { get; }

        public IMedicalVisitRepository MedicalVisitRepository { get; }

        //public IPatientRepository Patients { get; }

        //public IRoleRepository Roles { get; }

        //public IUserRepository Users { get; }

        Task Save();
    }
}
