using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        protected IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
        }
        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, $"Code for SystemCountryCode cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, $"Name for SystemCountryCode cannot be empty"));
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public SystemCountryCodePoco Get(String country_code)
        {
            return _repository.GetSingle(c => c.Code == country_code);
        }

        public List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            Add(pocos);
        }
        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            Add(pocos);
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

    }
}
