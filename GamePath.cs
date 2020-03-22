using DBX2_JP_MOD_TOOL.Properties;
using System.IO;

namespace DBX2_JP_MOD_TOOL
{
    public static class GamePath
    {
        public static string install = Settings.Default.installPath;
        public class Directory: IDirectory
        {
            public static readonly Directory Root = new Directory("");
            public static readonly Directory Bin = new Directory("bin");
            public static readonly Directory Cpk = new Directory("cpk");
            public static readonly Directory XV2Patcher = new Directory("xv2patcher");
            public static readonly Directory Msg = new Directory(Path.Combine("data", "msg"));
            public static readonly Directory Iggy = new Directory(Path.Combine("data", "system", "iggy"));

            public readonly string path;
            public string fullPath { get { return Path.Combine(install, path); } }

            private Directory(string path)
            {
                this.path = path;
            }

            public bool Exists() { return System.IO.Directory.Exists(fullPath); }

            public void Create() { System.IO.Directory.CreateDirectory(fullPath); }
        }
        public class File : IFile
        {
            public static readonly File Start = new File("start.exe", Directory.Root);
            public static readonly File Data1 = new File("data1.cpk", Directory.Cpk);
            public static readonly File SysFont = new File("sysfont01_ab.iggy", Directory.Iggy);
            public static readonly File SysTed = new File("sysfont01_ab.iggyted", Directory.Iggy);

            public readonly string fileName;
            public readonly Directory root;
            public string fullPath { get { return Path.Combine(root.fullPath, fileName); } }

            private File(string fileName, Directory root)
            {
                this.fileName = fileName;
                this.root = root;
            }

            public bool Exists() { return System.IO.File.Exists(fullPath); }
        }
    }
}
