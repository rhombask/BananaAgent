using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANANA.Agent
{
	/// <summary>
	/// 제  목: 바나나 에이전트
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-06 02:51
	/// 설  명: 
	/// </summary>
	static class Program
	{
		#region Main : 메인 함수
		/// <summary>
		/// 메인 함수
		/// </summary>
		/// <param name="args"></param>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			try
			{
				string _parameters	= string.Empty;
				#region 파리미터 정리
				// 프로토콜 마지막 부분에 /가 붙어서 오는 경우에는 마지막 / 문자를 없애도록 하자. 해당 문자열이 포함되면, 복호화에 문제가 생긴다.
				if ((args.Length > 0) && ((!string.IsNullOrEmpty(args[0])) && (args[0].Substring(args[0].Length - 1, 1) == "/")))
				{
					args[0]		= args[0].Substring(0, args[0].Length - 1);
					_parameters	= args[0];
				}
				_parameters		= _parameters.Replace("banana://", "");
				#endregion

				#region ========== 디버깅용 파라미터 강제 적용 ==========
				if (System.Diagnostics.Debugger.IsAttached)
				{
					// 파일 다운로드
					//_parameters	= "QUV2eDJueEJHUTNKYUU2a1NNOFAyT01xN3dsOVFMd0xMOVhqWmNPZXRYWGlkRGp3MEZjZEJSTUpRRGhTcit3UUFtbHhYWHFoR0Z3eUk4eENoV3JIbG8zeHlNNktOQkkwRVFJbXRhaDNjRHJ0OFQwcmRFWDFmMHpSaENoTEZ0Vng1cVRSSHc2aW84MD0=";

					// 도큐먼트 뷰어
					//_parameters		= "eGZMN0krSU9LK3QvTHNyUWYrUWVndTc0Wnp1R1U2RXltQ3owZ0FVQnNCV0lFMkZJNFJ5dE15dzdseFBadDVZNWJnRVJjZFFOZ3hkZFdsb2VYWmJndE9ES2lCK2Q5QURUZmQ5bnRRVDkwdVkwY3c1ck9abGtDQ1M2ZURGVlQxZlNibkNLeENkMGFERU14UVZMY2d2NXlRPT0=";

					// 도큐먼트 뷰어(zip - 4기가)
					//_parameters	= "aHR0cDovL2xvY2FsaG9zdDo1MTYzMS9TdG9yYWdlL1htbC8yMDE1MTIwOC80ZmQzZjUzNC1lMDBjLTQ2MTQtYTg5ZS01ODM1YWU1NjgyMjEueG1s";
				}
				#endregion

				#region 프로그램 인스턴스는 한 번만 실행하도록 처리
				Assembly _assembly	= Assembly.GetExecutingAssembly();
				var _attributeGuid	= (GuidAttribute)_assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
				Mutex _mutex		= new Mutex(false, _attributeGuid.Value);
				if (!_mutex.WaitOne(0, false))
				{
					// 이미 인스턴스가 존재하니, 해당 인스턴스의 RunApp static 함수를 호출하도록 하자.
					frmMain.RunApp(_parameters);

					return;
				}
				#endregion

				// 여기까지 왔다는 의미는 프로세스 중에 동일한 인스턴스가 존재하지 않다는 뜻이다.
				bool successSetup		= false;
				string _firstRun		= BANANA.Common.UserInfo.GetCookieFromWindows("IsFirstRun");
				bool _isFirstRun		= string.IsNullOrEmpty(_firstRun) ? true : Convert.ToBoolean(_firstRun);

				#region 처음 실행 시, 바나나 프레임워크 라이선스 자동 다운로드
				if (_isFirstRun)
				{
					successSetup	= false;
					string[] assArr	= new string[] {
						"BANANA.Common.v4.5"
						, "BANANA.International.v4.5"
						, "BANANA.Web.Controls.v4.5"
						, "BANANA.Windows.v4.5"
						, "BANANA.Windows.Controls.v4.5"
						, "AppManager"
					};

					// 디버깅 중일 때에는 라이선스를 자동으로 발급 받지 않는다.
					if (!System.Diagnostics.Debugger.IsAttached)
					{
						successSetup	= AppManager.Usage.Setup(assArr);
					}
				}
				#endregion
					
				#region 레지스트리 프로토콜 등록
				RegistryKey _registryRoot	= Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Classes\banana");
					
				// 레지스트리에 프로토콜이 등록되어 있지 않음
				if (_registryRoot == null)
				{
					Process.Start(new ProcessStartInfo
						{
							Verb					= "runas"
							, FileName			= typeof(BANANA.Agent.Setup.Program).Assembly.Location
							, UseShellExecute	= true
							, Arguments			= typeof(BANANA.Agent.Program).Assembly.Location
							//, CreateNoWindow = true
						}).WaitForExit();
				}
				#endregion

				#region 처음 실행 시, 알리 메시지 팝업
				if (_isFirstRun)
				{
					// 라이선스 다운로드가 성공했다면
					if (successSetup)
					{
						// 처음실행이 아니라고 저장
						BANANA.Common.UserInfo.SetCookieToWindows("IsFirstRun", "false");

						MessageBox.Show("바나나 에이전트 설치를 완료 하였습니다.\r\n설치 화면을 닫고, 링크를 다시 클릭하세요.", "바나나 에이전트");
					}
				}
				#endregion

				#region 라이브 업데이트 처리
				// 디버깅 중일 때에는 라이브 업데이트를 실행하지 않는다.
				if (!System.Diagnostics.Debugger.IsAttached)
				{
					AppManager.LiveUpdate _live	= new AppManager.LiveUpdate();
					bool _userAction			= _live.Update();
					if (!_userAction)
					{
						//MessageBox.Show("사용자가 라이브 업데이트를 취소 하셨습니다.");
					}
				}
				#endregion
				
				GC.Collect();
				Application.Run(new frmMain(_parameters));
				//Application.Run(new Views.DocumentViewer());
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
			}
		}
		#endregion
	}
}
