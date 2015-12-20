namespace BANANA.Agent.Views
{
	partial class DocumentDownloader
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentDownloader));
			this.label1 = new BANANA.Windows.Controls.Label();
			this.progressBar1 = new BANANA.Windows.Controls.ProgressBar();
			this._lblFileName = new BANANA.Windows.Controls.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "파일명:";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(16, 36);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(542, 14);
			this.progressBar1.TabIndex = 1;
			this.progressBar1.Text = "progressBar1";
			// 
			// _lblFileName
			// 
			this._lblFileName.AutoSize = true;
			this._lblFileName.Location = new System.Drawing.Point(62, 11);
			this._lblFileName.Name = "_lblFileName";
			this._lblFileName.Size = new System.Drawing.Size(0, 13);
			this._lblFileName.TabIndex = 2;
			// 
			// DocumentDownloader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(591, 66);
			this.ControlBox = false;
			this.Controls.Add(this._lblFileName);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DocumentDownloader";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "도큐먼트를 임시 폴더로 다운로드 하고 있습니다.";
			this.Load += new System.EventHandler(this.DocumentDownloader_Load);
			this.Shown += new System.EventHandler(this.DocumentDownloader_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Windows.Controls.Label label1;
		private Windows.Controls.ProgressBar progressBar1;
		private Windows.Controls.Label _lblFileName;
	}
}