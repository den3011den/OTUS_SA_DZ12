namespace OTUS_SA_DZ12_Domain.Robots
{

    /// <summary>
    /// Блюда заказа
    /// </summary>
    public class OrderDish : BaseEntity
    {

        /// <summary>
        /// ИД заказа
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Заказ
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// ИД блюда
        /// </summary>
        public int DishId { get; set; }

        /// <summary>
        /// Блюдо
        /// </summary>
        public virtual Dish Dish { get; set; }

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
