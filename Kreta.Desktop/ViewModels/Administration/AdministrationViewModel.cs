using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.Administration
{
    public partial class AdministrationViewModel : BaseViewModel
    {
        private EducationLevelViewModel _educationLevelViewModel=new ();
        private TypeOfEducationViewModel _typeOfEducationViewModel = new();

        [ObservableProperty]
        private BaseViewModel _currentAdministrationChildView;

        public AdministrationViewModel()
        {
            _currentAdministrationChildView = _educationLevelViewModel;
        }

        public AdministrationViewModel(EducationLevelViewModel educationLevelViewModel,
                                       TypeOfEducationViewModel typeOfEducationViewModel )
        {
            _educationLevelViewModel= educationLevelViewModel;
            _typeOfEducationViewModel = typeOfEducationViewModel;
            CurrentAdministrationChildView= _educationLevelViewModel;            
        }

        [RelayCommand]
        private async Task ShowEducationLevel()
        {
            await _educationLevelViewModel.InitializeAsync();
            CurrentAdministrationChildView = _educationLevelViewModel;
       }

        [RelayCommand]
        private async Task ShowTypeOfEducationViewModel()
        {
            await _typeOfEducationViewModel.InitializeAsync();
            CurrentAdministrationChildView = _typeOfEducationViewModel;
        }
    }
}
