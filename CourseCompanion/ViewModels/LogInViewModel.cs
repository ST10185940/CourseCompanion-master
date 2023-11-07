using CourseCompanion.Commands;
using CourseCompanion.DataAccess;
using CourseCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseCompanion.ViewModels
{
    public class LogInViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool loginSuccess;

        public bool LoginSuccess
        {
            get { return loginSuccess; }
            set
            {
                if (value != loginSuccess)
                {
                    loginSuccess = value;
                    OnPropertyChanged(nameof(LoginSuccess));
                }
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string? Username_in { get; set; }
        public string? Password_in { get; set; }
        public string? result { get; set; }
        public ICommand loginUser { get; set; }

        public static UserID_Dependency shared = new UserID_Dependency();

        public LogInViewModel()
        {
            loginUser = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin(object obj)
        {
            return true;
        }

        private async void Login(object obj)
        {
            if (!string.IsNullOrEmpty(Username_in) && !string.IsNullOrEmpty(Password_in))
            {
                await Login();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    result = "Please enter all credentials";
                });
            }
        }

        private async Task Login()
        {
            try
            {                                                                     
                using (var context = new AppData())
                {
                    try
                    {
                        var user =  await (context.user.FirstOrDefaultAsync(u => u.username == Username_in));
                        if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(Password_in.Trim(), user.password))
                        {

                            shared.ID = user.user_id;                                                        

                             Application.Current.Dispatcher.Invoke(() =>
                            {
                                result = "Login Successful";
                                loginSuccess = true;
                            });
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                result = "Log-In failed.  please re-enter credentials \n or register if you do not have an account; ";
                            });
     
                        }
                    }
                    catch (Exception e)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            result = "login failed " + e.Message;
                        }); 
                            
                    }
                    finally
                    {
                        context.Database.CloseConnection();
                       await context.DisposeAsync();
                    }
                }                       
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    result = "login failed:" + ex.Message;

                });
            }
        }
    }
}
