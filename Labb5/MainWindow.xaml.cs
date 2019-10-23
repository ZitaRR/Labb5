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
using System.Runtime.InteropServices;

namespace Labb5
{
    public partial class MainWindow : Window
    {
        private User currentUser;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).Name == "lbUsers")
            {
                if (lbUsers.SelectedItem is null)
                    return;

                currentUser = lbUsers.SelectedItem as User;
                txtName.Text = currentUser.Name;
                txtEMail.Text = currentUser.Email;
            }
            else if ((sender as ListBox).Name == "lbAdmins")
            {

            }
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (txtName.Text is "" || txtEMail.Text is "")
            {
                MessageBox.Show("Please enter a name and a e-mail address.", "ERROR");
                return;
            }

            string name = txtName.Text;
            string email = txtEMail.Text;
            User user = new User(name, email);
            Data.AddUser(user);
            List<User> users = Data.Load();
            lbUsers.ItemsSource = users;
            txtName.Text = "";
            txtEMail.Text = "";
        }

        private void UpdateUser(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {

        }

        private void RegularUser(object sender, RoutedEventArgs e)
        {

        }

        private void AdminUser(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNormal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
