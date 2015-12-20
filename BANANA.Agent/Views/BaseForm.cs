using System;
using System.Text;

namespace BANANA.Agent.Views
{
	/// <summary>
	/// 제  목: 바나나 에이전트 기본 부모 폼
	/// 작성자: 손정민(jmson@infinihance.com, 010-2865-4368)
	/// 작성일: 2015-12-10 17:09
	/// 설  명: 
	/// </summary>
	public class BaseForm : BANANA.Windows.BaseForm
	{
		#region ConvertNormalToBase64 : 일반 문자열을 Triple-DES 암호화 후, Base64 문자열로 변환
		/// <summary>
		/// 일반 문자열을 Triple-DES 암호화 후, Base64 문자열로 변환
		/// </summary>
		/// <param name="Normal">일반 문자열</param>
		/// <returns>Base64 문자열</returns>
		public string ConvertNormalToBase64(string Normal)
		{
			try
			{
				string _enc		= BANANA.Common.Encryption.DES.GetEncryptTripleDES(Normal);
				byte[] _temp	= Encoding.UTF8.GetBytes(_enc);
				return Convert.ToBase64String(_temp);
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region ConvertBase64ToNormal : Base64 문자열을 Triple-DES 복호화 후, 일반 문자열로 변환
		/// <summary>
		/// Base64 문자열을 Triple-DES 복호화 후, 일반 문자열로 변환
		/// </summary>
		/// <param name="Base64">Base64 문자열</param>
		/// <returns>일반 문자열</returns>
		public string ConvertBase64ToNormal(string Base64)
		{
			try
			{
				byte[] _temp	= Convert.FromBase64String(Base64);
				string _enc		= Encoding.UTF8.GetString(_temp);
				return BANANA.Common.Encryption.DES.GetDecryptTripleDES(_enc);
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region SetProperPath : 경로정보 조정 및 물리적 경로 반환
		/// <summary>
		/// 경로정보 조정 및 물리적 경로 반환
		/// </summary>
		/// <param name="Path">서버의 논리/물리적 경로</param>
		/// <returns></returns>
		public string SetProperPath(string Path)
		{
			string _retValue	= Path;

			try
			{
				// 혼재 되어 있는 경우는 오류
				if ((_retValue.IndexOf("/") > -1) && (_retValue.IndexOf("\\") > -1))
				{
					_retValue	= "";
					throw new ArgumentException("업로드할 서버의 경로에 논리 경로와 물리 경로가 혼재될 수는 없습니다.");
				}
				// 논리 경로
				else if (_retValue.IndexOf("/") > -1)
				{
					// 마지막 경로에 /가 있으면, 삭제한다.
					if (_retValue.Substring(_retValue.Length - 1) == "/")
					{
						_retValue		= _retValue.Substring(0, _retValue.Length - 1);
					}
				}
				// 물리 경로
				else if (_retValue.IndexOf("\\") > -1)
				{
					// 마지막 경로에 \가 있으면, 삭제한다.
					if (_retValue.Substring(_retValue.Length - 1) == "\\")
					{
						_retValue		= _retValue.Substring(0, _retValue.Length - 1);
					}
				}
			}
			catch
			{
				throw;
			}

			return _retValue;
		}
		#endregion
	}
}
