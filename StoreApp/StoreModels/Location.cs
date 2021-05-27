using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StoreModels
{
    /// <summary>
    /// Data structure used to define a store location
    /// </summary>
    public class Location
    {
        public Location()
        { }
        public Location(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }
        public Location(int id, string name, string address) : this(name, address)
        {
            this.Id = id;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }

        //This contains a lit of inventories of a particular location
        //public List<Dictionary<Product, Item>> Inventories { get; set; }

        public override string ToString()
        {
            return $"\t Location Name: {Name} \n\t Address: {Address}";
        }
    }
}
