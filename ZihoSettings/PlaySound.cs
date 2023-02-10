using System;
using System.Configuration;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using WMPLib;
using System.Text.RegularExpressions;
using System.IO.Enumeration;
using System.Diagnostics;

public class PlaySound
{
	public static void Play()
	{
		int h = DateTime.Now.Hour;
		string soundFile = GetSoundFile(h);
		if (soundFile != "noFile")
		{
            WmpPlay(soundFile);
        }
	}

	private static string GetSoundFile(int hour)
	{
        string key = "key" + hour.ToString();
        string? file = ConfigurationManager.AppSettings[key];
		if (file is not null) return file;
		else return "noFile";
    }

	private static void WmpPlay(string soundFile)
	{
		Debug.WriteLine(soundFile);

		WindowsMediaPlayer mp = new WindowsMediaPlayer();
		mp.settings.volume = 20;

        mp.URL = soundFile;

		//メディアの読込を待つ
		Thread.Sleep(1000);

		mp.controls.play();
        double t = mp.currentMedia.duration;

        Thread.Sleep((int) t *1000 + 1000);
	}
}
