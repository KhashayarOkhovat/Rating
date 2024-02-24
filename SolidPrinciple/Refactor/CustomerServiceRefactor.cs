using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Refactor
{
    public class RefactoredCustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger logger;
        private readonly INotificationSender notificationSender;
        public RefactoredCustomerService(INotificationSender notificationSender, ICustomerRepository customerRepository, ILogger logger)
        {
            this.logger = logger;
            this.notificationSender = notificationSender;
            this.customerRepository = customerRepository;
        }

        public void CreateCustomer(Customer customer)
        {
            try
            {
                customerRepository.Add(customer);
                notificationSender.SendNotification(new Notification { });
            }
            catch (Exception ex)
            {
              
                logger.Log(ex.ToString());

                throw ex;
            }
        }
    }

    public interface INotificationSender
    {
        void SendNotification(Notification notification);
    }

    public class Notification
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Reciver { get; set; }
    }


}
