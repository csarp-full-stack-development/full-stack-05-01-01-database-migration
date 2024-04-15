using Kreta.Shared.Assamblers;
using Kreta.Shared.Dtos;
using Kreta.Shared.Models;

namespace Kreta.HttpService.Services
{
    public class TypleOfEducationService : BaseService<TypeOfEducation, TypeOfEducationDto>, ITypeOfEducationService
    {
        public TypleOfEducationService(IHttpClientFactory? httpClientFactory, TypeOfEducationAssambler assambler) : base(httpClientFactory, assambler)
        {
        }
    }
}
