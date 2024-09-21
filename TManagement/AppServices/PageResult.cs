namespace TManagement.AppServices
{
    public class PageResult<T>
    {
        public List<T>? Data { get; set; }

        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
