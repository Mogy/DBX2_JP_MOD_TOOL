using System.IO;
using System.Threading.Tasks;

namespace DBX2_JP_MOD_TOOL
{
    public static class DeleteFileTask
    {
        const string DIR_DATA = @"bin\data";
        const string JA_MSG = @"bin\jaMsg.txt";
        const string README = @"bin\Readme.txt";
        public static async Task deleteTempFiles()
        {
            await Task.Run(() => {
                if (Directory.Exists(DIR_DATA))
                {
                    Directory.Delete(DIR_DATA, true);
                }
                if (File.Exists(JA_MSG))
                {
                    File.Delete(JA_MSG);
                }
                if (File.Exists(README))
                {
                    File.Delete(README);
                }
            });
        }
    }
}
