using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models;

namespace Kreta.HttpService.Services
{
    public class SubjectTypeService : BaseService<SubjectType, SubjectTypeDto>, ISubjectTypeService
    {
        public SubjectTypeService(IHttpClientFactory? httpClientFactory, SubjectTypeAssambler assambler) : base(httpClientFactory, assambler)
        {
        }
    }
}
