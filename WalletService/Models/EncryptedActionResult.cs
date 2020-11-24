using Remote_Interactive_Wallet_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Remote_Interactive_Wallet_Base.Models
{
    public class EncryptedActionResult<T> : ActionResult
    {
        private readonly T _result;

        public EncryptedActionResult(T result)
        {
            _result = result;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            EncryptedPayloadWrapper<T> wrap = new EncryptedPayloadWrapper<T>(_result);
            var objectResult = new ContentResult();
            objectResult.Content = wrap.ToString();
            //objectResult.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
            objectResult.ContentType = "text/text";

            //{
            //    StatusCode =  // _result.Exception != null
            //    //? Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError

            //};

            objectResult.ExecuteResult(context);
        }
    }
}
