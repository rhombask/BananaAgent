using BANANA.Common.Models.FileUpload;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANANA.Agent.Views
{
	/// <summary>
	/// 제  목: 도큐먼트 다운로더
	/// 작성자: 손정민(jmson@infinihance.com, 010-285-4368)
	/// 작성일: 2015-12-08 17:05
	/// 설  명: 
	/// </summary>
	public partial class DocumentDownloader : Views.BaseForm
	{
		// Fields
		WebClient _wc		= new WebClient();

		// Properties
		#region FileToDownload : 도큐먼트 뷰어에서 다운로드할 파일 모델
		/// <summary>
		/// 도큐먼트 뷰어에서 다운로드할 파일 모델
		/// </summary>
		public DownloadFile FileToDownload { get; set; }
		#endregion

		#region LocalFilePath : 로컬에 다운로드 받은 임시 파일 경로
		/// <summary>
		/// 로컬에 다운로드 받은 임시 파일 경로
		/// </summary>
		public string LocalFilePath { get; set; }
		#endregion

		#region Extension : 현재 핸들링 하는 파일의 확장자
		/// <summary>
		/// 현재 핸들링 하는 파일의 확장자
		/// </summary>
		public string Extension { get; set; }
		#endregion

		#region DownloadError : 다운로드 오류
		/// <summary>
		/// 다운로드 오류
		/// </summary>
		public Exception DownloadError { get; set; }
		#endregion

		// Constructor
		#region DocumentDownloader : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		public DocumentDownloader()
		{
			InitializeComponent();
		}
		#endregion

		#region DocumentDownloader_Load : 폼 로드 이벤트
		/// <summary>
		/// 폼 로드 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DocumentDownloader_Load(object sender, EventArgs e)
		{
			try
			{
				_wc.DownloadProgressChanged += _wc_DownloadProgressChanged;
				_wc.DownloadFileCompleted	+= _wc_DownloadFileCompleted;

				progressBar1.Maximum		= 100;
				_lblFileName.Text			= this.FileToDownload.FileName;
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region DocumentDownloader_Shown : 폼 보이기 이벤트
		/// <summary>
		/// 폼 보이기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DocumentDownloader_Shown(object sender, EventArgs e)
		{
			try
			{
				// 임시로 다운로드 받을 경로를 설정한다.
				string _localDirectory	= Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
				string _filePath		= string.Format("{0}\\{1}{2}"
					, _localDirectory
					, this.FileToDownload.Guid
					, this.Extension
					);
				this.LocalFilePath		= _filePath;

				// 파일 다운로드 시작
				_wc.DownloadFileAsync(new Uri(this.FileToDownload.FileWebUrl), _filePath);
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region _wc_DownloadFileCompleted : 다운로드 완료 이벤트
		/// <summary>
		/// 다운로드 완료 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			try
			{
				this.DownloadError	= e.Error;
				this.DialogResult	= System.Windows.Forms.DialogResult.OK;
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region _wc_DownloadProgressChanged : 다운로드 프로그레스 변경 이벤트
		/// <summary>
		/// 다운로드 프로그레스 변경 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			try
			{
				double _percent		= (double)e.BytesReceived / (double)this.FileToDownload.FileSize * (double)100;
				progressBar1.Value	= Convert.ToInt32(_percent);
			}
			catch
			{
				/*
				 * 오피스 문서가 Pdf로 변환 되었을 경우에는 DB에 가지고 있던 크기와 달라진다.
				 * 따라서, 수신 완료한 바이트가 다운로드 받아야 하는(DB에 가지고 있던) 크기를 초과할 수 있기 때문에 오류는 무시하도록 한다.
				 */
			}
		}
		#endregion
	}
}
