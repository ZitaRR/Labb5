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
            Data.Load();
            lbUsers.SelectionChanged += ListBox_SelectionChanged;
            lbUsers.ItemsSource = Data.SortNormalUsers();
            lbAdmins.ItemsSource = Data.SortAdminUsers();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = null;

            if ((sender as ListBox).Name == "lbUsers")
            {
                listbox = sender as ListBox;
                EnableButton(BtnAdmin);
                DisableButton(BtnNormal);
            }
            else if ((sender as ListBox).Name == "lbAdmins")
            {
                listbox = sender as ListBox;
                EnableButton(BtnNormal);
                DisableButton(BtnAdmin);
            }

            if (listbox.SelectedItem is null)
            {
                DisableButton(BtnNormal);
                DisableButton(BtnAdmin);
                DisableButton(BtnUpdate);
            }

            currentUser = listbox.SelectedItem as User;
            EnableButton(btnDelete);
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
            currentUser = new User(name, email);
            Data.AddUser(currentUser);
            lbUsers.ItemsSource = Data.SortNormalUsers();
            currentUser = null;
            Refresh();
            DisableButton(btnCreate);
        }

        private void UpdateUser()
        {
            if (currentUser is null)
                return;

            Data.UpdateUser(currentUser);
            lbUsers.ItemsSource = Data.SortNormalUsers();
            lbAdmins.ItemsSource = Data.SortAdminUsers();
        }

        private void DisableButton(Button button)
            => button.IsEnabled = false;

        private void EnableButton(Button button)
            => button.IsEnabled = true;

        private void Refresh()
        {
            if (currentUser is null)
            {
                UserInfoLabel.Content = "";
                txtName.Text = "";
                txtEMail.Text = "";
                lbUsers.SelectedItem = null;
                lbAdmins.SelectedItem = null;
                DisableButton(btnDelete);
                return;
            }

            UserInfoLabel.Content = currentUser.Info();
            txtName.Text = currentUser.Name;
            txtEMail.Text = currentUser.Email;
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            Data.DeleteUser(currentUser);
            UpdateUser();
            currentUser = null;
            DisableButton(btnDelete);
            Refresh();
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
            Refresh();
            DisableButton(BtnUpdate);
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            User user = lbUsers.SelectedItem as User;
            if (user is null)
                user = lbAdmins.SelectedItem as User;

            if (!(user is null))
            {
                if (Data.GetUserByEmail(txtEMail.Text) is null)
                    goto EmailDoesNotExist;
                DisableButton(btnCreate);
                if (user.Name != txtName.Text && txtName.Text != "")
                    EnableButton(BtnUpdate);
                else DisableButton(BtnUpdate);
                return;
            }

            EmailDoesNotExist:
            if (txtName.Text != "" && txtEMail.Text != "")
            {
                EnableButton(btnCreate);
                DisableButton(BtnUpdate);
            }
        }
    }
}
