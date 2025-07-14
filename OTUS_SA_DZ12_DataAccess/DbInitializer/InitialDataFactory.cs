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
                Name = "Создан",
                IsArchive = false,
            },

            new State()
            {
                Id = 2,
                Name = "Оплачен",
                IsArchive = false,
            },
            new State()
            {
                Id = 3,
                Name = "В производстве",
                IsArchive = false,
            },
            new State()
            {
                Id = 4,
                Name = "Приготовлен",
                IsArchive = false,
            },
            new State()
            {
                Id = 5,
                Name = "На выдаче",
                IsArchive = false,
            },
            new State()
            {
                Id = 6,
                Name = "Получен",
                IsArchive = false,
            },
            new State()
            {
                Id = 7,
                Name = "Самоотказ",
                IsArchive = false,
            },
            new State()
            {
                Id = 8,
                Name = "В доставке",
                IsArchive = true,
            },
            new State()
            {
                Id = 9,
                Name = "Доставлен",
                IsArchive = true,
            },
        };

        public static List<Dish> Dishes
        {
            get
            {
                var dishInstanceList = new List<Dish>();
                int notArchiveDishes = 20;
                int archiveDishes = 10;
                for (int i = 1; i <= notArchiveDishes; i++)
                {
                    dishInstanceList.Add(
                        new Dish()
                        {
                            Id = i,
                            Name = "Блюдо " + i.ToString(),
                            Description = "Описание блюда " + i.ToString(),
                            IsArchive = false,
                        }
                        );
                }
                for (int i = notArchiveDishes + 1; i <= (notArchiveDishes + archiveDishes); i++)
                {
                    dishInstanceList.Add(
                        new Dish()
                        {
                            Id = i,
                            Name = "Блюдо " + i.ToString(),
                            Description = "Описание блюда " + i.ToString(),
                            IsArchive = true,
                        }
                        );
                }
                return dishInstanceList;
            }
        }

        public static List<ReceiveMethod> ReceiveMethods => new List<ReceiveMethod>()
        {
            new ReceiveMethod()
            {
                Id = 1,
                Name = "В зале",
                IsArchive = false
            },

            new ReceiveMethod()
            {
                Id = 2,
                Name = "С собой",
                IsArchive = false
            },

            new ReceiveMethod()
            {
                Id = 3,
                Name = "Доставка",
                IsArchive = true
            },
        };

        public static List<Customer> Customers
        {
            get
            {
                var customerInstanceList = new List<Customer>();
                int notArchiveCustomers = 20;
                int archiveCustomers = 10;
                for (int i = 1; i <= notArchiveCustomers; i++)
                {
                    customerInstanceList.Add(
                        new Customer()
                        {
                            Id = i,
                            FirstName = "Имя " + i.ToString(),
                            LastName = "Фамилия " + i.ToString(),
                            MiddleName = "Отчество " + i.ToString(),
                            Address = "Адрес " + i.ToString(),
                            PhoneNumber = "+7-" + i.ToString() + i.ToString() + i.ToString() + "-" + i.ToString() + i.ToString() + i.ToString()
                                 + "-" + i.ToString() + i.ToString() + "-" + i.ToString() + i.ToString(),
                            Email = "address" + i.ToString() + "@" + "server" + i.ToString() + ".com",
                            AddDate = DateTime.Now,
                            IsBonusParticipant = (i % 2 == 0) ? true : false,
                            IsArchive = false,
                        }
                        );
                }
                for (int i = notArchiveCustomers + 1; i <= (notArchiveCustomers + archiveCustomers); i++)
                {
                    customerInstanceList.Add(
                        new Customer()
                        {
                            Id = i,
                            FirstName = "Имя " + i.ToString(),
                            LastName = "Фамилия " + i.ToString(),
                            MiddleName = "Отчество " + i.ToString(),
                            Address = "Адрес " + i.ToString(),
                            PhoneNumber = "+7-" + i.ToString() + i.ToString() + i.ToString() + "-" + i.ToString() + i.ToString() + i.ToString()
                                 + "-" + i.ToString() + i.ToString() + "-" + i.ToString() + i.ToString(),
                            Email = "address" + i.ToString() + "@" + "server" + i.ToString() + ".com",
                            AddDate = DateTime.Now,
                            IsBonusParticipant = (i % 2 == 0) ? true : false,
                            IsArchive = true,
                        }
                        );
                }
                return customerInstanceList;
            }
        }

        public static List<Order> Orders => new List<Order>()
        {
            new Order()
            {
                Id = 1,
                OrderDate = DateTime.Now.AddDays(-10),
                StateId = 6,
                Amount = 100.00,
                CustomerId = 1,
                ReceiveMethodId = 1,
                ReceiveDate = DateTime.Now.AddDays(-10).AddHours(1)
            },
            new Order()
            {
                Id = 2,
                OrderDate = DateTime.Now.AddDays(-9),
                StateId = 6,
                Amount = 1000.00,
                CustomerId = 1,
                ReceiveMethodId = 1,
                ReceiveDate = DateTime.Now.AddDays(-9).AddHours(1)
            },
            new Order()
            {
                Id = 3,
                OrderDate = DateTime.Now.AddDays(-8),
                StateId = 6,
                Amount = 500.00,
                CustomerId = 1,
                ReceiveMethodId = 1,
                ReceiveDate = DateTime.Now.AddDays(-8).AddHours(1)
            },
            new Order()
            {
                Id = 4,
                OrderDate = DateTime.Now.AddDays(-7),
                StateId = 6,
                Amount = 580.10,
                CustomerId = 1,
                ReceiveMethodId = 2,
                ReceiveDate = DateTime.Now.AddDays(-7).AddHours(1)
            },
            new Order()
            {
                Id = 5,
                OrderDate = DateTime.Now.AddDays(-6),
                StateId = 1,
                Amount = 2500.10,
                CustomerId = 1,
                ReceiveMethodId = 2
            },
            new Order()
            {
                Id = 6,
                OrderDate = DateTime.Now.AddDays(-5),
                StateId = 6,
                Amount = 100.00,
                CustomerId = 3,
                ReceiveMethodId = 1,
                ReceiveDate = DateTime.Now.AddDays(-5).AddHours(1)
            },
            new Order()
            {
                Id = 7,
                OrderDate = DateTime.Now.AddDays(-4),
                StateId = 6,
                Amount = 1000.00,
                CustomerId = 3,
                ReceiveMethodId = 1,
                ReceiveDate = DateTime.Now.AddDays(-4).AddHours(1)
            },
            new Order()
            {
                Id = 8,
                OrderDate = DateTime.Now.AddDays(-3),
                StateId = 6,
                Amount = 500.00,
                CustomerId = 3,
                ReceiveMethodId = 1,
                ReceiveDate = DateTime.Now.AddDays(-3).AddHours(1)
            },
            new Order()
            {
                Id = 9,
                OrderDate = DateTime.Now.AddMinutes(-90),
                StateId = 3,
                Amount = 580.10,
                CustomerId = 3,
                ReceiveMethodId = 2
            },
            new Order()
            {
                Id = 10,
                OrderDate = DateTime.Now.AddMinutes(-100),
                StateId = 3,
                Amount = 2500.10,
                CustomerId = 5,
                ReceiveMethodId = 2
            },
        };

        public static List<OrderDish> OrdersDishes => new List<OrderDish>()
        {
            new OrderDish()
            {
                Id = 1,
                OrderId = 1,
                DishId = 13,
                Quantity = 1,
                Price = 100
            },

            new OrderDish()
            {
                Id = 2,
                OrderId = 2,
                DishId = 3,
                Quantity = 1,
                Price = 500
            },
            new OrderDish()
            {
                Id = 3,
                OrderId = 2,
                DishId = 13,
                Quantity = 2,
                Price = 100
            },
            new OrderDish()
            {
                Id = 4,
                OrderId = 2,
                DishId = 14,
                Quantity = 2,
                Price = 100
            },
            new OrderDish()
            {
                Id = 5,
                OrderId = 2,
                DishId = 6,
                Quantity = 5,
                Price = 10
            },
            new OrderDish()
            {
                Id = 7,
                OrderId = 2,
                DishId = 8,
                Quantity = 3,
                Price = 10
            },
            new OrderDish()
            {
                Id = 8,
                OrderId = 2,
                DishId = 9,
                Quantity = 2,
                Price = 10
            },
            new OrderDish()
            {
                Id = 9,
                OrderId = 3,
                DishId = 17,
                Quantity = 1,
                Price = 500
            },
            new OrderDish()
            {
                Id = 10,
                OrderId = 4,
                DishId = 17,
                Quantity = 1,
                Price = 500
            },
            new OrderDish()
            {
                Id = 11,
                OrderId = 4,
                DishId = 11,
                Quantity = 1,
                Price = 50
            },
            new OrderDish()
            {
                Id = 12,
                OrderId = 4,
                DishId = 6,
                Quantity = 1,
                Price = 10
            },
            new OrderDish()
            {
                Id = 13,
                OrderId = 4,
                DishId = 7,
                Quantity = 2,
                Price = 10
            },
            new OrderDish()
            {
                Id = 14,
                OrderId = 4,
                DishId = 1,
                Quantity = 1,
                Price = 0.10
            },
            new OrderDish()
            {
                Id = 15,
                OrderId = 5,
                DishId = 20,
                Quantity = 1,
                Price = 2000
            },
            new OrderDish()
            {
                Id = 16,
                OrderId = 5,
                DishId = 16,
                Quantity = 1,
                Price = 500
            },
            new OrderDish()
            {
                Id = 17,
                OrderId = 5,
                DishId = 1,
                Quantity = 1,
                Price = 0.1
            },



            new OrderDish()
            {
                Id = 18,
                OrderId = 6,
                DishId = 13,
                Quantity = 1,
                Price = 100
            },

            new OrderDish()
            {
                Id = 19,
                OrderId = 7,
                DishId = 3,
                Quantity = 1,
                Price = 500
            },
            new OrderDish()
            {
                Id = 20,
                OrderId = 7,
                DishId = 13,
                Quantity = 2,
                Price = 100
            },
            new OrderDish()
            {
                Id = 21,
                OrderId = 7,
                DishId = 14,
                Quantity = 2,
                Price = 100
            },
            new OrderDish()
            {
                Id = 22,
                OrderId = 7,
                DishId = 6,
                Quantity = 5,
                Price = 10
            },
            new OrderDish()
            {
                Id = 23,
                OrderId = 7,
                DishId = 8,
                Quantity = 3,
                Price = 10
            },
            new OrderDish()
            {
                Id = 24,
                OrderId = 7,
                DishId = 9,
                Quantity = 2,
                Price = 10
            },
            new OrderDish()
            {
                Id = 25,
                OrderId = 8,
                DishId = 17,
                Quantity = 1,
                Price = 500
            },
            new OrderDish()
            {
                Id = 26,
                OrderId = 9,
                DishId = 17,
                Quantity = 1,
                Price = 500
            },
            new OrderDish()
            {
                Id = 27,
                OrderId = 9,
                DishId = 11,
                Quantity = 1,
                Price = 50
            },
            new OrderDish()
            {
                Id = 28,
                OrderId = 9,
                DishId = 6,
                Quantity = 1,
                Price = 10
            },
            new OrderDish()
            {
                Id = 29,
                OrderId = 9,
                DishId = 7,
                Quantity = 2,
                Price = 10
            },
            new OrderDish()
            {
                Id = 30,
                OrderId = 9,
                DishId = 1,
                Quantity = 1,
                Price = 0.10
            },
            new OrderDish()
            {
                Id = 31,
                OrderId = 10,
                DishId = 20,
                Quantity = 1,
                Price = 2000
            },
            new OrderDish()
            {
                Id = 32,
                OrderId = 10,
                DishId = 16,
                Quantity = 1,
                Price = 500
            },
            new OrderDish()
            {
                Id = 33,
                OrderId = 10,
                DishId = 1,
                Quantity = 1,
                Price = 0.1
            },


        };

        public static List<Feedback> Feedbacks => new List<Feedback>()
        {
            new Feedback()
            {
                Id = 1,
                OrderId = 1,
                DishId = 13,
                FeedbackText = "Текст отзыва на заказ 1 блюдо 13",
                Stars = 5,
            },

            new Feedback()
            {
                Id = 2,
                OrderId = 2,
                DishId = 13,
                FeedbackText = "Текст отзыва на заказ 2 блюдо 13",
                Stars = 5,
            },
            new Feedback()
            {
                Id = 3,
                OrderId = 2,
                DishId = 14,
                FeedbackText = "Текст отзыва на заказ 2 блюдо 14",
                Stars = 4,
            },

            new Feedback()
            {
                Id = 4,
                OrderId = 2,
                DishId = 6,
                FeedbackText = "Текст отзыва на заказ 2 блюдо 6",
                Stars = 5,
            },

            new Feedback()
            {
                Id = 5,
                OrderId = 7,
                DishId = 13,
                FeedbackText = "Текст отзыва на заказ 7 блюдо 13",
                Stars = 5,
            },

            new Feedback()
            {
                Id = 6,
                OrderId = 7,
                DishId = 14,
                FeedbackText = "Текст отзыва на заказ 7 блюдо 14",
                Stars = 4,
            },

            new Feedback()
            {
                Id = 7,
                OrderId = 7,
                DishId = 6,
                FeedbackText = "Текст отзыва на заказ 7 блюдо 6",
                Stars = 3,
            },
        };
    }
}
