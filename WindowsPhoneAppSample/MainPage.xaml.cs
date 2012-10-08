using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WindowsPhoneAppSample.ViewModel;

namespace WindowsPhoneAppSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly MainPageViewModel dataContext;

        public MainPage()
        {
            InitializeComponent();
            dataContext = new MainPageViewModel();
            DataContext = dataContext;
        }
    }
}