using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BANANA.Agent.Controllers.PdfViewer
{
	class StatusBusy : IDisposable
	{
		private string _oldStatus;
		//        private DevExpress.XtraBars.BarItemVisibility _oldWbStatus;
		private Cursor _oldCursor;

		public StatusBusy(string statusText)
		{
			//_oldStatus	= Views.DocumentViewer.Instance.StatusLabel.Text;
			//_oldCursor	= Views.DocumentViewer.Instance.Cursor;

			//Views.DocumentViewer.Instance.StatusLabel.Text	= statusText;
			//Views.DocumentViewer.Instance.Cursor			= Cursors.WaitCursor;

			Application.DoEvents();
		}

		#region IDisposable
		// IDisposable
		private bool _disposedValue = false; // Evitar llamadas recursivas

		protected void Dispose(bool disposing)
		{
			if (!_disposedValue)
				if (disposing)
				{
					//Views.DocumentViewer.Instance.StatusLabel.Text	= _oldStatus;
					//Views.DocumentViewer.Instance.Cursor			= _oldCursor;
				}
			_disposedValue = true;
		}

		public void Dispose()
		{
			//No cambiar este codigo, limpiar codigo en la funcion protected void Dispose(bool disposin)
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
