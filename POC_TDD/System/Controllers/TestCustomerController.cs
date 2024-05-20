using Microsoft.EntityFrameworkCore;
using Moq;
using POC_TDD.Fixtures;
using POC_TDD.Helpers;
using POC_TDD_App.Data;
using POC_TDD_App.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace POC_TDD.System.Controllers
{
    public class TestCustomerController
    {
        string apiUrl = "https://localhost:7254/api/Customer";


        #region Add Customer Tests

        [Fact]
        public async void AddCustomer_Should_ReturnStatusCode200()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();

            var response = await httpClient.PostAsJsonAsync(apiUrl, CustomerFixture.GetCustomerModel());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task AddCustomer_WithPayload_OnSuccess_Should_ReturnStatusCode200()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();

            var response = await httpClient.PostAsJsonAsync(apiUrl, CustomerFixture.GetCustomerModel());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var dbEntry = await dbContext.Customers.FirstOrDefaultAsync(customer => customer.Name == "Ritesh");

            Assert.NotNull(dbEntry);
        }

        [Fact]
        public async Task AddCustomer_WithBlankPayload_OnFailed_Should_ReturnStatusCode400()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();

            var response = await httpClient.PostAsJsonAsync(apiUrl, new CustomerModel());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddCustomer_WithPayload_ValidationFailed_Should_ReturnStatusCode400()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();
            var response = await httpClient.PostAsJsonAsync(apiUrl, new CustomerModel { City = "Ajmer", Email = "Test@gmail.com" });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        #endregion

        #region Get Customers Tests

        [Fact]
        public async void GetCutomers_Should_ReturnStatusCode200()
        {

            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();
            dbContext.Customers.Add(CustomerFixture.GetCustomerModel());
            dbContext.SaveChanges();

            var response = await httpClient.GetAsync(apiUrl);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetCutomers_OnNoUserFound_Should_ReturnStatusCode404()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();

            var response = await httpClient.GetAsync(apiUrl);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion

        #region Get Customer by Id Tests

        [Fact]
        public async void GetCutomer_Should_ReturnStatusCode200()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();
            var customer = CustomerFixture.GetCustomerModel();
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            var response = await httpClient.GetAsync(apiUrl+"/"+customer.Id);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetCutomer_OnNoUserFound_Should_ReturnStatusCode404()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();
            var customer = CustomerFixture.GetCustomerModel();
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            int customerId = 10;

            var response = await httpClient.GetAsync(apiUrl + "/" + customerId);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void GetCutomer_ValidationFailed_Should_ReturnStatusCode404()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();
            var customer = CustomerFixture.GetCustomerModel();
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            string customerId = "ABC";

            var response = await httpClient.GetAsync(apiUrl + "/" + customerId);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion


        #region Update Customer Tests


        [Fact]
        public async void UpdateCustomer_Should_ReturnStatusCode200()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();
            var customer = CustomerFixture.GetCustomerModel();
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            var response = await httpClient.PutAsJsonAsync(apiUrl+"/"+customer.Id, CustomerFixture.GetCustomerModel());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task UpdateCustomer_WithoutId_OnFailed_Should_ReturnStatusCode405()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();

            var response = await httpClient.PutAsJsonAsync(apiUrl, CustomerFixture.GetCustomerModel());

            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task UpdateCustomer_WithId_NoUserFound_Should_ReturnStatusCode400()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();
            var customer = CustomerFixture.GetCustomerModel();
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            int customerid = 11;

            var response = await httpClient.PutAsJsonAsync(apiUrl+"/"+ customerid, CustomerFixture.GetCustomerModel());

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateCustomer_WithPayload_ID_OnSuccess_Should_ReturnStatusCode200()
        {
            var api = new CustomerApiFactory();
            var httpClient = api.CreateClient();
            var dbContext = api.AddCustomerDbContext();
            var customer = CustomerFixture.GetCustomerModel();
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();

            var response = await httpClient.PutAsJsonAsync(apiUrl+"/"+customer.Id, new CustomerModel{ City="Ajmer1", Email="test@gmail.com", Name="Test1", Phone="77373400536" });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        


        #endregion
    }
}
