using Cafe.Models;
using CafeAdmin.Data;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для AddTable.xaml
    /// </summary>
    

    public partial class AddTable : Window
    {
        public string TName { get; set; }
        public string Description { get; set; }

        public int WaiterId { get; set; }
        public AddTable()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            TName = _name.Text;
            Description = _ds.Text;
            WaiterId = ((User)_waiters.SelectedItem).Id;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using(var dbContext=new CafeAdminDbContext())
            {
                _waiters.ItemsSource = dbContext.UserAccesLevels.Where(w => w.AccessLevel == AccessLevel.Waiter).Include(c => c.User).Select(k => k.User).ToArray();
            }
        }
    }
}
