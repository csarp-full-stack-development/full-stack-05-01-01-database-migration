using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models.SchoolCitizens;
using Kreta.Shared.Responses;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.SchoolCitizens
{
    public partial class TeacherViewModel : BaseViewModel
    {
        private readonly ITeacherService? _teacherService;

        [ObservableProperty]
        private ObservableCollection<Teacher> _teachers = new();

        [ObservableProperty]
        private Teacher _selectedTeacher;

        public TeacherViewModel()
        {
            SelectedTeacher = new Teacher();
        }

        public TeacherViewModel(ITeacherService? teacherService)
        {
            SelectedTeacher = new Teacher();
            _teacherService = teacherService;
        }

        public async override Task InitializeAsync()
        {
            await UpdateView();
        }

        [RelayCommand]
        private async Task DoSave(Teacher newTeacher)
        {
            if (_teacherService is not null)
            {
                ControllerResponse result;
                if (newTeacher.HasId)
                    result = await _teacherService.UpdateAsync(newTeacher);
                else
                    result = await _teacherService.InsertAsync(newTeacher);

                if (!result.HasError)
                {
                    await UpdateView();
                }
            }
        }

        [RelayCommand]
        private async Task DoRemove(Teacher teacherToDelete)
        {
            if (_teacherService is not null)
            {
                ControllerResponse result = await _teacherService.DeleteAsync(teacherToDelete.Id);
                if (result.IsSuccess)
                {
                    await UpdateView();
                }
            }
        }

        [RelayCommand]
        private void DoNewStudent()
        {
            SelectedTeacher = new Teacher();
        }

        private async Task UpdateView()
        {
            if (_teacherService is not null)
            {
                List<Teacher> students = await _teacherService.SelectAllAsync();
                Teachers = new ObservableCollection<Teacher>(students);
            }
        }

    }
}
