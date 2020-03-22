using System;
using System.IO;

namespace DBX2_JP_MOD_TOOL
{
    public static class AppPath
    {
        public class Directory : IDirectory
        {
            public static readonly Directory Bin = new Directory("bin");
            public static readonly Directory Data = new Directory(Path.Combine(Bin.path, "data"));
            public static readonly Directory Msg = new Directory(Path.Combine(Data.path, "msg"));
            public static readonly Directory Iggy = new Directory(Path.Combine(Data.path, "system", "iggy"));
            public static readonly Directory XV2Patcher = new Directory(Path.Combine(Bin.path, "xv2patcher"));
            public static readonly Directory XV2Bin = new Directory(Path.Combine(XV2Patcher.path, "bin"));
            public static readonly Directory XV2XV2Patcher = new Directory(Path.Combine(XV2Patcher.path, "xv2patcher"));

            public readonly string path;
            public string fullPath { get { return Path.Combine(Environment.CurrentDirectory, path); } }

            private Directory(string path)
            {
                this.path = path;
            }

            public bool Exists() { return System.IO.Directory.Exists(fullPath); }

            public void Delete() {
                if (Exists()) { System.IO.Directory.Delete(fullPath, true); }
            }
        }
        public class File : IFile
        {
            public static readonly File JaMsg = new File("jaMsg.txt", Directory.Bin);
            public static readonly File Readme = new File("Readme.txt", Directory.Bin);
            public static readonly File CriPakTools = new File("CriPakTools.exe", Directory.Bin);
            public static readonly File MsgPathcer = new File("DBX2_MsgPatcher.exe", Directory.Bin);
            public static readonly File MsgTool = new File("Dragon_Ball_Xenoverse_2_MSG_Tool.exe", Directory.Bin);
            public static readonly File ZhFont = new File("sysfont01_ab_zh.iggy", Directory.Iggy);
            public static readonly File ZhTex = new File("sysfont01_ab_zh.iggytex", Directory.Iggy);
            public static readonly File ZhTed = new File("sysfont01_ab_zh.iggyted", Directory.Iggy);

            public readonly string fileName;
            public readonly Directory root;
            public string fullPath { get { return Path.Combine(root.fullPath, fileName); } }

            private File(string fileName, Directory root)
            {
                this.fileName = fileName;
                this.root = root;
            }

            public bool Exists() { return System.IO.File.Exists(fullPath); }

            public void Delete() { if(Exists()) System.IO.File.Delete(fullPath); }

            public void Copy(IFile dst) { System.IO.File.Copy(fullPath, dst.fullPath, true); }
        }
    }
}
