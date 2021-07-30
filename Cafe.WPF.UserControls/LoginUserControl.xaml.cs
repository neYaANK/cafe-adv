using Cafe.Models;
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

namespace Cafe.UserControls
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        /// <summary>
        /// return true/false for user Id:int and Password:string
        /// </summary>
        public event Action<bool,int> LoginResult;

        public IUserLoginController _logginController;
        public IUserLoginController LogginController {
            get
            {
                if (_logginController == null)
                    throw new Exception("Не визначено LogginController");
                return _logginController;
             }
            set=>_logginController=value;
        }
        public int UserId { get; set; }
        private int passwordCounter { get; set; }
        private string ErrorMessage { get; set; }
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            waitersCB.ItemsSource = _logginController?.Users;
            if ((_logginController?.Users.Length ?? 0) > 0)
                waitersCB.SelectedValue = _logginController.Users[0].Id;
        }

        private void passwordPB_GotFocus(object sender, RoutedEventArgs e)
        {
            errorBlock.Visibility = Visibility.Collapsed;
        }
        private void passwordPB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CheckPassword();
        }
        private void CheckPassword()
        {

            if (LogginController.TryLogin(UserId, passwordPB.Password))
            {
                LoginResult?.Invoke(true,UserId);
            }
            else
            {
                errorBlock.Text = $"Пароль введено неправильно, спробуйте ще {LogginController.LoginCounter} раз.";
                errorBlock.Visibility = Visibility.Visible;
                if (LogginController.LoginCounter <=0)
                    LoginResult?.Invoke(true, UserId);
            }

        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            CheckPassword();
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            LoginResult?.Invoke(false, UserId);
        }


    }
}
