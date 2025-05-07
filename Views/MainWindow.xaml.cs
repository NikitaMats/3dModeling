using System.Windows;
using TestCase_Sputnik.Services;
using TestCase_Sputnik.ViewModels;
using TestCase_Sputnik.Services.Interfaces;

namespace TestCase_Sputnik.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            I3DRenderService renderService = new ThreeDRenderService(Viewport);

            DataContext = new MainViewModel(renderService);
        }
    }
}