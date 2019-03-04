using System;
namespace SafeCityWebAPI.Models
{
    public class SafetyRequest
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Url { get; set; }

        public SafetyRequest()
        {
        }
    }
}
