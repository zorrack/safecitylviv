using System;

namespace SafeCity.Model
{
    public class IssueMessageItem
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
