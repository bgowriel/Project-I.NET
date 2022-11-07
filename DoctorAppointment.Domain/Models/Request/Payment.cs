using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Domain.Models.Request
{
    public class Payment
    {
        public DateTime Date { get; private set; }

        public int Amount { get; private set; }
        public User Patient { get; private set; }
    }
}
