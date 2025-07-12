using OTUS_SA_DZ12_Models.RobotsModels.Customers;
using OTUS_SA_DZ12_Models.RobotsModels.Feedback;
using OTUS_SA_DZ12_Models.RobotsModels.OrderDish;
using OTUS_SA_DZ12_Models.RobotsModels.ReceiveMethod;
using OTUS_SA_DZ12_Models.RobotsModels.State;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.Order
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderResponse
    {
        /// <summary>
        /// ИД заказа
        /// </summary>        
        [DisplayName("Ид заказа")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Дата/время заказа
        /// </summary>
        [DisplayName("Дата/время заказа")]
        [Required]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [DisplayName("Статус заказа")]
        [Required]
        public StateResponse State { get; set; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        [DisplayName("Сумма")]
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        [DisplayName("Клиент")]
        public CustomerResponse? Customer { get; set; }

        /// <summary>
        /// Способ получения заказа
        /// </summary>
        [DisplayName("Способ получения заказа")]
        [Required]
        public ReceiveMethodResponse ReceiveMethod { get; set; }

        /// <summary>
        /// Дата/время получения заказа
        /// </summary>
        [DisplayName("Дата/время получения заказа")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// Блюда заказа
        /// </summary>
        [DisplayName("Блюда заказа")]
        public List<OrderOrderDishResponse> OrdersDishesList { get; set; }

        /// <summary>
        /// Отзывы на блюда заказа
        /// </summary>
        [DisplayName("Отзывы на блюда заказа")]
        public List<OrderFeedbackResponse> FeedbacksList { get; set; }

    }
}
