namespace OTUS_SA_DZ12_Domain.Robots
{

    /// <summary>
    /// Справочник методов доставки
    /// </summary>
    public class ReceiveMethod : BaseEntity
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
