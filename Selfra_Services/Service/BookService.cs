using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Selfra_Contract_Services.Interface;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.BookModel;
using Selfra_Services.Infrastructure;
using Selft.Contract.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Services.Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateBook(BookModifyModel bookModifyModel)
        {
            string bookurl = await _unitOfWork.UploadFileAsync(bookModifyModel.Filepath);
            var book = _mapper.Map<Book>(bookModifyModel);
            book.Filepath = bookurl;
            await _unitOfWork.GetRepository<Book>().AddAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<BookViewModel>> GetAllBooks()
        {
            var bookList = await _unitOfWork.GetRepository<Book>().GetAllAsync();
            var result = _mapper.Map<List<BookViewModel>>(bookList);
            return result;
        }

        public async Task<List<BookProgressViewModel>> GetAllUserBookProgress()
        {

            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var bookprogress = await _unitOfWork.GetRepository<ReadingProgress>().GetAllByPropertyAsync(
                b => b.UserId == Guid.Parse(userId),
                includeProperties: "Book"
                );
            var result = _mapper.Map<List<BookProgressViewModel>>(bookprogress);
            return result;
        }

        public async Task<BookViewModel> GetBook(string id)
        {
            var book = await _unitOfWork.GetRepository<Book>().GetByIdAsync(id);
            var result = _mapper.Map<BookViewModel>(book);
            return result;
        }

        //public async Task<BookProgressViewModel> GetUserBook(string bookid)
        //{
        //    var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
        //    var bookprogress = await _unitOfWork.GetRepository<ReadingProgress>().GetByPropertyAsync(
        //        b => b.UserId == Guid.Parse(userId) && b.BookId == bookid,
        //        includeProperties:"Book"
        //        );
        //    var result = _mapper.Map<BookProgressViewModel>(bookprogress);
        //    return result;
        //}

        public async Task<BookProgressViewModel> GetUserBookProgress(string bookid)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var bookprogress = await _unitOfWork.GetRepository<ReadingProgress>().GetByPropertyAsync(
                b => b.UserId == Guid.Parse(userId) && b.BookId == bookid,
                includeProperties: "Book"
                );
            var result = _mapper.Map<BookProgressViewModel>(bookprogress);
            return result;

        }

        public async Task StarReadBook(BookProgessModel bookProgessModel, string bookid)
        {

            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var bookprogress = _mapper.Map<ReadingProgress>(bookProgessModel);
            bookprogress.UserId = Guid.Parse(userId);
            bookprogress.BookId = bookid;
            await _unitOfWork.GetRepository<ReadingProgress>().AddAsync(bookprogress);
            await _unitOfWork.SaveAsync();
        }
    }
}
