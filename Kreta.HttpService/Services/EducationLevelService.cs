using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models;

namespace Kreta.HttpService.Services
{
    public class EducationLevelService : BaseService<EducationLevel, EducationLevelDto>, IEducationLavelService
    {
        public EducationLevelService(IHttpClientFactory? httpClientFactory, EducationLevelAssambler assambler, StudentAssambler studentAssambler) : base(httpClientFactory, assambler)
        {
        }


    }
}
