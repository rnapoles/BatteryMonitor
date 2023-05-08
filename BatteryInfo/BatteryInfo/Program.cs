/*
 * Created by SharpDevelop.
 * User: rnapoles
 * Date: 19/03/2019
 * Time: 11:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Management;
using System.Windows.Forms;


namespace BatteryInfo
{
	class Program
	{
		public static void Main(string[] args)
		{

	        String batterystatus;
	
	        PowerStatus pwr = SystemInformation.PowerStatus;
	        batterystatus = SystemInformation.PowerStatus.BatteryChargeStatus.ToString();
	
	        MessageBox.Show("battery charge status : " + batterystatus);
	
	        String batterylife;
	        batterylife = SystemInformation.PowerStatus.BatteryLifePercent.ToString();
	
	        MessageBox.Show(batterylife);
        
		}
	}
}