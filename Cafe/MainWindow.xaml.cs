using Cafe.Data;
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
using Cafe.Models;
namespace Cafe
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
            login.LogginController = new LoginController<CafeDbContext>(Models.AccessLevel.Waiter);
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
                    break;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetActiveFrame(CafeFrame.Login);
            WaiterDG.Tag = "None";
        }

        private void login_LoginResult(bool arg1, int arg2)
        {
            if (!arg1) Close();
            _currentUserId = arg2;
            SetActiveFrame(CafeFrame.Main);
        }


        private void Refresh()
        {
            using (var dbContext = new CafeDbContext())
            {
                switch (WaiterDG.Tag)
                {
                    case "Tables":
                        var tables = dbContext.ClientTables.Where(c => c.WaiterId == _currentUserId).Select(k => new { Id = k.Id, Name = k.Name, Description = k.Description }).ToArray();
                        WaiterDG.ItemsSource = tables;
                        break;
                    case "DoneDishes":
                        var dishes = GetAllOrdersGoods();
                        var arr = dishes.Where(c => c.IsDone == true & c.IsServed != true).ToList();
                        var cdcd = arr.Where(first => dbContext.ClientTables.Where(third => dbContext.Orders.Where(k => k.Id == first.OrdersId).Single().TableId == third.Id).Single().WaiterId == _currentUserId).ToList();

                        WaiterDG.ItemsSource = cdcd.Select(c => new DG_Good_2 { OrderId = c.OrdersId, GoodId = c.GoodId, Good = dbContext.Goods.Where(k => k.Id == c.GoodId).Select(f => f.Name).Single(), Amount = c.Amount, Table = dbContext.ClientTables.Where(third => dbContext.Orders.Where(k => k.Id == c.OrdersId).Single().TableId == third.Id).Single().Name });

                        break;
                    case "NotPaidOrders":
                        var arrr = dbContext.Orders.Include(k => k.OrdersGoods).ThenInclude(f => f.Good).Where(c => !c.IsPaid).Include(d => d.Table).Where(q=>q.Table.WaiterId==_currentUserId).ToList();
                        WaiterDG.ItemsSource = arrr.Select(c => new DG_Order_3 { Id = c.Id, OrderTime = c.OrderTime, Sum = c.Sum, TableName = c.Table.Name });

                        break;



                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            WaiterDG.Tag = "Tables";
            Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddOrderWaiter dialog = new AddOrderWaiter();
            dialog.CurrentUserId = _currentUserId;
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                using (var dbContext = new CafeDbContext())
                {
                    dbContext.Orders.Add(new Orders() { TableId = dialog.Table.Id, OrderTime = dialog.datetime, OrdersGoods = new List<OrdersGoods>(dialog.Goods.Select(c => new OrdersGoods { GoodId = c.Good.Id, Amount = c.Amount })) });
                    dbContext.SaveChanges();

                }
                Refresh();
            }
        }
        private List<OrdersGoods> GetAllOrdersGoods()
        {
            List<OrdersGoods> rs;
            using (var dbContext = new CafeDbContext())
            {
                var goods = dbContext.Orders.Include(k => k.OrdersGoods).Where(c => !c.IsPaid).Select(d => d.OrdersGoods).ToList();
                rs = goods.SelectMany(i => i).Distinct().ToList();
            }
            return rs;
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            WaiterDG.Tag = "DoneDishes";

            Refresh();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (WaiterDG.Tag == "NotPaidOrders" & WaiterDG.SelectedItem != null)
            {
                var selected = (DG_Good)WaiterDG.SelectedItem;
                var edit = new EditOrder();
                using (var dbContext = new CafeDbContext())
                {
                    edit.Order = dbContext.Orders.Where(c => c.Id == selected.OrderId).Single();
                    edit.Table = dbContext.Orders.Where(c => c.Id == edit.Order.Id).Select(k => k.Table).Single();
                }
                edit.ShowDialog();
                if (edit.DialogResult == true)
                {
                    using (var dbContext = new CafeDbContext())
                    {
                        var item = dbContext.Orders.Include(f => f.OrdersGoods).Where(c => c.Id == edit.Order.Id).Single();
                        edit.Goods.ForEach(it =>
                        {
                            //item.OrdersGoods.Add(c=>new OrdersGoods() { Amount=it.Amount,GoodId=it.Good.Id,})


                        });

                    }

                }

            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WaiterDG.Tag = "NotPaidOrders";
            Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Check-out

            if (WaiterDG.Tag == "NotPaidOrders" & WaiterDG.SelectedItem != null)
                using (var dbContext = new CafeDbContext())
                {
                    var item = (DG_Order_3)WaiterDG.SelectedItem;
                    dbContext.Orders.Where(c => c.Id == item.Id).Single().IsPaid = true;
                    dbContext.SaveChanges();
                    Refresh();
                }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new CafeDbContext())
            {
                if (WaiterDG.Tag == "DoneDishes" & WaiterDG.SelectedItem != null)
                {
                    var item = (DG_Good_2)WaiterDG.SelectedItem;
                    var Order = dbContext.Orders.Include(d => d.OrdersGoods).Where(c => c.Id == item.OrderId).Single();
                    var obj = Order.OrdersGoods.Where(c => c.GoodId == item.GoodId).Single();
                    obj.IsServed = true;
                    dbContext.SaveChanges();
                    Refresh();

                }
            }
        }
    }






    class DG_Good
    {
        public int OrderId { get; set; }
        public int GoodId { get; set; }
        public string Good { get; set; }
        public int Amount { get; set; }
    }
    class DG_Good_2
    {
        public int OrderId { get; set; }
        public int GoodId { get; set; }
        public string Table { get; set; }
        public string Good { get; set; }
        public int Amount { get; set; }
    }
    class DG_Order_3
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime OrderTime { get; set; }

        public int Sum { get; set; }

    }

}
