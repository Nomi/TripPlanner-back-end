using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TripPlannerAPI.Models
{
    public class TripType
    {
        public int Id { get; set; }
        [Required]
        [NotNull]
        public string? TypeName { get; set; }
    }
}
