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
        static CarouselView mainCarousel;

        public static int SelectedPage { get { return _selectedPage; } }
        public static CarouselView MainCaruosel { set { mainCarousel = value; } }

        public static void SelectPage(int pageNumber)
        {
            if (pageNumber < 1 && pageNumber > 3)
                throw new Exception("Page number out of range");

            if (mainCarousel == null)
                throw new Exception("Elements was null");

            switch (pageNumber)
            {
                case 1:
                    mainCarousel.Position = 0;
                    break;
                case 2:
                    mainCarousel.Position = 1;
                    break;
                case 3:
                    mainCarousel.Position = 2;
                    break;
            }

            _selectedPage = pageNumber;
        }
    }
}
