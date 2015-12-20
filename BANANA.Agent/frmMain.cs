using BANANA.Agent.Controllers;
using BANANA.Common;
using BANANA.Common.Models.FileUpload;
using BANANA.Web.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BANANA.Agent
{
	/// <summary>
	/// 제  목: 바나나 에이전트 메인
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-06 02:20
	/// 설  명: 
	/// </summary>
	public partial class frmMain : Views.BaseForm
	{
		Views.AboutBox _aboutBox					= new Views.AboutBox();
		static Views.DownloadManager _downManager	= new Views.DownloadManager();
		static Views.DocumentViewer _docuViewer		= new Views.DocumentViewer();
		static Windows.ScreenScraping _sc			= new Windows.ScreenScraping();
		bool _reallyClose							= false;

		#region frmMain : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		public frmMain()
		{
			InitializeComponent();

			base.DoNotMaximize	= true;
		}
		#endregion

		#region frmMain : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		public frmMain(string url) : this()
		{
			RunApp(url);
		}
		#endregion

		#region frmMain_Load : 폼 로드 이벤트
		/// <summary>
		/// 폼 로드 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmMain_Load(object sender, EventArgs e)
		{
			try
			{
				////string _serverUrl	= base.GetEncryptTripleDES("http://app.mlmerp.com");
				//string _updateUrl	= base.GetEncryptTripleDES("/LiveUpdate/BANANA.Agent");

				this.Text			= string.Format("{0} v{1}", this.Text, base.GetAppVersion());

				base.SetCookie("BananaAgentFirstRunTime", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

				string _showTip	= base.GetCookie("ShowTip");
				if (!string.IsNullOrEmpty(_showTip))
				{
					if (!Convert.ToBoolean(_showTip))
					{
						_ntfIcon.BalloonTipIcon	= ToolTipIcon.None;
					}
				}
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region OnLoad : 처음 로드 시, 메인 폼 숨기기
		/// <summary>
		/// 처음 로드 시, 메인 폼 숨기기
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			this.Visible			= false;
			this.ShowInTaskbar		= false;

			base.OnLoad(e);
		}
		#endregion

		#region frmMain_Shown : 폼 보이기 이벤트
		/// <summary>
		/// 폼 보이기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmMain_Shown(object sender, EventArgs e)
		{
			try
			{
				_tsslStatus.Text		= string.Format("{0}부터 에이전트가 정상적으로 실행중입니다.", base.GetCookie("BananaAgentFirstRunTime"));
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region frmMain_FormClosing : 폼 닫히는 이벤트
		/// <summary>
		/// 폼 닫히는 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (!_reallyClose)
				{
					_ntfIcon.Visible	= true;
					_ntfIcon.ShowBalloonTip(500);
					this.Hide();
					e.Cancel			= true;

					base.SetCookie("ShowTip", "false");

					string _showTip	= base.GetCookie("ShowTip");
					if (!string.IsNullOrEmpty(_showTip))
					{
						if (!Convert.ToBoolean(_showTip))
						{
							_ntfIcon.BalloonTipIcon	= ToolTipIcon.None;
						}
					}
				}
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region RunApp : 응용 프로그램을 실행
		/// <summary>
		/// 응용 프로그램을 실행
		/// </summary>
		/// <param name="args">Payload XML 파일의 암호화돤 Url</param>
		public static void RunApp(string args)
		{
			try
			{
				if (!string.IsNullOrEmpty(args))
				{
					// Xml 파일의 웹 Url은 Triple-DES 암호화된 base64 문자열을 일반 문자열로 변환
					Views.BaseForm _bf		= new Views.BaseForm();
					string _xmlFileWebUrl	= _bf.ConvertBase64ToNormal(args);

					// 웹 서버의 Xml 파일 내용을 스크래핑
					string _xmlFileContents	= _sc.GetHtmlSource(_xmlFileWebUrl);

					// Xml 컨텐츠를 복호화 처리
					_xmlFileContents		= BANANA.Common.Encryption.DES.GetDecryptTripleDES(_xmlFileContents);

					// Xml Document로 로드
					XmlDocument _xDoc		= new XmlDocument();
					_xDoc.LoadXml(_xmlFileContents);

					string _protocol		= _xDoc.SelectSingleNode("//BANANA/Agent/Command").InnerText;
					
					switch (_protocol)
					{
						#region 다운로드 매니저
						case "DownloadFiles":
						{
							_downManager.ShowInTaskbar			= true;
							_downManager.XmlFileContent			= _xmlFileContents;
							XmlNodeList _nodes					= _xDoc.SelectNodes("//BANANA/Agent/Items/FileInfo");
							foreach (XmlNode _node in _nodes)
							{
								var _file	= new DownloadFile()
									{
										Guid			= _node["Guid"].InnerText
										, FileName		= _node["FileName"].InnerText
										, FileSize		= Convert.ToInt64(_node["FileSize"].InnerText)
										, FileWebUrl	= _node["FileWebUrl"].InnerText
										, StartTime		= System.DateTime.Now
										, IsCompleted	= false
									};
								_downManager.FilesToDownload.Add(_file);
							}
							_downManager.DownloadStatus			= Common.DownloadStatus.Ready;
							/*
							 * 환경설정 생성
							 * 일관된 관리를 위해서, BANANA.Web.Controls를 참조 걸었다.
							 */
							string _enableOpenFolderButton		= _xDoc.SelectSingleNode("//BANANA/Agent/DownloadManager/EnableOpenFolderButton").InnerText;
							string _enableChangeFolderButton	= _xDoc.SelectSingleNode("//BANANA/Agent/DownloadManager/EnableChangeFolderButton").InnerText;
							string _enableCancelButton			= _xDoc.SelectSingleNode("//BANANA/Agent/DownloadManager/EnableCancelButton").InnerText;
							string _rootFolder					= _xDoc.SelectSingleNode("//BANANA/Agent/DownloadManager/RootFolder").InnerText;
							string _defaultFolder				= _xDoc.SelectSingleNode("//BANANA/Agent/DownloadManager/DefaultFolder").InnerText;
							DownloadManagerProperties _dmConfig	= new DownloadManagerProperties() {
								EnableOpenFolderButton			= Convert.ToBoolean(_enableOpenFolderButton)
								, EnableChangeFolderButton		= Convert.ToBoolean(_enableChangeFolderButton)
								, EnableCancelButton			= Convert.ToBoolean(_enableCancelButton)
								, RootFolder					= (DownloadRootFolder)Enum.Parse(typeof(DownloadRootFolder), _rootFolder)
								, DefaultFolder					= _defaultFolder
							};
							_downManager.InsertQueue();					// 큐에 담기
							_downManager.SetConfiguration(_dmConfig);	// 환경설정
							_downManager.ShowDialog();
							break;
						}
						#endregion

						#region 도큐먼트 뷰어
						case "ViewDocument":
						{
							XmlNode _node						= _xDoc.SelectSingleNode("//BANANA/Agent/FileInfo");
							var _file	= new DownloadFile()
								{
									Guid			= _node["Guid"].InnerText
									, FileName		= _node["FileName"].InnerText
									, FileSize		= Convert.ToInt64(_node["FileSize"].InnerText)
									, FileWebUrl	= _node["FileWebUrl"].InnerText
									, StartTime		= System.DateTime.Now
									, IsCompleted	= false
								};
							// 열려고 하는 파일이 워드, 엑셀 및 파워포인트인지 확인
							string _fileExt						= Path.GetExtension(_file.FileWebUrl).ToLower();
							switch (_fileExt)
							{
								case ".pdf":
								case ".hwp":
								case ".doc":
								case ".docx":
								case ".xls":
								case ".xlsx":
								case ".ppt":
								case ".pptx":
								//case ".zip":
								{
									_docuViewer.XmlFileContent			= _xmlFileContents;
									_docuViewer.Extension				= _fileExt;
									_docuViewer.FileToView				= _file;
									_docuViewer.ShowInTaskbar			= true;
									/*
									 * 환경설정 생성
									 * 일관된 관리를 위해서, BANANA.Web.Controls를 참조 걸었다.
									 */
									string _enableOpenButton			= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnableOpenButton").InnerText;
									string _enableSaveButton			= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnableSaveButton").InnerText;
									string _enablePrintButton			= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnablePrintButton").InnerText;
									string _enablePreviousButton		= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnablePreviousButton").InnerText;
									string _enableNextButton			= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnableNextButton").InnerText;
									string _enableScreenRatioComboBox	= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnableScreenRatioComboBox").InnerText;
									string _enableZoomOutButton			= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnableZoomOutButton").InnerText;
									string _enableZoomInButton			= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnableZoomInButton").InnerText;
									string _enableSearchButton			= _xDoc.SelectSingleNode("//BANANA/Agent/DocumentViewer/EnableSearchButton").InnerText;
									DocumentViewerProperties _dvConfig	= new DocumentViewerProperties() {
										EnableOpenButton				= Convert.ToBoolean(_enableOpenButton)
										, EnableSaveButton				= Convert.ToBoolean(_enableSaveButton)
										, EnablePrintButton				= Convert.ToBoolean(_enablePrintButton)
										, EnablePreviousButton			= Convert.ToBoolean(_enablePreviousButton)
										, EnableNextButton				= Convert.ToBoolean(_enableNextButton)
										, EnableScreenRatioComboBox		= Convert.ToBoolean(_enableScreenRatioComboBox)
										, EnableZoomOutButton			= Convert.ToBoolean(_enableZoomOutButton)
										, EnableZoomInButton			= Convert.ToBoolean(_enableZoomInButton)
										, EnableSearchButton			= Convert.ToBoolean(_enableSearchButton)
									};
									_docuViewer.SetConfiguration(_dvConfig);	// 환경설정
									_docuViewer.ShowDialog();
									break;
								}

								default:
								{
									throw new ArgumentException("워드, 엑셀 및 파워포인트 외에는 도큐먼트 뷰어로 열 수가 없습니다.");
								}
							}
							break;
						}
						#endregion
					}
				}
			}
			catch (Exception err)
			{
				if (err.GetType() == typeof(System.Net.WebException))
				{
					WebException _netError	= (WebException)err;
					if (_netError.Status == WebExceptionStatus.ProtocolError)
					{
						int _errorCode	= (int)((HttpWebResponse)_netError.Response).StatusCode;
						if (_errorCode == 404)
						{
							MessageBox.Show("웹 서버에서 프로토콜 파일에 대한 경로를 확인할 수 없습니다.\r\n\r\n자세한 정보는 사용자 컴퓨터의 로그를 확인하세요.", "바나나 에이전트");
						}
						else
						{
							MessageBox.Show(_netError.Message, "바나나 에이전트");
						}
					}
					else
					{
						MessageBox.Show(_netError.Message, "바나나 에이전트");
					}
					err.HelpLink			= string.Format("Status: {0}, HelpLink: {1}"
						, _netError.Status
						, _netError.HelpLink
						);
				}
				else
				{
					err.HelpLink	= args;
					MessageBox.Show(err.Message, "바나나 에이전트");
				}
				BANANA.Windows.Logger.Error(err);
			}
		}
		#endregion

		#region _ntfIcon_MouseDoubleClick : 시스템 트레이 아이콘 더블 클릭 이벤트
		/// <summary>
		/// 시스템 트레이 아이콘 더블 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _ntfIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.ShowInTaskbar		= true;
			this.WindowState		= FormWindowState.Normal;
			this.Show();
		}
		#endregion

		#region 종료ToolStripMenuItem_Click : 종료 클릭 이벤트
		/// <summary>
		/// 종료 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_reallyClose	= true;
			Application.Exit();
		}
		#endregion

		#region 다운로드매니저ToolStripMenuItem_Click : 다운로드 매니저 클릭 이벤트
		/// <summary>
		/// 다운로드 매니저 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 다운로드매니저ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				_downManager.ShowInTaskbar	= false;
				_downManager.ShowDialog();
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region 도큐먼트뷰어ToolStripMenuItem_Click : 도큐먼트 뷰어 클릭 이벤트
		/// <summary>
		/// 도큐먼트 뷰어 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 도큐먼트뷰어ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				_docuViewer.ShowInTaskbar	= false;
				_docuViewer.ShowDialog();
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region 프로그램정보ToolStripMenuItem_Click : 프로그램 정보 클릭 이벤트
		/// <summary>
		/// 프로그램 정보 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 프로그램정보ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				_aboutBox.ShowDialog();
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region 바나나프레임워크홈페이지ToolStripMenuItem_Click : 바나나 프레임워크 홈페이지 클릭 이벤트
		/// <summary>
		/// 바나나 프레임워크 홈페이지 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 바나나프레임워크홈페이지ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.bananaframework.net");
		}
		#endregion

		#region 컨텍스트 메뉴
		#region 다운로드매니저ToolStripMenuItem1_Click : 다운로드 매니저 클릭 이벤트
		/// <summary>
		/// 다운로드 매니저 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 다운로드매니저ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				_downManager.ShowInTaskbar	= true;
				_downManager.ShowDialog();
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region 도뮤먼트뷰어ToolStripMenuItem_Click : 도큐먼트 뷰어 클릭 이벤트
		/// <summary>
		/// 도큐먼트 뷰어 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 도뮤먼트뷰어ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				_docuViewer.ShowInTaskbar	= true;
				_docuViewer.ShowDialog();
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region 프로그램정보ToolStripMenuItem1_Click : 프로그램 정보 클릭 이벤트
		/// <summary>
		/// 프로그램 정보 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 프로그램정보ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			프로그램정보ToolStripMenuItem.PerformClick();
		}
		#endregion

		#region 종료ToolStripMenuItem1_Click : 종료 클릭 이벤트
		/// <summary>
		/// 종료 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 종료ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			_reallyClose	= true;
			Application.Exit();
		}
		#endregion
		#endregion
	}
}
