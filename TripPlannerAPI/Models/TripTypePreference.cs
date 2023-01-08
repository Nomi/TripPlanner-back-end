using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TripPlannerAPI.Models
{
    public class TripTypePreference
    {
        [Key]
        public int Id { get; set; }
        public int TripTypeId { get; set; }
        public TripType TripType { get; set; }
        public int PreferenceTypeId { get; set; }
        public PreferenceType PreferenceType { get; set; }
        public User User{ get; set; }
        public int Points { get; set; }
    }
}
