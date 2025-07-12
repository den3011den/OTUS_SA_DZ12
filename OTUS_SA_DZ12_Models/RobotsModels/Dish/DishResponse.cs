using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.Dish
{

    /// <summary>
    /// Блюдо
    /// </summary>
    public class DishResponse
    {
        /// <summary>
        /// ИД записи
        /// </summary>        
        [DisplayName("Ид записи")]
        [Required]
        [Range(1, int.MinValue)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        [Required]
        [MaxLength(200, ErrorMessage = "Длинна Наименования должна быть меньше или равна 200 символам")]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [DisplayName("Описание")]

        [MaxLength(500, ErrorMessage = "Длинна Описания должна быть меньше или равна 500 символам")]
        public string? Description { get; set; }

        /// <summary>
        /// Признак удаления в архив
        /// </summary>
        [DisplayName("Признак удаления в архив")]
        public bool IsArchive { get; set; } = false;
    }
}
