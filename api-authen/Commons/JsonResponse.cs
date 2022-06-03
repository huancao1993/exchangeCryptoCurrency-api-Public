using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trading.Authen.Api.Commons
{
    public class JsonResponse
    {
        public JsonResponse()
        {

        }
        public JsonResponse(string[] message = null, object data = null, Paging paging = null, int statusCode = 200)
        {
            Messages = message;
            Data = data;
            Paging = paging;
            StatusCode = statusCode;
        }
        public string[] Messages { get; set; }
        public string DeveloperMessage { get; set; }
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public Paging Paging { get; set; }
    }

    public class Paging
    {
        public Paging(int pageNumber, int pageSize, int totalPages, int totalRecords)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
        public Paging(int pageNumber, int pageSize, int totalRecords)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            //TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
