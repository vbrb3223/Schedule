using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Schedule.Data;

namespace Schedule.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        SQLiteConnection conn;
        public List<Event> Events = new List<Event>();

        public CalendarPage()
        {
            InitializeComponent();
            conn = new SQLiteConnection(App.FilePath);
            PagePresets();
            UploadEvents();
        }

        private void ButtonAddEvent_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextEditor.Text))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ошибка", "Введите текст события", "OK");
                });
                return;
            }

            if (DateSelector.Date == DateTime.Now.Date && TimeSelector.Time < DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 1, 0)))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ошибка", "Минимальное установленное время должно быть через минуту", "OK");
                });
                return;
            }

            Event newEvent = new Event()
            {
                Date = DateSelector.Date,
                Time = TimeSelector.Time,
                Text = TextEditor.Text
            };
            
            conn.CreateTable<Event>();
            conn.Insert(newEvent);
            UploadEvents();
        }

        public void PagePresets()
        {
            DateSelector.MinimumDate = DateTime.Now;
            DateSelector.Date = DateTime.Now;
            TimeSelector.Time = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 5, 0));
        }

        public void UploadEvents()
        {
            
            Events.Clear();
            EventsList.ItemsSource = null;
            conn.CreateTable<Event>();
            Events.AddRange(conn.Table<Event>().ToList());
            EventsList.ItemsSource = Events;
        }
    }
}