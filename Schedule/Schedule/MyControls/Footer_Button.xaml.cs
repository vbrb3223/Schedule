using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule.MyControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Footer_Button : ContentView
    {

        public static readonly BindableProperty LineSelectorProperty = BindableProperty.Create(nameof(LineSelectorColor),
            typeof(Color),
            typeof(Footer_Button),
            default(Color));

        public Color LineSelectorColor
        {
            get
            {
                return (Color)GetValue(LineSelectorProperty);
            }
            set
            {
                SetValue(LineSelectorProperty, value);
            }
        }


        public static readonly BindableProperty ButtonImageProperty = BindableProperty.Create(nameof(ButtonImageSource),
            typeof(ImageSource),
            typeof(Footer_Button),
            default(ImageSource));

        public ImageSource ButtonImageSource
        {
            get
            {
                return (ImageSource)GetValue(ButtonImageProperty);
            }
            set
            {
                SetValue(ButtonImageProperty, value);
            }
        }

        public Footer_Button()
        {
            InitializeComponent();

            LineSelector.SetBinding(BoxView.ColorProperty, new Binding(nameof(LineSelectorColor), source: this));
            ButtonImage.SetBinding(Image.SourceProperty, new Binding(nameof(ButtonImageSource), source: this));
        }
    }
}