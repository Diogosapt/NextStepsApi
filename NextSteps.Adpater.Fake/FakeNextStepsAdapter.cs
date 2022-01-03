using NextSteps.Business.Core.Common;
using NextSteps.Business.Models;
using NextSteps.Business.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextSteps.Adpater.Fake
{
    public class FakeNextStepsAdapter : IPersonPort
    {
        private List<Person> personList;

        public FakeNextStepsAdapter()
        {
            var person1 = new Person
            {
                //Id = Guid.NewGuid(),
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "Diogo",
                Surname = "Sá",
                Birthday = new DateTime(2000, 03, 10),
                Email = "diogo@email.com",
                Job = "Programador",
                Hobbies = new List<Hobbies>() {
                    new Hobbies {Id = Guid.NewGuid(), Hobby = "Cantar" },
                    new Hobbies {Id = Guid.NewGuid(), Hobby = "Dançar" }
                }
            };

            var person2 = new Person
            {
                Id = Guid.NewGuid(),
                Name = "Diogo1",
                Surname = "Sá1",
                Birthday = new DateTime(2001, 03, 10),
                Email = "diogo1@email.com",
                Job = "Programador1",
                Hobbies = new List<Hobbies>() {
                    new Hobbies {Id = Guid.NewGuid(), Hobby = "Jogar Futebol" },
                    new Hobbies {Id = Guid.NewGuid(), Hobby = "Dançar" }
                }
            };

            personList = new() { person1, person2 };
        }

        public Task<ApiResult<Person>> AddPerson(Person person)
        {
            var result = new ApiResult<Person>();

            List<Hobbies> hobbies = new();

            foreach (var item in person.Hobbies)
            {
                var newHobbie = item with
                {
                    Id = Guid.NewGuid()
                };

                hobbies.Add(newHobbie);
            }

            var newPerson = person with
            {
                Id = Guid.NewGuid(),
                Hobbies = hobbies
            };

            personList.Add(newPerson);

            result.AddSuccess($"Person with Id = {newPerson.Id} created sucessfully");

            result.Data = newPerson;

            return Task.FromResult(result);
        }

        public Task<ApiResult> DeletePerson(Guid Id)
        {
            var response = new ApiResult();

            var result = personList.Find(p => p.Id == Id);

            if (result != null)
            {
                personList.Remove(result);
                response.AddSuccess($"Person with Id = '{Id}' was successfuly deleted");
            }
            else
            {
                response.AddError($"Person with Id = '{Id}' Not Found", "Not Found");
            }

            return Task.FromResult(response);
        }

        public Task<ApiResult<Person>> GetById(Guid Id)
        {
            var response = new ApiResult<Person>();

            var result = personList.Find(p => p.Id == Id);

            if (result != null)
                response.Data = result;
            else
                response.AddError($"Person with Id = '{Id}' Not Found", "Not Found");

            return Task.FromResult(response);
        }

        public Task<ApiResult<PagedResult<Person>>> Search(Filters filters, int page, int pageSize)
        {
            var response = new ApiResult<PagedResult<Person>>();

            var person = personList.AsQueryable();

            if (filters.Id != Guid.Empty)
            {
                person = person.Where(p => p.Id.Equals(filters.Id));
            }

            if (!string.IsNullOrEmpty(filters.Name))
            {
                person = person.Where(p => p.Name.ToLower().Trim().Equals(filters.Name.ToLower().Trim()));
            }

            if (!string.IsNullOrEmpty(filters.Email))
            {
                person = person.Where(p => p.Email.Equals(filters.Email));
            }

            if (!string.IsNullOrEmpty(filters.Surname))
            {
                person = person.Where(p => p.Surname.Equals(filters.Surname));
            }

            if (!string.IsNullOrEmpty(filters.Job))
            {
                person = person.Where(p => p.Job.Equals(filters.Job));
            }

            person = person.OrderBy(p => p.Name);

            var result = person.ToList();

            var skip = (page - 1) * pageSize;

            response.Data = new PagedResult<Person>()
            {
                Page = page,
                PageSize = pageSize,
                Total = result.Count,
                Results = result.Skip(skip).Take(pageSize)
            };

            return Task.FromResult(response);
        }

        public Task<ApiResult<Person>> UpdatePerson(Person person)
        {
            var result = new ApiResult<Person>();

            var i = personList.FindIndex(p => p.Id == person.Id);

            if (i > -1)
            {
                personList[i] = person;

                result.Data = personList[i];

                result.AddSuccess($"Person Updated");
            }
            else
            {
                result.AddError($"Person with Id = '{person.Id}' Not Found", "Not Found");
            }

            return Task.FromResult(result);
        }
    }
}