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
    /// Логика взаимодействия для EditTable.xaml
    /// </summary>
    public partial class EditTable : Window
    {
        public string TName { get; set; }
        public string Description { get; set; }

        public int WaiterId { get; set; }
        public EditTable()
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
            using (var dbContext = new CafeAdminDbContext())
            {
                
                var cc= dbContext.UserAccesLevels.Where(w => w.AccessLevel == AccessLevel.Waiter).Include(c => c.User).Select(k => k.User).ToArray();

                _waiters.ItemsSource = cc;
            }
            User tmp=null;
            foreach(User k in _waiters.Items)
            {
                if (k.Id == WaiterId)
                {
                    tmp = k;
                    break;
                }
            }
            _waiters.SelectedIndex = _waiters.Items.IndexOf(tmp);
            _name.Text = TName;
            _ds.Text = Description;

        }
    }
}
