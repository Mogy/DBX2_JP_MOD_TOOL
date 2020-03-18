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
        const string START = "start.exe";
        const string CPK_DATA1 = @"cpk\data1.cpk";
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
            if (!existsInstallPath())
            {
                btnStart.Enabled = true;
                return;
            }

            // 作業所のExcelファイルの存在チェック
            if (!existsXlsxPath())
            {
                btnStart.Enabled = true;
                return;
            }

            // 作業所のExcelファイルの読込チェック
            var ds = await readExcelData();
            if (ds == null)
            {
                btnStart.Enabled = true;
                return;
            }

            // 設定を保存
            Settings.Default.Save();

            // 作業所のExcelファイルの解析
            await analyzeExcelData(ds);

            // CriPakToolsの実行
            await startCriPakTools();

            // MsgPatcherの実行
            await startPatcher();

            // XV2Patcherのインストール
            await installXV2Patcher();

            // システムフォントのチェック
            await checkSystemFont();

            appendLog();

            appendLog("日本語化が完了しました！");

            appendLog("ゲームをプレイする際はDBXV2.exeから起動してください。");

            btnStart.Enabled = true;
        }

        private bool existsInstallPath()
        {
            appendLog("インストール先の存在チェック...");

            var startExe = Path.Combine(txtDB.Text, START);
            var data1Cpk = Path.Combine(txtDB.Text, CPK_DATA1);

            if (!Directory.Exists(txtDB.Text) ||
                !File.Exists(startExe) ||
                !File.Exists(data1Cpk))
            {
                appendLog("NG", false);
                showErrorDialog("Dragon Ball Xenoverse 2 が見つかりませんでした");
                return false;
            }

            appendLog("OK", false);
            Settings.Default.installPath = txtDB.Text;

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
            if (ds == null)
            {
                appendLog("NG", false);
                showErrorDialog("作業所のExcelファイル が読み込めませんでした");
                return null;
            }

            appendLog("OK", false);
            Settings.Default.xlsxPath = txtXlsx.Text;

            return ds;
        }

        private async Task analyzeExcelData(DataSet ds)
        {
            appendLog("作業所のExcelファイルの解析中...");

            await LoadExcelTask.analyzeExcelData(ds);

            appendLog("完了", false);
        }

        private async Task startCriPakTools()
        {
            appendLog("CriPackToolsの実行...");

            await ProcessTask.startCriPakTools(txtDB.Text);

            appendLog("完了", false);
        }

        private async Task startPatcher()
        {
            appendLog("MsgPatcherの実行...");

            await ProcessTask.startPatcher(txtDB.Text);

            appendLog("完了", false);
        }

        private async Task installXV2Patcher()
        {
            appendLog("XV2Patcherのインストール...");

            await CopyFileTask.installXV2Patcher(txtDB.Text);

            appendLog("完了", false);
        }

        private async Task checkSystemFont()
        {
            appendLog("システムフォントのチェック...");

            if (await CopyFileTask.checkSystemFont(txtDB.Text))
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
