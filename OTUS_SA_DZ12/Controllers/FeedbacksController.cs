using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_Domain.Robots;
using OTUS_SA_DZ12_Models.RobotsModels.Feedback;
using System.Net;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    /// <summary>
    /// Отзывы
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Tags("Отзывы")]
    public class FeedbacksController : ControllerBase
    {

        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IOrderDishRepository _orderDishRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера работы с отзывами
        /// </summary>
        /// <param name="dishRepository">Репозиторий работы со справочником блюд</param>
        /// <param name="mapper">Маппер сущностей одна в другую</param>
        public FeedbacksController(IFeedbackRepository feedbackRepository, IOrderRepository orderRepository, IDishRepository dishRepository
            , IOrderDishRepository orderDishRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _orderRepository = orderRepository;
            _dishRepository = dishRepository;
            _orderDishRepository = orderDishRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Создание нового отзыва на блюдо заказа
        /// </summary>
        /// <param name="request">Параметры создаваемого отзыва - объект типа OrderFeedbackRequest</param>
        /// <returns>Возвращает созданый отзыв - объект типа OrderFeedbackResponse</returns>
        /// <response code="201">Успешное выполнение. Отзыв создан</response>
        /// <response code="400">Не удалось добавить отзыв. Причина описана в ответе</response>  
        /// <response code="404">Не найдено блюдо в заказе</response>  
        [HttpPost]
        [ProducesResponseType(typeof(OrderFeedbackResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderFeedbackResponse>> CreateFeedbackAsync(OrderFeedbackRequest request)
        {
            var foundFeedback = await _feedbackRepository.GetFeedbackByOrderIdAndDishIdAsync(request.OrderId, request.DishId);
            if (foundFeedback != null)
            {
                return BadRequest("Отзыв на заказ с ИД = " + request.OrderId.ToString() + " и блюдо в нём с ИД = " + request.DishId.ToString() +
                    " уже существует. ИД отзыва = " + foundFeedback.Id.ToString());
            }

            var foundOrder = await _orderRepository.GetByIdAsync(request.OrderId);
            if (foundOrder == null)
            {
                return BadRequest("Заказ с ИД = " + request.OrderId.ToString() + " не найден");
            }

            var dishOrder = await _dishRepository.GetByIdAsync(request.DishId);
            if (dishOrder == null)
            {
                return BadRequest("Блюдо с ИД = " + request.DishId.ToString() + " не найдено");
            }

            var foundOrderDish = await _orderDishRepository.GetOrderDishByOrderIdAndDishIdAsync(request.OrderId, request.DishId);
            if (foundOrderDish == null)
                return NotFound("Блюдо с ИД = " + request.DishId.ToString() + " не найдено в заказе " + request.OrderId.ToString());


            var addedFeedback = await _feedbackRepository.AddAsync(
                new Feedback
                {
                    OrderId = request.OrderId,
                    DishId = request.DishId,
                    FeedbackText = request.FeedbackText,
                    Stars = request.Stars
                });

            var routVar = "";
            if (Request != null)
            {
                routVar = new UriBuilder(Request.Scheme, Request.Host.Host, (int)Request.Host.Port, Request.Path.Value).ToString()
                    + "/" + addedFeedback.Id.ToString();
            }
            return Created(routVar, _mapper.Map<Feedback, OrderFeedbackResponse>(addedFeedback));
        }


        /// <summary>
        /// Получить отзыв по ИД отзыва
        /// </summary>
        /// <param name="id">ИД отзыва</param>        
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка при выполнении запроса к БД</response>
        /// <response code="404">Отзыв с заданным ИД не найден</response>
        /// <returns>Возвращает отзыв - объект типа OrderFeedbackResponse</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderFeedbackResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderFeedbackResponse>> GetFeedbackByIdAsync(int id)
        {
            try
            {
                var gotFeedback = await _feedbackRepository.GetByIdAsync(id);
                if (gotFeedback == null)
                {
                    return NotFound("Отзыв с ИД = " + id.ToString() + " не найден в БД");
                }
                else
                    return Ok(_mapper.Map<Feedback, OrderFeedbackResponse>(gotFeedback));
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }
    }
}
