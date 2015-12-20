namespace BANANA.Agent.Views
{
	partial class DownloadManager
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadManager));
			this.panel1 = new BANANA.Windows.Controls.Panel();
			this._btnClose = new BANANA.Windows.Controls.Button();
			this._btnCancel = new BANANA.Windows.Controls.Button();
			this._btnOpenFolder = new BANANA.Windows.Controls.Button();
			this._btnDownload = new BANANA.Windows.Controls.Button();
			this._btnChangeFolder = new BANANA.Windows.Controls.Button();
			this.panel2 = new BANANA.Windows.Controls.Panel();
			this._lblTransferSpeed = new BANANA.Windows.Controls.Label();
			this.progressBar1 = new BANANA.Windows.Controls.ProgressBar();
			this.label7 = new BANANA.Windows.Controls.Label();
			this._lblRemainingTime = new BANANA.Windows.Controls.Label();
			this.label5 = new BANANA.Windows.Controls.Label();
			this.label4 = new BANANA.Windows.Controls.Label();
			this._lblDataAmount = new BANANA.Windows.Controls.Label();
			this.label2 = new BANANA.Windows.Controls.Label();
			this._lblDownloadFolder = new BANANA.Windows.Controls.LinkLabel();
			this.label1 = new BANANA.Windows.Controls.Label();
			this.panel3 = new BANANA.Windows.Controls.Panel();
			this._lvDownloadFiles = new BANANA.Agent.Controls.ListViewNF();
			this._chFIleName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this._chFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this._chProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this._chFileUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this._btnClose);
			this.panel1.Controls.Add(this._btnCancel);
			this.panel1.Controls.Add(this._btnOpenFolder);
			this.panel1.Controls.Add(this._btnDownload);
			this.panel1.Controls.Add(this._btnChangeFolder);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 355);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(786, 43);
			this.panel1.TabIndex = 0;
			// 
			// _btnClose
			// 
			this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnClose.Location = new System.Drawing.Point(699, 12);
			this._btnClose.Name = "_btnClose";
			this._btnClose.Reserved = "닫기";
			this._btnClose.Size = new System.Drawing.Size(75, 23);
			this._btnClose.TabIndex = 3;
			this._btnClose.Text = "닫기";
			this._btnClose.UseVisualStyleBackColor = true;
			this._btnClose.ValidationGroup = null;
			this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
			// 
			// _btnCancel
			// 
			this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnCancel.Location = new System.Drawing.Point(699, 12);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Reserved = "취소";
			this._btnCancel.Size = new System.Drawing.Size(75, 23);
			this._btnCancel.TabIndex = 2;
			this._btnCancel.Text = "취소";
			this._btnCancel.UseVisualStyleBackColor = true;
			this._btnCancel.ValidationGroup = null;
			this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
			// 
			// _btnOpenFolder
			// 
			this._btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._btnOpenFolder.Location = new System.Drawing.Point(13, 12);
			this._btnOpenFolder.Name = "_btnOpenFolder";
			this._btnOpenFolder.Reserved = "폴더열기";
			this._btnOpenFolder.Size = new System.Drawing.Size(75, 23);
			this._btnOpenFolder.TabIndex = 1;
			this._btnOpenFolder.Text = "폴더열기";
			this._btnOpenFolder.UseVisualStyleBackColor = true;
			this._btnOpenFolder.ValidationGroup = null;
			this._btnOpenFolder.Click += new System.EventHandler(this._btnOpenFolder_Click);
			// 
			// _btnDownload
			// 
			this._btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnDownload.Location = new System.Drawing.Point(618, 12);
			this._btnDownload.Name = "_btnDownload";
			this._btnDownload.Reserved = "다운로드";
			this._btnDownload.Size = new System.Drawing.Size(75, 23);
			this._btnDownload.TabIndex = 0;
			this._btnDownload.Text = "다운로드";
			this._btnDownload.UseVisualStyleBackColor = true;
			this._btnDownload.ValidationGroup = null;
			this._btnDownload.Click += new System.EventHandler(this._btnDownload_Click);
			// 
			// _btnChangeFolder
			// 
			this._btnChangeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this._btnChangeFolder.Location = new System.Drawing.Point(94, 12);
			this._btnChangeFolder.Name = "_btnChangeFolder";
			this._btnChangeFolder.Reserved = "경로변경";
			this._btnChangeFolder.Size = new System.Drawing.Size(75, 23);
			this._btnChangeFolder.TabIndex = 0;
			this._btnChangeFolder.Text = "경로변경";
			this._btnChangeFolder.UseVisualStyleBackColor = true;
			this._btnChangeFolder.ValidationGroup = null;
			this._btnChangeFolder.Click += new System.EventHandler(this._btnChangeFolder_Click);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Transparent;
			this.panel2.Controls.Add(this._lblTransferSpeed);
			this.panel2.Controls.Add(this.progressBar1);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this._lblRemainingTime);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this._lblDataAmount);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this._lblDownloadFolder);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(786, 99);
			this.panel2.TabIndex = 1;
			// 
			// _lblTransferSpeed
			// 
			this._lblTransferSpeed.AutoSize = true;
			this._lblTransferSpeed.Location = new System.Drawing.Point(73, 45);
			this._lblTransferSpeed.Name = "_lblTransferSpeed";
			this._lblTransferSpeed.Size = new System.Drawing.Size(45, 13);
			this._lblTransferSpeed.TabIndex = 10;
			this._lblTransferSpeed.Text = "0 B / 초";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(76, 73);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(598, 13);
			this.progressBar1.TabIndex = 9;
			this.progressBar1.Text = "progressBar1";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 72);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(57, 13);
			this.label7.TabIndex = 8;
			this.label7.Text = "전체상태 :";
			// 
			// _lblRemainingTime
			// 
			this._lblRemainingTime.AutoSize = true;
			this._lblRemainingTime.Location = new System.Drawing.Point(484, 45);
			this._lblRemainingTime.Name = "_lblRemainingTime";
			this._lblRemainingTime.Size = new System.Drawing.Size(49, 13);
			this._lblRemainingTime.TabIndex = 7;
			this._lblRemainingTime.Text = "00:00:00";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(421, 45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "남은시간 :";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 45);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "전송속도 :";
			// 
			// _lblDataAmount
			// 
			this._lblDataAmount.AutoSize = true;
			this._lblDataAmount.Location = new System.Drawing.Point(484, 19);
			this._lblDataAmount.Name = "_lblDataAmount";
			this._lblDataAmount.Size = new System.Drawing.Size(50, 13);
			this._lblDataAmount.TabIndex = 4;
			this._lblDataAmount.Text = "0 B / 0 B";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(421, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "전체용량 :";
			// 
			// _lblDownloadFolder
			// 
			this._lblDownloadFolder.AutoSize = true;
			this._lblDownloadFolder.Font = new System.Drawing.Font("Malgun Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this._lblDownloadFolder.Location = new System.Drawing.Point(73, 19);
			this._lblDownloadFolder.Name = "_lblDownloadFolder";
			this._lblDownloadFolder.Size = new System.Drawing.Size(0, 13);
			this._lblDownloadFolder.TabIndex = 2;
			this._lblDownloadFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._lblDownloadFolder_LinkClicked);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "저장경로 :";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this._lvDownloadFiles);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 99);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(786, 256);
			this.panel3.TabIndex = 2;
			// 
			// _lvDownloadFiles
			// 
			this._lvDownloadFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._chFIleName,
            this._chFileSize,
            this._chProgress,
            this._chFileUrl});
			this._lvDownloadFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this._lvDownloadFiles.FullRowSelect = true;
			this._lvDownloadFiles.GridLines = true;
			this._lvDownloadFiles.Location = new System.Drawing.Point(10, 5);
			this._lvDownloadFiles.MultiSelect = false;
			this._lvDownloadFiles.Name = "_lvDownloadFiles";
			this._lvDownloadFiles.Size = new System.Drawing.Size(766, 246);
			this._lvDownloadFiles.TabIndex = 0;
			this._lvDownloadFiles.UseCompatibleStateImageBehavior = false;
			this._lvDownloadFiles.View = System.Windows.Forms.View.Details;
			// 
			// _chFIleName
			// 
			this._chFIleName.Text = "파일명";
			this._chFIleName.Width = 391;
			// 
			// _chFileSize
			// 
			this._chFileSize.Text = "크기";
			this._chFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this._chFileSize.Width = 108;
			// 
			// _chProgress
			// 
			this._chProgress.Text = "전송상태";
			this._chProgress.Width = 217;
			// 
			// _chFileUrl
			// 
			this._chFileUrl.Text = "파일Url";
			this._chFileUrl.Width = 0;
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.Description = "파일을 저장할 폴더를 선택하세요.";
			// 
			// DownloadManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImage = global::BANANA.Agent.Properties.Resources.back_01;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(786, 398);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DownloadManager";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "다운로드 매니저";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadManager_FormClosing);
			this.Load += new System.EventHandler(this.DownloadManager_Load);
			this.Shown += new System.EventHandler(this.DownloadManager_Shown);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Windows.Controls.Panel panel1;
		private Windows.Controls.Panel panel2;
		private Windows.Controls.Panel panel3;
		private Windows.Controls.Button _btnCancel;
		private Windows.Controls.Button _btnOpenFolder;
		private Windows.Controls.Button _btnDownload;
		private Windows.Controls.Button _btnChangeFolder;
		private Windows.Controls.LinkLabel _lblDownloadFolder;
		private Windows.Controls.Label label1;
		private Windows.Controls.Label _lblDataAmount;
		private Windows.Controls.Label label2;
		private Windows.Controls.Label _lblRemainingTime;
		private Windows.Controls.Label label5;
		private Windows.Controls.Label label4;
		private BANANA.Agent.Controls.ListViewNF _lvDownloadFiles;
		private System.Windows.Forms.ColumnHeader _chFIleName;
		private System.Windows.Forms.ColumnHeader _chFileSize;
		private System.Windows.Forms.ColumnHeader _chProgress;
		private System.Windows.Forms.ColumnHeader _chFileUrl;
		private Windows.Controls.Button _btnClose;
		private Windows.Controls.Label label7;
		private Windows.Controls.ProgressBar progressBar1;
		private Windows.Controls.Label _lblTransferSpeed;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
	}
}