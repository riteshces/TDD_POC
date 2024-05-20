using POC_TDD_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC_TDD.Fixtures
{
    public static class CustomerFixture
    {
        public static CustomerModel GetCustomerModel() { return new CustomerModel { City = "Ajmer", Email = "test@gmail.com", Name = "Ritesh", Phone = "7737400536" }; }

        public static List<CustomerModel> GetCustomerModels()
        {
            return new List<CustomerModel> {
            new CustomerModel { City = "Ajmer", Email = "test@gmail.com", Name = "Ritesh", Phone = "7737400536"},
             new CustomerModel {City="Jaipur", Email="test2@gmail.com", Name="Ritesh2", Phone="7737400530"  }
            };
        }
    }
}
