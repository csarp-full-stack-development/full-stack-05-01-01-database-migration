using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;

namespace Kreta.Desktop.ViewModels.SchoolGrades
{
    public partial class SchoolGradesViewModel : BaseViewModel
    {
        private ActualLessonGradesViewModel _actualLessonGradesViewModel;
        private TeachedClassesGradesViewModel _teachedClassesGradesViewModel;
        private HalfOfYearGradesViewModel _halfOfYearGradesViewModel;
        private EndOfYearGradesViewModel _endOfYearGradesViewModel;

        public SchoolGradesViewModel()
        {
            _currentSchoolOsztalyzatokChildView = new ActualLessonGradesViewModel();
            _actualLessonGradesViewModel = new ActualLessonGradesViewModel();
            _teachedClassesGradesViewModel = new TeachedClassesGradesViewModel();
            _halfOfYearGradesViewModel = new HalfOfYearGradesViewModel();
            _endOfYearGradesViewModel = new EndOfYearGradesViewModel();
        }

        public SchoolGradesViewModel(ActualLessonGradesViewModel actualLessonGradesViewModel, TeachedClassesGradesViewModel teachedClassesGradesViewModel, HalfOfYearGradesViewModel halfOfYearGradesViewModel, EndOfYearGradesViewModel endOfYearGradesViewModel)
        {
            _actualLessonGradesViewModel = actualLessonGradesViewModel;
            _teachedClassesGradesViewModel = teachedClassesGradesViewModel;
            _halfOfYearGradesViewModel = halfOfYearGradesViewModel;
            _endOfYearGradesViewModel = endOfYearGradesViewModel;

            _currentSchoolOsztalyzatokChildView = _actualLessonGradesViewModel;
        }

        [ObservableProperty]
        private BaseViewModel _currentSchoolOsztalyzatokChildView;

        [RelayCommand]
        public void ShowAktualisOraView()
        {
            CurrentSchoolOsztalyzatokChildView = _actualLessonGradesViewModel;
        }

        [RelayCommand]
        public void ShowTanitottOsztalyokView()
        {
            CurrentSchoolOsztalyzatokChildView = _teachedClassesGradesViewModel;
        }

        [RelayCommand]
        public void ShowFelevZarasView()
        {
            CurrentSchoolOsztalyzatokChildView = _halfOfYearGradesViewModel;
        }

        [RelayCommand]
        public void ShowEvVegeZarasView()
        {
            CurrentSchoolOsztalyzatokChildView = _endOfYearGradesViewModel;
        }
    }
}
