using System;
using System.Diagnostics;

public class ControlCMD
{
	internal static void SetZiho()
	{
		DateTime dt = DateTime.Now;
		dt.AddHours(1);
		string t = dt.ToString("HH");
		string exeFile = Environment.CurrentDirectory + "/ZihoSettings.exe p";
		string command = $"/k schtasks /create /tn \"ZihoHH\" /tr \"{exeFile}\" /sc hourly /st {t}:00 /f";
		ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", command);
		Process.Start(psi);
	}

	internal static void DeleteZiho()
	{
		string command = "/k schtasks /delete /tn ZihoHH /f";
        ProcessStartInfo psi2 = new ProcessStartInfo("cmd.exe", command);
        Process.Start(psi2);
    }
}
