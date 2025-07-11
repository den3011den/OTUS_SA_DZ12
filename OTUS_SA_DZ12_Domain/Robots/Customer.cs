namespace OTUS_SA_DZ12_Domain.Robots
{

    /// <summary>
    /// Клиенты
    /// </summary>
    public class Customer : BaseEntity
    {

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Дата/время добавления записи
        /// </summary>
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Признак участия в программе лояльности
        /// </summary>
        public bool IsBonusParticipant { get; set; } = false;

        /// <summary>
        /// Признак удаления в архив
        /// </summary>
        public bool IsArchive { get; set; } = false;

    }
}
