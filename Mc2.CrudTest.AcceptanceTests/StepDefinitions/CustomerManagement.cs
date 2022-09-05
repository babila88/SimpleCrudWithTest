using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Mc2.CrudTest.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class CustomerManagement
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };
        private readonly ScenarioContext _scenarioContext;
        private CreateCustomerRequest createCustomerRequest = new CreateCustomerRequest();
        public CustomerManagement(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"system error codes are following")]
        public void GivenSystemErrorCodesAreFollowing(Table table)
        {
            var systemErrors = table.CreateSet<SystemErrorCode>();
        }

        [When(@"user creates customer with (.*)")]
        public void WhenUserCreatesCustomerWithJohn(string firstName)
        {
            createCustomerRequest.FirstName = firstName;
        }

        [When(@"lastname of (.*)")]
        public void WhenLastnameOfDoe(string lastName)
        {
            createCustomerRequest.LastName = lastName;
        }

        [When(@"date of birth of (.*)")]
        public void WhenDateOfBirthOf_JAN(string birthdate)
        {
            createCustomerRequest.DateOfBirth = DateTime.Parse(birthdate);
        }


        [When(@"phoneNumber of (.*)")]
        public void WhenPhoneNumberOf(string phoneNumber)
        {
            createCustomerRequest.PhoneNumber = phoneNumber;
        }

        [When(@"id of (.*)")]
        public void WhenIdOf(int id)
        {
            createCustomerRequest.Id = id;
        }

        [When(@"email of (.*)")]
        public async Task WhenEmailOfJohnDoe_Com(string email)
        {
            createCustomerRequest.Email = email;
            var response = await _httpClient.PostAsJsonAsync("Customers", createCustomerRequest);
            var responseCustomer = await response.Content.ReadFromJsonAsync<CreateResponse>();
            _scenarioContext.Add("CreatetdCustomer", createCustomerRequest);
        }

        [Then(@"user can lookup customer by ID of  (.*) and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupCustomerByIDOfAndGetRecords(int id, string recoredCount)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomerResponse>($"Customers/{id}");
            response.Should().NotBeNull();
        }

        [When(@"user edit customer by ID of (.*) with new email of ""([^""]*)""")]
        public async Task WhenUserEditCustomerByIDOfWithNewEmailOf(int id, string newEmail)
        {
            var customerForEdit = await _httpClient.GetFromJsonAsync<CustomerResponse>($"Customers/{id}");
            customerForEdit.Email = newEmail;
            var response = await _httpClient.PutAsJsonAsync<CustomerResponse>($"Customers", customerForEdit);
            _scenarioContext.Remove("GetCustomer");
            _scenarioContext.Add("GetCustomer", customerForEdit);

        }

        [Then(@"return record email is ""([^""]*)""")]
        public void ThenReturnRecordEmailIs(string updatedEmail)
        {
            var expectedCustomer = _scenarioContext.Get<CustomerResponse>("GetCustomer");
            expectedCustomer.Email.Should().BeEquivalentTo(updatedEmail);
        }

        [When(@"user delete customer by ID of (.*)")]
        public async Task WhenUserDeleteCustomerByIDOf(int id)
        {
            var customerForEdit = await _httpClient.DeleteAsync($"Customers/{id}");

            try
            {
                var response = await _httpClient.GetFromJsonAsync<CustomerResponse>($"Customers/{id}");
            }
            catch (HttpRequestException ex)
            {
                ex.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

            }
        }

        //[Given(@"system has existing customer")]
        //public void GivenSystemHasExistingCustomer(Table table)
        //{
        //    ScenarioContext.StepIsPending();
        //}

        //[Then(@"system responds with ""([^""]*)""  error")]
        //public void ThenSystemRespondsWithError(string p0)
        //{
        //    ScenarioContext.StepIsPending();
        //}
    }
}
