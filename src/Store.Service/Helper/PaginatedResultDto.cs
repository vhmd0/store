using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Service.Helper;

public class PaginatedResultDto<T>
{
    public PaginatedResultDto(int pageIndex , int pageSize , int count , T data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }

    public T Data { get; set; }

}
