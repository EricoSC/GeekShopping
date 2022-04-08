using GeekShopping.ProductAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductAPI.Model
{
    [Table("Produtos")]
    public class Product : BaseEntity
    {
        [Column("Name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [Column("Price")]
        [Required]
        public decimal Price { get; set; }

        [Column("Description")]
        [Required]
        [StringLength(254)]
        public string Description { get; set; } = "";

        [Column("CategoryName")]
        [Required]
        public string CategoryName { get; set; } = "";

        [Column("ImageProduct_url")]
        [StringLength(300)]
        public string ImageUrl { get; set; } = "";
    }
}
