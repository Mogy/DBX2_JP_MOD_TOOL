using DBX2_JP_MOD_TOOL.Properties;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBX2_JP_MOD_TOOL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtDB.Text = Settings.Default.installPath;
            txtXlsx.Text = Settings.Default.xlsxPath;
        }
        private void btnDB_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Dragon Ball Xenoverse 2 のインストール先";
                fbd.SelectedPath = txtDB.Text;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtDB.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnXlsx_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "作業所のExcelファイル";
                ofd.InitialDirectory = Path.GetDirectoryName(txtXlsx.Text);
                ofd.Filter = "Excel ファイル|*.xlsx";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtXlsx.Text = ofd.FileName;
                }
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            // ログをクリア
            clearLog();

            appendLog("日本語化を開始します...");

            appendLog();

            // インストール先の存在チェック
            var result = existsInstallPath();

            // 作業所のExcelファイルの存在チェック
            if (result) result = existsXlsxPath();

            // 作業所のExcelファイルの読込チェック
            DataSet ds = null;
            if(result)
            {
                ds = await readExcelData();
                result = ds != null;
            }

            // 作業所のExcelファイルの解析
            if (result) await analyzeExcelData(ds);

            // CriPakToolsの実行
            if (result) result = await startCriPakTools();

            // MsgPatcherの実行
            if (result) result = await startPatcher();

            // XV2Patcherのインストール
            if (result) result = await installXV2Patcher();

            // システムフォントのチェック
            if (result) await checkSystemFont();

            // 一時ファイルの削除
            await deleteTempFiles();

            appendLog();

            if (result)
            {
                appendLog("日本語化が完了しました！");
                appendLog("ゲームをプレイする際はDBXV2.exeから起動してください。");
            }
            else
            {
                appendLog("日本語化に失敗しました。");
                btnStart.Enabled = true;
            }
        }

        private bool existsInstallPath()
        {
            appendLog("インストール先の存在チェック...");

            GamePath.install = txtDB.Text;

            if (!GamePath.Directory.Root.Exists() ||
                !GamePath.File.Start.Exists() ||
                !GamePath.File.Data1.Exists())
            {
                appendLog("NG", false);
                showErrorDialog("Dragon Ball Xenoverse 2 が見つかりませんでした");
                return false;
            }

            appendLog("OK", false);
            Settings.Default.installPath = GamePath.install;
            Settings.Default.Save();

            return true;
        }

        private bool existsXlsxPath()
        {
            appendLog("作業所のExcelファイルの存在チェック...");

            if (!File.Exists(txtXlsx.Text) ||
                !Path.GetExtension(txtXlsx.Text).Equals(".xlsx"))
            {
                appendLog("NG", false);
                showErrorDialog("作業所のExcelファイル が見つかりませんでした");
                return false;
            }

            appendLog("OK", false);

            return true;
        }

        private async Task<DataSet> readExcelData()
        {
            appendLog("作業所のExcelファイルの読込チェック...");

            var ds = await LoadExcelTask.readExcelData(txtXlsx.Text);

            if (ds != null)
            {
                appendLog("OK", false);
                Settings.Default.xlsxPath = txtXlsx.Text;
                Settings.Default.Save();
            }
            else
            {
                appendLog("NG", false);
                showErrorDialog("作業所のExcelファイル が読み込めませんでした");
                return null;
            }

            return ds;
        }

        private async Task analyzeExcelData(DataSet ds)
        {
            appendLog("作業所のExcelファイルの解析中...");

            await LoadExcelTask.analyzeExcelData(ds);

            appendLog("完了", false);
        }

        private async Task<bool> startCriPakTools()
        {
            appendLog("CriPackToolsの実行...");

            var result = await ProcessTask.startCriPakTools();

            if (result)
            {
                appendLog("完了", false);
            }
            else
            {
                appendLog("失敗", false);
                showErrorDialog("CriPackTools が見つかりませんでした");
            }

            return result;
        }

        private async Task<bool> startPatcher()
        {
            appendLog("MsgPatcherの実行...");

            var result = await ProcessTask.startPatcher();

            if (result)
            {
                appendLog("完了", false);
            }
            else
            {
                appendLog("失敗", false);
                showErrorDialog("MsgPatcher が見つかりませんでした");
            }

            return result;
        }

        private async Task<bool> installXV2Patcher()
        {
            appendLog("XV2Patcherのインストール...");

            var result = await CopyFileTask.installXV2Patcher();

            if (result)
            {
                appendLog("完了", false);
            }
            else
            {
                appendLog("失敗", false);
                showErrorDialog("XV2Patcher が見つかりませんでした");
            }

            return result;
        }

        private async Task checkSystemFont()
        {
            appendLog("システムフォントのチェック...");

            if (await CopyFileTask.checkSystemFont())
            {
                appendLog("OK", false);
            }
            else
            {
                appendLog("OK", false);
                appendLog();
                appendLog("システムフォントが見つからなかったため、中華フォントを追加しました。");
                appendLog("中華フォントは一部の日本語が表示出来ません。");
            }
        }

        private async Task deleteTempFiles()
        {
            appendLog("一時ファイルの削除...");

            await DeleteFileTask.deleteTempFiles();

            appendLog("完了", false);
        }

        private void showErrorDialog(string message) 
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void clearLog() {
            txtLog.Text = "";
        }

        private void appendLog(string log = "", bool withNewLine = true)
        {
            if (string.IsNullOrWhiteSpace(txtLog.Text))
            {
                txtLog.Text = log;
            }
            else if(withNewLine)
            {
                txtLog.Text = string.Join("\r\n", txtLog.Text, log);
            }
            else
            {
                txtLog.Text += log;
            }
        }
    }
}
