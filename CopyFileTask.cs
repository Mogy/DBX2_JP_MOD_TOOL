using System.IO;
using System.Threading.Tasks;

namespace DBX2_JP_MOD_TOOL
{
    public static class CopyFileTask
    {
        public static async Task<bool> installXV2Patcher()
        {
            return await Task.Run(() => {
                if (!AppPath.Directory.XV2Bin.Exists() ||
                    !AppPath.Directory.XV2XV2Patcher.Exists()) return false;

                copyDirectory(AppPath.Directory.XV2Bin.fullPath, GamePath.Directory.Bin.fullPath);
                copyDirectory(AppPath.Directory.XV2XV2Patcher.fullPath, GamePath.Directory.XV2Patcher.fullPath);

                return true;
            });
        }

        public static async Task<bool> checkSystemFont()
        {
            return await Task.Run(() => {
                if (GamePath.File.SysFont.Exists() ||
                    !AppPath.File.ZhFont.Exists()) return true;

                GamePath.Directory.Iggy.Create();
                AppPath.File.ZhFont.Copy(GamePath.File.SysFont);
                return false;
            });
        }

        private static void copyDirectory(string sourcePath, string destinationPath)
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
            DirectoryInfo destinationDirectory = new DirectoryInfo(destinationPath);

            if (destinationDirectory.Exists == false)
            {
                destinationDirectory.Create();
                destinationDirectory.Attributes = sourceDirectory.Attributes;
            }

            foreach (FileInfo fileInfo in sourceDirectory.GetFiles())
            {
                fileInfo.CopyTo(destinationDirectory.FullName + @"\" + fileInfo.Name, true);
            }

            foreach (DirectoryInfo directoryInfo in sourceDirectory.GetDirectories())
            {
                copyDirectory(directoryInfo.FullName, destinationDirectory.FullName + @"\" + directoryInfo.Name);
            }
        }
    }
}
