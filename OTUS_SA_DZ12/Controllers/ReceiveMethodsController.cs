using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_Domain.Robots;
using OTUS_SA_DZ12_Models.RobotsModels.ReceiveMethod;
using System.Net;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{

    /// <summary>
    /// Способы получения заказа
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Tags("Справочник способов получения заказов")]
    public class ReceiveMethodsController : ControllerBase
    {

        private readonly IReceiveMethodRepository _receiveMethodRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера работы со справочником способов получения заказов
        /// </summary>
        /// <param name="receiveMethodsRepository">Репозиторий работы со справочником  способов получения  заказов</param>
        /// <param name="mapper">Маппер сущностей одна в другую</param>
        public ReceiveMethodsController(IReceiveMethodRepository receiveMethodRepository, IMapper mapper)
        {
            _receiveMethodRepository = receiveMethodRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех способов получения заказов
        /// </summary>        
        /// <returns>Возвращает список всех способов получения заказов - объекты типа ReceiveMethodResponse</returns>           
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Не удалось выполнить запрос. Ошибка доступа к БД или неверные параметры запроса. Причина описана в ответе</response>               
        [HttpGet()]
        [ProducesResponseType(typeof(List<ReceiveMethodResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<ReceiveMethodResponse>>> GetAllReceiveMethodsAsync()
        {
            try
            {
                var gotReceiveMethods = await _receiveMethodRepository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<ReceiveMethod>, IEnumerable<ReceiveMethodResponse>>(gotReceiveMethods));
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }
    }
}

