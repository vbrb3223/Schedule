using System;
using SQLite;

namespace Schedule.Data
{
    public class Event
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Text { get; set; }
    }
}