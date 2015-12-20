using BANANA.Agent.Controllers;
using BANANA.Agent.Controllers.Events;
using BANANA.Common;
using BANANA.Common.Models.FileUpload;
using BANANA.Web.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANANA.Agent.Views
{
	/// <summary>
	/// 제  목: 다운로드 매니저
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-06 02:20
	/// 설  명: 
	/// </summary>
	public partial class DownloadManager : Views.BaseForm
	{
		// Fields
		long _totalBytesToReceive			= 0;
		long _allReceivedLength				= 0;
		string _saveDirectory				= string.Empty;
		DownloadHandler[] _handlers;
		System.DateTime _lastReceived;
		DownloadManagerProperties _prop;
		
		// 잔여시간 확인
		DownloadRemain _remainTime			= new DownloadRemain();

		// 전송속도 확인
		DownloadSpeed _downloadSpeed		= new DownloadSpeed();

		// Properties
		#region DownloadStatus : 다운로드 상태
		/// <summary>
		/// 다운로드 상태
		/// </summary>
		public DownloadStatus DownloadStatus { get; set; }
		#endregion

		#region FilesToDownload : 다운로드 받을 파일들의 모델 리스트
		List<DownloadFile> _filesToDownload		= new List<DownloadFile>();
		/// <summary>
		/// 다운로드 받을 파일들의 모델 리스트
		/// </summary>
		public List<DownloadFile> FilesToDownload
		{
			get { return _filesToDownload; }
			set { _filesToDownload = value ; }
		}
		#endregion

		#region XmlFileContent : 디버깅을 위한 XML 파일 내용
		/// <summary>
		/// 디버깅을 위한 XML 파일 내용
		/// </summary>
		public string XmlFileContent { get; set; }
		#endregion

		// Constructor
		#region DownloadManager : 생성자 함수
		/// <summary>
		/// 생성자 함수
		/// </summary>
		public DownloadManager()
		{
			InitializeComponent();

			base.DoNotMaximize	= true;
		}
		#endregion

		// Events
		#region DownloadManager_Load : 폼 로드 이벤트
		/// <summary>
		/// 폼 로드 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DownloadManager_Load(object sender, EventArgs e)
		{
			try
			{
				this.Text		= string.Format("{0} v{1}", this.Text, base.GetAppVersion());
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region DownloadManager_Shown : 폼 보이기 이벤트
		/// <summary>
		/// 폼 보이기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DownloadManager_Shown(object sender, EventArgs e)
		{
			try
			{
				if ((this.FilesToDownload == null) || (this.FilesToDownload.Count == 0))
				{
					_btnDownload.Enabled	= false;
				}
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region DownloadManager_FormClosing : 폼 닫기 이벤트
		/// <summary>
		/// 폼 닫기 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DownloadManager_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				// 한 번 다운로드가 완료된 창이면, 다운로드 버튼을 재 활성화 해 주고 리스트뷰의 아이템들은 삭제한다.
				if (this.DownloadStatus == Common.DownloadStatus.Completed)
				{
					_btnDownload.Enabled	= true;
					_lvDownloadFiles.Items.Clear();

					progressBar1.Maximum		= 0;
					progressBar1.Value			= 0;
					_lblTransferSpeed.Text		= "0 B / 초";
					_lblRemainingTime.Text		= "00:00:00";
					_lblDataAmount.Text			= "0 B / 0 B";
				}
				// 다운로드가 진행 중이면, 경고 메시지를 출력한다.
				else if (this.DownloadStatus == Common.DownloadStatus.Downloading)
				{
					DialogResult _res	= base.Ask("다운로드가 진행 중입니다. 진행 중인 다운로드를 취소 하시겠습니까?");
					if (_res == System.Windows.Forms.DialogResult.Yes)
					{
						_btnCancel.PerformClick();
					}
				}
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region _btnOpenFolder_Click : 폴더열기 버튼 클릭 이벤트
		/// <summary>
		/// 폴더열기 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnOpenFolder_Click(object sender, EventArgs e)
		{
			OpenDownloadFolder();
		}
		#endregion

		#region _lblDownloadFolder_LinkClicked : 저장경로 링크 클릭 이벤트
		/// <summary>
		/// 저장경로 링크 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _lblDownloadFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OpenDownloadFolder();
		}
		#endregion

		#region _btnChangeFolder_Click : 경로변경 버튼 클릭 이벤트
		/// <summary>
		/// 경로변경 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnChangeFolder_Click(object sender, EventArgs e)
		{
			try
			{
				//folderBrowserDialog1.RootFolder		= Environment.SpecialFolder.MyDocuments;	// 다운로드 경로의 root 경로를 제한한다.
				folderBrowserDialog1.SelectedPath	= _saveDirectory;
				DialogResult _res					= folderBrowserDialog1.ShowDialog();
				if (_res == System.Windows.Forms.DialogResult.OK)
				{
					_saveDirectory			= folderBrowserDialog1.SelectedPath;
					_lblDownloadFolder.Text	= _saveDirectory;
					base.SetCookie("SaveDirectory", _saveDirectory);
				}
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region _btnDownload_Click : 다운로드 버튼 클릭 이벤트
		/// <summary>
		/// 다운로드 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnDownload_Click(object sender, EventArgs e)
		{
			try
			{
				_btnOpenFolder.Enabled		= false;
				_btnChangeFolder.Enabled	= false;
				_btnDownload.Enabled		= false;
				_btnClose.Visible			= false;

				progressBar1.Maximum		= 100 * _filesToDownload.Count;
				progressBar1.Value			= 0;

				_handlers					= new DownloadHandler[_filesToDownload.Count];

				// 잔여시간 확인 시작
				_remainTime.DisplayLabel	= _lblRemainingTime;
				_remainTime.StartCheck();

				// 다운로드 스피드 확인 시작
				_downloadSpeed.DisplayLabel	= _lblTransferSpeed;
				_downloadSpeed.StartCheck();

				// 닫기 버튼 비 활성화
				this.DisableCloseButton();

				// 다운로드 상태 변경
				this.DownloadStatus			= Common.DownloadStatus.Downloading;

				// 모든 리스트에 있는 파일들 다운로드 시작
				for (int i = 0; i < _filesToDownload.Count; i++)
				{
					var _file					= _filesToDownload[i];
					string _localFileName		= string.Format("{0}\\{1}", _saveDirectory, _file.FileName);

					_handlers[i]					= new DownloadHandler();
					_handlers[i].BytesReceived		+= _handler_BytesReceived;
					_handlers[i].DownloadFinished	+= _handler_DownloadFinished;
					_handlers[i].DownloadFile		= _file;
					_handlers[i].LocalFileName		= _localFileName;
					_handlers[i].StartDownload();
				}
			}
			catch (Exception err)
			{
				err.HelpLink	= this.XmlFileContent;
				base.Error(err);
			}
		}
		#endregion

		#region _handler_BytesReceived : 다운로드 바이트 수신 이벤트
		/// <summary>
		/// 다운로드 바이트 수신 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _handler_BytesReceived(object sender, BytesReceivedEventArgs e)
		{
			try
			{
				if (_allReceivedLength == 0)
				{
					_lastReceived	= System.DateTime.Now;
				}

				_allReceivedLength	+= e.JustReceivedBytesLength;
				decimal _rateDec	= ((decimal)_allReceivedLength / (decimal)_totalBytesToReceive) * Convert.ToDecimal(100 * _filesToDownload.Count);
				decimal _rateRound	= Math.Round(_rateDec, MidpointRounding.ToEven);

				progressBar1.Invoke(new Action(() => {
					progressBar1.Value = Convert.ToInt32(_rateRound);
				}));

				
				// 총 받은 파일의 용량 표시
				_lblDataAmount.Text		= string.Format("{0} / {1}"
					, this.GetDataAmount(_allReceivedLength)
					, this.GetDataAmount(_totalBytesToReceive)
					);

				// 잔여시간 계산 체크인
				_remainTime.CheckIn(_allReceivedLength, _totalBytesToReceive);

				// 다운로드 스피드 체크인
				_downloadSpeed.CheckIn(e.JustReceivedBytesLength);

				// 개별 잔여량 표시
				string _receivedRestString	= string.Format("{0:N2}%"
					, (decimal)e.DownloadProgressChangedEventArgs.BytesReceived / (decimal)e.DownloadFile.FileSize * (decimal)100
					);
				this.ShowMessage(e.DownloadFile.FileWebUrl, Color.Black, _receivedRestString);
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region _handler_DownloadFinished : 다운로드 완료 이벤트
		/// <summary>
		/// 다운로드 완료 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void _handler_DownloadFinished(object sender, DownloadFinishedEventArgs e)
		{
			try
			{
				if (e.AsyncCompletedEventArgs.Error == null)
				{
					// 다운로드 완료 메시지 출력
					this.ShowMessage(e.DownloadFile.FileWebUrl, Color.Blue, "다운로드 완료");
				}
				else
				{
					// 오류 발생
					Exception _err	= e.AsyncCompletedEventArgs.Error;
					if (e.AsyncCompletedEventArgs.Error != null)
					{
						_err	= e.AsyncCompletedEventArgs.Error;
					}
					else if (e.AsyncCompletedEventArgs.Error.InnerException != null)
					{
						_err	= e.AsyncCompletedEventArgs.Error.InnerException;
					}

					// 전체 다운로드 받을 파일들의 바이트 합계 길이에서 차감
					_totalBytesToReceive	= _totalBytesToReceive - e.DownloadFile.FileSize;

					// 디버깅을 위해서 오류가 발생하였을 경우에는 로그 기록
					_err.HelpLink			= this.XmlFileContent;
					BANANA.Windows.Logger.Error(_err);

					// 오류 완료 메시지 출력
					this.ShowMessage(e.DownloadFile.FileWebUrl, Color.Red, _err.Message);
				}

				if (this.IsAllDownloadCompleted())
				{
					// 잔여시간 확인 중지
					_remainTime.StopCheck();

					// 다운로드 스피드 확인 중지
					_downloadSpeed.StopCheck();

					// 화면 정리
					this.TidyUpScreen();
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region _btnClose_Click : 닫기 버튼 클릭 이벤트
		/// <summary>
		/// 닫기 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnClose_Click(object sender, EventArgs e)
		{
			try
			{
				this.Close();
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		#region _btnCancel_Click : 취소 버튼 클릭 이벤트
		/// <summary>
		/// 취소 버튼 클릭 이벤트
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (DownloadHandler _handler in _handlers)
				{
					if (!_handler.DownloadFile.IsCompleted)
					{
						_handler.CancelDownload();
					}
				}
			}
			catch (Exception err)
			{
				base.Error(err);
			}
		}
		#endregion

		// Methods
		#region InsertQueue : 큐에 다운로드할 파일들 담기
		/// <summary>
		/// 큐에 다운로드할 파일들 담기
		/// </summary>
		public void InsertQueue()
		{
			try
			{
				foreach (DownloadFile _file in this.FilesToDownload)
				{
					string[] _row				= new string[] {
						_file.FileName
						, this.GetDataAmount(_file.FileSize)
						, ""
						, _file.FileWebUrl
					};

					ListViewItem listViewItem	= new ListViewItem(_row);
					_lvDownloadFiles.Items.Add(listViewItem);
					//_lvDownloadFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

					// 다운로드 받을 파일들의 바이트 길이 합산
					_totalBytesToReceive	+= _file.FileSize;
				}

				// 총 받을 파일의 용량 표시
				_lblDataAmount.Text		= string.Format("0 B / {0}", this.GetDataAmount(_totalBytesToReceive));
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region IsAllDownloadCompleted : 모든 다운로드 종료 여부 확인
		/// <summary>
		/// 모든 다운로드 종료 여부 확인
		/// </summary>
		/// <returns></returns>
		bool IsAllDownloadCompleted()
		{
			try
			{
				foreach (DownloadFile _file in _filesToDownload)
				{
					if (!_file.IsCompleted)
					{
						return false;
					}
				}
			}
			catch
			{
				throw;
			}

			return true;
		}
		#endregion

		#region TidyUpScreen : 화면 정리
		/// <summary>
		/// 화면 정리
		/// </summary>
		void TidyUpScreen()
		{
			try
			{
				_btnOpenFolder.Invoke(new Action(() => {
					_btnOpenFolder.Enabled		= this._prop.EnableOpenFolderButton ? true : false;
				}));
				_btnChangeFolder.Invoke(new Action(() => {
					_btnChangeFolder.Enabled	= this._prop.EnableChangeFolderButton ? true : false;
				}));
				_btnClose.Invoke(new Action(() => {
					_btnClose.Visible			= true;
				}));
				_btnCancel.Invoke(new Action(() => {
					_btnCancel.Visible			= false;
				}));

				this.DownloadStatus			= Common.DownloadStatus.Completed;
				this.FilesToDownload.Clear();
				
				// 닫기 버튼 활성화
				this.EnableCloseButton();
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
		public void SetConfiguration(DownloadManagerProperties Config)
		{
			try
			{
				this._prop						= Config;

				// 버튼들 처리
				this._btnOpenFolder.Enabled		= Config.EnableOpenFolderButton;
				this._btnChangeFolder.Enabled	= Config.EnableChangeFolderButton;
				this._btnCancel.Enabled			= Config.EnableCancelButton;

				// 디렉토리 변경 시, 루트 폴더 처리
				if (Config.RootFolder != DownloadRootFolder.NotSet)
				{
					folderBrowserDialog1.RootFolder	= (Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), Config.RootFolder.ToString());
				}

				// 다운로드할 기본 폴더 처리
				string _defaultFolder			= Config.DefaultFolder;
				_defaultFolder					= base.SetProperPath(_defaultFolder);

				// 기본 폴더가 공백이 아니면, 아래 로직 수행
				if (!string.IsNullOrEmpty(_defaultFolder))
				{
					string _drive					= Path.GetPathRoot(_defaultFolder);
					if (!Directory.Exists(_drive))
					{
						// 지정한 드라이브가 존재하지 않을 경우에는 1) 직전에 다운로드 받은 폴더, 2) 내 문서 폴더의 순서대로 설정
						string _tempSavedFolder		= base.GetCookie("SaveDirectory");
						_tempSavedFolder			= base.SetProperPath(_tempSavedFolder);
						if (!string.IsNullOrEmpty(_tempSavedFolder))
						{
							_saveDirectory			= _tempSavedFolder;
						}
						else
						{
							_saveDirectory			= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create);
						}
						_lblDownloadFolder.Text	= _saveDirectory;

						// 지정한 폴더가 없으면, 생성하자.
						if (!Directory.Exists(_saveDirectory))
						{
							Directory.CreateDirectory(_saveDirectory);
						}
					}
					else
					{
						// 지정한 드라이브가 존재할 경우에는 해당 폴더의 존재유무를 체크하고, 없으면 생성
						if (!Directory.Exists(_defaultFolder))
						{
							Directory.CreateDirectory(_defaultFolder);
						}
						_saveDirectory			= _defaultFolder;
						_lblDownloadFolder.Text	= _defaultFolder;
					}
					base.SetCookie("SaveDirectory", _saveDirectory);
				}
				// 기본 폴더가 공백이면, _saveDirectory의 경로를 지정해 준다.
				else
				{
					// 다운로드 받을 경로를 지정해 준다.
					// 1) 직전에 다운로드 받은 폴더, 2) 내 문서 폴더의 순서대로 설정
					string _tempSavedFolder		= base.GetCookie("SaveDirectory");
					_tempSavedFolder			= base.SetProperPath(_tempSavedFolder);
					if (!string.IsNullOrEmpty(_tempSavedFolder))
					{
						_saveDirectory			= _tempSavedFolder;
					}
					else
					{
						_saveDirectory			= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create);
					}
					_lblDownloadFolder.Text	= _saveDirectory;

					// 지정한 폴더가 없으면, 생성하자.
					if (!Directory.Exists(_saveDirectory))
					{
						Directory.CreateDirectory(_saveDirectory);
					}
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region ShowMessage : 리스트뷰의 아이템에 메시지 출력
		/// <summary>
		/// 리스트뷰의 아이템에 메시지 출력
		/// 다운로드 Url이 키 값이다.
		/// </summary>
		/// <param name="_url"></param>
		/// <param name="_color"></param>
		/// <param name="_message"></param>
		void ShowMessage(string _url, Color _color, string _message)
		{
			try
			{
				_lvDownloadFiles.Invoke((MethodInvoker)delegate()
				{
					foreach (ListViewItem itemRow in this._lvDownloadFiles.Items)
					{
						// 현재 행을 찾음
						if (itemRow.SubItems[3].Text == _url)
						{
							itemRow.ForeColor			= _color;
							itemRow.SubItems[2].Text	= _message;
							break;
						}
					}
				});
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region OpenDownloadFolder : 다운로드 폴더 열기
		/// <summary>
		/// 다운로드 폴더 열기
		/// </summary>
		void OpenDownloadFolder()
		{
			System.Diagnostics.Process.Start("explorer.exe", _saveDirectory);
		}
		#endregion
	}
}
