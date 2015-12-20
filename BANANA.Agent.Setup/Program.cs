using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANANA.Agent.Setup
{
	/// <summary>
	/// 제  목: 바나나 에이전트 초기 셋업 프로그램
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-13 22:00
	/// 설  명: 
	/// </summary>
	public class Program
	{
		#region Main : 메인 함수
		/// <summary>
		/// 메인 함수
		/// 바나나 에이전트를 구동하는데 필요한 일반적인 작업들을 처리한다.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			try
			{
				//BANANA.Windows.Logger.Info("BANANA.Agent.Setup 진입");

				if ((args == null) || (args.Length == 0))
				{
					Console.Write("바나나 에이전트의 파일 경로를 파라미터로 전달해 주세요(예: C:\\>BANANA.Agent.Setup.exe D:\\emp\\BANANA.Agent.exe).");
					Console.Read();
					return;
				}

				// 기존 바나나 프로토콜 존재여부 확인 및 삭제
				UnregisterProtocol();
				
				//BANANA.Windows.Logger.Info("프로토콜 등록 삭제 완료");

				// 바나나 프로토콜 등록
				RegisterProtocol(args[0]);
				
				//BANANA.Windows.Logger.Info("프로토콜 등록 완료");

				// 시작 프로그램 등록
				RegisterStartUp(args[0]);
				
				//BANANA.Windows.Logger.Info("시작 프로그램 등록 완료");

				Console.Write("바나나 에이전트 초기 셋업을 완료 하였습니다.");
			}
			catch (Exception err)
			{
				BANANA.Windows.Logger.Error(err);
				Console.Write(err.Message);
				Console.Read();
			}
		}
		#endregion

		#region UnregisterProtocol : 기존 바나나 프로토콜 존재여부 확인 및 삭제
		/// <summary>
		/// 기존 바나나 프로토콜 존재여부 확인 및 삭제
		/// </summary>
		static void UnregisterProtocol()
		{
			try
			{
				RegistryKey _classRoot		= Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Classes", true);

				RegistryKey _bananaRoot		= _classRoot.OpenSubKey("banana", true);
				if (_bananaRoot == null) return;

				RegistryKey _icon			= _bananaRoot.OpenSubKey("DefaultIcon", true);
				if (_icon == null)
				{
					_classRoot.DeleteSubKey("banana", false);
					return;
				}

				RegistryKey _shell			= _bananaRoot.OpenSubKey("Shell", true);
				if (_shell == null)
				{
					_bananaRoot.DeleteSubKey("DefaultIcon", false);
					_classRoot.DeleteSubKey("banana", false);
					return;
				}

				RegistryKey _open			= _shell.OpenSubKey("Open", true);
				if (_open == null)
				{
					_bananaRoot.DeleteSubKey("Shell", false);
					_bananaRoot.DeleteSubKey("DefaultIcon", false);
					_classRoot.DeleteSubKey("banana", false);
					return;
				}

				RegistryKey _command		= _open.OpenSubKey("command", true);
				if (_command == null)
				{
					_open.DeleteSubKey("Open", false);
					_bananaRoot.DeleteSubKey("Shell", false);
					_bananaRoot.DeleteSubKey("DefaultIcon", false);
					_classRoot.DeleteSubKey("banana", false);
					return;
				}
				
				_open.DeleteSubKey("command", false);
				_shell.DeleteSubKey("Open", false);
				_bananaRoot.DeleteSubKey("Shell", false);
				_bananaRoot.DeleteSubKey("DefaultIcon", false);
				_classRoot.DeleteSubKey("banana", false);
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region RegisterProtocol : 바나나 프로토콜 등록
		/// <summary>
		/// 바나나 프로토콜 등록
		/// </summary>
		/// <param name="AgentApplicationPath">바나나 에이전트 파일 경로</param>
		static void RegisterProtocol(string AgentApplicationPath)
		{
			try
			{
				RegistryKey _classRoot		= Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Classes", true);

				RegistryKey _bananaRoot		= _classRoot.CreateSubKey("banana");
				_bananaRoot.SetValue("", "URL:banana Protocol");
				_bananaRoot.SetValue("URL Protocol", "");

				RegistryKey _icon			= _bananaRoot.CreateSubKey("DefaultIcon");
				_icon.SetValue("", string.Format("{0}, 1", AgentApplicationPath));

				RegistryKey _shell			= _bananaRoot.CreateSubKey("Shell");

				RegistryKey _open			= _shell.CreateSubKey("Open");

				RegistryKey _command		= _open.CreateSubKey("command");
				_command.SetValue("", string.Format("\"{0}\" \"%1\"", AgentApplicationPath));
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region RegisterStartUp : 시작 프로그램 등록
		/// <summary>
		/// 시작 프로그램 등록
		/// </summary>
		/// <param name="AgentApplicationPath">바나나 에이전트 파일 경로</param>
		static void RegisterStartUp(string AgentApplicationPath)
		{
			try
			{
				RegistryKey _runRoot		= Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
				object _val					= _runRoot.GetValue("BananaAgent");
				if (_val != null)
				{
					_runRoot.DeleteValue("BananaAgent");
				}
				_runRoot.SetValue("BananaAgent", AgentApplicationPath);
			}
			catch
			{
				throw;
			}
		}
		#endregion
	}
}
