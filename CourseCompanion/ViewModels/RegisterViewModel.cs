using CourseCompanion.Commands;
using CourseCompanion.DataAccess;
using CourseCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseCompanion.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool registrationSuccess;
        public bool RegistrationSuccess
        {
            get { return registrationSuccess; }
            set
            {
                if (value != registrationSuccess)
                {
                    registrationSuccess = value;
                    OnPropertyChanged(nameof(RegistrationSuccess));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Username_in { get; set; }
        public string Password_in { get; set; }
        public string result { get; set; }

        public static UserID_Dependency shared = new UserID_Dependency();   

        public ICommand registerUser { get; set; }


        public RegisterViewModel()
        {
            registerUser = new RelayCommand(Register, CanRegister);
        }

        private async void Register(object obj)
        {

            await Register();
        }

        private bool CanRegister(object obj)
        {
            return true;
        }

        private async Task Register()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Username_in) || !string.IsNullOrEmpty(Password_in))
                {

                    try
                    { 
                       await Task.Run(async () => { 
                        using (var context = new AppData())
                        {
                            try
                            {
                                var existingUser = await context.user.FirstOrDefaultAsync(x => x.username == Username_in);

                                if (existingUser != null)
                                {
                                    Application.Current.Dispatcher.Invoke(() => { result = "That username has been taken , use another!";}); 
                                }
                                else
                                {
                                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password_in.Trim());
                                    var currentUser = new user { username = Username_in, password = hashedPassword };
                                    await context.user.AddAsync(currentUser);
                                    await context.SaveChangesAsync();
                                 
                                    int userId = context.user
                                        .Where(u => u.username == Username_in)
                                        .Select(u => u.user_id)
                                        .FirstOrDefault();
                                    shared.ID = userId;

                                       Application.Current.Dispatcher.Invoke( () =>
                                       {
                                        result = $"{currentUser.username} your account has been created";
                                        RegistrationSuccess = true;
                                       });
                                       
                                }
                            }catch (Exception) { result = "connection error , try again "; }finally { await context.Database.CloseConnectionAsync(); await context.DisposeAsync(); }
                          
                         } 
                        });
                    }
                    catch (Exception ex)
                    {
                        Application.Current.Dispatcher.Invoke(() =>{ result = "registration failed:" + ex.Message; });
                    }
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() => { result = "Please fill in all fields ,to proceed"; }); 
                }
            }catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() => { result = "registration failed:" + ex.Message; });
                
            }
        }

    }
}
