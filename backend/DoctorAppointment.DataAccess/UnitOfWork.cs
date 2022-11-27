using DoctorAppointment.Application.Interfaces;

namespace DoctorAppointment.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public IAppointmentRepository AppointmentRepository { get; private set; }
        public IMedicalVisitRepository MedicalVisitRepository { get; private set; }

        public IBillRepository BillRepository { get; private set; }

        public UnitOfWork(DatabaseContext context,
                          IAppointmentRepository appointmentRepository,
                          IMedicalVisitRepository medicalVisitRepository,
                          IBillRepository billRepository)
        {
            _databaseContext = context;
            AppointmentRepository = appointmentRepository;
            MedicalVisitRepository = medicalVisitRepository;
            BillRepository = billRepository;
        }

        public async Task Save()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }
    }
}
