using BANANA.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace BANANA.Agent.Controllers
{
	/// <summary>
	/// 제  목: 다운로드 스피드 계산 클래스
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-08 04:58
	/// 설  명: 
	/// </summary>
	class DownloadSpeed
	{
		// Fields
		Timer _timer				= new Timer();
		System.DateTime _dtStart	= System.DateTime.Now;
		List<double> _checkPoint	= new List<double>();

		// Properties
		#region DisplayLabel : 다운로드 스피드를 표시할 라벨 컨트롤
		/// <summary>
		/// 다운로드 스피드를 표시할 라벨 컨트롤
		/// </summary>
		public Label DisplayLabel { get; set; }
		#endregion

		// Constructor
		#region DownloadRemain : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		public DownloadSpeed()
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
		/// <param name="JustReceivedBytes">수신한 바이트(누적 아님)</param>
		public void CheckIn(long JustReceivedBytes)
		{
			double _dur		= (System.DateTime.Now - _dtStart).TotalMilliseconds;

			try
			{
				if (_dur > 0)
				{
					double _speed		= JustReceivedBytes / _dur * (double)1000;
					_checkPoint.Add(_speed);
				}

				_dtStart		= System.DateTime.Now;
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
				if (_checkPoint.Count > 0)
				{
					this.DisplayLabel.Invoke(new Action(() => {
						double _avg				= _checkPoint.ToArray().Average();
						this.DisplayLabel.Text	= string.Format("{0} / 초", BANANA.Common.Miscellaneous.GetDataAmount((long)Math.Round(_avg)));
					}));
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion
	}
}
