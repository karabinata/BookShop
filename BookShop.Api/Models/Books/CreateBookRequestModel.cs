using System;
using System.ComponentModel.DataAnnotations;


namespace BookShop.Api.Models.Books
{
    public class CreateBookRequestModel : EditBookRequestModel
    {
        public string Categories { get; set; }
    }
}