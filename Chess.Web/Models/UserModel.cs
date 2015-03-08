namespace Chess.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public bool WantMarketingMails { get; set; }
    }
}