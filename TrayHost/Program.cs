using System;
using System.Windows.Forms;

namespace Universal.TrayHost
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.Run(new TrayApplicationContext());
		}
	}
}
