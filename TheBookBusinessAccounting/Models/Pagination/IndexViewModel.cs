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
        public string StatusId { get; set; }

        public Dictionary<int, string> Statuses { get; } = new Dictionary<int, string>()
        {
            {0, "все" },
            {1, "будет приобретён" },
            {2, "в работе" },
            {3, "списан" }            
        };
    }
}