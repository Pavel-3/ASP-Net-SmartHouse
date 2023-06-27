namespace SmartHouse.WebAPI.Requests
{
    public class GetUsersByPagingInfoRequest
    {            
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
