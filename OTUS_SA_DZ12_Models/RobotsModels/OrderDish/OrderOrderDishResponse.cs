using OTUS_SA_DZ12_Models.RobotsModels.Dish;

namespace OTUS_SA_DZ12_Models.RobotsModels.OrderDish
{

    /// <summary>
    /// Блюдо в заказе
    /// </summary>
    public class OrderOrderDishResponse
    {
        /// <summary>
        /// Блюдо
        /// </summary>
        public OrderDishResponse Dish { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Цена за единицу
        /// </summary>
        public double Price { get; set; }
    }
}
