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
using System.Xml.Serialization;
using System.IO;

namespace SweetDreams
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static List<User> UsersList;
        public static Frame MainFrame;
        public static MenuItem FindItem;
        public static MenuItem RegisterItem;
        public static bool IsSignIn = false;
        public static readonly string pathToUsers = "Users.xml";
        public static User CurrentUser;

        public MainWindow()
        {
            InitializeComponent();
            UsersList = GetUsers();
            MainFrame = MFrame;
            FindItem = FindMenu;
            RegisterItem = Register;
            CurrentUser = null;
            MainFrame.Source= new Uri(@"Pages\SignIn.xaml", UriKind.RelativeOrAbsolute);        

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri(@"Pages\Register.xaml", UriKind.RelativeOrAbsolute);
        }

        private void FindMenu_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri(@"Pages\Find.xaml", UriKind.RelativeOrAbsolute);
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = IsSignIn ? new Uri(@"Pages\HomePage.xaml", UriKind.RelativeOrAbsolute) :
                new Uri(@"Pages\SignIn.xaml", UriKind.RelativeOrAbsolute);
        }

        public static bool CreateUser(User addUser)
        {
            List<User> Users = GetUsers();
            Users.Add(addUser);
           
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            try
            {
                using (FileStream fs = new FileStream(pathToUsers, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, Users);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<User> GetUsers()
        {
            List<User> Users = new List<User>();
            XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
            FileInfo fi = new FileInfo(pathToUsers);
            if (fi.Exists)
            {
                using (FileStream fs = new FileStream(pathToUsers, FileMode.OpenOrCreate))
                {
                    Users = (List<User>)formatter.Deserialize(fs);
                }
            }
            return Users == null ? new List<User>() : Users;
        }
    }
}
