using System;
using CustomerService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.QualityTools.Testing.Fakes.Stubs;
using System.Linq;
using CustomerService.Refactor;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Moq;

namespace CustomerServiceTest
{
    [TestClass]
    public class RefactoredCustomerServiceTests
    {
        [TestMethod]
        public void CreateCustomer_WithValidData_ShouldCreateCustomerInDbAndSentNotification()
        {

            //arrange

            var customerRepoStub = new CustomerService.Fakes.StubICustomerRepository
            {
                AddCustomer = (customer) => { },
                InstanceObserver = new StubObserver()
            };

            var loggerStub = new CustomerService.Fakes.StubILogger
            {
                LogString = (log) => { },
                InstanceObserver = new StubObserver()
            };

            var notificationSenderStub = new CustomerService.Refactor.Fakes.StubINotificationSender
            {
                SendNotificationNotification = (n) => { },
                InstanceObserver = new StubObserver()

            };

            var customerService = new RefactoredCustomerService(notificationSenderStub, customerRepoStub, loggerStub);
            //act
            customerService.CreateCustomer(new Customer { });

            //Assert
            Assert.IsTrue(((StubObserver)customerRepoStub.InstanceObserver).GetCalls().Any(call => call.StubbedMethod.Name == nameof(ICustomerRepository.Add)));
            Assert.IsTrue(((StubObserver)notificationSenderStub.InstanceObserver).GetCalls().Any(call => call.StubbedMethod.Name == nameof(INotificationSender.SendNotification)));
            Assert.IsFalse(((StubObserver)loggerStub.InstanceObserver).GetCalls().Any(call => call.StubbedMethod.Name == nameof(ILogger.Log)));

        }

        [TestMethod]
        public void CreateCustomer_WithValidData_ShouldCreateCustomerInDbAndSentNotification_Moq()
        {
            //arrange
            var customerRepository = new Mock<ICustomerRepository>();
            customerRepository.Setup(p => p.Add(It.IsAny<Customer>()));

            var logger = new Mock<ILogger>();
            logger.Setup(p => p.Log(It.IsAny<string>()));

            var notificationSender = new Mock<INotificationSender>();
            notificationSender.Setup(p => p.SendNotification(It.IsAny<Notification>()));

            var customerService = new RefactoredCustomerService(notificationSender.Object, customerRepository.Object,logger.Object);
            //act
            customerService.CreateCustomer(new Customer { });

            //Assert
            notificationSender.Verify(p => p.SendNotification(It.IsAny<Notification>()), Times.AtLeastOnce());
            customerRepository.Verify(p => p.Add(It.IsAny<Customer>()), Times.Once());
            logger.Verify(p => p.Log(It.IsAny<string>()), Times.Never());
        }

        [TestMethod]

        public void CreateCustomer_WithNotValidData_ShouldGetUserDataNotValidException_And_LogTheException()
        {

            //arrange
            var exceptionMessage = "User data is not valid!!";
            var customerRepoStub = new CustomerService.Fakes.StubICustomerRepository
            {
                AddCustomer = (customer) => { throw new Exception(exceptionMessage); },
                InstanceObserver = new StubObserver()
            };

            var loggerStub = new CustomerService.Fakes.StubILogger
            {
                LogString = (log) => { },
                InstanceObserver = new StubObserver()
            };

            var notificationSenderStub = new CustomerService.Refactor.Fakes.StubINotificationSender
            {
                SendNotificationNotification = (n) => { },
                InstanceObserver = new StubObserver()

            };

            var customerService = new RefactoredCustomerService(notificationSenderStub, customerRepoStub, loggerStub);
            try
            {
                customerService.CreateCustomer(new Customer { });
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exceptionMessage, ex.Message.ToString());

            }

            Assert.IsTrue(((StubObserver)loggerStub.InstanceObserver).GetCalls().Any(call => call.StubbedMethod.Name == nameof(ILogger.Log)));



        }
    }
}
