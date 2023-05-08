/*
 * Created by SharpDevelop.
 * User: rnapoles
 * Date: 19/03/2019
 * Time: 12:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace BatteryInfoNotify
{
	public sealed class NotificationIcon
	{
		
		private NotifyIcon notifyIcon;
		private ContextMenu notificationMenu;
		System.Timers.Timer aTimer;
		String batterylife = "0%";

		#region Initialize icon and menu
		public NotificationIcon()
		{
			notifyIcon = new NotifyIcon();
			notificationMenu = new ContextMenu(InitializeMenu());
			
			notifyIcon.DoubleClick += IconDoubleClick;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
			notifyIcon.Icon = (Icon)resources.GetObject("$this.Icon");
			notifyIcon.ContextMenu = notificationMenu;
			
			
			notifyIcon.BalloonTipTitle = "Carga";
	        
			aTimer = new System.Timers.Timer();	   
			aTimer.Interval = 2000;		
			aTimer.Elapsed += OnTimedEvent;
			aTimer.Enabled = true;			
        
		}
		

		void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
		{
	        
			//String batterystatus;
	
	        PowerStatus pwr = SystemInformation.PowerStatus;
	        String batterystatus = SystemInformation.PowerStatus.BatteryChargeStatus.ToString();
	
	        //MessageBox.Show("battery charge status : " + batterystatus);
	
	        float percent = SystemInformation.PowerStatus.BatteryLifePercent * 100;
	        batterylife = percent.ToString() + "%" ;
	        
	        int statusPos = batterystatus.IndexOf("Charging");
	        
	        if(percent >= 95 && (statusPos != -1)){
	        	notifyIcon.BalloonTipText = "Desconectar cable.";
	        	notifyIcon.ShowBalloonTip(1000);
	        }
	        
		}

		
		
		private MenuItem[] InitializeMenu()
		{
			MenuItem[] menu = new MenuItem[] {
				new MenuItem("About", menuAboutClick),
				new MenuItem("Exit", menuExitClick)
			};
			return menu;
		}

		static NotificationIcon notificationIcon;

		#endregion
		
		#region Main - Program entry point
		/// <summary>Program entry point.</summary>
		/// <param name="args">Command Line Arguments</param>
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			bool isFirstInstance;
			// Please use a unique name for the mutex to prevent conflicts with other programs
			using (Mutex mtx = new Mutex(true, "BatteryInfoNotify", out isFirstInstance)) {
				if (isFirstInstance) {
					
									
					notificationIcon = new NotificationIcon();
					notificationIcon.notifyIcon.Visible = true;
					Application.Run();
					notificationIcon.notifyIcon.Dispose();
				} else {
					// The application is already running
					// TODO: Display message box or change focus to existing application instance
					
					
				}
			} // releases the Mutex
		}
		#endregion
		
		#region Event Handlers
		private void menuAboutClick(object sender, EventArgs e)
		{
			MessageBox.Show("About This Application");
		}
		
		private void menuExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		private void IconDoubleClick(object sender, EventArgs e)
		{
			//MessageBox.Show("The icon was double clicked");
			notificationIcon.notifyIcon.BalloonTipText = notificationIcon.batterylife;
			notificationIcon.notifyIcon.ShowBalloonTip(1000);
		}
		#endregion
	}
}
