namespace OTUS_SA_DZ12_Common
{
    public static class SD
    {
        public static int SqlCommandConnectionTimeout = 180;
        public enum GetAllItems
        {
            ArchiveOnly,
            NotArchiveOnly,
            All
        }

        public enum OrderDateEnum
        {
            ASC,
            DESC
        }
    }
}
