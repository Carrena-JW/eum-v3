using Eum.Core.Shared.Domains;
using Eum.Core.Shared.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eum.Core.Shared.Infra.Mvc.Filters
{
    public class ChangeResponseStatusProxy
    {
        public int StatusCode { get; set; }

        public string Status { get; set; }
    }

    public class GlobalApiResponseWrappingFilter : ActionFilterAttribute
    {
        public const string ChangeResponseStatusItemsKey = "ChangeResponseStatus";
        public const string StatusItemsKey = "ChangeResponseStatus";
        public const string StatusCodeItemsKey = "ChangeResponseStatus";
        private readonly IErrorDataRepository _errorDataRepository;
        private readonly ILogger<GlobalApiResponseWrappingFilter> _logger;
        private static IEnumerable<int> _filters = new int[] { 401, 402 };

        public GlobalApiResponseWrappingFilter(IErrorDataRepository errorDataRepository,
            ILogger<GlobalApiResponseWrappingFilter> logger)
        {
            _errorDataRepository = errorDataRepository;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.StatusCode != null && _filters.Contains(objectResult.StatusCode.Value) ||
                context.Result is StatusCodeResult) { }
            else
            {
                var controller = context.Controller.GetType();
                if (controller.IsDefined(typeof(ApiControllerAttribute), true))
                {
                    var action = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;
                    if (action.IsDefined(typeof(IgnorResponseWrapAttribute), false)) return;
                    if (action.IsDefined(typeof(ApiExplorerSettingsAttribute), false))
                    {
                        var attb = GetCustomAttributes(action, typeof(ApiExplorerSettingsAttribute)).FirstOrDefault() as ApiExplorerSettingsAttribute;
                        if (attb.IgnoreApi)
                        {
                            //if (context.Exception != null)
                            //{
                            //    var ex = context.Exception;
                            //    var item = new ChangeResponseStatusProxy()
                            //    {
                            //        Status = ex.Message,
                            //        StatusCode = 512
                            //    };
                            //    context.HttpContext.Items.Add(ChangeResponseStatusItemsKey, item);
                            //    Write(context);
                            //    context.ExceptionHandled = true;
                            //}
                            return;
                        }
                    }
                    // }
                    // var apidefs = context.ActionDescriptor.EndpointMetadata
                    //     .Where(c => c is HttpMethodAttribute && (c as HttpMethodAttribute).Template.StartsWith("api"));
                    // if (apidefs != null && apidefs.Count() > 0)
                    // {
                    var code = "ok";
                    var message = "";
                    object result = null;
                    object error = null;
                    if (context.Exception != null)
                    {
                        var ex = context.Exception;
                        var type = ex.GetType();
                        var pErrorCode = type.GetProperty("ErrorCode");

                        if (pErrorCode != null)
                        {
                            code = Convert.ToString(pErrorCode.GetValue(ex));
                            message = ex.Message;
                        }
                        else
                        {
                            code = ex.GetType().Name;
                            message = ex.Message;
                        }
                        error = new
                        {
                            ex.Message,
                            Type = code,
                            ex.StackTrace
                        };

                        Write(context);
                        context.ExceptionHandled = true;
                    }
                    if (context.Result is ObjectResult)
                    {
                        result = (context.Result as ObjectResult).Value;
                    }

                    if (!action.IsDefined(typeof(ReturnCustomResultAttribute), false))
                    {
                        var value = new
                        {
                            Result = result,
                            Code = code,
                            Message = message,
                            Error = error
                        };
                        context.Result = new ObjectResult(value);
                    }

                }
            }
            base.OnActionExecuted(context);
        }

        private void Write(ActionExecutedContext context)
        {
            var userName = context.HttpContext.User.Identity.Name;
            var path = context.HttpContext.Request.Path;
            var machineName = Environment.MachineName;
            var exception = context.Exception;
            try
            {
                if (exception != null)
                {
                    var type = exception.GetType().Name;
                    var message = exception.Message;
                    var detail = exception.ToString();
                    _logger.LogError(exception.Message, exception);
                    _errorDataRepository.Create(new Error
                    {
                        MachineName = machineName,
                        Path = path,
                        Type = type,
                        Message = message,
                        Detail = detail,
                        CreatedId = userName
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
