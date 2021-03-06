using Cafe.Models;
using CafeAdmin.Data;
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
using System.Windows.Shapes;

namespace CafeAdmin
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public string UName { get; set; }
        public string Password { get; set; }
        public AccessLevel Level { get; set; }
        public AddUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UName = _name.Text;
            Password = _pass.Text;
            
            var levels = Enum.GetValues(typeof(AccessLevel));
            var selected = levels.GetValue((int)uAccess.SelectedIndex);

            Level = (AccessLevel)selected;

            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // uAccess.ItemsSource = AccessLevel; BIND ENUM TO COMBOBOX
            uAccess.ItemsSource = Enum.GetValues(typeof(AccessLevel)).Cast<AccessLevel>();
            uAccess.SelectedIndex = 0;
        }
    }
}
