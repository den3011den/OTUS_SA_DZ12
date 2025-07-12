using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OTUS_SA_DZ12_Models.RobotsModels.Customers
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class CustomerResponse
    {

        /// <summary>
        /// ИД записи
        /// </summary>        
        [DisplayName("Ид записи")]
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [DisplayName("Имя")]
        [MaxLength(100, ErrorMessage = "Длинна имени должна быть меньше или равна 100 символам")]
        public string FirstName { get; set; }


        /// <summary>
        /// Фамилия
        /// </summary>
        [DisplayName("Фамимлия")]
        [MaxLength(100, ErrorMessage = "Длинна фамилии должна быть меньше или равна 100 символам")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [DisplayName("Отчество")]
        [MaxLength(100, ErrorMessage = "Длинна отчества должна быть меньше или равна 100 символам")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [DisplayName("Адрес")]
        [MaxLength(500, ErrorMessage = "Длинна адреса должна быть меньше или равна 500 символам")]
        public string Address { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [DisplayName("Номер телефона")]
        [Required]
        [MaxLength(100, ErrorMessage = "Номер телефона должен быть меньше или равен 100 символам")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        [DisplayName("Электронная почта")]
        [MaxLength(200, ErrorMessage = "Адрес электронной почты должен быть меньше или равен 200 символам")]
        public string Email { get; set; }

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        [DisplayName("Дата/время добавления записи")]
        [Required]
        public DateTime AddDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Признак участия в программе лояльности
        /// </summary>
        [DisplayName("Признак участия в программе лояльности")]
        public bool IsBonusParticipant { get; set; } = false;

        /// <summary>
        /// Признак удаления в архив
        /// </summary>
        [DisplayName("Признак удаления в архив")]
        public bool IsArchive { get; set; } = false;
    }
}
