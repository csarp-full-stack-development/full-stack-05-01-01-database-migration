using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models;
using Kreta.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.Administration
{
    public partial class TypeOfEducationViewModel : BaseViewModel
    {
        private readonly ITypeOfEducationService? _typeOfEducationService;
        private readonly ISchoolClassService? _schoolClassService;

        [ObservableProperty]
        private string _title = "Szakmák";

        [ObservableProperty]
        private ObservableCollection<TypeOfEducation>? _typeOfEducations = new ObservableCollection<TypeOfEducation>();

        [ObservableProperty]
        private ObservableCollection<SchoolClass>? schoolClasses = new();

        [ObservableProperty]
        private SchoolClass? _selectedSchoolClass = new();

        [ObservableProperty]
        private TypeOfEducation? _selectedTypeOfEducation = new();

        [ObservableProperty]
        private ObservableCollection<SchoolClass> _schoolClassesWithoutTypeOfEducation = new();
        [ObservableProperty]
        private SchoolClass _selectedSchoolClassesWithoutTypeOfEducation = new();

        public TypeOfEducationViewModel()
        {
        }
        public TypeOfEducationViewModel(
            ITypeOfEducationService? typeOfEducationService,
            ISchoolClassService? schoolClassService)
        {
            _typeOfEducationService = typeOfEducationService;
            _schoolClassService = schoolClassService;
        }

        public async override Task InitializeAsync()
        {
            await UpdateView();
            await base.InitializeAsync();
        }

        [RelayCommand]
        private void DoNewTypeOfEducation()
        {
            SelectedTypeOfEducation = new TypeOfEducation();
        }

        [RelayCommand]
        private async Task DoDelete(TypeOfEducation typeOfEducation)
        {
            if (_typeOfEducationService is not null && typeOfEducation.HasId)
            {
                ControllerResponse response = await _typeOfEducationService.DeleteAsync(typeOfEducation.Id);
                if (response.IsSuccess)
                    await UpdateView();
            }
        }

        [RelayCommand]
        private async Task DoSave(TypeOfEducation toSave)
        {
            if (_typeOfEducationService is not null)
            {
                ControllerResponse response;
                if (toSave.HasId)
                    response = await _typeOfEducationService.UpdateAsync(toSave);
                else
                    response = await _typeOfEducationService.InsertAsync(toSave);
                if (response.IsSuccess)
                    await UpdateView();
            }
        }

        [RelayCommand]
        private async Task GetByTypeOfEducationId(Guid typeOfEducationId)
        {
            if (_schoolClassService is not null)
            {
                List<SchoolClass> schoolClasses = await _schoolClassService.GetByTypeOfEducationIdAsync(typeOfEducationId);
                SchoolClasses = new ObservableCollection<SchoolClass>(schoolClasses);
                await UpdateView();
            }
        }

        [RelayCommand]
        private async Task MoveSchoolClassToWithoutTypeOfEducation()
        {
            if (_schoolClassService is not null && SelectedSchoolClass is not null)
            {
                SelectedSchoolClass.TypeOfEducationId = null;
                ControllerResponse result = await _schoolClassService.UpdateAsync(SelectedSchoolClass);
                if (result.IsSuccess)
                    await UpdateView();
            }
        }

        [RelayCommand]
        private async Task MoveToWithTypeOfEducation()
        {
            if (_schoolClassService is not null && SelectedTypeOfEducation is not null)
            {
                SelectedSchoolClassesWithoutTypeOfEducation.TypeOfEducationId = SelectedTypeOfEducation.Id;
                ControllerResponse result = await _schoolClassService.UpdateAsync(SelectedSchoolClassesWithoutTypeOfEducation);
                if (result.IsSuccess)
                    await UpdateView();
            }
        }

        private async Task UpdateView()
        {
            if (_typeOfEducationService is not null && SelectedTypeOfEducation is not null)
            {
                TypeOfEducation? storedPreviusSelectedTypeOfEducation = SelectedTypeOfEducation is not null ? SelectedTypeOfEducation : new();
                SelectedTypeOfEducation = null;
                List<TypeOfEducation> typeOfEducations = await _typeOfEducationService.SelectAllAsync();
                TypeOfEducations = new ObservableCollection<TypeOfEducation>(typeOfEducations);
                SelectedTypeOfEducation = typeOfEducations.FirstOrDefault(typeOfEducation => typeOfEducation.Id == storedPreviusSelectedTypeOfEducation.Id);
                SelectedTypeOfEducation = SelectedTypeOfEducation is not null ? SelectedTypeOfEducation : new();
            }
            if (_schoolClassService is not null)
            {

                List<SchoolClass> schoolClasses = await _schoolClassService.GetSchoolClassesWithoutTypeOfEducation();
                SchoolClassesWithoutTypeOfEducation = new ObservableCollection<SchoolClass>(schoolClasses);
            }
        }
    }
}
