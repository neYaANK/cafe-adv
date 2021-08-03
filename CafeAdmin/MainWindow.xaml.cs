using Cafe.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cafe.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _currentUserId;
        public MainWindow()
        {
            InitializeComponent();
            login.LogginController = new LoginController<CafeAdminDbContext>(AccessLevel.Admin);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetActiveFrame(CafeFrame.Login);
        }
        private void Refresh()
        {
            using (var dbContext = new CafeAdminDbContext())
            {
                switch (AdminDG.Tag)
                {
                    case "Tables":
                        AdminDG.ItemsSource = dbContext.ClientTables.Include(k => k.Waiter).Select(c => new DataGridCTable { Id = c.Id, TableName = c.Name, Description = c.Description, Waiter = c.Waiter.Name }).ToArray();
                        break;


                    case "Orders":
                        AdminDG.ItemsSource = dbContext.Orders
                    .Select(s => new DataGridOrder
                    {
                        Id = s.Id,
                        TableName = dbContext.ClientTables.Where(tb => tb.Id == s.TableId).Select(c => c.Name).Single(),
                        Date = s.OrderTime.ToLocalTime(),
                        IsPaid = s.IsPaid
                    }).ToArray();

                        break;

                    case "Goods":
                        AdminDG.ItemsSource = dbContext.Goods.Select(c => new DataGridGood { Id = c.Id, Name = c.Name, Price = c.Price }).ToArray();
                        break;

                    case "Users":
                        AdminDG.ItemsSource = dbContext.UserAccesLevels.Select(w => new DataGridUser{ Id = w.Id, Name = w.User.Name, Level = w.AccessLevel }).ToArray();
                        break;

                    default:

                        break;


                }
            }
        }

        private void SetActiveFrame(CafeFrame cafeFrame)
        {
            login.Visibility = Visibility.Collapsed;
            AdminPanel.Visibility = Visibility.Collapsed;
            switch (cafeFrame)
            {
                case CafeFrame.Login:
                    login.Visibility = Visibility.Visible;
                    break;
                case CafeFrame.Main:
                    AdminPanel.Visibility = Visibility.Visible;
                    Output.Content = "Welcome!";
                    break;

            }
        }
        private List<OrdersGoods> GetAllOrdersGoods()
        {
            List<OrdersGoods> rs;
            using (var dbContext = new CafeAdminDbContext())
            {
                var goods = dbContext.Orders.Include(k => k.OrdersGoods).Select(d => d.OrdersGoods).ToList();
                rs = goods.SelectMany(i => i).Distinct().ToList();
            }
            return rs;
        }
        private void login_LoginResult(bool arg1, int arg2)
        {
            if (!arg1) Close();
            _currentUserId = arg2;
            SetActiveFrame(CafeFrame.Main);
        }

        #region Button_Click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminDG.Tag = "Users";
            Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminDG.Tag = "Tables";
            Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AdminDG.Tag = "Orders";
            Refresh();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AdminDG.Tag = "Goods";
            Refresh();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new CafeAdminDbContext())
            {
                switch (AdminDG.Tag)
                {
                    case "Tables":
                        var dialog = new AddTable();
                        dialog.ShowDialog();
                        if (dialog.DialogResult == true)
                        {
                            dbContext.ClientTables.Add(new ClientTable() { Name = dialog.TName, Description = dialog.Description, WaiterId = dialog.WaiterId });
                            dbContext.SaveChanges();
                            Output.Content = "Added to tables successfuly";
                        }
                        break;


                    case "Orders":
                        var order = new AddOrder();
                        order.ShowDialog();
                        if (order.DialogResult == true)
                        {
                            var ord = new Orders();
                            ord.TableId = order.Table.Id;
                            ord.OrderTime = order.datetime;
                            ord.Goods = new List<Goods>();
                            ord.OrdersGoods = new List<OrdersGoods>(order.Goods.Select(c => new OrdersGoods() { Amount = c.Amount, GoodId = c.Good.Id }));

                            dbContext.Add(ord);
                            //ord.Goods.AddRange(order.Goods.Select(s => s.Good).ToList());

                            dbContext.SaveChanges();
                            Output.Content = "Added to orders successfuly";
                        }

                        break;

                    case "Goods":
                        var goods = new AddGood();
                        goods.ShowDialog();
                        if (goods.DialogResult == true)
                        {
                            dbContext.Goods.Add(new Goods() { IsFood = goods.IsFood, Name = goods.GName, Price = goods.Price });
                            dbContext.SaveChanges();
                            Output.Content = "Added to goods successfuly";

                        }
                        break;

                    case "Users":
                        var addusr = new AddUser();
                        addusr.ShowDialog();
                        if (addusr.DialogResult == true)
                        {
                            var addedUser = new User() { Name = addusr.UName, Password = addusr.Password };
                            dbContext.Users.Add(addedUser);
                            dbContext.SaveChanges();

                            dbContext.UserAccesLevels.Add(new UserAccesLevel() { AccessLevel = addusr.Level, UserId = addedUser.Id });
                            dbContext.SaveChanges();
                            Output.Content = "Added to users successfuly";
                        }
                        break;

                    default:
                        Output.Content = "You can't add to this table";
                        break;


                }
                Refresh();

            }
        }


        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if ((string)AdminDG.Tag == "Orders" & AdminDG.SelectedItem != null)
            {
                AdminDG.Tag = "None";
                using (var dbContext = new CafeAdminDbContext())
                {
                    var ordr = (DataGridOrder)AdminDG.SelectedItem;
                    var am = GetAllOrdersGoods();
                    var order = dbContext.Orders.Include(k => k.Goods).Where(c => c.Id == ordr.Id).Single();
                    AdminDG.ItemsSource = order.Goods.Select(c => new { Id = c.Id, Name = c.Name, IsFood = c.IsFood, Amount = am.Where(g => g.OrdersId == ordr.Id & g.GoodId == c.Id).Single().Amount });
                    Output.Content = "Goods for order #" + order.Id;
                }


            }
            else { Output.Content = "You didn't select row in Orders table."; }
        }










        #endregion

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (AdminDG.SelectedItem != null & AdminDG.Tag != "None" & AdminDG.Tag!= "Orders")
            {
                using (var dbContext = new CafeAdminDbContext())
                {
                    switch (AdminDG.Tag)
                    {
                        case "Tables":
                            var dialog = new EditTable();
                            int dialogId = ((DataGridCTable)AdminDG.SelectedItem).Id;
                            dialog.TName= dbContext.ClientTables.Where(c => c.Id == dialogId).Select(c => c.Name).Single();
                            dialog.Description= dbContext.ClientTables.Where(c => c.Id == dialogId).Select(c => c.Description).Single();
                            dialog.WaiterId= dbContext.ClientTables.Where(c => c.Id == dialogId).Select(c => c.WaiterId).Single();
                            

                            dialog.ShowDialog();
                            if (dialog.DialogResult == true)
                            {
                                var table = dbContext.ClientTables.Where(c => c.Id == dialogId).Single();
                                table.Name = dialog.TName;
                                table.Description = dialog.Description;
                                table.WaiterId = dialog.WaiterId;
                                dbContext.SaveChanges();
                                Output.Content = "Edited tables successfuly";
                            }
                            break;
                        case "Goods":
                            var goods = new EditGood();
                            int goodId = ((DataGridGood)AdminDG.SelectedItem).Id;
                            goods.GName = dbContext.Goods.Where(c => c.Id == goodId).Select(c => c.Name).Single();
                            goods.Price= dbContext.Goods.Where(c => c.Id == goodId).Select(c => c.Price).Single();
                            goods.IsFood= dbContext.Goods.Where(c => c.Id == goodId).Select(c => c.IsFood).Single();

                            goods.ShowDialog();
                            if (goods.DialogResult == true)
                            {
                                var good = dbContext.Goods.Where(c => c.Id == goodId).Single();
                                good.Name = goods.GName;
                                good.Price = goods.Price;
                                good.IsFood = goods.IsFood;
                                dbContext.SaveChanges();
                                Output.Content = "Edited goods successfuly";
                            }
                            break;

                        case "Users":
                            var addusr = new EditUser();
                            int usrID = ((DataGridUser)AdminDG.SelectedItem).Id;

                            addusr.UName = dbContext.Users.Where(c => c.Id == usrID).Select(c => c.Name).Single();
                            addusr.Password = dbContext.Users.Where(c => c.Id == usrID).Select(c => c.Password).Single();
                            addusr.Level = dbContext.UserAccesLevels.Where(c => c.UserId == usrID).Select(c => c.AccessLevel).Single();

                            addusr.ShowDialog();
                            if (addusr.DialogResult == true)
                            {
                                var user=dbContext.Users.Where(c => c.Id == usrID).Single();
                                user.Name = addusr.UName;
                                user.Password = addusr.Password;
                                dbContext.SaveChanges();

                                dbContext.UserAccesLevels.Where(c => c.UserId == user.Id).Single().AccessLevel = addusr.Level;
                                dbContext.SaveChanges();
                                Output.Content = "Edited users successfuly";
                            }
                            break;

                        default:
                            Output.Content = "You can't edit at this table";
                            break;


                    }
                }
                Refresh();

            }
        }
    }

    class DataGridOrder
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public DateTime Date { get; set; }

        public bool IsPaid { get; set; }
    }

    class DataGridUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccessLevel Level { get; set; }
    }
    class DataGridGood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    class DataGridCTable
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string Description { get; set; }
        public string Waiter { get; set; }
    }
}
