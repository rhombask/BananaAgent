namespace BANANA.Agent.Views.PdfViewer
{
	partial class SearchPdf
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
			this.chkFullSearch = new System.Windows.Forms.CheckBox();
			this.chkSearchUp = new System.Windows.Forms.CheckBox();
			this.chkWholeWord = new System.Windows.Forms.CheckBox();
			this.btnCancelSearch = new System.Windows.Forms.Button();
			this.btnSearchNext = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// chkFullSearch
			// 
			this.chkFullSearch.AutoSize = true;
			this.chkFullSearch.Checked = true;
			this.chkFullSearch.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFullSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkFullSearch.Location = new System.Drawing.Point(173, 145);
			this.chkFullSearch.Name = "chkFullSearch";
			this.chkFullSearch.Size = new System.Drawing.Size(137, 17);
			this.chkFullSearch.TabIndex = 13;
			this.chkFullSearch.Text = "Search in full document";
			this.chkFullSearch.UseVisualStyleBackColor = true;
			// 
			// chkSearchUp
			// 
			this.chkSearchUp.AutoSize = true;
			this.chkSearchUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkSearchUp.Location = new System.Drawing.Point(173, 125);
			this.chkSearchUp.Name = "chkSearchUp";
			this.chkSearchUp.Size = new System.Drawing.Size(75, 17);
			this.chkSearchUp.TabIndex = 12;
			this.chkSearchUp.Text = "Search &up";
			this.chkSearchUp.UseVisualStyleBackColor = true;
			// 
			// chkWholeWord
			// 
			this.chkWholeWord.AutoSize = true;
			this.chkWholeWord.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chkWholeWord.Location = new System.Drawing.Point(173, 105);
			this.chkWholeWord.Name = "chkWholeWord";
			this.chkWholeWord.Size = new System.Drawing.Size(113, 17);
			this.chkWholeWord.TabIndex = 11;
			this.chkWholeWord.Text = "Match &whole word";
			this.chkWholeWord.UseVisualStyleBackColor = true;
			// 
			// btnCancelSearch
			// 
			this.btnCancelSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCancelSearch.Location = new System.Drawing.Point(453, 138);
			this.btnCancelSearch.Name = "btnCancelSearch";
			this.btnCancelSearch.Size = new System.Drawing.Size(75, 23);
			this.btnCancelSearch.TabIndex = 8;
			this.btnCancelSearch.Text = "&Cancel";
			this.btnCancelSearch.UseVisualStyleBackColor = true;
			// 
			// btnSearchNext
			// 
			this.btnSearchNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearchNext.Location = new System.Drawing.Point(453, 105);
			this.btnSearchNext.Name = "btnSearchNext";
			this.btnSearchNext.Size = new System.Drawing.Size(75, 23);
			this.btnSearchNext.TabIndex = 9;
			this.btnSearchNext.Text = "&Next";
			this.btnSearchNext.UseVisualStyleBackColor = true;
			// 
			// btnSearch
			// 
			this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnSearch.Location = new System.Drawing.Point(453, 76);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(75, 23);
			this.btnSearch.TabIndex = 10;
			this.btnSearch.Text = "&Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(121, 82);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Search:";
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(173, 79);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(274, 20);
			this.txtSearch.TabIndex = 6;
			// 
			// SearchPdf
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(649, 238);
			this.Controls.Add(this.chkFullSearch);
			this.Controls.Add(this.chkSearchUp);
			this.Controls.Add(this.chkWholeWord);
			this.Controls.Add(this.btnCancelSearch);
			this.Controls.Add(this.btnSearchNext);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtSearch);
			this.Name = "SearchPdf";
			this.Text = "SearchPdf";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkFullSearch;
		private System.Windows.Forms.CheckBox chkSearchUp;
		private System.Windows.Forms.CheckBox chkWholeWord;
		private System.Windows.Forms.Button btnCancelSearch;
		private System.Windows.Forms.Button btnSearchNext;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtSearch;

	}
}