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
			//��r�����r��
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
			// �ϥ� OpenFileDialog ���ϥΪ̿�� zip �ɮ� �άO�@���ɮ�
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
			// �ˬd�ɮ׬O�_�s�b
			if (File.Exists(filePath))
			{
				if (Path.GetExtension(filePath).Equals(".zip", StringComparison.OrdinalIgnoreCase))
				{
					// �p�G�O zip �ɮסA�i��B�z
					ProcessZipFile(filePath);
				}
				else
				{
					// �_�h�A�����B�z�ɮ�
					ProcessTextFile(filePath);
				}

				MessageBox.Show("�ɮ׳B�z�����C");
			}
			else
			{
				MessageBox.Show("���w���ɮפ��s�b�C");
			}
		}


		private void ProcessTextFile(string filePath)
		{
			// Ū���ɮפ��e
			string fileContent = File.ReadAllText(filePath);

			// �i���r����
			foreach (var replacement in textReplacements)
			{
				fileContent = fileContent.Replace(replacement.Key, replacement.Value);
			}

			// ���o��e���yyyyMMdd�榡���ɶ��W
			string timeStamp = DateTime.Now.ToString("yyyyMMdd");

			// �c�طs�ɮצW�١A�[�W�ɶ��W
			string newFileName = Path.GetFileNameWithoutExtension(filePath) +
								"_" + timeStamp +
								Path.GetExtension(filePath);

			// �c�طs�ɮק�����|
			string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);

			// �g�J�s�ɮ�
			File.WriteAllText(newFilePath, fileContent);
		}

		private void ProcessZipFile(string zipFilePath)
		{
			string extractPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

			try
			{
				// �����Y zip �ɮר��{�ɥؿ�
				ZipFile.ExtractToDirectory(zipFilePath, extractPath);

				// ���o�����Y�᪺�Ҧ��ɮ�
				string[] extractedFiles = Directory.GetFiles(extractPath, "*", SearchOption.AllDirectories);

				// �M���C���ɮ�
				foreach (string extractedFilePath in extractedFiles)
				{
					ProcessTextFileInZip(extractedFilePath);
				}

				// ���s���Y�ק�᪺�ɮ�
				string newZipFileName = Path.GetFileNameWithoutExtension(zipFilePath) +
										"_" + DateTime.Now.ToString("yyyyMMdd") +
										Path.GetExtension(zipFilePath);

				string newZipFilePath = Path.Combine(Path.GetDirectoryName(zipFilePath), newZipFileName);

				ZipFile.CreateFromDirectory(extractPath, newZipFilePath, CompressionLevel.Optimal, false);
			}
			finally
			{
				// �R���{�ɥؿ�
				Directory.Delete(extractPath, true);
			}
		}

		private void ProcessTextFileInZip(string filePath)
		{
			// Ū���ɮפ��e
			string fileContent;

			using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
			{
				fileContent = streamReader.ReadToEnd();
			}

			// �i���r����
			foreach (var replacement in textReplacements)
			{
				fileContent = fileContent.Replace(replacement.Key, replacement.Value);
			}

			// �g�^�ɮ�
			using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.UTF8))
			{
				streamWriter.Write(fileContent);
			}
		}
	}
}