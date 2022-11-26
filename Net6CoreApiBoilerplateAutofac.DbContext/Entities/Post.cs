﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Net6CoreApiBoilerplateAutofac.Infrastructure.DbUtility;

namespace Net6CoreApiBoilerplateAutofac.DbContext.Entities
{
    public class Post : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Oid { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public long BlogId { get; set; }
        public Blog Blog { get; set; }

        [Required]
        [ForeignKey("Author")]
        public long AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
