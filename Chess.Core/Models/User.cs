namespace Chess.Core.Models
{
    using System;

    using Chess.Core.Authentication;

    public class User
    {
        public User(int id, string username, string password, string firstName, string lastName)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool Approved { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Postcode { get; private set; }
        public string Country { get; private set; }
        public bool WantMarketingMails { get; private set; }

        public void Approve()
        {
            this.Approved = true;
        }

        public User SetAdditionalInfo(string phone, string address, string city, string postcode, string country, bool wantMarketingmails)
        {
            this.Phone = phone;
            this.Address = address;
            this.City = city;
            this.Postcode = postcode;
            this.Country = country;
            this.WantMarketingMails = wantMarketingmails;
            return this;
        }

        public void Update(string firstName, string lastName, string phone, string address, string city, string postcode, string country, bool wantMarketingmails)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SetAdditionalInfo(phone, address, city, postcode, country, wantMarketingmails);
        }

        public void UpdateEmail(string email)
        {
            this.Username = email;
        }
    }
}
