using NextSteps.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextSteps.Business.Tests.Builder
{
    public class PersonBuilder
    {
        protected Guid _id;

        protected string _name;

        protected string _email;

        protected string _surname;

        protected string _job;

        protected DateTime _birthday;

        protected IEnumerable<Hobbies> _hobbies;

        public PersonBuilder()
        {
            _id = Guid.Empty;
            _name = string.Empty;
            _surname = string.Empty;
            _email = string.Empty;
            _job = string.Empty;
            _birthday = DateTime.Now;
            _hobbies = new List<Hobbies>();
        }

        public PersonBuilder With_Id(Guid id)
        {
            _id = id;
            return this;
        }

        public PersonBuilder With_Name(string name)
        {
            _name = name;
            return this;
        }

        public PersonBuilder With_Surname(string surname)
        {
            _surname = surname;
            return this;
        }

        public PersonBuilder With_Email(string email)
        {
            _email = email;
            return this;
        }

        public PersonBuilder With_Job(string job)
        {
            _job = job;
            return this;
        }

        public PersonBuilder With_Birthday(DateTime birthday)
        {
            _birthday = birthday;
            return this;
        }

        public PersonBuilder With_Hobbies(List<Hobbies> hobbies)
        {
            _hobbies = hobbies;
            return this;
        }

        public PersonBuilder WithTestValues()
        {
            _id = Guid.Parse("1ae1f7b5-3a33-4137-8091-c4b079a48012");
            _name = "Antonio";
            _surname = "Silva";
            _email = "antonio@email.pt";
            _job = "Programador";
            _hobbies = new List<Hobbies>()
            {
                new Hobbies{ Hobby = "dançar"},
                new Hobbies{ Hobby = "cantar"}
            };

            return this;
        }

        public Models.Person Build()
        {
            return new Models.Person
            {
                Id = _id,
                Name = _name,
                Surname = _surname,
                Email = _email,
                Job = _job,
                Birthday = _birthday,
                Hobbies = _hobbies
            };
        }
    }
}