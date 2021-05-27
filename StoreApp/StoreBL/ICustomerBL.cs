using System.Collections.Generic;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public interface ICustomerBL
    {
        List<Customer> GetAllCustomers(); 
        Customer GetCustomer(Customer customer);
        Customer AddCustomer(Customer customer);
    }
}