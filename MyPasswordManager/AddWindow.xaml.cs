using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Shapes;

namespace MyPasswordManager
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var name = NameTextbox.Text;
            var username = UsernameTextbox.Text;
            var password = PasswordTextbox.Text;

            var loginData = new LoginData() 
            {
                Name = name,
                Username = username,
                Password = password
            };

            var mainWindow = Application.Current.MainWindow as MainWindow;

            // Read
            var jsonString = Encryptor.Decrypt(mainWindow.MasterPassword);
            var logins = JsonSerializer.Deserialize<LoginJsonRoot>(jsonString);
            logins.Logins.Add(loginData);

            jsonString = JsonSerializer.Serialize(logins);
            Encryptor.Encrypt(mainWindow.MasterPassword, jsonString);

            if (Application.Current.MainWindow is MainWindow window)
            {
                window.LoadContent();
            }

            Close();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
