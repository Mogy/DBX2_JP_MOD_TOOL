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
            if (GamePath.File.SysFont.Exists() ||
                    !AppPath.File.ZhFont.Exists()) return true;

            GamePath.Directory.Iggy.Create();
            AppPath.File.ZhFont.Copy(GamePath.File.SysFont);
            if(await createZhTed()) AppPath.File.ZhTed.Copy(GamePath.File.SysTed);

            return false;
        }

        private static async Task<bool> createZhTed()
        {
            if (!await ProcessTask.startExtractIggyTexPack()) return false;

            if (!AppPath.Directory.ZhTex.Exists() ||
                !AppPath.File.ZhXml.Exists()) return false;

            await Task.Run(() =>
            {
                var str = "";
                using (var sr = new StreamReader(AppPath.File.ZhXml.fullPath))
                {
                    str = sr.ReadToEnd().Replace("unk_id=\"0\"", "unk_id=\"65535\"");
                }
                using (var sw = new StreamWriter(AppPath.File.ZhXml.fullPath))
                {
                    sw.Write(str);
                }
            });

            if (!await ProcessTask.startRepackIggyTexPack()) return false;

            return true;
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
