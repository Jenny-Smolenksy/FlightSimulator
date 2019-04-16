using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.ViewModels.Windows;
using FlightSimulator.Model;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for manual.xaml
    /// </summary>
    public partial class manual : UserControl
    {
        private ViewModels.ManualViewModel viewModel;
        public manual()
        {
            InitializeComponent();
            viewModel = new ViewModels.ManualViewModel();
            this.DataContext = viewModel;
            this.Joystick.Moved += viewModel.Joystick_Moved;
            
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
