using MiddlewareNewZealand.Api.Models.Clients;
using System.Threading.Tasks;

namespace MiddlewareNewZealand.Api.Repositories.Interfaces
{
    public interface IMiddlewareNewZealandClient
    {
        Task<Company> GetByCompanyId(int companyId);
    }
}
