using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OTUS_SA_DZ12_Business.Repository.IRepository;
using OTUS_SA_DZ12_Domain.Robots;
using OTUS_SA_DZ12_Models.RobotsModels.Customers;
using System.Net;

namespace OTUS_SA_DZ12_WebAPI.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Tags("Клиенты")]
    public class CustomersController : ControllerBase
    {


        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера работы с клиентами
        /// </summary>
        /// <param name="customerRepository">Репозиторий работы с клиентами</param>
        /// <param name="mapper">Маппер сущностей одна в другую</param>
        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех клиентов
        /// </summary>        
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка при выполнении запроса к БД</response>
        /// <returns>Возвращает список всех Клиентов - объекты типа CustomerResponse</returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<CustomerResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<CustomerResponse>>> GetAllCustomersAsync()
        {
            try
            {
                var gotCustomers = await _customerRepository.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResponse>>(gotCustomers));
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }
    }
}