using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_Domain.Robots;
using OTUS_SA_DZ12_Models.RobotsModels.Order;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера работы с заказами
        /// </summary>
        /// <param name="orderRepository">Репозиторий работы с заказами</param>
        /// <param name="stateRepository">Репозиторий работы со справочником статусов заказов</param>
        /// <param name="mapper">Маппер сущностей одна в другую</param>
        public OrdersController(IOrderRepository orderRepository, IStateRepository stateRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _stateRepository = stateRepository;
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
    }
}
