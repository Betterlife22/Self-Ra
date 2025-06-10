using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.BookModel;

namespace SELF_RA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("GetAllBook")]
        public async Task<IActionResult> GetAllBook(int index, int pagesize)
        {
            var booklist = await _bookService.GetAllBooks(index,pagesize);
            var response = BaseResponseModel<List<BookViewModel>>.OkDataResponse(booklist, "Load successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById([FromQuery] string bookid)
        {
            var book = await _bookService.GetBook(bookid);
            var response = BaseResponseModel<BookViewModel>.OkDataResponse(book, "Load Successfully");
            return new OkObjectResult(response);
        }
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook([FromForm] BookModifyModel bookModifyModel)
        {
            await _bookService.CreateBook(bookModifyModel);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Create successfully");
            return new OkObjectResult(response);
        }
        [HttpPost("StartReadBook")]
        public async Task<IActionResult> StartReadBook([FromBody] BookProgessModel bookProgessModel,[FromQuery]string bookid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _bookService.StarReadBook(bookProgessModel,bookid);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Start successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetUserBookProgress")]
        public async Task<IActionResult> GetUserBookProgress (string bookid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookprogress = await _bookService.GetUserBookProgress(bookid);
            var response = BaseResponseModel<BookProgressViewModel>.OkDataResponse(bookprogress, "Load Successfully");
            return new OkObjectResult(response);
        }
        [HttpGet("GetAllUserBookProgress")]
        public async Task<IActionResult> GetAllUserBookProgress(int index, int pagesize)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var bookprogressList = await _bookService.GetAllUserBookProgress(index,pagesize);
            var response = BaseResponseModel<List<BookProgressViewModel>>.OkDataResponse(bookprogressList, "Load Successfully");
            return new OkObjectResult(response);

        }
        [HttpPut("UpdateProgress")]
        public async Task<IActionResult> UpdateProgress([FromBody] BookProgessModel bookProgessModel, string bookid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _bookService.UpdateBookProgress(bookProgessModel, bookid);
            var response = BaseResponseModel<string>.OkMessageResponseModel("Update successfully");
            return new OkObjectResult(response);
        }
    }
}
