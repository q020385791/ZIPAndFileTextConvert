namespace CheckMarxScanConvert
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnSelectFolder = new Button();
			btnConvert = new Button();
			folderPathTextBox = new TextBox();
			SuspendLayout();
			// 
			// btnSelectFolder
			// 
			btnSelectFolder.Location = new Point(347, 13);
			btnSelectFolder.Name = "btnSelectFolder";
			btnSelectFolder.Size = new Size(120, 23);
			btnSelectFolder.TabIndex = 0;
			btnSelectFolder.Text = "選擇專案資料夾";
			btnSelectFolder.UseVisualStyleBackColor = true;
			btnSelectFolder.Click += btnSelectFolder_Click;
			// 
			// btnConvert
			// 
			btnConvert.Location = new Point(347, 42);
			btnConvert.Name = "btnConvert";
			btnConvert.Size = new Size(119, 23);
			btnConvert.TabIndex = 1;
			btnConvert.Text = "轉換";
			btnConvert.UseVisualStyleBackColor = true;
			btnConvert.Click += btnConvert_Click;
			// 
			// folderPathTextBox
			// 
			folderPathTextBox.Location = new Point(23, 10);
			folderPathTextBox.Name = "folderPathTextBox";
			folderPathTextBox.Size = new Size(308, 23);
			folderPathTextBox.TabIndex = 2;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(487, 77);
			Controls.Add(folderPathTextBox);
			Controls.Add(btnConvert);
			Controls.Add(btnSelectFolder);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnSelectFolder;
		private Button btnConvert;
		private TextBox folderPathTextBox;
	}
}