using Talabat.APIs.Dtos;

namespace Talabat.APIs.Helpers
{

  
    public class Pagination<T>
    {
        // This Standard Response for any endpoints work with Pagination way
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int pageIndex, int pageSize,int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }

 


      
    }
}
