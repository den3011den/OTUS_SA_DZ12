using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.Dish
{

    /// <summary>
    /// Блюдо
    /// </summary>
    public class OrderDishResponse
    {
        /// <summary>
        /// ИД записи
        /// </summary>        
        [DisplayName("Ид записи")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        [Required]
        [MaxLength(200, ErrorMessage = "Длинна Наименования должна быть меньше или равна 200 символам")]
        public string Name { get; set; }

    }
}
