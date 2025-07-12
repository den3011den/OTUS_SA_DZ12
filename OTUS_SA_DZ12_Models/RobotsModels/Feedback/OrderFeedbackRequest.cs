using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.Feedback
{
    public class OrderFeedbackRequest
    {
        /// <summary>
        /// ИД заказа
        /// </summary>
        [Required]
        [DisplayName("ИД заказа")]
        public int OrderId { get; set; }


        /// <summary>
        /// ИД блюда
        /// </summary>
        [Required]
        [DisplayName("ИД блюда")]
        public int DishId { get; set; }

        /// <summary>
        /// Отзыв
        /// </summary>
        [Required]
        [DisplayName("Текст отзыва")]
        [MinLength(10, ErrorMessage = "Должно быть от 10 до 1000 символов")]
        [MaxLength(1000, ErrorMessage = "Должно быть от 10 до 1000 символов")]
        public string FeedbackText { get; set; }

        /// <summary>
        /// Оценка (от 1 до 5)
        /// </summary>
        [Required]
        [DisplayName("Оценка (от 1 до 5)")]
        [Range(1, 5, ErrorMessage = "Оценка может быть от 1 до 5")]
        public int Stars { get; set; }
    }
}
