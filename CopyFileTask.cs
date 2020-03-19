using System.IO;
using System.Threading.Tasks;

namespace DBX2_JP_MOD_TOOL
{
    public static class CopyFileTask
    {
        const string DIR_BIN = @"bin";
        const string DIR_XV2_PATCHER = @"xv2patcher";
        const string DIR_IGGY = @"data\system\iggy";
        const string FONT_IGGY = "sysfont01_ab.iggy";
        const string FONT_ZH_IGGY = "sysfont01_ab_zh.iggy";
        public static async Task<bool> installXV2Patcher(string path)
        {
            var src = Path.Combine(DIR_BIN, DIR_XV2_PATCHER);
            var dst = path;

            return await Task.Run(() => {
                if (!Directory.Exists(Path.Combine(src, DIR_BIN)) ||
                    !Directory.Exists(Path.Combine(src, DIR_XV2_PATCHER))) return false;

                copyDirectory(Path.Combine(src, DIR_BIN), Path.Combine(dst, DIR_BIN));
                copyDirectory(Path.Combine(src, DIR_XV2_PATCHER), Path.Combine(dst, DIR_XV2_PATCHER));

                return true;
            });
        }

        public static async Task<bool> checkSystemFont(string path)
        {
            var dirIggy = Path.Combine(path, DIR_IGGY);
            var sysIggy = Path.Combine(dirIggy, FONT_IGGY);
            var zhIggy = Path.Combine(DIR_BIN, DIR_IGGY, FONT_ZH_IGGY);

            return await Task.Run(() => {
                if (File.Exists(sysIggy) ||
                    !File.Exists(zhIggy)) return true;

                Directory.CreateDirectory(dirIggy);
                File.Copy(zhIggy, sysIggy, true);
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
