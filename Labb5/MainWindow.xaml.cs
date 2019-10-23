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
            ListBox listbox = null;

            if ((sender as ListBox).Name == "lbUsers" ||
                (sender as ListBox).Name == "lbAdmins")
                listbox = sender as ListBox;

            if (listbox.SelectedItem is null)
                return;

            currentUser = listbox.SelectedItem as User;
            Refresh();
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
            lbUsers.ItemsSource = Data.SortNormalUsers();
            txtName.Text = "";
            txtEMail.Text = "";
        }

        private void UpdateUser()
        {
            if (currentUser is null)
                return;

            Data.UpdateUser(currentUser);
            lbUsers.ItemsSource = Data.SortNormalUsers();
            lbAdmins.ItemsSource = Data.SortAdminUsers();

            Refresh();
        }

        private void Refresh()
        {
            UserInfoLabel.Content = currentUser.Info();
            txtName.Text = currentUser.Name;
            txtEMail.Text = currentUser.Email;
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {

        }

        private void BtnNormal_Click(object sender, RoutedEventArgs e)
        {
            currentUser.Permission = User.Permissions.Normal;
            UpdateUser();
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            currentUser.Permission = User.Permissions.Admin;
            UpdateUser();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text is "")
                return;
            if (txtEMail.Text is "")
                return;
            if (txtName.Text == currentUser.Name && txtEMail.Text == currentUser.Email)
                return;

            currentUser.Name = txtName.Text;
            currentUser.Email = txtEMail.Text;
            UpdateUser();
        }
    }
}
