using Schedule.MyControls;
using Schedule.Pages;
using Schedule.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Schedule
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        readonly List<Footer_Button> Buttons_Footer_List;

        public MainPage()
        {
            InitializeComponent();

            Buttons_Footer_List = new List<Footer_Button>() { Button_Calendar, Button_Notepad, Button_Settings };

            PageSelector.MainCaruosel = MainCarousel;

            List<PageContent> pages = new List<PageContent>() { new PageContent() { Content = new CalendarPage().Content },
                                                                new PageContent() { Content = new NotepadPage().Content }, 
                                                                new PageContent() { Content = new SettingsPage().Content } };
            MainCarousel.ItemsSource = pages;
        }

        private void Footer_Button_Calendar_Tapped(object sender, EventArgs e)
        {
            try
            {
                PageSelector.SelectPage(1);
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
                });
            }
        }

        private void Footer_Button_Notepad_Tapped(object sender, EventArgs e)
        {
            try
            {
                PageSelector.SelectPage(2);
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
                });
            }
        }

        private void Footer_Button_Settings_Tapped(object sender, EventArgs e)
        {
            try
            {
                PageSelector.SelectPage(3);
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
                });
            }
        }

        private void MainCarousel_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            for (int i = 0; i < Buttons_Footer_List.Count; i++)
                Buttons_Footer_List[i].LineSelectorColor = i == MainCarousel.Position ? Color.FromHex(Styles.Footer_LineSelector_Color_Active) : 
                                                                                        Color.FromHex(Styles.Footer_LineSelector_Color_Passive);
        }

        class PageContent
        {
            public View Content { get; set; }
        }
    }
}