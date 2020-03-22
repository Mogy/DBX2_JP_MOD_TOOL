using System.Threading.Tasks;

namespace DBX2_JP_MOD_TOOL
{
    public static class DeleteFileTask
    {
        public static async Task deleteTempFiles()
        {
            await Task.Run(() => {
                AppPath.Directory.Data.Delete();
                AppPath.File.JaMsg.Delete();
                AppPath.File.Readme.Delete();
            });
        }
    }
}
