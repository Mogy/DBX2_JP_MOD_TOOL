using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DBX2_JP_MOD_TOOL
{
    public static class ProcessTask
    {
        const string PTN_EN_MSG = @"^*._en\.msg$";
        const string PTN_ZH_IGGY = @"sysfont01_ab_zh\.iggy";
        public static async Task<bool> startCriPakTools()
        {
            return await Task.Run(() => {
                if (!AppPath.File.CriPakTools.Exists()) return false;

                var criPakTools = new Process();
                criPakTools.StartInfo.FileName = AppPath.File.CriPakTools.fileName;
                criPakTools.StartInfo.WorkingDirectory = AppPath.Directory.Bin.fullPath;

                criPakTools.StartInfo.Arguments = createArguments(GamePath.File.Data1.fullPath, PTN_EN_MSG);
                criPakTools.Start();
                criPakTools.WaitForExit();

                criPakTools.StartInfo.Arguments = createArguments(GamePath.File.Data1.fullPath, PTN_ZH_IGGY);
                criPakTools.Start();
                criPakTools.WaitForExit();

                return true;
            });
        }

        public static async Task<bool> startPatcher()
        {
            return await Task.Run(() => {
                if (!AppPath.File.MsgPathcer.Exists() ||
                    !AppPath.File.MsgTool.Exists()) return false;

                var patcher = new Process();
                patcher.StartInfo.FileName = AppPath.File.MsgPathcer.fileName;
                patcher.StartInfo.WorkingDirectory = AppPath.Directory.Bin.fullPath;
                patcher.StartInfo.Arguments = createArguments(AppPath.Directory.Msg.fullPath, GamePath.Directory.Msg.fullPath);
                patcher.Start();
                patcher.WaitForExit();

                return true;
            });
        }
        private static string createArguments(params string[] args)
        {
            return string.Join(" ", args.Select(x => '"' + x + '"'));
        }
    }
}
