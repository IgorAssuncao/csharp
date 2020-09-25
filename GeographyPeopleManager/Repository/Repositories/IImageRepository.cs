using System.IO;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadFile(string id, string extension, Stream stream);
    }
}