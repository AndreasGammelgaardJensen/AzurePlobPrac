using System.ComponentModel.DataAnnotations;

namespace AzureBlopProject.Models
{
    public class Container
    {
        [Required]
        public string Name { get; set; }
    }
}
