using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.IO;

namespace SweetDreams.Pages
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        public static string status;       
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(Name.Text))
            {
                StatusBlock.Foreground = Brushes.Red;
                StatusBlock.Text = "Fill name field";               
                return;
            }
            foreach (var item in MainWindow.UsersList)
            {
                if (item.Name.ToLower() == Name.Text.ToLower())
                {
                    StatusBlock.Foreground = Brushes.Red;
                    StatusBlock.Text = "User with that name already exists";                    
                    return;
                }                   
            }

            User addUser = new User();
            addUser.Name = Name.Text;
            addUser.PhoneNumber = Phone.Text;
            addUser.PersonalKey = Guid.NewGuid();
            if (Gender.Text == "Male")
                addUser.Gender = gender.male;
            else
                addUser.Gender = gender.female;

            if (LookingFor.Text == "Male")
                addUser.LookingFor = gender.male;
            else
                addUser.LookingFor = gender.female;
            addUser.UserID = MainWindow.UsersList.Count + 1;

            MainWindow.UsersList.Add(addUser);
            MainWindow.CreateUser(addUser);
            MainWindow.IsSignIn = true;
            MainWindow.CurrentUser = addUser;
            MainWindow.MainFrame.Source = new Uri(@"Pages\HomePage.xaml", UriKind.RelativeOrAbsolute);
            MainWindow.RegisterItem.Visibility = Visibility.Collapsed;
            MainWindow.FindItem.Visibility = Visibility.Visible;
        }
    }
}
