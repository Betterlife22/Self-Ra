using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.MessageModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpPost("StartConversation")]
        public async Task<IActionResult> StartConservation([FromQuery] string secondUserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _messageService.StartConversation(secondUserId);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Start successfully");
            return new OkObjectResult(response);
        }
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody]SendMessageModel sendMessageModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _messageService.SendMessage(sendMessageModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Send successfully");
            return new OkObjectResult(response);
        }
        [HttpPost("AddMembertoGroup")]
        public async Task<IActionResult> AddMembertoGroup([FromQuery] string userid, string conversationid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _messageService.AddMembertoGroup(userid, conversationid);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Add successfully");
            return new OkObjectResult(response);
        }
        [HttpPost("CreateGroupConversation")]
        public async Task<IActionResult> CreateGroupConversation([FromBody] GroupModel groupModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _messageService.CreateGroupConversation(groupModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Send successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetConversation")]
        public async Task<IActionResult> GetConversation([FromQuery]string conversationId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var conversationn = await _messageService.GetConversation(conversationId);
            var response = BaseResponse<ConversationViewModel>.OkDataResponse(conversationn, "Load successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllUserConservation")]
        public async Task<IActionResult> GetAllUserConservation()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var conversationn = await _messageService.GetAllUserConservation();
            var response = BaseResponse<List<ConversationViewModel>>.OkDataResponse(conversationn, "Load successfully");
            return new OkObjectResult(response);
        }
    }
}
