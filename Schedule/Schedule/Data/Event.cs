using System;
using SQLite;

namespace Schedule.Data
{
    class Event
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Text { get; set; }

        public Event(DateTime date, TimeSpan time, string text)
        {
            Date = date;
            Time = time;
            Text = text;
        }
    }
}
