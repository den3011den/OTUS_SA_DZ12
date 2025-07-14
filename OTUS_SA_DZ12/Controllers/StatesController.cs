using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_Domain.Robots;
using OTUS_SA_DZ12_Models.RobotsModels.State;
using System.Net;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    /// <summary>
    /// Статусы заказов
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Tags("Справочник статусов заказа")]
    public class StatesController : ControllerBase
    {

        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера работы со справочником статусов заказов
        /// </summary>
        /// <param name="stateRepository">Репозиторий работы со справочником статусов заказов</param>
        /// <param name="mapper">Маппер сущностей одна в другую</param>
        public StatesController(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех статусов
        /// </summary>
        /// <returns>Возвращает список всех статусов - объекты типа StateResponse</returns>        
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка при выполнении запроса к БД</response>        
        [HttpGet()]
        [ProducesResponseType(typeof(List<StateResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<StateResponse>>> GetAllStatesAsync()
        {
            try
            {
                var gotStates = await _stateRepository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<State>, IEnumerable<StateResponse>>(gotStates));
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }
    }
}
