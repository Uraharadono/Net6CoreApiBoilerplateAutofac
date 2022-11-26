using System.ComponentModel.DataAnnotations;

namespace Net6CoreApiBoilerplateAutofac.Api.Models
{
    public class PostViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public long BlogId { get; set; }
    }
}
