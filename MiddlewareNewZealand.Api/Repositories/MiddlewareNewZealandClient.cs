using MiddlewareNewZealand.Api.Constants;
using MiddlewareNewZealand.Api.Models.Clients;
using MiddlewareNewZealand.Api.Repositories.Interfaces;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MiddlewareNewZealand.Api.Repositories
{
    public class MiddlewareNewZealandClient : IMiddlewareNewZealandClient
    {
        private readonly HttpClient _httpClient;
        public MiddlewareNewZealandClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Company> GetByCompanyId(int companyId)
        {
            var urlSegment = MiddlewareNewZealandConstants.GetById.Replace("{id}", companyId.ToString());
            var response = await _httpClient.GetAsync(urlSegment);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var doc = XDocument.Parse(content);
            var serializer = new XmlSerializer(typeof(Company));
            return (Company)serializer.Deserialize(new StringReader(doc.ToString()));
        }
    }
}
