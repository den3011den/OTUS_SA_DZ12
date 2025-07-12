using System.ComponentModel.DataAnnotations.Schema;

namespace OTUS_SA_DZ12_Domain.Robots
{
    /// <summary>
    /// Отзывы
    /// </summary>
    public class Feedback : BaseEntity
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
        /// Отзыв
        /// </summary>

        [Column("Feedback")]
        public string FeedbackText { get; set; }

        /// <summary>
        /// Оценка (от 1 до 5)
        /// </summary>
        public int Stars { get; set; }
    }
}
