using System;
using System.Windows.Forms;
using Microsoft.Owin.Hosting;
using Ecommittees.TrayHost;

namespace Universal.TrayHost
{
	public sealed class TrayApplicationContext : ApplicationContext
	{
		private readonly NotifyIcon trayIcon;
		private readonly IDisposable server;

		public TrayApplicationContext()
		{
			this.trayIcon = new NotifyIcon();
			Application.ApplicationExit += Application_ApplicationExit;
			InitializeComponent();

			var options = new StartOptions("http://localhost:5012/")
			{
				ServerFactory = "WinInsider.Owin.Host.SocketListener"
			};

			this.server = WebApp.Start<Startup>(options);
		}

		private void Application_ApplicationExit(object sender, EventArgs e)
		{
			Application.ApplicationExit -= Application_ApplicationExit;
			this.server.Dispose();
			this.trayIcon.Visible = false;
		}

		private void InitializeComponent()
		{
			this.trayIcon.BalloonTipIcon = ToolTipIcon.Info;
			this.trayIcon.BalloonTipText = "Is running";
			this.trayIcon.BalloonTipTitle = "Universal Web API host";
			this.trayIcon.Text = "Universal Web API host";
			this.trayIcon.Icon = Resources.TrayIcon;
			this.trayIcon.DoubleClick += TrayIcon_DoubleClick;

			var contextMenu = new ContextMenuStrip();
			contextMenu.SuspendLayout();
			contextMenu.Items.Add(new ToolStripMenuItem("&Close", null, Close));
			contextMenu.ResumeLayout(false);
			this.trayIcon.ContextMenuStrip = contextMenu;
			this.trayIcon.Visible = true;
		}

		private void TrayIcon_DoubleClick(object sender, EventArgs e)
		{
			this.trayIcon.ShowBalloonTip(10000);
		}

		private void Close(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
