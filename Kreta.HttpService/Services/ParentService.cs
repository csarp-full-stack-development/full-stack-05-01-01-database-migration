using Kreta.Shared.Dtos;
using Kreta.Shared.Models.SchoolCitizens;
using Kreta.Shared.Assamblers;


namespace Kreta.HttpService.Services
{
    public class ParentService : BaseService<Parent, ParentDto>, IParentService
    {
        public ParentService(IHttpClientFactory? httpClientFactory,ParentAssambler assambler) : base(httpClientFactory, assambler)
        {
        }
    }
}
