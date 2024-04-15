using Kreta.Shared.Models;

namespace Kreta.HttpService.Services
{
    public interface ISchoolClassService : IBaseService<SchoolClass>
    {
        public Task<List<SchoolClass>> SelectAllIncludedAsync();
        public Task<List<SchoolClass>> GetByTypeOfEducationIdAsync(Guid typeOfEducationID);
        Task<List<SchoolClass>> GetSchoolClassesWithoutTypeOfEducation();
        public Task<List<SchoolClass>> GetAllSchoolClassWithSubjectsAsync();
        public Task<List<Subject>> GetSubjectNotStudiedInTheSchoolClass(Guid subjectId);
    }
}
