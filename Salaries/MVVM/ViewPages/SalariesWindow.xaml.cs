using Salaries.MVVM.ViewPageModels;
using System.Windows;

namespace Salaries.MVVM.ViewPages
{
    /// <summary>
    /// Interaction logic for SalariesWindow.xaml
    /// </summary>
    public partial class SalariesWindow : Window
    {
        public SalariesWindow()
        {
            InitializeComponent();
            DataContext = new SalariesWindowModel();
        }
    }
}