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
        /// Получить список клиентов
        /// </summary>
        /// <param name="nameSubstringFilter">Позволяет фильтровать список клиентов по нахождению указанной в данном параметре подстроки в Фамилии (LastName), Имени (FirstName) и Отчестве (MiddleName). Если параметр пустой фильтрация не производится</param>
        /// <param name="orderByName">Указывает упорядочивание по наименованию клиента (FirstName + LastName + MiddleName) - ASC-по возрастанию , DESC-по убыванию. По умолчанию - АSC </param>
        /// <param name="itemsOnPage">Количество клиентов на странице при постаничном запросе. Если itemsOnPage и pageNumber оба равны нулю - выдаются все клиенты</param>
        /// <param name="pageNumber">Номер запрашиваемой страницы при постаничном запросе. Если itemsOnPage и pageNumber оба равны нулю - выдаются все клиенты</param>
        /// <returns>Возвращает список Клиентов - объекты типа CustomerResponse</returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Не удалось выполнить запрос. Ошибка доступа к БД или неверные параметры запроса. Причина описана в ответе</response>  
        /// <response code="404">Не найдено элементов соответствующих заданным условиям запроса</response>          
        [HttpGet()]
        [ProducesResponseType(typeof(List<CustomerResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<CustomerResponse>>> GetAllCustomersAsync(string nameSubstringFilter = "", string orderByName = "ASC", int itemsOnPage = 0, int pageNumber = 0)
        {

            try
            {
                if (itemsOnPage < 0 || pageNumber < 0)
                    return BadRequest("Один из параметров itemsOnPage или pageNumber меньше нуля");


                var gotCustomers = await _customerRepository.GetCustomersWithNameSubstringFilterAsync(nameSubstringFilter);
                if (gotCustomers == null || gotCustomers.Count() <= 0)
                {
                    return NotFound("Не найдено ни одного клиента с заданными параметрами");
                }

                // Всё что ниже - Так делать не правильно. Но задание по сис анализу и пректированию API, а не по программированию

                string orderByNameUpper = orderByName.Trim().ToUpper();

                if (orderByNameUpper != "DESC" && orderByNameUpper != "ASC")
                    return BadRequest("Параметр orderByName может принимать только значения ASC или DESC");

                if (orderByNameUpper == "DESC")
                    gotCustomers = gotCustomers.OrderByDescending(o => o.FirstName.ToUpper()).ThenByDescending(o => o.LastName.ToUpper()).ThenByDescending(o => o.MiddleName.ToUpper());
                else
                    gotCustomers = gotCustomers.OrderBy(o => o.FirstName.ToUpper()).ThenBy(o => o.LastName.ToUpper()).ThenBy(o => o.MiddleName.ToUpper());

                if (itemsOnPage > 0 || pageNumber > 0)
                {
                    int lostItemsCount = (pageNumber - 1) * itemsOnPage;
                    if (lostItemsCount > gotCustomers.Count())
                        return NotFound("Нет элементов на заданной странице. Попробуйте меньший номер страницы");

                    List<Customer> gotCustomersFiltered = new List<Customer>();
                    int itemsCount = 0;
                    int itemsAdded = 0;
                    foreach (var customer in gotCustomers)
                    {
                        if (itemsCount < lostItemsCount)
                            itemsCount++;
                        else
                        {
                            gotCustomersFiltered.Add(customer);
                            itemsAdded++;
                            if (itemsAdded >= itemsOnPage)
                                break;
                        }
                    }

                    if (gotCustomersFiltered.Count() <= 0)
                    {
                        return NotFound("Не найдено элементов с заданными условиями");
                    }
                    else
                    {
                        return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResponse>>(gotCustomersFiltered));
                    }
                }

                return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerResponse>>(gotCustomers)); ;
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }
    }
}