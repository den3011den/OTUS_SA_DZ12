using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_Domain.Robots;
using OTUS_SA_DZ12_Models.RobotsModels.Dish;
using System.Net;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    /// <summary>
    /// Блюда
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Tags("Справочник блюд")]
    public class DishesController : ControllerBase
    {

        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера работы со справочником блюд
        /// </summary>
        /// <param name="dishRepository">Репозиторий работы со справочником блюд</param>
        /// <param name="mapper">Маппер сущностей одна в другую</param>
        public DishesController(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех блюд
        /// </summary>        
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка при выполнении запроса к БД</response>
        /// <returns>Возвращает список блюд - объекты типа DishResponse</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(List<DishResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<DishResponse>>> GetAllDishesAsync()
        {
            try
            {
                var gotDishes = await _dishRepository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<Dish>, IEnumerable<DishResponse>>(gotDishes));
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }
    }
}
