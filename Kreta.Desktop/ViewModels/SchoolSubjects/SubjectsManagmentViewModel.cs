using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.ViewModels.Base;
using Kreta.HttpService.Services;
using Kreta.Shared.Models;
using Kreta.Shared.Models.SwitchTable;
using Kreta.Shared.Responses;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Kreta.Desktop.ViewModels.SchoolSubjects
{
    public partial class SubjectsManagmentViewModel : BaseViewModel
    {
        private ISubjectService? _subjectService { get; set; }
        private ISchoolClassSubjectsService? _schoolClassSubjectsService { get; set; }

        [ObservableProperty]
        private Subject? _selectedSubject = new Subject();

        [ObservableProperty]
        private SchoolClassSubjects? _selectedSchoolClassSubjects = new SchoolClassSubjects();

        [ObservableProperty]
        private ObservableCollection<Subject> _subjects = new ObservableCollection<Subject>();

        [ObservableProperty]
        private ObservableCollection<SchoolClass> _schoolClassesWhoNotStudySubject = new ObservableCollection<SchoolClass>();

        [ObservableProperty]
        private SchoolClass _selectedSchoolClassesWhoNotStudySubject = new();

        public SubjectsManagmentViewModel()
        {
        }

        public string Title { get; set; } = "Tantárgyak kezelése";

        public SubjectsManagmentViewModel(ISubjectService subjectService, ISchoolClassSubjectsService? schoolClassSubjectsService)
        {
            _subjectService = subjectService;
            _schoolClassSubjectsService = schoolClassSubjectsService;
        }

        public override async Task InitializeAsync()
        {
            await UpdateView();
            await base.InitializeAsync();
        }

        [RelayCommand]
        private async Task GetSchoolClassWhoNotStudySubject()
        {
            await UpdateSchoolClassWhoNotStudySubject();
        }

        [RelayCommand]
        private async Task MoveToSchoolClassNotStudiedSubject()
        {
            if (SelectedSchoolClassSubjects is not null && _schoolClassSubjectsService is not null)
            {
                ControllerResponse response = await _schoolClassSubjectsService.MoveToNotStudyingAsync(SelectedSchoolClassSubjects);
                if (response.IsSuccess)
                {
                    await UpdateView();
                }
            }
        }

        [RelayCommand]
        private async Task MoveToSchoolClassStudiedSubject()
        {
            if (_schoolClassSubjectsService is not null && SelectedSubject is not null && SelectedSchoolClassesWhoNotStudySubject is not null)
            {
                SchoolClassSubjects newSchoolClassSubject = new SchoolClassSubjects()
                {
                    SchoolClassId = SelectedSchoolClassesWhoNotStudySubject.Id,
                    SubjectId = SelectedSubject.Id,
                };
                ControllerResponse response = await _schoolClassSubjectsService.MoveToStudyingAsync(newSchoolClassSubject);
                if (response.IsSuccess)
                    await UpdateView();
            }
        }

        private async Task UpdateView()
        {
            await UpdateSchoolClassWhoNotStudySubject();
            await UpdateSubjectsWithSchoolClass();
        }

        private async Task UpdateSubjectsWithSchoolClass()
        {
            if (_subjectService != null)
            {
                Subject? savedSelectedSubject = SelectedSubject is not null ? SelectedSubject : new();
                List<Subject> subjects = await _subjectService.GetAllSubjectsWithSchoolClassesAsync();
                Subjects = new ObservableCollection<Subject>(subjects);
                SelectedSubject = subjects.FirstOrDefault(subject => subject.Id == savedSelectedSubject.Id);
                SelectedSubject = SelectedSubject is not null ? SelectedSubject : new();
            }
        }

        private async Task UpdateSchoolClassWhoNotStudySubject()
        {
            if (_subjectService is not null && SelectedSubject is not null && SelectedSubject.HasId && SelectedSubject.HasId)
            {
                List<SchoolClass> schoolClasses = await _subjectService.GetSchoolClassWhoNotStudyingSubject(SelectedSubject.Id);
                SchoolClassesWhoNotStudySubject = new ObservableCollection<SchoolClass>(schoolClasses);
            }
        }

    }
}
