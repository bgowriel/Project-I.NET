using DoctorAppointment.Application.Interfaces;

namespace DoctorAppointment.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public IAppointmentRepository AppointmentRepository { get; private set; }
        
        public IMedicalVisitRepository MedicalVisitRepository { get; private set; }

        public IOfficeRepository OfficeRepository { get; private set; }

        public IBillRepository BillRepository { get; private set; }

        public UnitOfWork(DatabaseContext context,
                          IOfficeRepository officeRepository,
                          IAppointmentRepository appointmentRepository,
                          IMedicalVisitRepository medicalVisitRepository,
                          IBillRepository billRepository)
        {
            _databaseContext = context;
            AppointmentRepository = appointmentRepository;
            MedicalVisitRepository = medicalVisitRepository;
            BillRepository = billRepository;
            OfficeRepository = officeRepository;
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
