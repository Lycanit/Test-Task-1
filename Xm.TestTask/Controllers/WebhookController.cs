using Microsoft.AspNetCore.Mvc;

namespace Xm.TestTask.Controllers
{
    public class WebhookController : ControllerBase
    {
        private readonly IHandlerProvider _handlerProvider;
        public WebhookController(IHandlerProvider handlerProvider) 
        { 
            _handlerProvider = handlerProvider;
        }


        [HttpPost]
        public async Task<IActionResult> HandleAsync()
        {
            if (!Request.Headers.ContainsKey("x-xdt"))
            {
                throw new ApplicationException("Headers do not contain key: x-xdt");
            }
            var dataType = Request.Headers["x-xdt"].First();
            await Task.Run(() => HandleRequest(dataType));
            return Ok();
        }


        private byte[] ReadRequestBody()
        {
            using (var memoryStream = new MemoryStream())
            {
                Request.Body.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private void HandleRequest(string dataType)
        {
            var dataBody = ReadRequestBody();
            var handler = _handlerProvider.Provide(dataType);
            handler.Handle(dataBody);
        }


    }
}