using Cafe.Models;
using CafeAdmin.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xceed.Wpf.Toolkit;

namespace CafeAdmin
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public List<GoodInOrder> Goods { get; set; } = new List<GoodInOrder>();
        public ClientTable Table { get; set; }
        public DateTime datetime { get; set; }

        public ObservableCollection<GoodInOrder> Lst { get; set; } = new ObservableCollection<GoodInOrder>();

        public AddOrder()
        {
            InitializeComponent();
        }
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
           
            using(var dbContext=new CafeAdminDbContext())
            {
                if(dbContext.Goods.ToArray().Length==0 || dbContext.UserAccesLevels.Where(w=>w.AccessLevel!=AccessLevel.Admin).ToArray().Length==0 || dbContext.ClientTables.ToArray().Length == 0)
                {
                    System.Windows.MessageBox.Show("Please ensure that you have at least 1 row in Goods, Tables and at least 1 user with non-admin access");
                }
                var good = dbContext.Goods.Select(a => a).ToArray();
               
                var tables = dbContext.ClientTables.ToArray();
                _table.ItemsSource = tables;
                goodName.ItemsSource = good;
                goodAmount.Content = 0;
                OrderGoods.ItemsSource = Lst;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
            Table = (ClientTable)_table.SelectedItem;
            datetime = _date.Value.Value;
            Goods = Lst.ToList();
            DialogResult = true;
            Close();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            var gd = new GoodInOrder() { Good = (Goods)goodName.SelectedItem, Amount = (int)goodAmount.Content };
            if (gd.Amount > 0) {
                var srch = Lst.Where(c => c.Good.Name == gd.Good.Name).FirstOrDefault();
                if (srch != null)
                {
                    var ind = Lst.IndexOf(srch);
                    Lst[ind].Amount += gd.Amount;
                    
                    
                }
                else
                {
                    Lst.Add(gd);
                }
                OrderGoods.ItemsSource = null;
                OrderGoods.ItemsSource = Lst;
            }

        }

        private void goodAmount_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            var btn = (ButtonSpinner)sender;
            if (e.Direction == Xceed.Wpf.Toolkit.SpinDirection.Increase) btn.Content = (int)btn.Content + 1;
            else if(((int)btn.Content)>0) btn.Content = (int)btn.Content - 1;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var gd = new GoodInOrder() { Good = (Goods)goodName.SelectedItem, Amount = (int)goodAmount.Content };
            if (gd.Amount > 0)
            {
                var srch = Lst.Where(c => c.Good.Name == gd.Good.Name).FirstOrDefault();
                if (srch != null)
                {
                    var ind = Lst.IndexOf(srch);
                    if (Lst[ind].Amount <= gd.Amount) Lst.RemoveAt(ind);
                    else Lst[ind].Amount -= gd.Amount;
                }
                OrderGoods.ItemsSource = null;
                OrderGoods.ItemsSource = Lst;
            }
        }
    }
}
