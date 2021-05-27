namespace StoreModels
{
    public class Address
    {
        private string _street;
        public Address(string street1, string street2, string city, string state, int zipcode)
        {
            this._street = 
            this.City = city;
            this.State = state;
            this.Zipcode = zipcode;
        }
        public object Street1 { get; private set; }
        public string Street2 { get; private set; }
        public string Street
        {
            get { return _street; }
            set
            {
                _street = Street2.Equals("") ? $"{Street1}" : $"{Street1} {Street2}";
            }

        }
        public string City { get; private set; }
        public string State { get; private set; }
        public int Zipcode { get; private set; }

        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {Zipcode}";
        }
    }
}