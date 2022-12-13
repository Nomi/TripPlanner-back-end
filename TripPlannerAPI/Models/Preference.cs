using System.ComponentModel.DataAnnotations;

namespace TripPlannerAPI.Models
{
    public class Preference
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string preferenceStr { get; set; }
        public Preference()
        {
        }
        public Preference(string str)
        {
            preferenceStr=str;
        }

    }
}
