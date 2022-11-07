namespace DoctorAppointment.Domain.Models.Request
{
    public class MedicineRequest
    {
        public string? Name { get; set; }
        
        public string? Code { get; set; }
        
        public float? Price { get; set; }
        
        public int? Quantity { get; set; }
        
        public string? Description { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        
        public DateTime? ExpirationDate { get; set; }
    }
}
