using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.SchoolClasses
{
    public partial class SchoolClassesViewModel : BaseViewModel
    {
        private readonly SchoolClassesManagmentViewModel _schoolClassesManagmentViewModel;
        private readonly SchoolClassesSubjectsViewModel _schoolClassesSubjectsViewModel;
        private readonly SchoolClassesTeachersViewModel _schoolClassesTeachersViewModel;
        private readonly SchoolClassesTimeTableViewModel _schoolClassesTimeTableViewModel;
        private readonly SchoolClessesStudentsViewModel _schoolClessesStudentsViewModel;

        public SchoolClassesViewModel()
        {
            _currentSchoolClassChildView = new SchoolClassesManagmentViewModel();
            _schoolClassesManagmentViewModel=new SchoolClassesManagmentViewModel();
            _schoolClassesSubjectsViewModel=new SchoolClassesSubjectsViewModel();
            _schoolClassesTeachersViewModel=new SchoolClassesTeachersViewModel();
            _schoolClassesTimeTableViewModel=new SchoolClassesTimeTableViewModel();
            _schoolClessesStudentsViewModel=new SchoolClessesStudentsViewModel();
        }

        public SchoolClassesViewModel(SchoolClassesManagmentViewModel schoolClassesManagmentViewModel, SchoolClassesSubjectsViewModel schoolClassesSubjectsViewModel, SchoolClassesTeachersViewModel schoolClassesTeachersViewModel, SchoolClassesTimeTableViewModel schoolClassesTimeTableViewModel, SchoolClessesStudentsViewModel schoolClessesStudentsViewModel)
        {
            _schoolClassesManagmentViewModel = schoolClassesManagmentViewModel;
            _schoolClassesSubjectsViewModel = schoolClassesSubjectsViewModel;
            _schoolClassesTeachersViewModel = schoolClassesTeachersViewModel;
            _schoolClassesTimeTableViewModel = schoolClassesTimeTableViewModel;
            _schoolClessesStudentsViewModel = schoolClessesStudentsViewModel;

            _currentSchoolClassChildView = _schoolClassesManagmentViewModel;
        }

        [ObservableProperty]
        private BaseViewModel _currentSchoolClassChildView = new SchoolClassesManagmentViewModel();

        [RelayCommand]
        public async Task ShowSchoolClassManagmentView()
        {
            await _schoolClassesManagmentViewModel.InitializeAsync();
            CurrentSchoolClassChildView = _schoolClassesManagmentViewModel;
        }

        [RelayCommand]
        public void ShowSchoolClassSubjectsView()
        {
            CurrentSchoolClassChildView= _schoolClassesSubjectsViewModel;
        }

        [RelayCommand]
        public void ShowSchoolClassTeachersView()
        {
            CurrentSchoolClassChildView = _schoolClassesTeachersViewModel;
        }

        [RelayCommand]
        public void ShowSchoolClassTimeTabeView()
        {
            CurrentSchoolClassChildView = _schoolClassesTimeTableViewModel;
        }

        [RelayCommand]
        public void ShowSchholClassStudents()
        {
            CurrentSchoolClassChildView = _schoolClessesStudentsViewModel;
        }
    }
}
