using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_Domain.Robots;
using OTUS_SA_DZ12_Models.RobotsModels.Order;
using OTUS_SA_DZ12_Models.RobotsModels.OrderDish;
using OTUS_SA_DZ12_Models.RobotsModels.State;
using System.Net;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    /// <summary>
    /// Заказы
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Tags("Заказы")]
    public class OrdersController : ControllerBase
    {

        private readonly IStateRepository _stateRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDishRepository _orderDishRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IReceiveMethodRepository _receiveMethodRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера работы с заказами
        /// </summary>
        /// <param name="orderRepository">Репозиторий работы с заказами</param>
        /// <param name="stateRepository">Репозиторий работы со справочником статусов заказов</param>
        /// <param name="mapper">Маппер сущностей одна в другую</param>
        public OrdersController(IOrderRepository orderRepository, IStateRepository stateRepository
            , IOrderDishRepository orderDishRepository
            , ICustomerRepository customerRepository
            , IDishRepository dishRepository
            , IReceiveMethodRepository receiveMethodRepository
            , IMapper mapper)
        {
            _orderRepository = orderRepository;
            _stateRepository = stateRepository;
            _orderDishRepository = orderDishRepository;
            _customerRepository = customerRepository;
            _dishRepository = dishRepository;
            _receiveMethodRepository = receiveMethodRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Получить заказ по ИД заказа
        /// </summary>
        /// <param name="id">ИД заказа (целочисленный номер заказа)</param>        
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка при выполнении запроса к БД</response>
        /// <response code="404">Заказ с заданным ИД не найден</response>
        /// <returns>Возвращает заказ - объект типа OrderResponse</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResponse>> GetOrderByIdAsync(int id)
        {
            try
            {
                var gotOrder = await _orderRepository.GetByIdAsync(id);
                if (gotOrder == null)
                {
                    return NotFound("Заказ с ИД = " + id.ToString() + " не найден в БД");
                }
                else
                    return Ok(_mapper.Map<Order, OrderResponse>(gotOrder));
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }


        /// <summary>
        /// Получить статус заказа по ИД заказа
        /// </summary>
        /// <param name="id">ИД заказа (целочисленный номер заказа)</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка при выполнении запроса к БД</response>
        /// <response code="404">Заказ с заданным ИД не найден</response>
        /// <returns>Возвращает статус заказа - объект типа StateResponse</returns>
        [HttpGet("{id}/status")]
        [ProducesResponseType(typeof(StateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<StateResponse>> GetOrderStateByOrderIdAsync(int id)
        {
            try
            {
                var gotState = await _orderRepository.GetOrderStateByOrderIdAsync(id);
                if (gotState == null)
                {
                    return NotFound("Заказ с ИД = " + id.ToString() + " не найден в БД");
                }
                else
                    return Ok(_mapper.Map<State, StateResponse>(gotState));
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }


        /// <summary>
        /// Просмотр истории заказов
        /// </summary>
        /// <param name="orderById">Указывает упорядочивание по ИД заказа - ASC-по возрастанию номера заказа, DESC-по убыванию номера заказа. По умолчанию - АSC </param>
        /// <param name="itemsOnPage">Количество заказов на странице при постаничном запросе. Если itemsOnPage и pageNumber оба равны нулю - выдаются все заказы</param>
        /// <param name="pageNumber">Номер запрашиваемой страницы при постаничном запросе. Если itemsOnPage и pageNumber оба равны нулю - выдаются все заказы</param>
        /// <returns>Возвращает список заказов с учётом указанных при запросе параметров - список объектов типа OrderResponse</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<OrderResponse>>> GetAllOrderAsync(string orderById = "ASC", int itemsOnPage = 0, int pageNumber = 0)
        {
            try
            {
                if (itemsOnPage < 0 || pageNumber < 0)
                    return BadRequest("Один из параметров itemsOnPage или pageNumber меньше нуля");

                var gotOrders = await _orderRepository.GetAllAsync();
                if (gotOrders == null)
                {
                    return NotFound("Не найдено ни одного заказа");
                }

                // Всё что ниже - Так делать не правильно. Но задание по сис анализу и пректированию API, а не по программированию

                string orderByIdUpper = orderById.Trim().ToUpper();

                if (orderByIdUpper != "DESC" && orderByIdUpper != "ASC")
                    return BadRequest("Параметр orderById может принимать только значения ASC или DESC");

                if (orderByIdUpper == "DESC")
                    gotOrders = gotOrders.OrderByDescending(o => o.Id);
                else
                    gotOrders = gotOrders.OrderBy(o => o.Id);

                if (itemsOnPage > 0 || pageNumber > 0)
                {
                    int lostItemsCount = (pageNumber - 1) * itemsOnPage;
                    if (lostItemsCount > gotOrders.Count())
                        return NotFound("Нет элементов на заданной странице. Попробуйте меньший номер страницы");

                    List<Order> gotOrdersFiltered = new List<Order>();
                    int itemsCount = 0;
                    int itemsAdded = 0;
                    foreach (var order in gotOrders)
                    {
                        if (itemsCount < lostItemsCount)
                            itemsCount++;
                        else
                        {
                            gotOrdersFiltered.Add(order);
                            itemsAdded++;
                            if (itemsAdded >= itemsOnPage)
                                break;
                        }
                    }

                    if (gotOrdersFiltered.Count() <= 0)
                    {
                        return NotFound("Не найдено элементов с заданными условиями");
                    }
                    else
                    {
                        return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponse>>(gotOrdersFiltered));
                    }
                }

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponse>>(gotOrders));
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }

        /// <summary>
        /// Повторение заказа
        /// </summary>
        /// <param name="id">ИД повторяемого заказа</param>
        /// <response code="201">Успешное выполнение. Заказ создан</response>
        /// <response code="400">Не удалось добавить заказ. Причина описана в ответе</response>  
        /// <response code="404">Не найден заказ с заданным ИД</response>  
        /// <returns>Возвращает созданый заказ - объект типа OrderResponse</returns>
        [HttpPost("id")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResponse>> RepeatOrderAsync(int id)
        {
            var foundOrder = await _orderRepository.GetByIdAsync(id);
            if (foundOrder == null)
            {
                return NotFound("Заказ с ИД = " + id.ToString() + " не найден");
            }

            Order orderForAdding = new Order();

            orderForAdding.OrderDate = DateTime.Now;
            orderForAdding.StateId = 1;
            orderForAdding.State = await _stateRepository.GetByIdAsync(1);
            orderForAdding.Amount = foundOrder.Amount;
            orderForAdding.CustomerId = foundOrder.CustomerId;
            orderForAdding.Customer = foundOrder.Customer;
            orderForAdding.ReceiveMethodId = foundOrder.ReceiveMethodId;
            orderForAdding.ReceiveMethod = foundOrder.ReceiveMethod;

            var addedOrder = await _orderRepository.AddAsync(orderForAdding);
            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedOrder.Id.ToString();
                routVar = routVar.Replace("/id", "");
            }

            foreach (OrderDish orderDish in foundOrder.OrdersDishesList)
            {
                await _orderDishRepository.AddAsync(
                    new OrderDish
                    {
                        OrderId = addedOrder.Id,
                        DishId = orderDish.DishId,
                        Dish = orderDish.Dish,
                        Quantity = orderDish.Quantity,
                        Price = orderDish.Price
                    });
            }
            //await Task.Delay(2000);            

            var gotOrder = await _orderRepository.GetByIdAsync(addedOrder.Id);
            return Created(routVar, _mapper.Map<Order, OrderResponse>(gotOrder));
        }


        /// <summary>
        /// Создание заказа
        /// </summary>        
        /// <param name="request">Данные создаваемого заказа - объект типа OrderCreateRequest</param>
        /// <response code="201">Успешное выполнение. Заказ создан</response>
        /// <response code="400">Не удалось создать заказ. Причина описана в ответе</response>  
        /// <response code="404">Не найдена одна из составляющих заказа</response>  
        /// <returns>Возвращает созданый заказ - объект типа OrderResponse</returns>
        [HttpPost()]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderResponse>> CreateOrderAsync(OrderCreateRequest request)
        {

            Customer? foundCustomer = null;
            if (request.CustomerId != null && request.CustomerId != 0)
            {
                foundCustomer = await _customerRepository.GetByIdAsync((int)request.CustomerId);
                if (foundCustomer == null)
                {
                    return NotFound("Не найден клиент с ИД = " + request.CustomerId.ToString());
                }

                if (foundCustomer.IsArchive)
                    return BadRequest("Клиент с ИД = " + foundCustomer.Id.ToString() + " удалён в архив в справочнике клиентов");
            }
            else
            {
                foundCustomer = null;
            }



            var foundReceiveMethod = await _receiveMethodRepository.GetByIdAsync(request.ReceiveMethodId);
            if (foundReceiveMethod == null)
            {
                return NotFound("Не найден способ получения с ИД = " + request.CustomerId.ToString());
            }

            if (foundReceiveMethod.IsArchive)
                return BadRequest("Способ получения с ИД = " + foundReceiveMethod.Id.ToString() + " удалён в архив в справочнике способов получения заказов");

            double summaDishes = 0;

            if (request.OrdersDishesList == null || request.OrdersDishesList.Count() <= 0)
            {
                return BadRequest("Нет блюд в заказе");
            }

            foreach (var orderDish in request.OrdersDishesList)
            {
                var foundOrderDish = await _dishRepository.GetByIdAsync(orderDish.DishId);
                if (foundOrderDish == null)
                    return NotFound("Не найдено блюдо с ИД = " + orderDish.DishId.ToString());
                if (foundOrderDish.IsArchive)
                    return BadRequest("Блюдо с ИД = " + orderDish.DishId.ToString() + " удалено в архив в справочнике блюд");
                if (orderDish.Quantity <= 0)
                    return BadRequest("У блюда с ИД = " + orderDish.DishId.ToString() + " установлено нулевое или отрицательное количество");
                if (orderDish.Price <= 0)
                    return BadRequest("У блюда с ИД = " + orderDish.DishId.ToString() + " установлена нулевая или отрицательная цена");
                summaDishes = summaDishes + (orderDish.Quantity * orderDish.Price);
            }

            var queryDuplicate = request.OrdersDishesList
                .GroupBy(e => e.DishId)
                .Where(g => g.Count() > 1)
                .Select(g => new { DishId = g.Key, Count = g.Count() })
                .ToList();

            if (queryDuplicate != null && queryDuplicate.Count() > 0)
            {
                string duplStr = "";
                foreach (var query in queryDuplicate)
                    duplStr = duplStr + " Блюдо с ИД = " + query.DishId.ToString() + " встречается " + query.Count.ToString() + " раза\n";
                return BadRequest("В заказе есть повторяющиеся блюда:\n" + duplStr);
            }

            Order orderForAdding = new Order();

            orderForAdding.OrderDate = DateTime.Now;
            orderForAdding.StateId = 1;
            orderForAdding.State = await _stateRepository.GetByIdAsync(1);
            orderForAdding.Amount = summaDishes;
            if (foundCustomer != null)
            {
                orderForAdding.CustomerId = foundCustomer.Id;
                orderForAdding.Customer = foundCustomer;
            }
            orderForAdding.ReceiveMethodId = foundReceiveMethod.Id;
            orderForAdding.ReceiveMethod = foundReceiveMethod;

            var addedOrder = await _orderRepository.AddAsync(orderForAdding);
            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedOrder.Id.ToString();
                routVar = routVar.Replace("/id", "");
            }

            foreach (OrderDishCreateRequest orderDish in request.OrdersDishesList)
            {
                await _orderDishRepository.AddAsync(
                    new OrderDish
                    {
                        OrderId = addedOrder.Id,
                        DishId = orderDish.DishId,
                        Dish = await _dishRepository.GetByIdAsync(orderDish.DishId),
                        Quantity = orderDish.Quantity,
                        Price = orderDish.Price
                    });
            }

            var gotOrder = await _orderRepository.GetByIdAsync(addedOrder.Id);
            return Created(routVar, _mapper.Map<Order, OrderResponse>(gotOrder));
        }
    }
}
