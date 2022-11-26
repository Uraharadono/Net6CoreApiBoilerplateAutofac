using System.ComponentModel.DataAnnotations;

namespace Net6CoreApiBoilerplateAutofac.Api.Models
{
    public class BlogViewModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
