using Kreta.Shared.Models.SchoolCitizens;

namespace Kreta.HttpService.Services
{
    public interface IStudentService : IBaseService<Student>
    {
        public Task<List<Student>> GetStudentsByEducationId(Guid id);
        Task<List<Student>> GetStudentsWithoutEducationLevel();
        public Task<List<Student>> SelectAllIncludedAsync();
    }
}
