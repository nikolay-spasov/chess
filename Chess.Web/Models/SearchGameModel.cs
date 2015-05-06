namespace Chess.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SearchGameModel
    {
        [Range(0, 60)]
        public int Minutes { get; set; }

        public string Color { get; set; }
    }
}