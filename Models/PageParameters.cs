using DynamicData.Operators;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace TestCatalogAvalonia.Models
{
    public class PageParameters : ReactiveObject
    {

        public PageParameters(int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        [Reactive] public int TotalCount { get; set; }

        [Reactive] public int PageCount { get; set; }

        [Reactive] public int CurrentPage { get; set; }

        [Reactive] public int PageSize { get; set; }

        [Reactive] public IEnumerable<int> Pages { get; set; }

        public void Update(IPageResponse response)
        {
            PageSize = response.PageSize;
            PageCount = response.Pages;
            Pages = Enumerable.Range(1, PageCount);
            TotalCount = response.TotalSize;
        }
    }
}