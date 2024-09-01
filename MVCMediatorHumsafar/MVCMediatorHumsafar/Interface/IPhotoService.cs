using CloudinaryDotNet.Actions;

namespace MVC_Humsafar_Mubarak.Interface
{
    public interface IPhotoService
    {
        public Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        public Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
