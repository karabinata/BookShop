using System;
using System.ComponentModel.DataAnnotations;

using static BookShop.Data.DataConstants;

namespace BookShop.Api.Models.Books
{
    public class EditBookRequestModel
    {
        [Required]
        [MinLength(BookTitleMinLength)]
        [MaxLength(BookTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(BookDescriptionMinLength)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Copies { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        public int? Edition { get; set; }

        public int? AgeRestriction { get; set; }
    }
}
