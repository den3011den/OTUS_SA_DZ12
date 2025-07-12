using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.State
{

    /// <summary>
    /// Статус заказа
    /// </summary>
    public class StateResponse
    {
        /// <summary>
        /// ИД записи
        /// </summary>        
        [DisplayName("Ид записи")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        [Required]
        [MaxLength(100, ErrorMessage = "Длинна Наименования должна быть меньше или равна 100 символам")]
        public string Name { get; set; }

        /// <summary>
        /// Признак удаления в архив
        /// </summary>
        [DisplayName("Признак удаления в архив")]
        public bool IsArchive { get; set; } = false;
    }
}
