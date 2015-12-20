using BANANA.Common.Models.FileUpload;
using System;
using System.ComponentModel;
using System.Net;

namespace BANANA.Agent.Controllers.Events
{
	/// <summary>
	/// 제  목: 다운로드 완료 이벤트 아규먼트
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-07 12:03
	/// 설  명: 
	/// </summary>
	public class DownloadFinishedEventArgs : EventArgs
	{
		// Properties
		#region DownloadFile : 다운로드 파일 모델
		/// <summary>
		/// 다운로드 파일 모델
		/// </summary>
		public DownloadFile DownloadFile { get; set; }
		#endregion
		
		#region AsyncCompletedEventArgs : 웹 클라이언트의 다운로드 완료 이벤트 아규먼트
		/// <summary>
		/// 웹 클라이언트의 다운로드 완료 이벤트 아규먼트
		/// </summary>
		public AsyncCompletedEventArgs AsyncCompletedEventArgs { get; set; }
		#endregion
		
		// Constructor
		#region DownloadFinishedEventArgs : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		/// <param name="_downloadFile"></param>
		/// <param name="_downloadDataCompletedEventArgs"></param>
		public DownloadFinishedEventArgs(DownloadFile _downloadFile, AsyncCompletedEventArgs _asyncCompletedEventArgs)
		{
			this.DownloadFile				= _downloadFile;
			this.AsyncCompletedEventArgs	= _asyncCompletedEventArgs;
		}
		#endregion
	}
}
