using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfile of {poco.Id} should be provided"));
                }
                else
                {
                    string[] phoneComponents = poco.ContactPhone.Split('-');
                    if (phoneComponents.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfile of {poco.Id} must correspond to a valid phone number"));
                    }
                    else if (phoneComponents[0].Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfile of {poco.Id} must correspond to a valid phone number"));
                    }
                    else if (phoneComponents[1].Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfile of {poco.Id} must correspond to a valid phone number"));
                    }
                    else if (phoneComponents[2].Length != 4)
                    {
                        exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfile of {poco.Id} must correspond to a valid phone number"));
                    }
                }
                if (!string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    string[] Company_Website = poco.CompanyWebsite.Split('.');
                    if (!Company_Website[2].Equals("ca") && !Company_Website[2].Equals("biz") && !Company_Website[2].Equals("com"))
                    {
                        exceptions.Add(new ValidationException(600, $"Invalid email provided for CompanyProfile of {poco.Id}"));
                    }
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
