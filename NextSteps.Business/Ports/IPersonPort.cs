using NextSteps.Business.Core.Common;
using NextSteps.Business.Models;
using System;
using System.Threading.Tasks;

namespace NextSteps.Business.Ports
{
    public interface IPersonPort
    {
        Task<ApiResult<Person>> AddPerson(Person person);

        Task<ApiResult<Person>> UpdatePerson(Person person);

        Task<ApiResult> DeletePerson(Guid Id);

        Task<ApiResult<Person>> GetById(Guid Id);

        Task<ApiResult<PagedResult<Person>>> Search(Filters filters, int page, int pageSize);
    }
}