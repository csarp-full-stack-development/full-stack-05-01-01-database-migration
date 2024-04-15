using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.Desktop.ViewModels.SchoolClasses;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.SchoolSubjects
{
    public partial class SchoolSubjectsViewModel : BaseViewModel
    {
        private readonly SubjectsManagmentViewModel _subjectsManagmentViewModel;
        private readonly SubjectsOfSchoollClassesViewModel _subjectsOfSchoollClassesModel;
        private readonly SubjectsOfTeachersViewModel _subjectsOfTeachersViewModel;

        [ObservableProperty]
        private BaseViewModel _currentSchoolClassChildView = new SubjectsManagmentViewModel();

        public SchoolSubjectsViewModel()
        {
            _subjectsManagmentViewModel = new SubjectsManagmentViewModel();
            _subjectsOfSchoollClassesModel= new SubjectsOfSchoollClassesViewModel();
            _subjectsOfTeachersViewModel = new SubjectsOfTeachersViewModel();
        }

        public SchoolSubjectsViewModel(SubjectsManagmentViewModel subjectsManagmentViewModel, SubjectsOfSchoollClassesViewModel subjectsOfSchoollClassesModel, SubjectsOfTeachersViewModel subjectsOfTeachersViewModel)
        {
            _subjectsManagmentViewModel = subjectsManagmentViewModel;
            _subjectsOfSchoollClassesModel = subjectsOfSchoollClassesModel;
            _subjectsOfTeachersViewModel = subjectsOfTeachersViewModel;
            CurrentSchoolClassChildView = _subjectsManagmentViewModel;
            
        }

        [RelayCommand]
        private async Task ShowSubjectsManagmentView()
        {
            await _subjectsManagmentViewModel.InitializeAsync();
            CurrentSchoolClassChildView = _subjectsManagmentViewModel;
        }

        [RelayCommand]
        private void ShowSubjectsOfSchoollClassesView()
        {
            CurrentSchoolClassChildView = _subjectsOfSchoollClassesModel;
        }

        [RelayCommand]
        private void ShowSubjectsOfTeachersView()
        {
            CurrentSchoolClassChildView = _subjectsOfTeachersViewModel;
        }
    }
}