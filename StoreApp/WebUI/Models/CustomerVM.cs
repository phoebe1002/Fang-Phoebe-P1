using StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    /// <summary>
    /// View Model. This contains the neccessary information I want presented to the
    /// end user, or some info that is vital to data processing (i.e the id)
    /// </summary>
    public class CustomerVM
    {
        public CustomerVM()
        {}
        public CustomerVM(Customer customer)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            MiddleName = customer.MiddleName;
            LastName = customer.LastName;
            PhoneNumber = customer.PhoneNumber;
        }
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }


        //public string Email { get; set; }

        public string FullName
        {
            get 
            {
                if (MiddleName == "") return $"{FirstName} {LastName}";
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }
    }
}