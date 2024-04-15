using Kreta.Shared.Models;

namespace Kreta.HttpService.Services
{
    public interface ISubjectService : IBaseService<Subject>
    {
        public Task<List<Subject>> GetAllSubjectsWithSchoolClassesAsync();
        Task<List<SchoolClass>> GetSchoolClassWhoNotStudyingSubject(Guid id);
    }
}
