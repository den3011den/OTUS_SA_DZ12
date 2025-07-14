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
        /// Получить список блюд
        /// </summary>
        /// <param name="nameSubstringFilter">Позволяет фильтровать список блюд по нахождению указанной в данном параметре подстроки в Наименовании блюда. Если параметр пустой фильтрация не производится</param>
        /// <param name="orderByName">Указывает упорядочивание по наименованию - ASC-по возрастанию , DESC-по убыванию. По умолчанию - АSC </param>
        /// <param name="itemsOnPage">Количество блюд на странице при постаничном запросе. Если itemsOnPage и pageNumber оба равны нулю - выдаются все блюда</param>
        /// <param name="pageNumber">Номер запрашиваемой страницы при постаничном запросе. Если itemsOnPage и pageNumber оба равны нулю - выдаются все блюда</param>
        /// <returns>Возвращает список Блюд - объекты типа DishResponse</returns>        
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Не удалось выполнить запрос. Ошибка доступа к БД или неверные параметры запроса. Причина описана в ответе</response>  
        /// <response code="404">Не найдено элементов соответствующих заданным условиям запроса</response>          
        [HttpGet()]
        [ProducesResponseType(typeof(List<DishResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<List<DishResponse>>> GetAllDishesAsync(string nameSubstringFilter = "", string orderByName = "ASC", int itemsOnPage = 0, int pageNumber = 0)
        {

            try
            {
                if (itemsOnPage < 0 || pageNumber < 0)
                    return BadRequest("Один из параметров itemsOnPage или pageNumber меньше нуля");


                var gotDishes = await _dishRepository.GetDishesWithNameSubstringFilterAsync(nameSubstringFilter);
                if (gotDishes == null || gotDishes.Count() <= 0)
                {
                    return NotFound("Не найдено ни одного блюда с заданными параметрами");
                }

                // Всё что ниже - Так делать не правильно. Но задание по сис анализу и пректированию API, а не по программированию

                string orderByNameUpper = orderByName.Trim().ToUpper();

                if (orderByNameUpper != "DESC" && orderByNameUpper != "ASC")
                    return BadRequest("Параметр orderByName может принимать только значения ASC или DESC");

                if (orderByNameUpper == "DESC")
                    gotDishes = gotDishes.OrderByDescending(o => o.Name.ToUpper());
                else
                    gotDishes = gotDishes.OrderBy(o => o.Name.ToUpper());

                if (itemsOnPage > 0 || pageNumber > 0)
                {
                    int lostItemsCount = (pageNumber - 1) * itemsOnPage;
                    if (lostItemsCount > gotDishes.Count())
                        return NotFound("Нет элементов на заданной странице. Попробуйте меньший номер страницы");

                    List<Dish> gotDishesFiltered = new List<Dish>();
                    int itemsCount = 0;
                    int itemsAdded = 0;
                    foreach (var dish in gotDishes)
                    {
                        if (itemsCount < lostItemsCount)
                            itemsCount++;
                        else
                        {
                            gotDishesFiltered.Add(dish);
                            itemsAdded++;
                            if (itemsAdded >= itemsOnPage)
                                break;
                        }
                    }

                    if (gotDishesFiltered.Count() <= 0)
                    {
                        return NotFound("Не найдено элементов с заданными условиями");
                    }
                    else
                    {
                        return Ok(_mapper.Map<IEnumerable<Dish>, IEnumerable<DishResponse>>(gotDishesFiltered));
                    }
                }

                return Ok(_mapper.Map<IEnumerable<Dish>, IEnumerable<DishResponse>>(gotDishes)); ;
            }
            catch (Exception ex)
            {
                return BadRequest("Ошибка при выполнении запроса: " + ex.Message);
            }
        }
    }
}
