namespace DoctorAppointment.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IAppointmentRepository AppointmentRepository { get; }

        public IBillRepository BillRepository { get; }
        
		public IOfficeRepository OfficeRepository { get; }

		public IMedicalVisitRepository MedicalVisitRepository { get; }

        public IAvailableDateRepository AvalaibleDateRepository { get; }

        Task Save();
    }
}
