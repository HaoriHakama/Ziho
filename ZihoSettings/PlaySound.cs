using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WMPLib;

public class PlaySound
{
    WindowsMediaPlayer mp;

    public PlaySound()
    {
        mp = new WindowsMediaPlayer();
        string soundFile = GetSoundFile();
        mp.URL = soundFile;
        mp.MediaChange += Mp_MediaChange;
    }

    private string GetSoundFile()
    {
        int h = DateTime.Now.Hour;
        string key = "key" + h.ToString();
        string? file = ConfigurationManager.AppSettings[key];
        if (file is not null) return file;
        else return "noFile";
    }

    public void Play()
    {
        mp.controls.play();
    }

    private static void Mp_MediaChange(object Item)
    {
        //throw new NotImplementedException();
    }
}

