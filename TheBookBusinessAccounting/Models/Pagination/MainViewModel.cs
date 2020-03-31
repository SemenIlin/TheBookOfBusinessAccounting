using System.Collections.Generic;

namespace TheBookBusinessAccounting.Models.Pagination
{
    public class MainViewModel
    {
        public IEnumerable<UserViewModel> UserViewModels { get; set; }
        public PageInfo PageInfo { get; set; }

        public string ActionName { get; set; }
        public string SearchText { get; set; }
    }
}