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
    /// Логика взаимодействия для EditGood.xaml
    /// </summary>
    public partial class EditGood : Window
    {
        public string GName { get; set; }
        public int Price { get; set; }
        public bool IsFood { get; set; }
        public EditGood()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GName = _name.Text;
            Price = Int32.Parse(_price.Text);
            IsFood = (string)uAccess.SelectedItem == "Food";
            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            uAccess.Items.Add("Food");
            uAccess.Items.Add("Drink");
            uAccess.SelectedIndex = IsFood ? 0 : 1;
            _name.Text = GName;
            _price.Text = Price.ToString();
        }
    }
}
