using System.Collections.Generic;
namespace StoreModels
{
    /// <summary>
    /// Data structure used to define a customer
    /// </summary>
    public class Customer
    {
        public Customer()
        {}

        public Customer(Customer customer) : this(customer.Id, customer.FirstName, customer.MiddleName, customer.LastName, customer.PhoneNumber)
        {
            // this.Id = customer.Id;
            // this.FirstName = customer.FirstName;
            // this.MiddleName = customer.MiddleName;
            // this.LastName = customer.LastName;
            // this.PhoneNumber = customer.PhoneNumber;
            
        }
        public Customer(string firstname, string middlename, string lastname) 
        {
            this.FirstName = firstname;
            this.MiddleName = middlename;
            this.LastName = lastname;
        }
        
        public Customer(string firstname, string middlename, string lastname, string phoneNumber) : this (firstname, middlename, lastname)
        {
            this.PhoneNumber = phoneNumber;
        }

        public Customer(int id, string firstname, string middlename, string lastname, string phoneNumber) : this(firstname, middlename, lastname, phoneNumber)
        {
            this.Id = id;
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public int Id { get; set; }
        //public string Email { get; set; }
        public string FullName
        {
            get 
            {
                if (MiddleName == "") return $"{FirstName} {LastName}";
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }

        //ToDo: list of orders
        public HashSet<Order> Orders {get; set;} 

        // print customer name and a list of orders
        public override string ToString()
        {
            return FullName;
        }

    }
}