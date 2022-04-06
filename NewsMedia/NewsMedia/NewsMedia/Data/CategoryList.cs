using System;
using System.ComponentModel.DataAnnotations;
using NewsMedia.Data;


namespace NewsMedia.Data
{
    public class CategoryList
    {

        public int Id { get; set; }
        public string ListOfCategories { get; set; }
    }
}