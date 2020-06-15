using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var encoding = Encoding.UTF8.GetBytes(PasswordTextbox.Password);

            var sha2 = SHA256.Create();
            var hash = sha2.ComputeHash(encoding);

            using var fileStream = File.Open("password.bin", FileMode.OpenOrCreate, FileAccess.Read);
            var buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, buffer.Length);

            if (CompareHash(hash, buffer))
            {
                var mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.MasterPassword = PasswordTextbox.Password;
                DialogResult = true;
            }
            else
            {
                InvalidPasswordLabel.Content = "Invalid Password";
            }
        }

        private static bool CompareHash(byte[] hash, byte[] buffer)
        {
            if (hash.Length == buffer.Length)
            {
                int i = 0;
                while ((i < hash.Length) && (hash[i] == buffer[i]))
                {
                    i += 1;
                }

                if (i == hash.Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
