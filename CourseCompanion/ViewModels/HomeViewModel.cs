using CourseCompanion.Commands;
using CourseCompanion.DataAccess;
using CourseCompanion.Models;
using CourseCompanion.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows;

namespace CourseCompanion.ViewModels
{
    public class HomeViewModel
    {
        public ObservableCollection<module> ModulesInfo;
        public string searchName { get; set; }

        public string hrsDone { get; set; }

        public DateTime start_date { get; set; }

        public ICommand showAddModuleWindow { get; set; }

        public ICommand AddHrsCommand { get; set; }

        public UserID_Dependency shared;
       

        public string result;

        public HomeViewModel()
        {
            showUserModules();       
            showAddModuleWindow = new RelayCommand(ShowWindow, CanShowWindow);
            AddHrsCommand = new RelayCommand(AddHrsDone, CanAddHrsDone);

            if (LogInViewModel.shared != null && LogInViewModel.shared.ID != 0)
            {
                shared = LogInViewModel.shared;
            }
            else if (RegisterViewModel.shared != null && RegisterViewModel.shared.ID != 0)
            {
                shared = RegisterViewModel.shared;
            }


        }


        private bool CanAddHrsDone(object obj)
        {
            return true;
        }

        private async void AddHrsDone(object obj)
        {

            if (!string.IsNullOrEmpty(searchName) && double.TryParse(hrsDone, out double hrsDone1))
            {
                await Task.Run( async() =>
                {
                    try
                    {

                        using (var context = new AppData())
                        {
                            var change = context.module
                            .Where(module => module.name == searchName && module.user_id == shared.ID)
                            .SingleOrDefault();

                            if (change != null)
                            {
                                change.hrs_left = change.hrs_left - hrsDone1;
                                await context.SaveChangesAsync();                              
                                await context.Database.CloseConnectionAsync();
                                showUserModules();

                                Application.Current.Dispatcher.Invoke(() =>{
                                    showUserModules();
                                    result = $"{change.name} has been updated";});
                            }
                            else
                            {
                                Application.Current.Dispatcher.Invoke(() =>{ result = $"Module {searchName} doesn't exist";});

                            }
                        }

                    }
                    catch (Exception)
                    {
                     
                        Application.Current.Dispatcher.Invoke(() => {  result = "module could not be altered";  });
                    }

                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => { result = "Enter fields to proceed"; });                
            }

        }

        // open window where users can add a new module
        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
            AddModule addModuleWindow = new AddModule();
            addModuleWindow.Show();
        }

        private async void showUserModules()
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var context = new AppData())
                    {
                        try
                        {
                            if (context != null && shared.ID > 0)
                            {

                                ModulesInfo = new ObservableCollection<module>(context.module
                                    .Where(u => u.user_id == shared.ID)
                                    .ToList()
                               );                            
                            }
                            else
                            {
                                Application.Current.Dispatcher.Invoke(() =>{ result = "Could not get module data";}); 
                               
                            }
                        }
                        catch (NullReferenceException)
                        {
                            Application.Current.Dispatcher.Invoke(() =>{ result = "Could not get module data";});
                           
                        }
                        catch (Exception)
                        {

                            Application.Current.Dispatcher.Invoke(() => { result = "Could not get module data"; });                          
                        }
                        finally
                        {
                            context.Database.CloseConnectionAsync();
                            context.DisposeAsync();
                        }
                    }
                }
                catch (ArgumentNullException)
                {
                    Application.Current.Dispatcher.Invoke(() =>{ result = "Could not get module data"; });
                }
                catch (NullReferenceException)
                {
                    Application.Current.Dispatcher.Invoke(() => { result = "Could not get module data"; });                  
                }
            });

        }
    }
}
