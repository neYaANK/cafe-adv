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

namespace Cafe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _currentUserId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            login.LogginController = new LoginController<CafeDbContext>(Models.AccessLevel.Waiter);
        }
        
        private void SetActiveFrame(CafeFrame cafeFrame)
        {
            login.Visibility = Visibility.Collapsed;
            dg.Visibility = Visibility.Collapsed;
            switch (cafeFrame)
            {
                case CafeFrame.Login:
                    login.Visibility = Visibility.Visible;
                    break;
                case CafeFrame.Main:
                    dg.Visibility = Visibility.Visible;
                    using (var dbContext = new CafeDbContext())
                    {
                        //var badWaiter = dbContext.Waiter.Single(s => s.Id == 1);
                        //badWaiter.Name = "Semen Semenov";
                        //dbContext.SaveChanges();

                        var waiters = dbContext.UserAccesLevels
                            .Where(w => w.AccessLevel == Models.AccessLevel.Waiter)
                            .Include(i => i.User)
                            .Select(w => new { Id = w.Id, Name = w.User.Name })
                            .ToArray();

                        

                        dg.ItemsSource = waiters;

                    }
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
    }
}
