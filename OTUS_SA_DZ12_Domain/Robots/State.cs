namespace OTUS_SA_DZ12_Domain.Robots
{

    /// <summary>
    /// Справочник статусов заказов
    /// </summary>
    public class State : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Признак удаления в архив
        /// </summary>
        public bool IsArchive { get; set; }
    }
}
