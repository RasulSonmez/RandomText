using System.ComponentModel.DataAnnotations;

namespace RandomText.Models
{
    public class RandomLetter
    {
        public int Id { get; set; }      
        public string? Words { get; set; }
        public string? LetterOfCount { get; set; }
    }
}
