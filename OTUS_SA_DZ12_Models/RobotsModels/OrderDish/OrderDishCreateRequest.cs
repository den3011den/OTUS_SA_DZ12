using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.OrderDish
{

    /// <summary>
    /// Блюдо в заказе при создании заказа
    /// </summary>
    public class OrderDishCreateRequest
    {
        /// <summary>
        /// Блюдо
        /// </summary>
        [Required]
        public int DishId { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        [Required]
        public double Quantity { get; set; }

        /// <summary>
        /// Цена за единицу
        /// </summary>
        [Required]
        public double Price { get; set; }
    }
}
