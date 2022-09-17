using MiddlewareNewZealand.Api.Models.ViewModels;
using System.Threading.Tasks;

namespace MiddlewareNewZealand.Api.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> GetByCompanyId(int companyId);
    }
}
