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
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {           
            if (MainWindow.UsersList.Count == 0)
            {
                
                SignInStatus.Foreground = Brushes.Red;
                SignInStatus.Content = "Authorisation error, press REGISTER";
                
                return;             
            }

            foreach (var item in MainWindow.UsersList)
            {
                if (item.Name.ToLower() == NameToSignIn.Text.ToLower())
                {
                    MainWindow.CurrentUser = item;
                    MainWindow.MainFrame.Source = new Uri(@"Pages\HomePage.xaml", UriKind.RelativeOrAbsolute);
                    MainWindow.FindItem.Visibility = Visibility.Visible;
                    MainWindow.RegisterItem.Visibility = Visibility.Collapsed;
                    MainWindow.IsSignIn = true;
                    break;
                }
            }

            SignInStatus.Foreground = Brushes.Red;
            SignInStatus.Content = "Authorisation error, press REGISTER";

        }
    }
}
