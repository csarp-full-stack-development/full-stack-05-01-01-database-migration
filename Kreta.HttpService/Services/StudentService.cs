using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models.SchoolCitizens;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Kreta.HttpService.Services
{
    public class StudentService : BaseService<Student, StudentDto>, IStudentService
    {
        public StudentService(IHttpClientFactory? httpClientFactory, StudentAssambler studentAssambler) : base(httpClientFactory, studentAssambler)
        {
        }

        public async Task<List<Student>> SelectAllIncludedAsync()
        {
            if (_httpClient is not null)
            {
                try
                {

                    List<StudentDto>? resultDto = await _httpClient.GetFromJsonAsync<List<StudentDto>>($"api/Student/included");
                    if (resultDto is not null)
                    {
                        List<Student> result = resultDto.Select(entity => _assambler.ToModel(entity)).ToList();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return new List<Student>();
        }

        public async Task<List<Student>> GetStudentsByEducationId(Guid educationId)
        {
            if (_httpClient is not null)
            {
                try
                {

                    List<StudentDto>? resultDto = await _httpClient.GetFromJsonAsync<List<StudentDto>>($"api/Student/byeducationid/{educationId}");
                    if (resultDto is not null)
                    {
                        List<Student> result = resultDto.Select(entity => _assambler.ToModel(entity)).ToList();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return new List<Student>();
        }

        public async Task<List<Student>> GetStudentsWithoutEducationLevel()
        {
            if (_httpClient is not null)
            {
                try
                {

                    List<StudentDto>? resultDto = await _httpClient.GetFromJsonAsync<List<StudentDto>>($"api/Student/withouteducationlevel");
                    if (resultDto is not null)
                    {
                        List<Student> result = resultDto.Select(entity => _assambler.ToModel(entity)).ToList();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return new List<Student>();
        }
    }
}
