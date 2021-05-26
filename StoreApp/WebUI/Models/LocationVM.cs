using StoreModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class LocationVM
    {
        public LocationVM()
        {}
        public LocationVM(Location location)
        {
            Id = location.Id;
            Name = location.Name;
            Address = location.Address;
        }
        [Required]
        [DisplayName("Branch Location Name")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public int Id { get; set; }
    }
}