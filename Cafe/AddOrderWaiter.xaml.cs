using Cafe.Data;
using Cafe.Models;
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

namespace Cafe
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWaiter.xaml
    /// </summary>
    public partial class AddOrderWaiter : Window
    {
        public int CurrentUserId { get; set; }
        public List<GoodInOrder> Goods { get; set; } = new List<GoodInOrder>();
        public ClientTable Table { get; set; }
        public DateTime datetime { get; set; }

        public ObservableCollection<GoodInOrder> Lst { get; set; } = new ObservableCollection<GoodInOrder>();

        public AddOrderWaiter()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            goodAmount.Content = 0;
            using (var dbContext = new CafeDbContext())
            {
                if (dbContext.Goods.ToArray().Length == 0 || dbContext.UserAccesLevels.Where(w => w.AccessLevel != AccessLevel.Admin).ToArray().Length == 0 || dbContext.ClientTables.ToArray().Length == 0)
                {
                }
                var good = dbContext.Goods.Select(a => a).ToArray();

                var tables = dbContext.ClientTables.Where(c=>c.WaiterId==CurrentUserId).ToArray();
                _table.ItemsSource = tables;
                goodName.ItemsSource = good;
                goodAmount.Content = 0;
                OrderGoods.ItemsSource = Lst;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            Table = (ClientTable)_table.SelectedItem;
            datetime = DateTime.Now;
            Goods = Lst.ToList();
            DialogResult = true;
            Close();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var gd = new GoodInOrder() { Good = (Goods)goodName.SelectedItem, Amount = (int)goodAmount.Content };
            if (gd.Amount > 0)
            {
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
            else if (((int)btn.Content) > 0) btn.Content = (int)btn.Content - 1;
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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender,e);
        }
    }
}
