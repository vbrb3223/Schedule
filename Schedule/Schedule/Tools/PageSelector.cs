using System;
using Schedule.MyControls;
using Schedule.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule.Tools
{
    public static class PageSelector
    {
        static int _selectedPage = 1;
        static Footer_Button button_1;
        static Footer_Button button_2;
        static Footer_Button button_3;
        static CarouselView mainCarousel;

        public static int SelectedPage { get { return _selectedPage; } }
        public static Footer_Button Button_1 { set { button_1 = value; } }
        public static Footer_Button Button_2 { set { button_2 = value; } }
        public static Footer_Button Button_3 { set { button_3 = value; } }
        public static CarouselView MainCaruosel { set { mainCarousel = value; } }

        public static void SelectPage(int pageNumber)
        {
            if (pageNumber < 1 && pageNumber > 3)
                throw new Exception("Page number out of range");

            if (button_1 == null || button_2 == null || button_3 == null || mainCarousel == null)
                throw new Exception("Elements was null");

            button_1.LineSelectorColor = Color.FromHex(Styles.Footer_LineSelector_Color_Passive);
            button_2.LineSelectorColor = Color.FromHex(Styles.Footer_LineSelector_Color_Passive);
            button_3.LineSelectorColor = Color.FromHex(Styles.Footer_LineSelector_Color_Passive);

            switch (pageNumber)
            {
                case 1:
                    mainCarousel.Position = 0;
                    button_1.LineSelectorColor = Color.FromHex(Styles.Footer_LineSelector_Color_Active);
                    break;
                case 2:
                    mainCarousel.Position = 1;
                    button_2.LineSelectorColor = Color.FromHex(Styles.Footer_LineSelector_Color_Active);
                    break;
                case 3:
                    mainCarousel.Position = 2;
                    button_3.LineSelectorColor = Color.FromHex(Styles.Footer_LineSelector_Color_Active);
                    break;
            }

            _selectedPage = pageNumber;
        }
    }
}
