namespace BANANA.Agent.Views
{
	partial class DocumentViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentViewer));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this._tsbOpen = new System.Windows.Forms.ToolStripButton();
			this._tsbSave = new System.Windows.Forms.ToolStripButton();
			this._tsbPrint = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._tsbPrev = new System.Windows.Forms.ToolStripButton();
			this._tsbNext = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this._tscbRatio = new System.Windows.Forms.ToolStripComboBox();
			this._tsbZoomOut = new System.Windows.Forms.ToolStripButton();
			this._tsbZoomIn = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this._tsbSearch = new System.Windows.Forms.ToolStripButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tvwOutline = new System.Windows.Forms.TreeView();
			this.collapsibleSplitter1 = new BANANA.Agent.CollapsibleSplitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.pageViewer1 = new BANANA.Agent.Views.PdfViewer.PageViewer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsbOpen,
            this._tsbSave,
            this._tsbPrint,
            this.toolStripSeparator1,
            this._tsbPrev,
            this._tsbNext,
            this.toolStripSeparator2,
            this._tscbRatio,
            this._tsbZoomOut,
            this._tsbZoomIn,
            this.toolStripSeparator3,
            this._tsbSearch});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1134, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// _tsbOpen
			// 
			this._tsbOpen.Image = global::BANANA.Agent.Properties.Resources.dmdskres_373_9_16x16x32;
			this._tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._tsbOpen.Name = "_tsbOpen";
			this._tsbOpen.Size = new System.Drawing.Size(51, 22);
			this._tsbOpen.Text = "열기";
			this._tsbOpen.Click += new System.EventHandler(this._tsbOpen_Click);
			// 
			// _tsbSave
			// 
			this._tsbSave.Enabled = false;
			this._tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("_tsbSave.Image")));
			this._tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._tsbSave.Name = "_tsbSave";
			this._tsbSave.Size = new System.Drawing.Size(51, 22);
			this._tsbSave.Text = "저장";
			this._tsbSave.Click += new System.EventHandler(this._tsbSave_Click);
			// 
			// _tsbPrint
			// 
			this._tsbPrint.Enabled = false;
			this._tsbPrint.Image = global::BANANA.Agent.Properties.Resources.FeedbackTool_109_12_16x16x32;
			this._tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._tsbPrint.Name = "_tsbPrint";
			this._tsbPrint.Size = new System.Drawing.Size(51, 22);
			this._tsbPrint.Text = "인쇄";
			this._tsbPrint.Click += new System.EventHandler(this._tsbPrint_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// _tsbPrev
			// 
			this._tsbPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._tsbPrev.Enabled = false;
			this._tsbPrev.Image = global::BANANA.Agent.Properties.Resources.netshell_21611_1_16x16x32;
			this._tsbPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._tsbPrev.Name = "_tsbPrev";
			this._tsbPrev.Size = new System.Drawing.Size(23, 22);
			this._tsbPrev.Text = "이전 페이지";
			this._tsbPrev.Click += new System.EventHandler(this._tsbPrev_Click);
			// 
			// _tsbNext
			// 
			this._tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._tsbNext.Enabled = false;
			this._tsbNext.Image = global::BANANA.Agent.Properties.Resources.netshell_1611_1_16x16x32;
			this._tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._tsbNext.Name = "_tsbNext";
			this._tsbNext.Size = new System.Drawing.Size(23, 22);
			this._tsbNext.Text = "다음 페이지";
			this._tsbNext.Click += new System.EventHandler(this._tsbNext_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// _tscbRatio
			// 
			this._tscbRatio.Enabled = false;
			this._tscbRatio.Items.AddRange(new object[] {
            "10%",
            "20%",
            "30%",
            "40%",
            "50%",
            "60%",
            "70%",
            "80%",
            "90%",
            "100%"});
			this._tscbRatio.Name = "_tscbRatio";
			this._tscbRatio.Size = new System.Drawing.Size(75, 25);
			this._tscbRatio.DropDownClosed += new System.EventHandler(this._tscbRatio_DropDownClosed);
			// 
			// _tsbZoomOut
			// 
			this._tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._tsbZoomOut.Enabled = false;
			this._tsbZoomOut.Image = global::BANANA.Agent.Properties.Resources.ZoomOut;
			this._tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._tsbZoomOut.Name = "_tsbZoomOut";
			this._tsbZoomOut.Size = new System.Drawing.Size(23, 22);
			this._tsbZoomOut.Text = "축소";
			this._tsbZoomOut.Click += new System.EventHandler(this._tsbZoomOut_Click);
			// 
			// _tsbZoomIn
			// 
			this._tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._tsbZoomIn.Enabled = false;
			this._tsbZoomIn.Image = global::BANANA.Agent.Properties.Resources.ZoomIn;
			this._tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._tsbZoomIn.Name = "_tsbZoomIn";
			this._tsbZoomIn.Size = new System.Drawing.Size(23, 22);
			this._tsbZoomIn.Text = "확대";
			this._tsbZoomIn.Click += new System.EventHandler(this._tsbZoomIn_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// _tsbSearch
			// 
			this._tsbSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this._tsbSearch.Enabled = false;
			this._tsbSearch.Image = global::BANANA.Agent.Properties.Resources.SearchFolder_323_3_16x16x32;
			this._tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this._tsbSearch.Name = "_tsbSearch";
			this._tsbSearch.Size = new System.Drawing.Size(23, 22);
			this._tsbSearch.Text = "검색";
			this._tsbSearch.Click += new System.EventHandler(this._tsbSearch_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(266, 564);
			this.panel1.TabIndex = 1;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tvwOutline);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(5, 5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 554);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "목차";
			// 
			// tvwOutline
			// 
			this.tvwOutline.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwOutline.Font = new System.Drawing.Font("Courier New", 9.75F);
			this.tvwOutline.Location = new System.Drawing.Point(3, 16);
			this.tvwOutline.Name = "tvwOutline";
			this.tvwOutline.Size = new System.Drawing.Size(250, 535);
			this.tvwOutline.TabIndex = 8;
			// 
			// collapsibleSplitter1
			// 
			this.collapsibleSplitter1.AnimationDelay = 20;
			this.collapsibleSplitter1.AnimationStep = 20;
			this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
			this.collapsibleSplitter1.ControlToHide = this.panel1;
			this.collapsibleSplitter1.ExpandParentForm = false;
			this.collapsibleSplitter1.Location = new System.Drawing.Point(266, 25);
			this.collapsibleSplitter1.Name = "collapsibleSplitter1";
			this.collapsibleSplitter1.TabIndex = 2;
			this.collapsibleSplitter1.TabStop = false;
			this.collapsibleSplitter1.UseAnimations = false;
			this.collapsibleSplitter1.VisualStyle = BANANA.Agent.VisualStyles.Mozilla;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.groupBox2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(274, 25);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(5);
			this.panel2.Size = new System.Drawing.Size(860, 564);
			this.panel2.TabIndex = 3;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.pageViewer1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(5, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(850, 554);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "PDF 문서";
			// 
			// pageViewer1
			// 
			this.pageViewer1.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.pageViewer1.BorderColor = System.Drawing.Color.Black;
			this.pageViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pageViewer1.DrawBorder = false;
			this.pageViewer1.DrawShadow = false;
			this.pageViewer1.Location = new System.Drawing.Point(3, 16);
			this.pageViewer1.Margin = new System.Windows.Forms.Padding(10);
			this.pageViewer1.Name = "pageViewer1";
			this.pageViewer1.PageColor = System.Drawing.SystemColors.AppWorkspace;
			this.pageViewer1.PageSize = new System.Drawing.Size(0, 0);
			this.pageViewer1.PaintMethod = BANANA.Agent.Views.PdfViewer.PageViewer.DoubleBufferMethod.BuiltInOptimizedDoubleBuffer;
			this.pageViewer1.ScrollPosition = new System.Drawing.Point(-10, -10);
			this.pageViewer1.Size = new System.Drawing.Size(844, 535);
			this.pageViewer1.TabIndex = 0;
			this.pageViewer1.NextPage += new BANANA.Agent.Views.PdfViewer.PageViewer.MovePageHandler(this.pageViewer1_NextPage);
			this.pageViewer1.PreviousPage += new BANANA.Agent.Views.PdfViewer.PageViewer.MovePageHandler(this.pageViewer1_PreviousPage);
			this.pageViewer1.PaintControl += new BANANA.Agent.Views.PdfViewer.PageViewer.PaintControlHandler(this.pageViewer1_PaintControl);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 589);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1134, 22);
			this.statusStrip1.TabIndex = 12;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(39, 17);
			this.StatusLabel.Text = "Ready";
			// 
			// printDialog1
			// 
			this.printDialog1.UseEXDialog = true;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "PDF 파일 (*.pdf)|*.pdf";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "PDF 파일 (*.pdf)|*.pdf";
			// 
			// DocumentViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(230)))));
			this.ClientSize = new System.Drawing.Size(1134, 611);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.collapsibleSplitter1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DocumentViewer";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "도큐먼트 뷰어";
			this.Load += new System.EventHandler(this.DocumentViewer_Load);
			this.Shown += new System.EventHandler(this.DocumentViewer_Shown);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton _tsbOpen;
		private System.Windows.Forms.ToolStripButton _tsbSave;
		private System.Windows.Forms.ToolStripButton _tsbPrint;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton _tsbPrev;
		private System.Windows.Forms.ToolStripButton _tsbNext;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton _tsbZoomOut;
		private System.Windows.Forms.ToolStripButton _tsbZoomIn;
		private System.Windows.Forms.ToolStripComboBox _tscbRatio;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton _tsbSearch;
		private System.Windows.Forms.Panel panel1;
		private CollapsibleSplitter collapsibleSplitter1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TreeView tvwOutline;
		private System.Windows.Forms.StatusStrip statusStrip1;
		public System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private PdfViewer.PageViewer pageViewer1;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;




	}
}