using Microsoft.AspNetCore.Mvc;

namespace TManagement.AppServices
{
    public class DataTableFilterInput
    {
        [BindProperty(Name ="Length")]
        public int PageSize { get; set; }

        public int Start { get; set; }

        [BindProperty(Name = "columns[order[0][column]][name]")]
        public string? OrderByColumn { get; set; }

        [BindProperty(Name = "order[0][dir]")]
        public string? OrderBy { get; set; }

        [BindProperty(Name = "search[value]")]
        public string? Filter { get; set; }
    }
}
