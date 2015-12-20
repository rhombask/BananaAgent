using BANANA.Agent.Controllers.Events;
using BANANA.Common.Models.FileUpload;
using BANANA.Windows.Controls;
using System;
using System.ComponentModel;
using System.Net;

namespace BANANA.Agent.Controllers
{
	/// <summary>
	/// 제  목: 다운로드 핸들러
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-07 07:59
	/// 설  명: 
	/// </summary>
	public class DownloadHandler
	{
		// Fields
		long _lastReceivedByteSumLength	= 0;
		WebClient _webClient;

		// Events
		#region BytesReceived : 다운로드 파일 바이트 수신 이벤트
		/// <summary>
		/// 다운로드 파일 바이트 수신 이벤트
		/// </summary>
		public event EventHandler<BytesReceivedEventArgs> BytesReceived;
		#endregion

		#region _webClient_DownloadProgressChanged : 다운로드 프로그레스 변경 이벤트
		/// <summary>
		/// 다운로드 프로그레스 변경 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			try
			{
				long _justReceivedByteLength	= e.BytesReceived - _lastReceivedByteSumLength;
				_lastReceivedByteSumLength		= e.BytesReceived;

				// Caller의 이벤트 핸들러로 이벤트 전달
				if (this.BytesReceived != null)
				{
					this.BytesReceived(this, new BytesReceivedEventArgs(this.DownloadFile, _justReceivedByteLength, e));
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region DownloadFinished : 다운로드 완료 이벤트
		/// <summary>
		/// 다운로드 완료 이벤트
		/// </summary>
		public event EventHandler<DownloadFinishedEventArgs> DownloadFinished;
		#endregion

		#region _webClient_DownloadDataCompleted : 다운로드 완료 이벤트
		/// <summary>
		/// 다운로드 완료 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			try
			{
				// 다운로드할 파일의 완료여부 저장
				this.DownloadFile.IsCompleted	= true;

				// Caller의 이벤트 핸들러로 이벤트 전달
				if (this.DownloadFinished != null)
				{
					this.DownloadFinished(this, new DownloadFinishedEventArgs(this.DownloadFile, e));
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion

		// Properties
		#region DownloadFile : 다운로드 파일 모델
		/// <summary>
		/// 다운로드 파일 모델
		/// </summary>
		public DownloadFile DownloadFile { get; set; }
		#endregion

		#region ProgressBar : 프로그레스 바 컨트롤
		/// <summary>
		/// 프로그레스 바 컨트롤
		/// </summary>
		public ProgressBar ProgressBar { get; set; }
		#endregion

		#region LocalFileName : 다운로드해서 저장할 로컬 파일명(경로 포함)
		/// <summary>
		/// 다운로드해서 저장할 로컬 파일명(경로 포함)
		/// </summary>
		public string LocalFileName { get; set; }
		#endregion

		// Constructor
		#region DownloadHandler : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		public DownloadHandler()
		{
			this._webClient							= new WebClient();
			this._webClient.DownloadProgressChanged	+= _webClient_DownloadProgressChanged;
			this._webClient.DownloadFileCompleted	+= _webClient_DownloadFileCompleted;
		}
		#endregion

		// Methods
		#region StartDownload : 다운로드 시작
		/// <summary>
		/// 다운로드 시작
		/// </summary>
		public void StartDownload()
		{
			try
			{
				this._webClient.DownloadFileAsync(new Uri(this.DownloadFile.FileWebUrl), this.LocalFileName);
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region CancelDownload : 다운로드 취소
		/// <summary>
		/// 다운로드 취소
		/// </summary>
		public void CancelDownload()
		{
			try
			{
				this._webClient.CancelAsync();
			}
			catch
			{
				throw;
			}
		}
		#endregion
	}
}
