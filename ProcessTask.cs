using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DBX2_JP_MOD_TOOL
{
    public static class ProcessTask
    {
        const string CRI_PAK_TOOLS = "CriPakTools.exe";
        const string MSG_PATCHER = "DBX2_MsgPatcher.exe";
        const string MSG_TOOL = "Dragon_Ball_Xenoverse_2_MSG_Tool.exe";
        const string CPK_DATA1 = @"cpk\data1.cpk";
        const string CPK_DATA2 = @"cpk\data2.cpk";
        const string DIR_MSG = @"data\msg";
        const string PTN_EN_MSG = @"^*._en\.msg$";
        const string PTN_ZH_IGGY = "^sysfont01_ab_zh.iggy$";
        const string DIR_BIN = @"bin";
        public static async Task<bool> startCriPakTools(string path)
        {
            var data1Cpk = Path.Combine(path, CPK_DATA1);
            var data2Cpk = Path.Combine(path, CPK_DATA2);

            return await Task.Run(() => {
                if (!File.Exists(Path.Combine(DIR_BIN, CRI_PAK_TOOLS))) return false;

                var criPakTools = new Process();
                criPakTools.StartInfo.FileName = CRI_PAK_TOOLS;
                criPakTools.StartInfo.WorkingDirectory = DIR_BIN;

                criPakTools.StartInfo.Arguments = createArguments(data1Cpk, PTN_EN_MSG);
                criPakTools.Start();
                criPakTools.WaitForExit();

                criPakTools.StartInfo.Arguments = createArguments(data1Cpk, PTN_ZH_IGGY);
                criPakTools.Start();
                criPakTools.WaitForExit();

                criPakTools.StartInfo.Arguments = createArguments(data2Cpk, PTN_EN_MSG);
                criPakTools.Start();
                criPakTools.WaitForExit();

                criPakTools.StartInfo.Arguments = createArguments(data2Cpk, PTN_ZH_IGGY);
                criPakTools.Start();
                criPakTools.WaitForExit();

                return true;
            });
        }

        public static async Task<bool> startPatcher(string path)
        {
            var output = Path.Combine(path, DIR_MSG);

            return await Task.Run(() => {
                if (!File.Exists(Path.Combine(DIR_BIN, MSG_PATCHER)) ||
                    !File.Exists(Path.Combine(DIR_BIN, MSG_TOOL))) return false;

                var patcher = new Process();
                patcher.StartInfo.FileName = MSG_PATCHER;
                patcher.StartInfo.WorkingDirectory = DIR_BIN;
                patcher.StartInfo.Arguments = createArguments(DIR_MSG, output);
                patcher.Start();
                patcher.WaitForExit();

                return true;
            });
        }
        private static string createArguments(params string[] args)
        {
            return String.Join(" ", args.Select(x => '"' + x + '"'));
        }
    }
}
