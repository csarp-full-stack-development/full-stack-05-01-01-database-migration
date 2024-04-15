using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models;

namespace Kreta.HttpService.Services
{
    public class PublicSpaceService : BaseService<PublicSpace, PublicSpaceDto>, IPublicSpaceService
    {
        public PublicSpaceService(IHttpClientFactory? httpClientFactory, PublicSpaceAssambler assambler) : base(httpClientFactory, assambler)
        {
        }
    }
}
