using Selfra_Core.Base;
using Selfra_ModelViews.Model.BookModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfra_Contract_Services.Interface
{
    public interface IBookService
    {

        Task CreateBook(BookModifyModel bookModifyModel);
        Task <PaginatedList<BookViewModel>> GetAllBooks(int index, int pageSize);
        Task <BookViewModel> GetBook(string id);
        Task StarReadBook (BookProgessModel bookProgessModel, string bookid);
        //Task<BookProgressViewModel> GetUserBook (string id);
        Task<BookProgressViewModel> GetUserBookProgress(string bookid);
        Task<PaginatedList<BookProgressViewModel>> GetAllUserBookProgress(int index, int pageSize);
        Task UpdateBookProgress (BookProgessModel bookProgessModel,string bookid);

    }
}
