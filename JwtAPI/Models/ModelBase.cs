namespace JwtAPI.Models
{
    public class ModelBase
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public int RecordState { get; set; }
    }
}
