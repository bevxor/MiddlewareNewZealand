using MiddlewareNewZealand.Api.Models.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace MiddlewareNewZealand.Api.Examples
{
    public class CompanyExample : IMultipleExamplesProvider<Company>
    {
		public IEnumerable<SwaggerExample<Company>> GetExamples()
		{
			yield return SwaggerExample.Create(
				"MWNZ",
				new Company()
				{
					Id = 1,
					Name = "MWNZ",
					Description = "..is awesome"
				});
			yield return SwaggerExample.Create(
				"Other",
				new Company()
				{
					Id = 2,
					Name = "Other",
					Description = "....is not"
				});
		}
	}
}
