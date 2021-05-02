using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace Schedule.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            DateSelector.MinimumDate = DateTime.Now;
            DateSelector.Date = DateTime.Now;
            TimeSelector.Time = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 5, 0));
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

            
        }
    }
}