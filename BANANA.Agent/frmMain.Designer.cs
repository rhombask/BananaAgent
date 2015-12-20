namespace BANANA.Agent
{
	partial class frmMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this._ntfIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.다운로드매니저ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.도뮤먼트뷰어ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.프로그램정보ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.종료ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this._tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.종료ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.보기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.다운로드매니저ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.도큐먼트뷰어ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.도움말ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.프로그램정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.바나나프레임워크홈페이지ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.textBox1 = new BANANA.Windows.Controls.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.contextMenuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// _ntfIcon
			// 
			this._ntfIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this._ntfIcon.BalloonTipText = "바나나 에이전트가 실행중입니다. 바나나 에이전트는 웹 사이트의 추가 기능들을 지원하기 위해서 대기 중입니다.";
			this._ntfIcon.BalloonTipTitle = "바나나 에이전트";
			this._ntfIcon.ContextMenuStrip = this.contextMenuStrip1;
			this._ntfIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_ntfIcon.Icon")));
			this._ntfIcon.Text = "바나나 에이전트가 실행중입니다. 바나나 에이전트는 웹 사이트의 추가 기능들을 지원하기 위해서 대기 중입니다.";
			this._ntfIcon.Visible = true;
			this._ntfIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._ntfIcon_MouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.다운로드매니저ToolStripMenuItem1,
            this.도뮤먼트뷰어ToolStripMenuItem,
            this.toolStripSeparator1,
            this.프로그램정보ToolStripMenuItem1,
            this.종료ToolStripMenuItem1});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(162, 98);
			// 
			// 다운로드매니저ToolStripMenuItem1
			// 
			this.다운로드매니저ToolStripMenuItem1.Image = global::BANANA.Agent.Properties.Resources.downloadmanager;
			this.다운로드매니저ToolStripMenuItem1.Name = "다운로드매니저ToolStripMenuItem1";
			this.다운로드매니저ToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
			this.다운로드매니저ToolStripMenuItem1.Text = "다운로드 매니저";
			this.다운로드매니저ToolStripMenuItem1.Click += new System.EventHandler(this.다운로드매니저ToolStripMenuItem1_Click);
			// 
			// 도뮤먼트뷰어ToolStripMenuItem
			// 
			this.도뮤먼트뷰어ToolStripMenuItem.Image = global::BANANA.Agent.Properties.Resources.documentviewer;
			this.도뮤먼트뷰어ToolStripMenuItem.Name = "도뮤먼트뷰어ToolStripMenuItem";
			this.도뮤먼트뷰어ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.도뮤먼트뷰어ToolStripMenuItem.Text = "도뮤먼트 뷰어";
			this.도뮤먼트뷰어ToolStripMenuItem.Click += new System.EventHandler(this.도뮤먼트뷰어ToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
			// 
			// 프로그램정보ToolStripMenuItem1
			// 
			this.프로그램정보ToolStripMenuItem1.Image = global::BANANA.Agent.Properties.Resources.about;
			this.프로그램정보ToolStripMenuItem1.Name = "프로그램정보ToolStripMenuItem1";
			this.프로그램정보ToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
			this.프로그램정보ToolStripMenuItem1.Text = "프로그램 정보";
			this.프로그램정보ToolStripMenuItem1.Click += new System.EventHandler(this.프로그램정보ToolStripMenuItem1_Click);
			// 
			// 종료ToolStripMenuItem1
			// 
			this.종료ToolStripMenuItem1.Image = global::BANANA.Agent.Properties.Resources.exit;
			this.종료ToolStripMenuItem1.Name = "종료ToolStripMenuItem1";
			this.종료ToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
			this.종료ToolStripMenuItem1.Text = "종료";
			this.종료ToolStripMenuItem1.Click += new System.EventHandler(this.종료ToolStripMenuItem1_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackgroundImage = global::BANANA.Agent.Properties.Resources.back_02;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsslStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 172);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(378, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// _tsslStatus
			// 
			this._tsslStatus.Name = "_tsslStatus";
			this._tsslStatus.Size = new System.Drawing.Size(323, 17);
			this._tsslStatus.Text = "2015-12-05 02:12부터 에이전트가 정상적으로 실행중입니다.";
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackgroundImage = global::BANANA.Agent.Properties.Resources.back_02;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.보기ToolStripMenuItem,
            this.도움말ToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(378, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// 파일ToolStripMenuItem
			// 
			this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.종료ToolStripMenuItem});
			this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
			this.파일ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.파일ToolStripMenuItem.Text = "파일";
			// 
			// 종료ToolStripMenuItem
			// 
			this.종료ToolStripMenuItem.Image = global::BANANA.Agent.Properties.Resources.exit;
			this.종료ToolStripMenuItem.Name = "종료ToolStripMenuItem";
			this.종료ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.종료ToolStripMenuItem.Text = "종료";
			this.종료ToolStripMenuItem.Click += new System.EventHandler(this.종료ToolStripMenuItem_Click);
			// 
			// 보기ToolStripMenuItem
			// 
			this.보기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.다운로드매니저ToolStripMenuItem,
            this.도큐먼트뷰어ToolStripMenuItem});
			this.보기ToolStripMenuItem.Name = "보기ToolStripMenuItem";
			this.보기ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.보기ToolStripMenuItem.Text = "보기";
			// 
			// 다운로드매니저ToolStripMenuItem
			// 
			this.다운로드매니저ToolStripMenuItem.Image = global::BANANA.Agent.Properties.Resources.downloadmanager;
			this.다운로드매니저ToolStripMenuItem.Name = "다운로드매니저ToolStripMenuItem";
			this.다운로드매니저ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.다운로드매니저ToolStripMenuItem.Text = "다운로드 매니저";
			this.다운로드매니저ToolStripMenuItem.Click += new System.EventHandler(this.다운로드매니저ToolStripMenuItem_Click);
			// 
			// 도큐먼트뷰어ToolStripMenuItem
			// 
			this.도큐먼트뷰어ToolStripMenuItem.Image = global::BANANA.Agent.Properties.Resources.documentviewer;
			this.도큐먼트뷰어ToolStripMenuItem.Name = "도큐먼트뷰어ToolStripMenuItem";
			this.도큐먼트뷰어ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.도큐먼트뷰어ToolStripMenuItem.Text = "도큐먼트 뷰어";
			this.도큐먼트뷰어ToolStripMenuItem.Click += new System.EventHandler(this.도큐먼트뷰어ToolStripMenuItem_Click);
			// 
			// 도움말ToolStripMenuItem
			// 
			this.도움말ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.프로그램정보ToolStripMenuItem,
            this.바나나프레임워크홈페이지ToolStripMenuItem});
			this.도움말ToolStripMenuItem.Name = "도움말ToolStripMenuItem";
			this.도움말ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
			this.도움말ToolStripMenuItem.Text = "도움말";
			// 
			// 프로그램정보ToolStripMenuItem
			// 
			this.프로그램정보ToolStripMenuItem.Image = global::BANANA.Agent.Properties.Resources.about;
			this.프로그램정보ToolStripMenuItem.Name = "프로그램정보ToolStripMenuItem";
			this.프로그램정보ToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
			this.프로그램정보ToolStripMenuItem.Text = "프로그램 정보";
			this.프로그램정보ToolStripMenuItem.Click += new System.EventHandler(this.프로그램정보ToolStripMenuItem_Click);
			// 
			// 바나나프레임워크홈페이지ToolStripMenuItem
			// 
			this.바나나프레임워크홈페이지ToolStripMenuItem.Image = global::BANANA.Agent.Properties.Resources.website;
			this.바나나프레임워크홈페이지ToolStripMenuItem.Name = "바나나프레임워크홈페이지ToolStripMenuItem";
			this.바나나프레임워크홈페이지ToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
			this.바나나프레임워크홈페이지ToolStripMenuItem.Text = "바나나 프레임워크 홈페이지";
			this.바나나프레임워크홈페이지ToolStripMenuItem.Click += new System.EventHandler(this.바나나프레임워크홈페이지ToolStripMenuItem_Click);
			// 
			// textBox1
			// 
			this.textBox1.AutoTab = false;
			this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.textBox1.Location = new System.Drawing.Point(83, 38);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(264, 79);
			this.textBox1.TabIndex = 3;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "바나나 에이전트는 바나나 프레임워크로 개발된 웹 사이트로부터 rich-function들을 수행하기 위한 에이전트입니다.\r\n파일을 다운로드 하거나," +
    " 문서를 읽기 전용으로 여는 등의 기능들을 지원합니다.";
			this.textBox1.ValidationGroup = null;
			this.textBox1.WaterMarkColor = System.Drawing.Color.Empty;
			this.textBox1.WaterMarkText = "";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::BANANA.Agent.Properties.Resources.back_02;
			this.pictureBox1.Image = global::BANANA.Agent.Properties.Resources.BANANA_Agent;
			this.pictureBox1.Location = new System.Drawing.Point(13, 38);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 64);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::BANANA.Agent.Properties.Resources.back_02;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(378, 194);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "바나나 에이전트";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Shown += new System.EventHandler(this.frmMain_Shown);
			this.contextMenuStrip1.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon _ntfIcon;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel _tsslStatus;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 보기ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 다운로드매니저ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 도큐먼트뷰어ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 도움말ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 프로그램정보ToolStripMenuItem;
		private System.Windows.Forms.PictureBox pictureBox1;
		private Windows.Controls.TextBox textBox1;
		private System.Windows.Forms.ToolStripMenuItem 바나나프레임워크홈페이지ToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 다운로드매니저ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 도뮤먼트뷰어ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 종료ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem 프로그램정보ToolStripMenuItem1;
	}
}

