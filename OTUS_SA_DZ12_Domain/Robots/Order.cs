namespace OTUS_SA_DZ12_Domain.Robots
{

    /// <summary>
    /// Заказы
    /// </summary>
    public class Order : BaseEntity
    {

        /// <summary>
        /// Дата/время заказа
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// ИД статуса заказа
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public virtual State State { get; set; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// ИД клиента
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        public virtual Customer? Customer { get; set; }


        /// <summary>
        /// ИД способа получения заказа
        /// </summary>
        public int ReceiveMethodId { get; set; }

        /// <summary>
        /// Способ получения заказа
        /// </summary>
        public virtual ReceiveMethod ReceiveMethod { get; set; }

        /// <summary>
        /// Дата/время получения заказа
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        public virtual ICollection<OrderDish> OrdersDishesList { get; set; }
        public virtual ICollection<Feedback> FeedbacksList { get; set; }

    }
}
