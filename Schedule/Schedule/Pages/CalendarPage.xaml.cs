using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Schedule.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        List<Frame> eventsToSelection = new List<Frame>();
        bool Selector = false;
        readonly SQLiteConnection conn;
        public List<Event> Events = new List<Event>();
        int eventToEditID = 0;

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
            PagePresets();
        }

        public void PagePresets()
        {
            DateSelector.MinimumDate = DateTime.Now;
            DateSelector.Date = DateTime.Now;
            TimeSelector.Time = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 5, 0));
            TextEditor.Text = "";
        }

        public void UploadEvents()
        {
            
            Events.Clear();
            EventsList.ItemsSource = null;
            conn.CreateTable<Event>();
            Events.AddRange(conn.Table<Event>().ToList());
            EventsList.ItemsSource = Events;
        }

        private async void EditEvent_Clicked(object sender, EventArgs e)
        {
            this.popuplayout.IsVisible = !this.popuplayout.IsVisible;
            this.popuplayout.AnchorX = 1;
            this.popuplayout.AnchorY = 1;

            Animation scaleAnimation = new Animation(
                f => this.popuplayout.Scale = f,
                0.5,
                1,
                Easing.SinInOut);

            Animation fadeAnimation = new Animation(
                f => this.popuplayout.Opacity = f,
                0.2,
                1,
                Easing.SinInOut);

            scaleAnimation.Commit(this.popuplayout, "popupScaleAnimation", 250);
            fadeAnimation.Commit(this.popuplayout, "popupFadeAnimation", 250);
            await MainGrid.FadeTo(0);
            popup_Loaded((sender as SwipeItem).BindingContext as Event);
        }

        private void DeleteEvent_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await App.Current.MainPage.DisplayAlert("Подтверждение", "Вы хотите удалить событие?", "Да", "Нет"))
                {
                    conn.CreateTable<Event>();
                    Event currentEvent = (sender as SwipeItem).BindingContext as Event;
                    conn.Delete(conn.Table<Event>().Where(ev => ev.Id == currentEvent.Id).FirstOrDefault());
                    UploadEvents();
                }
            });
        }

        private void popup_Cancel_Clicked(object sender, EventArgs e)
        {
            popup_Close();
        }

        private void popup_Save_Clicked(object sender, EventArgs e)
        {
            if (popup_EditorText.Text.Length != 0)
            {
                var EventToEdit = Events.Where(ev => ev.Id == eventToEditID).FirstOrDefault();
                EventToEdit.Text = popup_EditorText.Text;
                EventToEdit.Date = popup_DPDate.Date;
                EventToEdit.Time = popup_TPTime.Time;
                conn.Update(EventToEdit);
                UploadEvents();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Операция выполнена", "Событие успешно изменено", "OK");
                });
                popup_Close();
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ошибка", "Введите текст события", "OK");
                });
            }
        }

        private async void popup_Close()
        {
            await Task.WhenAny<bool>
                  (
                    this.popuplayout.FadeTo(0, 200, Easing.SinInOut)
                  );

            this.popuplayout.IsVisible = !this.popuplayout.IsVisible;
            await MainGrid.FadeTo(100);
        }

        private void popup_Loaded(Event currentEvent)
        {
            popup_DPDate.Date = currentEvent.Date;
            popup_TPTime.Time = currentEvent.Time;
            popup_EditorText.Text = currentEvent.Text;
            eventToEditID = currentEvent.Id;
        }

        private void EventSelector_Tapped(object sender, EventArgs e)
        {
            var eventContainer = ((sender as SwipeView).Content as Frame);
            if (Selector)
            {
                if (eventContainer.BackgroundColor == Color.Default)
                {
                    eventContainer.BackgroundColor = Color.FromHex("#ea7273");
                    eventsToSelection.Add(eventContainer);
                }
                else
                {
                    eventContainer.BackgroundColor = Color.Default;
                    eventsToSelection.Remove(eventContainer);
                }
            }
        }

        private void SelectorButton_Tapped(object sender, EventArgs e)
        {
            var image = (sender as Image);

            if (Selector)
            {
                image.Source = ImageSource.FromResource("Schedule.Images.select.png");
                eventsToSelection.ForEach(ev => ev.BackgroundColor = Color.Default);
                eventsToSelection.Clear();
                SelectedTrashButton.IsVisible = false;
            }
            else
            {
                image.Source = ImageSource.FromResource("Schedule.Images.select_active.png");
                SelectedTrashButton.IsVisible = true;
            }

            Selector = !Selector;
        }

        private void SelectorTrash_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await App.Current.MainPage.DisplayAlert("Подтверждение", "Вы хотите удалить выбранные события?", "Да", "Нет"))
                {
                    conn.CreateTable<Event>();
                    eventsToSelection.ForEach(ev => {
                        var eventContainer = (ev.Parent as SwipeView).Parent as StackLayout;
                        var eventInfo = (eventContainer.BindingContext) as Event;
                        conn.Delete(conn.Table<Event>().Where(evnt => evnt.Id == eventInfo.Id).FirstOrDefault());
                    });
                    UploadEvents();
                }
            });
            
        }
    }
}