using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace MyPasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoadContent();
        }

        public void LoadContent()
        {
            var jsonString = Encryptor.Decrypt("master_password");
            var logins = JsonSerializer.Deserialize<LoginJsonRoot>(jsonString);

            var loginData = new List<LoginData>();
            foreach (var login in logins.Logins)
            {
                loginData.Add(new LoginData() { Name = login.Name, Username = login.Username, Password = login.Password });
            }

            lvLoginData.ItemsSource = loginData;
        }

        private void MenuItem_Add_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddWindow
            {
                Owner = this,
                ShowInTaskbar = false
            };

            window.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                LoadContent();
            }
        }
    }
}
