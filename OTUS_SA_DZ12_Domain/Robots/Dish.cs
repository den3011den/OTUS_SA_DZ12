namespace OTUS_SA_DZ12_Domain.Robots
{

    /// <summary>
    /// Блюда
    /// </summary>
    public class Dish : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Признак удаления в архив
        /// </summary>
        public bool IsArchive { get; set; }

    }
}
