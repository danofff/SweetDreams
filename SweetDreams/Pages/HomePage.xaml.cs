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

namespace SweetDreams.Pages
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            NameHome.Content = MainWindow.CurrentUser.Name;
            PhoneHome.Content = MainWindow.CurrentUser.PhoneNumber;
            GenderHome.Content = MainWindow.CurrentUser.Gender;
            LookingForHome.Content = MainWindow.CurrentUser.LookingFor;
        }
    }
}
