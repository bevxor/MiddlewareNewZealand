using AutoMapper;
using MiddlewareNewZealand.Api.Models.ViewModels;
using MiddlewareNewZealand.Api.Repositories.Interfaces;
using MiddlewareNewZealand.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace MiddlewareNewZealand.Api.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMiddlewareNewZealandClient _middlewareNewZealandClient;
        private readonly IMapper _mapper;

        public CompanyService(IMiddlewareNewZealandClient middlewareNewZealandClient,
            IMapper mapper)
        {
            _middlewareNewZealandClient = middlewareNewZealandClient;
            _mapper = mapper;
        }

        public async Task<Company> GetByCompanyId(int companyId)
        {
            var company =  await _middlewareNewZealandClient.GetByCompanyId(companyId);

            if (company == null)
                return null;

            return _mapper.Map<Company>(company);
        }
    }
}
