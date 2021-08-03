using Cafe.Data;
using Cafe.Models;
using CafeKitchen.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CafeKitchen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int _currentUserId { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            login.LogginController = new LoginController<CafeKitchenDbContext>(AccessLevel.Cook);
        }
        private void SetActiveFrame(CafeFrame cafeFrame)
        {
            login.Visibility = Visibility.Collapsed;
            WaiterPanel.Visibility = Visibility.Collapsed;
            switch (cafeFrame)
            {
                case CafeFrame.Login:
                    login.Visibility = Visibility.Visible;
                    break;
                case CafeFrame.Main:
                    WaiterPanel.Visibility = Visibility.Visible;
                    BarPanel.Tag = "None";
                    break;

            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetActiveFrame(CafeFrame.Login);
        }
        private void login_LoginResult(bool arg1, int arg2)
        {
            if (!arg1) Close();
            _currentUserId = arg2;
            SetActiveFrame(CafeFrame.Main);
        }
        private List<OrdersGoods> GetAllOrdersGoods()
        {
            List<OrdersGoods> rs;
            using (var dbContext = new CafeKitchenDbContext())
            {
                var goods = dbContext.Orders.Include(k => k.OrdersGoods).ThenInclude(f=>f.Good).Select(d => d.OrdersGoods).ToList();
                rs = goods.SelectMany(i => i).Distinct().Where(f=>f.Good.IsFood).ToList();
            }
            return rs;
        }

        private void Refresh()
        {
            using (var dbContext = new CafeKitchenDbContext())
            {
                switch (BarPanel.Tag)
                {
                    case "Orders":
                        var goods = GetAllOrdersGoods().Where(c => !c.IsDone).ToList();
                        var rs = goods.Where(c => c.CreatorId == null);
                        BarPanel.ItemsSource = rs.Select(a => new DG_Good
                        {
                            OrderId = a.OrdersId,
                            GoodId = a.GoodId,
                            Good = dbContext.Goods.Where(k => k.Id == a.GoodId).Select(f => f.Name).Single(),
                            Amount = a.Amount,
                            Tablename = dbContext.ClientTables
                            .Where(d => dbContext.Orders.Where(j => a.OrdersId == j.Id).Single().TableId == d.Id).Select(p => p.Name).Single(),
                            WaiterName = dbContext.ClientTables
                            .Where(d => dbContext.Orders.Where(j => a.OrdersId == j.Id).Single().TableId == d.Id).Include(t => t.Waiter).Select(v => v.Waiter.Name).Single()
                        });
                        break;
                    case "MyOrders":
                        goods = GetAllOrdersGoods().Where(c => !c.IsDone).ToList();
                        rs = goods.Where(c => c.CreatorId == _currentUserId);
                        BarPanel.ItemsSource = rs.Select(a => new DG_Good
                        {
                            OrderId = a.OrdersId,
                            GoodId = a.GoodId,
                            Good = dbContext.Goods.Where(k => k.Id == a.GoodId).Select(f => f.Name).Single(),
                            Amount = a.Amount,
                            Tablename = dbContext.ClientTables
                            .Where(d => dbContext.Orders.Where(j => a.OrdersId == j.Id).Single().TableId == d.Id).Select(p => p.Name).Single(),
                            WaiterName = dbContext.ClientTables
                            .Where(d => dbContext.Orders.Where(j => a.OrdersId == j.Id).Single().TableId == d.Id).Include(t => t.Waiter).Select(v => v.Waiter.Name).Single()
                        });
                        break;


                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                BarPanel.Tag = "Orders";
            Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (BarPanel.Tag == "Orders" & BarPanel.SelectedItem != null)
            {
                var item = (DG_Good)BarPanel.SelectedItem;

                using (var dbContext = new CafeKitchenDbContext())
                {
                    var order = dbContext.Orders.Include(w => w.OrdersGoods).Where(o => o.Id == item.OrderId).Single();
                    var ogitem = order.OrdersGoods.Where(c => c.GoodId == item.GoodId).Single();
                    ogitem.CreatorId = _currentUserId;
                    dbContext.SaveChanges();
                }
                Refresh();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BarPanel.Tag = "MyOrders";
            Refresh();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (BarPanel.Tag == "MyOrders" & BarPanel.SelectedItem != null)
            {
                var item = (DG_Good)BarPanel.SelectedItem;

                using (var dbContext = new CafeKitchenDbContext())
                {
                    var order = dbContext.Orders.Include(w => w.OrdersGoods).Where(o => o.Id == item.OrderId).Single();
                    var ogitem = order.OrdersGoods.Where(c => c.GoodId == item.GoodId).Single();
                    ogitem.IsDone = true;
                    dbContext.SaveChanges();
                }
                Refresh();
            }
        }
    }

    class DG_Good
    {
        public int OrderId { get; set; }
        public int GoodId { get; set; }
        public string Good { get; set; }
        public string Tablename { get; set; }
        public string WaiterName { get; set; }
        public int Amount { get; set; }
        
    }
}
