using CourseCompanion.Commands;
using CourseCompanion.DataAccess;
using CourseCompanion.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseCompanion.ViewModels
{
    public class AddModuleViewModel
    {
        public ICommand AddModuleCommand { get; set; }
        public string Name_in { get; set; }
        public string Code_in { get; set; }
        public string Credits_in { get; set; }
        public string ClassHrsPerWeek_in { get; set; }
        public string SemesterWeeks_in { get; set; }
        public string Semester_in { get; set; }

        public string result;

        public UserID_Dependency shared { get; set; }

        public AddModuleViewModel()
        {         

            if (LogInViewModel.shared != null && LogInViewModel.shared.ID != 0)
            {
                shared = LogInViewModel.shared;
            }
            else if (RegisterViewModel.shared != null && RegisterViewModel.shared.ID != 0)
            {
                shared = RegisterViewModel.shared;
            }

            AddModuleCommand = new RelayCommand(AddModule, CanAddModule);
        }

        private async void AddModule(object obj)
        {
            AddModule();
        }

        private bool CanAddModule(object obj)
        {
            return true;
        }

        private async Task AddModule()
        {

            if (!string.IsNullOrWhiteSpace(Name_in) && !String.IsNullOrWhiteSpace(Code_in))
            {
                if (int.TryParse(Credits_in, out int Credits1))
                {
                    if (double.TryParse(ClassHrsPerWeek_in, out double ClassHrsPerWeek1))
                    {
                        if (int.TryParse(SemesterWeeks_in, out int SemesterWeeks1))
                        {
                            if (int.TryParse(Semester_in, out int Semester1))
                            {
                                double self_hrs = ((Credits1 * 10) / SemesterWeeks1) - ClassHrsPerWeek1;
                                    await Task.Run(async () =>
                                    {
                                        try
                                        {
                                            using (var context = new AppData())
                                            {
                                                var newModule = new module { name = Name_in, code = Code_in, credits = Credits1, weekly_hrs = ClassHrsPerWeek1, num_weeks = SemesterWeeks1, selfstudy_hrs = self_hrs, hrs_left = self_hrs, user_id = shared.ID, semester = Semester1 };
                                                await context.module.AddAsync(newModule);
                                                context.SaveChanges();
                                                
                                            
                                                Application.Current.Dispatcher.Invoke(() => {
                                                    Name_in = "";
                                                    Code_in = "";
                                                    Credits_in = "";
                                                    ClassHrsPerWeek_in = "";
                                                    SemesterWeeks_in = "";
                                                    Semester_in = "";
                                                    result = $" module: {newModule.name} has been created"; });

                                            }
                                        }
                                        catch (Exception) {
                                            Application.Current.Dispatcher.Invoke(() => { result = "module already exists"; });
                                        }
                                    });
                            }
                            else { Application.Current.Dispatcher.Invoke(() => { result = "semester should be a whole number e.g 1"; }); }

                        } else { Application.Current.Dispatcher.Invoke(() => { result = "semester weeks should be a whole number e.g 12"; }); }

                    }else { Application.Current.Dispatcher.Invoke(() => { result = "class hrs should be a number e.g 5 or 3.5 "; }); }
                }
                else { Application.Current.Dispatcher.Invoke(() => { result = "credits should be a whole number "; }); }
            }
            else { Application.Current.Dispatcher.Invoke(() => { result = "Enter all module details"; }); }
        }
    }
}
