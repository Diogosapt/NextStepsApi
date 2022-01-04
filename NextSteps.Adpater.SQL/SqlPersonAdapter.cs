using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using NextSteps.Business.Core.Common;
using NextSteps.Business.Models;
using NextSteps.Business.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextSteps.Adpater.SQL
{
    public class SqlPersonAdapter : IPersonPort
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SqlPersonAdapter(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<Person>> AddPerson(Person person)
        {
            var response = new ApiResult<Person>();

            var validator = await _unitOfWork.GetRepository<Models.Person>()
                .QuerySingleAsync(
                    predicate: c => c.Email.Equals(person.Email)
                );

            if (validator != null)
            {
                return response.AddError($"Person with e-mail '{person.Email}' already exists", "Already Exists");
            }
            var result = _mapper.Map<Business.Models.Person, Models.Person>(person);

            for (int i = 0; i < person.Hobbies.Count(); i++)
            {
                await _unitOfWork.GetRepository<Models.Person>().Create(result);
            }

            var operations = await _unitOfWork.SaveChangesAsync();

            if (operations > 0)
            {
                response.AddSuccess($"Person created");
            }
            else
            {
                response.AddError($"Person creation failed", "Operation Failed");
            }
            return response;
        }

        public async Task<ApiResult> DeletePerson(Guid Id)
        {
            var response = new ApiResult();

            Models.Person person;

            var toDelete = new List<Models.Person>();

            person = await _unitOfWork.GetRepository<Models.Person>()
                .QuerySingleAsync(predicate: p => p.Id.Equals(Id));

            if (person == null)
            {
                return response.AddInfo($"Person '{Id}' not found");
            }

            if (person.Id == Id)
            {
                toDelete.Add(person);
            }
            else
            {
                response.AddInfo("Attempt to delete a Person");
            }

            if (!toDelete.Any())
            {
                return response.AddError($"No valid Person to Delete");
            }

            _unitOfWork.GetRepository<Models.Person>().DeleteRange(toDelete);
            var operations = await _unitOfWork.SaveChangesAsync();

            if (operations > 0)
            {
                response.AddSuccess($"Person Deleted");
            }
            else
            {
                response.AddError($"Delete Persons failed", "Operation Failed");
            }

            return response;
        }

        public async Task<ApiResult<Person>> GetById(Guid Id)
        {
            var response = new ApiResult<Person>();

            var person = await _unitOfWork.GetRepository<Models.Person>()
                .QuerySingleAsync(
                    predicate: p => p.Id == Id,
                    include: source => source
                    .Include(c => c.Hobbies)

                );

            var result = _mapper.Map<Models.Person, Business.Models.Person>(person);

            if (result != null)
            {
                response.Data = result;
            }
            else
            {
                response.AddError($"Person '{Id}' Not Found", "Not Found");
            }
            return response;
        }

        public async Task<ApiResult<PagedResult<Person>>> Search(Filters filters, int page, int pageSize)
        {
            var response = new ApiResult<PagedResult<Person>>();

            var query = PredicateBuilder.New<Models.Person>(true);

            if (filters.Id != Guid.Empty)
            {
                query = query.And(c => c.Id.Equals(filters.Id));
            }

            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.And(c => c.Name.ToLower().Contains(filters.Name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filters.Surname))
            {
                query = query.And(c => c.Surname.ToLower().Contains(filters.Surname.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filters.Job))
            {
                query = query.And(c => c.Job.ToLower().Contains(filters.Job.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filters.Email))
            {
                query = query.And(c => c.Email.ToLower().Contains(filters.Email.ToLower()));
            }

            if (filters.Hobbies != null && filters.Hobbies.Any())
            {
                foreach (var item in filters.Hobbies)
                {
                    query = query.And(c =>
                        c.Hobbies.Any(t => item.Equals(t.Hobby.ToLower())));
                }
            }

            var results = await _unitOfWork.GetRepository<Models.Person>()
            .QueryMultipleAsync(
            predicate: query,
            include: source => source
                .Include(c => c.Hobbies),
            orderBy: p => p.OrderBy(c => c.Name));

            var persons = _mapper.Map<IEnumerable<Models.Person>,
              IEnumerable<Business.Models.Person>>(results);

            var result = persons.ToList();

            var skip = (page - 1) * pageSize;

            response.Data = new PagedResult<Person>()
            {
                Page = page,
                PageSize = pageSize,
                Total = result.Count,
                Results = result.Skip(skip).Take(pageSize)
            };

            return response;
        }

        public async Task<ApiResult<Person>> UpdatePerson(Person person)
        {
            var response = new ApiResult<Person>();
            var operations = 0;

            var result = _mapper.Map<Business.Models.Person, Models.Person>(person);

            var oldHobbies = await _unitOfWork.GetRepository<Models.Hobbies>()
                .QueryMultipleAsync(
                    predicate: p => p.PersonId == person.Id
                );

            if (oldHobbies.Any())
            {
                _unitOfWork.GetRepository<Models.Hobbies>().DeleteRange(oldHobbies);
                operations = await _unitOfWork.SaveChangesAsync();

                if (operations <= 0)
                {
                    response.AddError($"Hobbies delete failed", "Operation Failed");
                }
            }

            if (person.Hobbies.Any())
            {
                foreach (var item in result.Hobbies)
                {
                    item.PersonId = person.Id;
                }

                await _unitOfWork.GetRepository<Models.Hobbies>().CreateRange(result.Hobbies);
                operations = await _unitOfWork.SaveChangesAsync();
            }

            _unitOfWork.GetRepository<Models.Person>().Update(result);
            operations = await _unitOfWork.SaveChangesAsync();

            if (operations > 0)
            {
                response.AddSuccess($"Person Updated");
            }
            else
            {
                response.AddError($"Person update failed", "Operation Failed");
            }
            return response;
        }
    }
}