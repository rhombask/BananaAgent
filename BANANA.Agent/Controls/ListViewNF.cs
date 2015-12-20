﻿using System;
using System.Windows.Forms;

namespace BANANA.Agent.Controls
{
	/// <summary>
	/// 아이템 변경 시, 깜빡거리지 않는 리스트뷰
	/// http://stackoverflow.com/questions/442817/c-sharp-flickering-listview-on-update
	/// </summary>
	class ListViewNF : System.Windows.Forms.ListView
	{
		public ListViewNF()
		{
			// Activate double buffering
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

			// Enable the OnNotifyMessage event so we get a chance to filter out 
			// Windows messages before they get to the form's WndProc
			this.SetStyle(ControlStyles.EnableNotifyMessage, true);
		}

		protected override void OnNotifyMessage(Message m)
		{
			// Filter out the WM_ERASEBKGND message
			if(m.Msg != 0x14)
			{
				base.OnNotifyMessage(m);
			}
		}
	}
}
