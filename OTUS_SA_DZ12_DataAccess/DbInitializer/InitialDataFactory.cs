using OTUS_SA_DZ12_Domain.Robots;

namespace OTUS_SA_DZ12_DataAccess.DbInitializer
{

    /// <summary>
    /// Данные для начального заполенния БД
    /// </summary>
    public static class InitialDataFactory
    {
        public static List<State> States => new List<State>()
        {
            new State()
            {
                Id = 1,
            },

            new State()
            {
                Id = 2,

            },
        };

        public static List<Dish> Dishes => new List<Dish>()
        {
            new Dish()
            {
                Id = 1,
            },

            new Dish()
            {
                Id = 2,

            },
        };

        public static List<ReceiveMethod> ReceiveMethods => new List<ReceiveMethod>()
        {
            new ReceiveMethod()
            {
                Id = 1,
            },

            new ReceiveMethod()
            {
                Id = 2,

            },
        };

        public static List<Customer> Customers => new List<Customer>()
        {
            new Customer()
            {
                Id = 1,
            },

            new Customer()
            {
                Id = 2,

            },
        };

        public static List<Order> Orders => new List<Order>()
        {
            new Order()
            {
                Id = 1,
            },

            new Order()
            {
                Id = 2,

            },
        };

        public static List<OrderDish> OrdersDishes => new List<OrderDish>()
        {
            new OrderDish()
            {
                Id = 1,
            },

            new OrderDish()
            {
                Id = 2,
            },
        };

        public static List<Feedback> Feedbacks => new List<Feedback>()
        {
            new Feedback()
            {
                Id = 1,
            },

            new Feedback()
            {
                Id = 2,
            },
        };
    }
}
