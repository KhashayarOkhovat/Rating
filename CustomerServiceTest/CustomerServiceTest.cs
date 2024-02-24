using CustomerService;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CustomerService.Fakes;

namespace CustomerServiceTest
{
    [TestClass]
    public class CustomerServiceTest
    {
        [TestMethod]
        public void CreateCustomer_WithValidData_ShudCreateCustomer()
        {
            //arrange
            var customerAdded = false;
            var notificationSent = false;
            var logAdded = false;

            using (ShimsContext.Create())
            {
                 ShimCustomerRepository.AllInstances.AddCustomer = (a, b) => { customerAdded = true; };

                var shimCustomerRepository = new CustomerService.Fakes.ShimCustomerRepository()
                {

                };


                System.IO.Fakes.ShimFile.WriteAllTextStringString = (a, b) => { logAdded = true; };


                //Shim Does not Support Observer on method call
                //https://www.dotnetcurry.com/visualstudio/963/microsoft-fakes-framework-visual-studio

                var customerService = new CustomerService.CustomerService();
                var shimCustomerService = new CustomerService.Fakes.ShimCustomerService()
                {
                    SendNotificationCustomer = (a) => { notificationSent = true; },
                    CreateCustomerCustomer = (a) => customerService.CreateCustomer(a)
                };
                customerService = shimCustomerService;
                SetPrivateField(shimCustomerService.Instance, "_repository", shimCustomerRepository.Instance);
                customerService.CreateCustomer(new Customer());
                shimCustomerService.Instance.CreateCustomer(new Customer());
                Assert.IsTrue(customerAdded);
                Assert.IsTrue(notificationSent);
                Assert.IsFalse(logAdded);         
            }
        }

        private void SetPrivateField(CustomerService.CustomerService customerServiceInstance, string fieldName, object value)
        {
            customerServiceInstance.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(customerServiceInstance, value);
        }
    }
}
