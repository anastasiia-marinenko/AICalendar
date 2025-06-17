using System;
using System.Collections.Generic;

namespace AICalendar.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<Participant> Participants { get; set; } = new List<Participant>();
    }
}