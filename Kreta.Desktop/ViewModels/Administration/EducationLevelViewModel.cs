using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models;
using Kreta.Shared.Models.SchoolCitizens;
using Kreta.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.Administration
{
    public partial class EducationLevelViewModel : BaseViewModel
    {
        private readonly IEducationLavelService? _educationLevelService;
        private readonly IStudentService? _studentService;

        [ObservableProperty]
        private ObservableCollection<EducationLevel> _educationLevels = new();

        [ObservableProperty]
        private EducationLevel? _selectedEducationLevel = new();

        [ObservableProperty]
        private ObservableCollection<Student> _studentsWithEducationLevel = new();

        [ObservableProperty]
        private Student? _selectedStudentWithEducationLevel = new();

        [ObservableProperty]
        private ObservableCollection<Student> _studentsWithoutEducationLevel = new();

        [ObservableProperty]
        private Student? _selectedStudentWithoutEducationLevel = new();

        public EducationLevelViewModel()
        {
        }
        public EducationLevelViewModel(
            IEducationLavelService? educationLevelService,
            IStudentService? studentService)
        {
            _educationLevelService = educationLevelService;
            _studentService = studentService;
        }

        public string Title { get; set; } = "Tanulmányi szint kezelése";

        public override async Task InitializeAsync()
        {
            await UpdateView();
            await base.InitializeAsync();
        }

        [RelayCommand]
        private void DoNew()
        {
            SelectedEducationLevel = new();
        }

        [RelayCommand]
        private async Task DoRemove(EducationLevel educationLevelToDelete)
        {
            if (_educationLevelService is not null)
            {
                ControllerResponse result = await _educationLevelService.DeleteAsync(educationLevelToDelete.Id);
                if (result.IsSuccess)
                {
                    await UpdateView();
                }
            }
        }

        [RelayCommand]
        private async Task DoSave(EducationLevel educationLevelToSave)
        {
            if (_educationLevelService is not null)
            {
                ControllerResponse result = new();
                if (educationLevelToSave.HasId)
                    result = await _educationLevelService.UpdateAsync(educationLevelToSave);
                else
                    result = await _educationLevelService.InsertAsync(educationLevelToSave);
                if (result.IsSuccess)
                    await UpdateView();
            }
        }

        [RelayCommand]
        private async Task GetStudentsByEducationLevelId()
        {
            if (_studentService is not null &&
                SelectedEducationLevel is not null &&
                SelectedEducationLevel.HasId)
            {
                List<Student> studentsWithEducationLevelId = await _studentService.GetStudentsByEducationId(SelectedEducationLevel.Id);
                StudentsWithEducationLevel = new ObservableCollection<Student>(studentsWithEducationLevelId);
            }
        }

        [RelayCommand]
        private async Task MoveStudentToWithoutEducationLevel()
        {
            if (_studentService is not null && SelectedStudentWithEducationLevel is not null)
            {
                EducationLevel? storedSelectedEducationLevel = SelectedEducationLevel;
                SelectedStudentWithEducationLevel.EducationLevelId = Guid.Empty;
                ControllerResponse result = await _studentService.UpdateAsync(SelectedStudentWithEducationLevel);
                if (result.IsSuccess)
                    await UpdateView();
                if (storedSelectedEducationLevel is not null)
                    SelectedEducationLevel = EducationLevels.FirstOrDefault(educationLevel => educationLevel.Id == storedSelectedEducationLevel.Id);
                SelectedStudentWithEducationLevel = SelectedStudentWithEducationLevel ?? new();
            }
        }

        [RelayCommand]
        private async Task MoveStudentToWithEducationLevel()
        {
            if (_studentService is not null &&
                SelectedStudentWithoutEducationLevel is not null
                && SelectedEducationLevel is not null)
            {
                EducationLevel? storedSelectedEducationLevel = SelectedEducationLevel;
                SelectedStudentWithoutEducationLevel.EducationLevelId = SelectedEducationLevel.Id;
                ControllerResponse response = await _studentService.UpdateAsync(SelectedStudentWithoutEducationLevel);
                if (response.IsSuccess)
                    await UpdateView();
                if (storedSelectedEducationLevel is not null)
                    SelectedEducationLevel = EducationLevels.FirstOrDefault(educationLevel => educationLevel.Id == storedSelectedEducationLevel.Id);
                SelectedStudentWithEducationLevel = SelectedStudentWithEducationLevel ?? new();
            }
        }

        private async Task UpdateView()
        {
            if (_educationLevelService is not null && _studentService is not null)
            {
                List<EducationLevel> result = await _educationLevelService.SelectAllAsync();
                EducationLevels = new ObservableCollection<EducationLevel>(result);

                List<Student> withoutEducationLevelResult =
                    await _studentService.GetStudentsWithoutEducationLevel();
                StudentsWithoutEducationLevel = new ObservableCollection<Student>(withoutEducationLevelResult);
                if (EducationLevels.Any())
                    SelectedEducationLevel = EducationLevels.FirstOrDefault();
                SelectedEducationLevel = SelectedEducationLevel ?? new();

            }
        }
    }
}
