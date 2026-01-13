using System.Threading.Tasks;

namespace Fitness.Helpers
{
    public static class Help
    {
        public static async Task<string> SaveAsync(this IFormFile file, string folderPath)
        {
            string uniqueName = Guid.NewGuid().ToString() + file.FileName;

            string path = Path.Combine(folderPath, uniqueName);

            using FileStream fs = new(path, FileMode.Create);

            await file.CopyToAsync(fs);

            return uniqueName;
        }

        public static bool CheckType(this IFormFile file, string type="image")
        {
            return file.ContentType.Contains(type);
        }

    }
}
