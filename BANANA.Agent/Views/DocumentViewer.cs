using BANANA.Agent.Controllers.PdfViewer;
using BANANA.Common.Models.FileUpload;
using BANANA.Web.Controls;
using PDFLibNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANANA.Agent.Views
{
	/// <summary>
	/// 제  목: 도큐먼트 뷰어
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-08 16:04
	/// 설  명: 
	/// </summary>
	public partial class DocumentViewer : Views.BaseForm
	{
		[DllImport("user32.dll")]
		static extern int GetForegroundWindow();

		// Fields
		DocumentDownloader _downloader;
		PDFWrapper _pdfDoc = null;
		public static DocumentViewer Instance;
		DocumentViewerProperties _prop;
		
		#region Mouse Scrolling/Navigation Private Fields
		public enum CursorStatus
		{
			Select,
			Move,
			Zoom,
			Snapshot
		}

		Rectangle EmptyRectangle = new Rectangle(-1, -1, 0, 0);
		CursorStatus _cursorStatus = CursorStatus.Move;
		Point _pointStart = Point.Empty;
		Point _pointCurrent = Point.Empty;

		Point _bMouseCapturedStart = Point.Empty;
		bool _bMouseCaptured = false;
		int _pointX = 0;
		int _pointY = 0;
		#endregion

		// Properties
		#region FileToView : 도큐먼트 뷰어에서 확인할 파일 모델
		/// <summary>
		/// 도큐먼트 뷰어에서 확인할 파일 모델
		/// </summary>
		public DownloadFile FileToView { get; set; }
		#endregion

		#region Extension : 현재 핸들링 하는 파일의 확장자
		/// <summary>
		/// 현재 핸들링 하는 파일의 확장자
		/// </summary>
		public string Extension { get; set; }
		#endregion

		#region XmlFileContent : 디버깅을 위한 XML 파일 내용
		/// <summary>
		/// 디버깅을 위한 XML 파일 내용
		/// </summary>
		public string XmlFileContent { get; set; }
		#endregion

		// Constructor
		#region DocumentViewer : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		public DocumentViewer()
		{
			InitializeComponent();

			
			Instance	= this;
		}
		#endregion
		
		// Events
		#region DocumentViewer_Load : 폼 로드 이벤트
		/// <summary>
		/// 폼 로드 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DocumentViewer_Load(object sender, EventArgs e)
		{
			try
			{
				this.Text			= string.Format("{0} v{1}", this.Text, base.GetAppVersion());

				// 목차는 일단 숨김다.
				collapsibleSplitter1.ToggleState();
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region DocumentViewer_Shown : 폼 보이기 이벤트
		/// <summary>
		/// 폼 보이기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DocumentViewer_Shown(object sender, EventArgs e)
		{
			try
			{
				if (this.FileToView == null)
				{
					return;
				}

				// 도큐먼트를 다운로드 한다.
				_downloader					= new DocumentDownloader();
				_downloader.FileToDownload	= this.FileToView;
				_downloader.Extension		= this.Extension;
				DialogResult _res			= _downloader.ShowDialog();
				if (_res == System.Windows.Forms.DialogResult.OK)
				{
					// 다운로드 받은 파일의 로컬 경로를 저장해둔다.
					this.FileToView.PhysicalFilePath	= _downloader.LocalFilePath;

					if (_downloader.DownloadError != null)
					{
						BANANA.Windows.Logger.Error(_downloader.DownloadError);
						MessageBox.Show("도큐먼트를 다운로드 하는 중에 다음과 같은 오류가 발생하였습니다.\r\n\r\n" + _downloader.DownloadError.Message, "바나나 에이전트");
						return;
					}

					// 변환된 Pdf 파일을 로드한다.
					LoadPdfFile();
				}
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region DocumentViewer_FormClosing : 폼 닫기 이벤트
		/// <summary>
		/// 폼 닫기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DocumentViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			Gma.UserActivityMonitor.HookManager.MouseDown	-= new MouseEventHandler(HookManager_MouseDown);
			Gma.UserActivityMonitor.HookManager.MouseUp		-= new MouseEventHandler(HookManager_MouseUp);
			Gma.UserActivityMonitor.HookManager.MouseMove	-= new MouseEventHandler(HookManager_MouseMove);
		}
		#endregion
		
		#region DocumentViewer_Resize : 폼 크기 변경 이벤트
		/// <summary>
		/// 폼 크기 변경 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void DocumentViewer_Resize(object sender, EventArgs e)
		{
			if (_pdfDoc != null)
			{
				using (StatusBusy sb = new StatusBusy("PDF 문서의 크기를 변경하고 있습니다."))
				{
					FitWidth();
					Render();
				}
			}
		}
		#endregion
		
		#region tvwOutline_NodeMouseClick : 북마크 클릭 이벤트
		/// <summary>
		/// 북마크 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		void tvwOutline_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			try
			{
				using (StatusBusy sb = new StatusBusy("PDF 북마크를 검색하고 있습니다."))
				{
					OutlineItem ol = (OutlineItem)e.Node.Tag;
					if (ol != null)
					{
						ol.DoAction();
						switch (ol.GetKind())
						{
							case LinkActionKind.actionGoTo:
							case LinkActionKind.actionGoToR:
								//Transform the coordinates
								PointF loc = _pdfDoc.PointUserToDev(new PointF((float)ol.Destination.Left, (float)ol.Destination.Top));
								if (ol.Destination.ChangeTop)
									ScrolltoTop((int)loc.Y);
								else
									ScrolltoTop(0);
								break;
							case LinkActionKind.actionLaunch:
								break;
							case LinkActionKind.actionMovie:
								break;
							case LinkActionKind.actionURI:
								break;
						}
						FitWidth();
						Render();
					}
				}
			}
			catch (Exception err)
			{
				//base.Error(err);
			}
		}
		#endregion

		#region tvwOutline_BeforeExpand : 북마크 확장 이벤트
		/// <summary>
		/// 북마크 확장 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void tvwOutline_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			try
			{
				using (StatusBusy sb = new StatusBusy("PDF 북마크를 검색하고 있습니다."))
				{

					OutlineItem ol = (OutlineItem)e.Node.Tag;
					if (ol != null)
					{
						if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "dummy")
						{
							e.Node.Nodes.Clear();
							foreach (OutlineItem col in ol.Childrens)
							{
								TreeNode tn = new TreeNode(col.Title);
								tn.Tag = col;
								if (col.KidsCount > 0)
									tn.Nodes.Add(new TreeNode("dummy"));
								e.Node.Nodes.Add(tn);
							}
						}
					}
				}
			}
			catch (Exception err)
			{
				//base.Error(err);
			}
		}
		#endregion

		#region Mouse Scrolling
		private CursorStatus GetCursorStatus(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Middle)
			{
				return CursorStatus.Move;
			}
			return _cursorStatus;
		}
		private bool IsActive
		{
			get
			{
				return GetForegroundWindow() == this.Handle.ToInt32();
			}
		}
		private bool MouseInPage(Point p)
		{
			if (IsActive)
			{
				return pageViewer1.MouseInPage(p);
			}
			return false;
		}

		private void ScrolltoTop(int y)
		{
			Point dr = this.pageViewer1.ScrollPosition;
			if (_pdfDoc.PageHeight > pageViewer1.Height)
				dr.Y = y;
			pageViewer1.ScrollPosition = dr;
		}

		private PDFLibNet.PageLink SearchLink(Point location)
		{
			Point p = pageViewer1.PointToClient(location);
			List<PageLink> links = _pdfDoc.GetLinks(_pdfDoc.CurrentPage);
			if (links != null)
			{
				//Search for a link
				foreach (PDFLibNet.PageLink pl in links)
				{
					//Transform location
					Point loc = Point.Ceiling(_pdfDoc.PointUserToDev(pl.Bounds.Location));
					//Adjust size, 72dpi
					Size siz = Size.Round(new SizeF(pl.Bounds.Size.Width * (float)_pdfDoc.RenderDPI / 72f, pl.Bounds.Size.Height * (float)_pdfDoc.RenderDPI / 72f));
					//Translate
					loc = pageViewer1.PointUserToPage(loc);
					Rectangle linkLoc = new Rectangle(loc, siz);
					if (linkLoc.Contains(p))
						//Link found!
						return pl;
				}
			}
			return null;
		}

		void HookManager_MouseUp(object sender, MouseEventArgs e)
		{
			Point pos = pageViewer1.PointToClient(e.Location);
			if (MouseInPage(pos) && _bMouseCaptured)
			{
				switch (GetCursorStatus(e))
				{
					case CursorStatus.Move:
						
						break;
					case CursorStatus.Zoom:
						if (!_pointCurrent.Equals(EmptyPoint))
						{
							if (e.Button == MouseButtons.Left && _pdfDoc!=null)
								_pdfDoc.ZoomIN();
							else if (e.Button == MouseButtons.Right && _pdfDoc!=null)
								_pdfDoc.ZoomOut();
						}
						else
						{
							//Zoom on rectangle
							
						}    
						break;
				}
				pageViewer1.Cursor = Cursors.Default;
			}
			ReleaseRubberFrame();
			_bMouseCaptured = false;
		}

		void HookManager_MouseDown(object sender, MouseEventArgs e)
		{
			Point pos = pageViewer1.PointToClient(e.Location);
			if (MouseInPage(pos) && e.Button == MouseButtons.Left)
			{
				PDFLibNet.PageLink link = SearchLink(e.Location);
				if (link != null)
				{
					switch (link.Action.Kind)
					{
						case LinkActionKind.actionGoTo:

							PDFLibNet.PageLinkGoTo plgo = (link.Action as PDFLibNet.PageLinkGoTo);
							if (plgo.Destination != null)
							{
								_pdfDoc.CurrentPage = plgo.Destination.Page;
								PointF loc = _pdfDoc.PointUserToDev(new PointF((float)plgo.Destination.Left, (float)plgo.Destination.Top));
								if (plgo.Destination.ChangeTop)
									ScrolltoTop((int)loc.Y);
								else
									ScrolltoTop(0);
								_pdfDoc.RenderPage(pageViewer1.Handle);
								Render();
							}
							else if (plgo.DestinationName != null)
							{
								
							}
							break;
						case LinkActionKind.actionURI:
							PDFLibNet.PageLinkURI uri = (link.Action as PDFLibNet.PageLinkURI);
							if (MessageBox.Show("Launching external application" + Environment.NewLine + uri.URL, Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
							{
								System.Diagnostics.Process p = new System.Diagnostics.Process();
								p.StartInfo.FileName = GetDefaultBrowserPath();
								p.StartInfo.Arguments = uri.URL;
								p.Start();
							}
							break;
					}
				}
				else
				{
					_pointCurrent = e.Location;
					_pointStart = e.Location;
					_bMouseCaptured = true;
				}
			}
		}

		void HookManager_MouseMove(object sender, MouseEventArgs e)
		{
			Point pos = pageViewer1.PointToClient(e.Location);
			if (_pdfDoc != null)
			{
				if (MouseInPage(pos) && _bMouseCaptured)
				{
					//Handled by the control
				}
				else if (MouseInPage(pos))
				{
					//Check if we have the pointer on a link
					if (GetCursorStatus(e) == CursorStatus.Move)
					{
						PDFLibNet.PageLink link = SearchLink(e.Location);
						if (link != null)
						{
							pageViewer1.Cursor = Cursors.Hand;
						}
						else
						{
							pageViewer1.Cursor = Cursors.Default;
						}
					}
				}
			}
			_pointCurrent = e.Location; //Update current Point
		}
		#endregion

		#region Rubber Frame
		Point EmptyPoint = new Point(-1, -1);
		Point _lastPoint = new Point(-1, -1);
		private void DrawRubberFrame()
		{
			
			if (!_lastPoint.Equals(EmptyPoint) ||
				(_bMouseCaptured && !_pointCurrent.Equals(EmptyPoint))
			)

				if (!_lastPoint.Equals(EmptyPoint))
				{
					ReleaseRubberFrame();
				}
			_lastPoint = _pointCurrent;
			DrawRubberFrame(_pointStart, _pointCurrent);
		
		}

		private void DrawRubberFrame(Point p1, Point p2)
		{
			Rectangle rc = new Rectangle();

			// Convert the points to screen coordinates.
			//p1 = PointToScreen(p1);
			//p2 = PointToScreen(p2);
			// Normalize the rectangle.
			if (p1.X < p2.X)
			{
				rc.X = p1.X;
				rc.Width = p2.X - p1.X;
			}
			else
			{
				rc.X = p2.X;
				rc.Width = p1.X - p2.X;
			}
			if (p1.Y < p2.Y)
			{
				rc.Y = p1.Y;
				rc.Height = p2.Y - p1.Y;
			}
			else
			{
				rc.Y = p2.Y;
				rc.Height = p1.Y - p2.Y;
			}
			// Draw the reversible frame.
			ControlPaint.DrawReversibleFrame(rc,
							Color.Gray, FrameStyle.Dashed);

		}
		
		private void ReleaseRubberFrame()
		{
			if (!_lastPoint.Equals(EmptyPoint))
			{
				DrawRubberFrame(_pointStart, _lastPoint);
			}
			_lastPoint = EmptyPoint;
		}
		#endregion

		#region LoadPdfFile : PDF 파일 열기
		/// <summary>
		/// PDF 파일 열기
		/// </summary>
		void LoadPdfFile()
		{
			try
			{
				if (_pdfDoc == null)
				{
					_pdfDoc					= new PDFWrapper();
					_pdfDoc.PDFLoadBegin	+= new PDFLoadBeginHandler(_pdfDoc_PDFLoadBegin);
					_pdfDoc.PDFLoadCompeted += new PDFLoadCompletedHandler(_pdfDoc_PDFLoadCompeted);
				}
				pageViewer1.Visible = false;
				using (StatusBusy sb = new StatusBusy("PDF 파일을 읽고 있습니다."))
				{
					if (LoadPdfFile(this.FileToView.PhysicalFilePath))
					{
						_pdfDoc.CurrentPage = 1;
						
						// 파일명을 제목 표시줄에 출력해 준다.
						//this.Text		= string.Format("{0} - {1}", this.FileToView.FileName, this.Text);
						this.Text		= string.Format("{0} - {1}", _pdfDoc.Title, this.Text);

						#region 목차 로드
						tvwOutline.Nodes.Clear();

						int _outLines	= 0;
						foreach (PDFLibNet.OutlineItem ol in _pdfDoc.Outline)
						{
							TreeNode tn		= new TreeNode(ol.Title);
							tn.Tag = ol;
							if (ol.KidsCount > 0)
							{
								tn.Nodes.Add(new TreeNode("dummy"));
							}

							tvwOutline.Nodes.Add(tn);

							_outLines++;
						}

						// 목차 내용이 있으면, 보여주기
						if (_outLines > 0)
						{
							collapsibleSplitter1.ToggleState();
						}
						#endregion

						this.FitWidth();
						//this.FitHeight();

						pageViewer1.Visible		= true;
						this.Render();

						_tsbSave.Enabled		= this._prop.EnableSaveButton ? true : false;
						_tsbPrint.Enabled		= this._prop.EnablePrintButton ? true : false;
						_tsbPrev.Enabled		= this._prop.EnablePrintButton ? true : false;
						_tsbNext.Enabled		= this._prop.EnableNextButton ? true : false;
						_tscbRatio.Enabled		= this._prop.EnableScreenRatioComboBox ? true : false;
						_tsbZoomOut.Enabled		= this._prop.EnableZoomOutButton ? true : false;
						_tsbZoomIn.Enabled		= this._prop.EnableZoomInButton ? true : false;
						_tsbSearch.Enabled		= this._prop.EnableSearchButton ? true : false;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private bool LoadPdfFile(string filename)
		{
			try
			{
				return _pdfDoc.LoadPDF(filename);
			}
			catch (System.Security.SecurityException)
			{
				// 암호화된 PDF 문서 처리
				 //frmPassword frm = new frmPassword();
				 //if (frm.ShowDialog() == DialogResult.OK)
				 //{
				 //	if (!frm.UserPassword.Equals(String.Empty))
				 //		_pdfDoc.UserPassword = frm.UserPassword;
				 //	if (!frm.OwnerPassword.Equals(String.Empty))
				 //		_pdfDoc.OwnerPassword = frm.OwnerPassword;
				 //	return LoadFile(filename);
				 //}
				 //else
				 //{
				 //	MessageBox.Show("File encrypted",Text);
				 //	return false;
				 //}
				 return false;
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region FitWidth : 문서를 화면 넓이에 맞춤
		/// <summary>
		/// 문서를 화면 넓이에 맞춤
		/// </summary>
		private void FitWidth()
		{
			using (PictureBox p = new PictureBox())
			{
				p.Width = pageViewer1.ClientSize.Width;
				_pdfDoc.FitToWidth(p.Handle);
			}
			_pdfDoc.RenderPage(pageViewer1.Handle);
		}
		#endregion

		#region FitHeight : 문서를 화면 높이에 맞춤
		/// <summary>
		/// 문서를 화면 높이에 맞춤
		/// </summary>
		private void FitHeight()
		{
			using (PictureBox p = new PictureBox())
			{
				p.Width = pageViewer1.ClientSize.Height;
				_pdfDoc.FitToHeight(p.Handle);
			}
			_pdfDoc.RenderPage(pageViewer1.Handle);
			pageViewer1.Height = _pdfDoc.PageHeight;
		}
		#endregion

		#region GetDefaultBrowserPath : 컴퓨터의 기본 브라우저 경로 반환
		/// <summary>
		/// 컴퓨터의 기본 브라우저 경로 반환
		/// </summary>
		/// <returns></returns>
		private static string GetDefaultBrowserPath()
		{
			string key = @"htmlfile\shell\open\command";
			Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(key, false);
			// get default browser path
			return ((string)registryKey.GetValue(null, null)).Split('"')[1];
		}
		#endregion

		#region Render : 페이지 크기 계산
		/// <summary>
		/// 페이지 크기 계산
		/// </summary>
		
		private void Render()
		{
			pageViewer1.PageSize	= new Size(_pdfDoc.PageWidth, _pdfDoc.PageHeight);
			//txtPage.Text = string.Format("{0}/{1}", _pdfDoc.CurrentPage, _pdfDoc.PageCount);
		}
		#endregion

		#region _pdfDoc_PDFLoadBegin : PDF 로드중 이벤트
		/// <summary>
		/// PDF 로드중 이벤트
		/// </summary>
		void _pdfDoc_PDFLoadBegin()
		{
			try
			{
				//UpdateParamsUI(false);

				tvwOutline.BeforeExpand -= new TreeViewCancelEventHandler(tvwOutline_BeforeExpand);
				tvwOutline.NodeMouseClick -= new TreeNodeMouseClickEventHandler(tvwOutline_NodeMouseClick);
			
				this.Resize			-= new EventHandler(DocumentViewer_Resize);
				this.FormClosing	-= new FormClosingEventHandler(DocumentViewer_FormClosing);
			
				Gma.UserActivityMonitor.HookManager.MouseDown	-= new MouseEventHandler(HookManager_MouseDown);
				Gma.UserActivityMonitor.HookManager.MouseUp		-= new MouseEventHandler(HookManager_MouseUp);
				Gma.UserActivityMonitor.HookManager.MouseMove	-= new MouseEventHandler(HookManager_MouseMove);
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region _pdfDoc_PDFLoadCompeted : PDF 로드완료 이벤트
		/// <summary>
		/// PDF 로드완료 이벤트
		/// </summary>
		void _pdfDoc_PDFLoadCompeted()
		{
			try
			{
				tvwOutline.BeforeExpand += new TreeViewCancelEventHandler(tvwOutline_BeforeExpand);
				tvwOutline.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvwOutline_NodeMouseClick);
			
				this.Resize			+= new EventHandler(DocumentViewer_Resize);
				this.FormClosing	+= new FormClosingEventHandler(DocumentViewer_FormClosing);
			
				Gma.UserActivityMonitor.HookManager.MouseDown += new MouseEventHandler(HookManager_MouseDown);
				Gma.UserActivityMonitor.HookManager.MouseUp += new MouseEventHandler(HookManager_MouseUp);
				Gma.UserActivityMonitor.HookManager.MouseMove += new MouseEventHandler(HookManager_MouseMove);

				//UpdateParamsUI();
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region _tsbOpen_Click : 열기 버튼 클릭 이벤트
		/// <summary>
		/// 열기 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tsbOpen_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult _res	= openFileDialog1.ShowDialog();
				if (_res == System.Windows.Forms.DialogResult.OK)
				{
					FileInfo _fi		= new FileInfo(openFileDialog1.FileName);

					this.FileToView		= new DownloadFile()
					{
						FileName			= _fi.Name.Replace(_fi.Extension, "")
						, FileSize			= _fi.Length
						, PhysicalFilePath	= _fi.FullName
					};

					// Pdf 파일을 로드한다.
					LoadPdfFile();
				}
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}
		#endregion

		#region _tsbSave_Click : 저장 버튼 클릭 이벤트
		/// <summary>
		/// 저장 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tsbSave_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult _res	= saveFileDialog1.ShowDialog();
				if (_res == System.Windows.Forms.DialogResult.OK)
				{
					System.IO.File.Copy(this.FileToView.PhysicalFilePath
						, saveFileDialog1.FileName
						, true
						);

					MessageBox.Show("PDF 문서를 저장했습니다.");
				}
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}
		#endregion

		#region _tsbPrint_Click : 인쇄 버튼 클릭 이벤트
		/// <summary>
		/// 인쇄 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tsbPrint_Click(object sender, EventArgs e)
		{
			try
			{
				string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".ps";

				if (printDialog1.ShowDialog() == DialogResult.OK)
				{
					using (StatusBusy sb = new StatusBusy("PDF 문서를 인쇄 중입니다."))
					{
						if (printDialog1.PrinterSettings.PrintToFile)
						{
							saveFileDialog1.Filter = "PostScript File (*.ps)|*.ps";
							if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
							{
								_pdfDoc.PrintToFile(saveFileDialog1.FileName, 1, _pdfDoc.PageCount);
							}

						}
						else
						{
							_pdfDoc.PrintToFile(fileName, 1, _pdfDoc.PageCount);
							RawPrinterHelper.SendFileToPrinter(printDialog1.PrinterSettings.PrinterName, fileName);
						}
					}
				}
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}
		#endregion

		#region _tsbPrev_Click : 이전 페이지 버튼 클릭 이벤트
		/// <summary>
		/// 이전 페이지 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tsbPrev_Click(object sender, EventArgs e)
		{
			try
			{
				using (StatusBusy sb = new StatusBusy("PDF 파일을 읽고 있습니다."))
				{
					if (_pdfDoc != null && !IsDisposed)
					{
						_pdfDoc.PreviousPage();
						_pdfDoc.RenderPage(pageViewer1.Handle);
						Render();
					}
				}
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}

		private bool pageViewer1_PreviousPage(object sender)
		{
			try
			{
				using (StatusBusy sb = new StatusBusy("PDF 파일을 읽고 있습니다."))
				{
					if (_pdfDoc.CurrentPage > 1)
					{
						_pdfDoc.PreviousPage();
						_pdfDoc.RenderPage(pageViewer1.Handle);
						Render();
						return true;
					}
				}
			}
			catch
			{
				throw;
			}
			return false;
		}
		#endregion

		#region _tsbNext_Click : 다음 페이지 버튼 클릭 이벤트
		/// <summary>
		/// 다음 페이지 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tsbNext_Click(object sender, EventArgs e)
		{
			try
			{
				using (StatusBusy sb = new StatusBusy("PDF 파일을 읽고 있습니다."))
				{
					if (_pdfDoc != null)
					{
						_pdfDoc.NextPage();
						_pdfDoc.RenderPage(pageViewer1.Handle);
						Render();
					}
				}
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}

		private bool pageViewer1_NextPage(object sender)
		{
			try
			{
				using (StatusBusy sb = new StatusBusy("PDF 파일을 읽고 있습니다."))
				{
					if (_pdfDoc.CurrentPage < _pdfDoc.PageCount)
					{
						_pdfDoc.NextPage();
						_pdfDoc.RenderPage(pageViewer1.Handle);
						Render();
						return true;
					}
				}
			}
			catch
			{
				throw;
			}
			return false;
		}
		#endregion

		#region _tscbRatio_DropDownClosed : 비율 조정 완료 이벤트
		/// <summary>
		/// 비율 조정 완료 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tscbRatio_DropDownClosed(object sender, EventArgs e)
		{
			try
			{
				if (_tscbRatio.SelectedItem == null)
				{
					return;
				}

				string _selectedValue	= _tscbRatio.SelectedItem.ToString();
				_selectedValue			= _selectedValue.Replace("%", "");
				double _ratio			= Convert.ToDouble(_selectedValue) / (double)100 * (double)100;

				_pdfDoc.Zoom			= _ratio;
				_pdfDoc.RenderPage(pageViewer1.Handle);
				Render();
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}
		#endregion

		#region _tsbZoomOut_Click : 줌아웃 버튼 클릭 이벤트
		/// <summary>
		/// 줌아웃 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tsbZoomOut_Click(object sender, EventArgs e)
		{
			try
			{
			using (StatusBusy sb = new StatusBusy("PDF 문서를 축소하고 있습니다."))
			{
				if (_pdfDoc != null)
				{
					_pdfDoc.ZoomOut();
					_pdfDoc.RenderPage(pageViewer1.Handle);
					Render();
				}
			}
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}
		#endregion

		#region _tsbZoomIn_Click : 줌인 버튼 클릭 이벤트
		/// <summary>
		/// 줌인 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tsbZoomIn_Click(object sender, EventArgs e)
		{
			try
			{
				using (StatusBusy sb = new StatusBusy("PDF 문서를 확대하고 있습니다."))
				{
					if (_pdfDoc != null)
					{
						_pdfDoc.ZoomIN();
						_pdfDoc.RenderPage(pageViewer1.Handle);
						Render();
					}
				}
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}
		#endregion

		/// <summary>
		/// 검색 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _tsbSearch_Click(object sender, EventArgs e)
		{
			try
			{
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				base.Error(err);
			}
		}

		#region pageViewer1_PaintControl : PDF 문서 페인트 이벤트
		/// <summary>
		/// PDF 문서 페인트 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="g"></param>
		private void pageViewer1_PaintControl(object sender, Graphics g)
		{
			try
			{
				if (_pdfDoc != null)
				{
					Rectangle r				= new Rectangle(pageViewer1.PageLocation, pageViewer1.CurrentView.Size);
					_pdfDoc.ClientBounds	= r;
					_pdfDoc.CurrentX		= pageViewer1.CurrentView.X;
					_pdfDoc.CurrentY		= pageViewer1.CurrentView.Y;
					_pdfDoc.DrawPageHDC(g.GetHdc());
					g.ReleaseHdc();
	/*
					if (_pdfDoc.RenderDPI >= g.DpiX)
					{
						foreach (PageLink pl in _pdfDoc.GetLinks(_pdfDoc.CurrentPage))
						{
							Point loc = Point.Round(_pdfDoc.PointUserToDev(pl.Bounds.Location));
							//Adjust size, 72dpi
							Size siz = new Size((int)(1.5 * pl.Bounds.Size.Width * _pdfDoc.RenderDPI / g.DpiX),(int)( 1.5 * pl.Bounds.Size.Height *_pdfDoc.RenderDPI / g.DpiY));
							//Translate
							loc = pageViewControl1.PointUserToPage(loc);
							Rectangle linkLoc = new Rectangle(loc, siz);
							g.DrawRectangle(Pens.Blue, linkLoc);
						}
					}
	 */

				}
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region SetConfiguration : 환경설정
		/// <summary>
		/// 환경설정
		/// </summary>
		/// <param name="Config">환경설정 모델</param>
		public void SetConfiguration(DocumentViewerProperties Config)
		{
			try
			{
				// 버튼들 처리
				this._tsbOpen.Enabled			= Config.EnableOpenButton;
				this._tsbSave.Enabled			= Config.EnableSaveButton;
				this._tsbPrint.Enabled			= Config.EnablePrintButton;
				this._tsbPrev.Enabled			= Config.EnablePreviousButton;
				this._tsbNext.Enabled			= Config.EnableNextButton;
				this._tscbRatio.Enabled			= Config.EnableScreenRatioComboBox;
				this._tsbZoomOut.Enabled		= Config.EnableZoomOutButton;
				this._tsbZoomIn.Enabled			= Config.EnableZoomInButton;
				this._tsbSearch.Enabled			= Config.EnableSearchButton;

				this._prop						= Config;
			}
			catch
			{
				throw;
			}
		}
		#endregion
	}
}
