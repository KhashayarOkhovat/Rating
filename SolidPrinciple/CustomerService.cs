using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace CustomerService
{
    public class CustomerService
    {
        private readonly CustomerRepository _repository;
        public CustomerService()
        {
            _repository = new CustomerRepository();
        }
        public void CreateCustomer(Customer customer)
        {
            try
            {
                _repository.Add(customer);//1
                SendNotification(customer);//2
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"C:\Error.txt", ex.ToString());//3
            }
        }

        private void SendNotification(Customer customer)
        {
            SmtpClient client = new SmtpClient("Host");
            client.Credentials = new NetworkCredential("username", "password");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("sender@gmail.com");
            mailMessage.To.Add("recipient@gmail.com");
            mailMessage.Body = "body";
            mailMessage.Subject = "subject";
            client.Send(mailMessage);
        }
    }


    public interface ICustomerRepository 
    {
        Customer Get(object id);
        void Add(Customer item);
    }
    public class CustomerRepository 
    {
        public Customer Get(object id)
        {
            return  new Customer();
        }

        public void Add(Customer item)
        {

        }

    }

}
