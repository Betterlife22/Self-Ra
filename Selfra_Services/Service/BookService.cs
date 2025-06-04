using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Selfra_Contract_Services.Interface;
using Selfra_Core.Base;
using Selfra_Entity.Model;
using Selfra_ModelViews.Model.BookModel;
using Selfra_ModelViews.Model.CourseModel;
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

        public async Task<PaginatedList<BookViewModel>> GetAllBooks(int index, int pageSize)
        {
            var bookList =  _unitOfWork.GetRepository<Book>().GetQueryableByProperty();
            var QueryBook = bookList.ProjectTo<BookViewModel>(_mapper.ConfigurationProvider);
            PaginatedList<BookViewModel> paginateList = await _unitOfWork.GetRepository<BookViewModel>().GetPagingAsync(QueryBook.AsQueryable(), index, pageSize);
            return paginateList;
        }

        public async Task<PaginatedList<BookProgressViewModel>> GetAllUserBookProgress(int index, int pageSize)
        {

            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var bookprogress = _unitOfWork.GetRepository<ReadingProgress>().GetQueryableByProperty(
                b => b.UserId == Guid.Parse(userId),
                includeProperties: "Book"
                );
            var QueryBook = bookprogress.ProjectTo<BookProgressViewModel>(_mapper.ConfigurationProvider);
            PaginatedList<BookProgressViewModel> paginatedList = await _unitOfWork.GetRepository<BookProgressViewModel>().GetPagingAsync(QueryBook.AsQueryable(), index, pageSize);
            return paginatedList;
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

        public async Task UpdateBookProgress(BookProgessModel bookProgessModel,string bookid)
        {
            var userId = Authentication.GetUserIdFromHttpContextAccessor(_httpContextAccessor);
            var existedProgress = await _unitOfWork.GetRepository<ReadingProgress>().GetByPropertyAsync(
                b => b.UserId == Guid.Parse(userId));
            _mapper.Map(bookProgessModel, existedProgress);
            await _unitOfWork.GetRepository<ReadingProgress>().UpdateAsync(existedProgress);
            await _unitOfWork.SaveAsync();
            
        }
    }
}
