using Trading.Authen.Api.Commons;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Trading.Authen.Services.Dto;

namespace Trading.Authen.Api.Base
{
    public abstract class MainController : ControllerBase
    {
    protected IMapper _mapper;
    protected MainController()
    {
    }
    [NonAction]
    public virtual OkObjectResult MOk()
    {
        return Ok(new JsonResponse { Messages = new[] { Constants.Message.Successfull }, StatusCode = (int)HttpStatusCode.OK });
    }

    [NonAction]
    public virtual OkObjectResult MOk(params string[] messages)
    {
        return Ok(new JsonResponse { Messages = messages, StatusCode = (int)HttpStatusCode.OK });
    }
    [NonAction]
    public virtual OkObjectResult MOk(object value)
    {
        return Ok(new JsonResponse { Messages = new[] { Constants.Message.Successfull }, Data = value, StatusCode = (int)HttpStatusCode.OK });
    }
    [NonAction]
    public virtual OkObjectResult MOk(object value, params string[] messages)
    {
        return Ok(new JsonResponse { Messages = messages, Data = value, StatusCode = (int)HttpStatusCode.OK });
    }

    [NonAction]
    public virtual async Task<OkObjectResult> ResultPagination<T>(ISearchBase searchBase, IQueryable<T> query)
    {
        var total = await query.CountAsync();
        var totaPage = total <= searchBase.PageSize ? 1 : (total % searchBase.PageSize) == 0 ? (total / searchBase.PageSize) : (total / searchBase.PageSize) + 1;
        var data = await query.Skip((searchBase.PageNumber - 1) * searchBase.PageSize).Take(searchBase.PageSize).ToListAsync();
        return Ok(new JsonResponse { Messages = new[] { Constants.Message.Successfull }, Data = data, StatusCode = (int)HttpStatusCode.OK, Paging = new Paging(searchBase.PageNumber, searchBase.PageSize, totaPage, total) });
    }
    [NonAction]
    public virtual async Task<OkObjectResult> ResultPagination<T, R>(ISearchBase searchBase, IQueryable<T> query)
    {
        var total = await query.CountAsync();
        var data = await query.Skip((searchBase.PageNumber - 1) * searchBase.PageSize).Take(searchBase.PageSize).ToListAsync();
        var totaPage = total <= searchBase.PageSize ? 1 : (total % searchBase.PageSize) == 0 ? (total / searchBase.PageSize) : (total / searchBase.PageSize) + 1;
        var result = _mapper.Map<IList<R>>(data);
        return Ok(new JsonResponse { Messages = new[] { Constants.Message.Successfull }, Data = result, StatusCode = (int)HttpStatusCode.OK, Paging = new Paging(searchBase.PageNumber, searchBase.PageSize, totaPage, total) });
    }
}
}
