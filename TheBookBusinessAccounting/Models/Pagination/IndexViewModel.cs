using System.Collections.Generic;

namespace TheBookBusinessAccounting.Models.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<ItemViewModel> ItemViewModels { get; set; }
        public PageInfo PageInfo { get; set; }
        public string CurrentCategory { get; set; }

        public string ActionName { get; set; }
        public string SearchText { get; set; }
    }
}