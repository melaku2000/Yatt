using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Yatt.Models.Dtos;
using Yatt.Web.Repositories;
using Yatt.Models.RequestFeatures;

namespace Yatt.Web.Pages
{
    public partial class Index
    {
        private List<JobDto> jobs { get; set; }// = new List<JobDto>();
        public MetaData MetaData { get; set; } = new MetaData();
        private PageParameter _pageParameters = new PageParameter();

        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
       
        [Inject]
        public IYattRepository<JobDto> jobRepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //var auth = ((await authState).User);
            //if (auth != null)
            //{
            //    userId = auth.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value;
            //}

            await LoadInitial();
        }
        async Task LoadInitial()
        {
            var pagingResponse = await jobRepo.GetPagedList("jobs/pagedList", _pageParameters);
            jobs = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
        private async Task SelectedPage(int page)
        {
            _pageParameters.PageNumber = page;
            await LoadInitial();
        }
        private async Task OnPageSizeChange(ChangeEventArgs eventArgs)
        {
            if (eventArgs.Value.ToString() == "-1")
                return;
            _pageParameters.PageNumber = 1;
            _pageParameters.PageSize = int.Parse(eventArgs.Value.ToString());
            await LoadInitial();
        }
        private async Task SearchChanged(ChangeEventArgs args)
        {
            var searchTerm = args.Value!.ToString();
            Console.WriteLine(searchTerm);
            _pageParameters.PageNumber = 1;
            _pageParameters.SearchTerm = searchTerm;
            await LoadInitial();
        }
        private async Task SortChanged(string orderBy)
        {
            _pageParameters.OrderBy = orderBy;
            await LoadInitial();
        }
    }
}
