using OTUS_SA_DZ12_Models.RobotsModels.Dish;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.Feedback
{

    /// <summary>
    /// Отзыв
    /// </summary>
    public class OrderFeedbackResponse
    {
        /// <summary>
        /// ИД отзыва
        /// </summary>
        [DisplayName("Ид отзыва")]
        [Required]
        [Range(1, int.MinValue)]
        public int Id { get; set; }

        /// <summary>
        /// Блюдо
        /// </summary>
        [DisplayName("Блюдо")]
        [Required]
        public OrderDishResponse Dish { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        [DisplayName("Отзыв")]
        [Required]
        [MaxLength(1000, ErrorMessage = "Длинна отзыва быть меньше или равна 1000 символам")]
        public string FeedbackText { get; set; }

        /// <summary>
        /// Оценка (от 1 до 5)
        /// </summary>
        [DisplayName("Оценка (от 1 до 5)")]
        [Required]
        [Range(1, 5)]
        public int Stars { get; set; }
    }
}
