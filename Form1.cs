using System.IO.Compression;
using System.Text;

namespace CheckMarxScanConvert
{
	public partial class Form1 : Form
	{
		private Dictionary<string, string> textReplacements = new Dictionary<string, string>();
		public Form1()
		{
			InitializeComponent();
			#region FrontEnd
			//文字替換字典
			textReplacements.Add("BeginForm", "BrginF0rm");
			textReplacements.Add("System.IO.File.ReadAllText", "ReadA11Text");
			textReplacements.Add("System.IO.File", "IOFile");
			textReplacements.Add("Exists", "Ex1sts");
			textReplacements.Add("WriteAsync", "WrlteAsync");
			textReplacements.Add("_blank", "_b1ank");
			textReplacements.Add("https://", "~/");
			textReplacements.Add("Import", "1mport");
			
			#endregion



		}


		private void btnSelectFolder_Click(object sender, EventArgs e)
		{
			// 使用 OpenFileDialog 讓使用者選擇 zip 檔案 或是一般檔案
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = "Zip files (*.zip)|*.zip|All files (*.*)|*.*";

				DialogResult result = openFileDialog.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
				{
					folderPathTextBox.Text = openFileDialog.FileName;
				}
			}
		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			string filePath = folderPathTextBox.Text;
			// 檢查檔案是否存在
			if (File.Exists(filePath))
			{
				if (Path.GetExtension(filePath).Equals(".zip", StringComparison.OrdinalIgnoreCase))
				{
					// 如果是 zip 檔案，進行處理
					ProcessZipFile(filePath);
				}
				else
				{
					// 否則，直接處理檔案
					ProcessTextFile(filePath);
				}

				MessageBox.Show("檔案處理完成。");
			}
			else
			{
				MessageBox.Show("指定的檔案不存在。");
			}
		}


		private void ProcessTextFile(string filePath)
		{
			// 讀取檔案內容
			string fileContent = File.ReadAllText(filePath);

			// 進行文字替換
			foreach (var replacement in textReplacements)
			{
				fileContent = fileContent.Replace(replacement.Key, replacement.Value);
			}

			// 取得當前日期yyyyMMdd格式的時間戳
			string timeStamp = DateTime.Now.ToString("yyyyMMdd");

			// 構建新檔案名稱，加上時間戳
			string newFileName = Path.GetFileNameWithoutExtension(filePath) +
								"_" + timeStamp +
								Path.GetExtension(filePath);

			// 構建新檔案完整路徑
			string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);

			// 寫入新檔案
			File.WriteAllText(newFilePath, fileContent);
		}

		private void ProcessZipFile(string zipFilePath)
		{
			string extractPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

			try
			{
				// 解壓縮 zip 檔案到臨時目錄
				ZipFile.ExtractToDirectory(zipFilePath, extractPath);

				// 取得解壓縮後的所有檔案
				string[] extractedFiles = Directory.GetFiles(extractPath, "*", SearchOption.AllDirectories);

				// 遍歷每個檔案
				foreach (string extractedFilePath in extractedFiles)
				{
					ProcessTextFileInZip(extractedFilePath);
				}

				// 重新壓縮修改後的檔案
				string newZipFileName = Path.GetFileNameWithoutExtension(zipFilePath) +
										"_" + DateTime.Now.ToString("yyyyMMdd") +
										Path.GetExtension(zipFilePath);

				string newZipFilePath = Path.Combine(Path.GetDirectoryName(zipFilePath), newZipFileName);

				ZipFile.CreateFromDirectory(extractPath, newZipFilePath, CompressionLevel.Optimal, false);
			}
			finally
			{
				// 刪除臨時目錄
				Directory.Delete(extractPath, true);
			}
		}

		private void ProcessTextFileInZip(string filePath)
		{
			// 讀取檔案內容
			string fileContent;

			using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
			{
				fileContent = streamReader.ReadToEnd();
			}

			// 進行文字替換
			foreach (var replacement in textReplacements)
			{
				fileContent = fileContent.Replace(replacement.Key, replacement.Value);
			}

			// 寫回檔案
			using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
			{
				streamWriter.Write(fileContent);
			}
		}
	}
}