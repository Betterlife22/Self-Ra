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
        Task <List<BookViewModel>> GetAllBooks();
        Task <BookViewModel> GetBook(string id);
        Task StarReadBook (BookProgessModel bookProgessModel, string bookid);
        //Task<BookProgressViewModel> GetUserBook (string id);
        Task<BookProgressViewModel> GetUserBookProgress(string bookid);
        Task<List<BookProgressViewModel>> GetAllUserBookProgress();
    }
}
