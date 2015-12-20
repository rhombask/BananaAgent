using BANANA.Common.Models.FileUpload;
using System;
using System.Net;

namespace BANANA.Agent.Controllers.Events
{
	/// <summary>
	/// 제  목: 다운로드 바이트 수신 이벤트 아규먼트
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-07 12:03
	/// 설  명: 
	/// </summary>
	public class BytesReceivedEventArgs : EventArgs
	{
		// Properties
		#region DownloadFile : 다운로드 파일 모델
		/// <summary>
		/// 다운로드 파일 모델
		/// </summary>
		public DownloadFile DownloadFile { get; set; }
		#endregion

		#region JustReceivedBytesLength : 방금 수신한 바이트 길이
		/// <summary>
		/// 방금 수신한 바이트 길이
		/// 일반 웹 클라이언트는 현재까지 수신한 누적 바이트 길이만을 반환한다.
		/// </summary>
		public long JustReceivedBytesLength { get; set; }
		#endregion

		#region DownloadProgressChangedEventArgs : 웹 클라이언트의 프로그레스 변경 이벤트 아규먼트
		/// <summary>
		/// 웹 클라이언트의 프로그레스 변경 이벤트 아규먼트
		/// </summary>
		public DownloadProgressChangedEventArgs DownloadProgressChangedEventArgs { get; set; }
		#endregion

		// Constructor
		#region BytesReceivedEventArgs : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		/// <param name="_downloadFile"></param>
		/// <param name="_justReceivedBytes"></param>
		/// <param name="_downloadProgressChangedEventArgs"></param>
		public BytesReceivedEventArgs(DownloadFile _downloadFile, long _justReceivedBytes, DownloadProgressChangedEventArgs _downloadProgressChangedEventArgs)
		{
			this.DownloadFile						= _downloadFile;
			this.JustReceivedBytesLength			= _justReceivedBytes;
			this.DownloadProgressChangedEventArgs	= _downloadProgressChangedEventArgs;
		}
		#endregion
	}
}
