using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_153551_BOHDAN.UI.Domain.Entities
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }  // Уникальный ID

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;  // Название

        [StringLength(500)]
        public string? Description { get; set; }  // Описание

        [Required]
        public int CategoryId { get; set; }  // Внешний ключ к категории

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }  // Навигационное свойство

        [Required]
        [Range(0, 1000000)]
        public decimal Calories { get; set; }  // Кол-во каллорий

        [StringLength(255)]
        public string? Image { get; set; }  // Путь к изображению

        [StringLength(50)]
        public string? MimeType { get; set; }  // MIME тип изображения
    }
}
