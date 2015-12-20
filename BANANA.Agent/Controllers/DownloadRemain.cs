using BANANA.Windows.Controls;
using System;
using System.Diagnostics;
using System.Timers;

namespace BANANA.Agent.Controllers
{
	/// <summary>
	/// 제  목: 남은 다운로드 시간 계산 클래스
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-08 04:58
	/// 설  명: 
	/// </summary>
	class DownloadRemain
	{
		// Fields
		Timer _timer				= new Timer();
		Stopwatch _stopWatch		= new Stopwatch();
		TimeSpan _timeSpan			= new TimeSpan();

		// Properties
		#region DisplayLabel : 잔여시간을 표시할 라벨 컨트롤
		/// <summary>
		/// 잔여시간을 표시할 라벨 컨트롤
		/// </summary>
		public Label DisplayLabel { get; set; }
		#endregion

		// Constructor
		#region DownloadRemain : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		public DownloadRemain()
		{
			_timer.Elapsed		+= _timer_Elapsed;
			_timer.Interval		= 1000;
			_timer.AutoReset	= true;
		}
		#endregion

		// Methods
		#region StartCheck : 시간 체크 시작
		/// <summary>
		/// 시간 체크 시작
		/// </summary>
		public void StartCheck()
		{
			try
			{
				// 스탑와치 시작
				_stopWatch.Start();
				
				// 타이머 시작
				_timer.Enabled		= true;
				_timer.Start();
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region CheckIn : 중간 체크인
		/// <summary>
		/// 중간 체크인
		/// </summary>
		/// <param name="ReceivedBytes">이제까지 수신한 바이트</param>
		/// <param name="TotalBytes">총 바이트</param>
		public void CheckIn(long ReceivedBytes, long TotalBytes)
		{
			try
			{
				if (ReceivedBytes == 0)
				{
					this._timeSpan		= TimeSpan.Zero;
				}
				else
				{
					float _elapsedMin	= ((float)_stopWatch.ElapsedMilliseconds / 1000) / 60;
					float _timeLeft		= (_elapsedMin / ReceivedBytes) * (TotalBytes - ReceivedBytes);
					this._timeSpan		= TimeSpan.FromMinutes(_timeLeft);
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region StopCheck : 시간 체크 중지
		/// <summary>
		/// 시간 체크 중지
		/// </summary>
		public void StopCheck()
		{
			try
			{
				// 스탑와치 중지
				_stopWatch.Stop();
				
				// 타이머 중지
				_timer.Enabled		= false;
				_timer.Stop();
				
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region _timer_Elapsed : 타이머 틱 이벤트
		/// <summary>
		/// 타이머 틱 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				this.DisplayLabel.Invoke(new Action(() => {
					this.DisplayLabel.Text	= this._timeSpan.ToString(@"hh\:mm\:ss");
				}));
			}
			catch
			{
				throw;
			}
		}
		#endregion
	}
}
