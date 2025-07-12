using OTUS_SA_DZ12_Models.RobotsModels.OrderDish;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.Order
{
    /// <summary>
    /// Запрос создания заказа
    /// </summary>
    public class OrderCreateRequest
    {

        /// <summary>
        /// ИД клиента
        /// </summary>
        [DisplayName("ИД клиента")]
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// ИД способа получения заказа
        /// </summary>
        [DisplayName("ИД способа получения заказа")]
        [Required]
        public int ReceiveMethodId { get; set; }

        /// <summary>
        /// Блюда заказа
        /// </summary>
        [DisplayName("Блюда заказа")]
        public List<OrderDishCreateRequest> OrdersDishesList { get; set; }
    }
}
