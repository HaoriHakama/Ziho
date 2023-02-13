using System;
using System.Threading;
using System.Configuration;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using WMPLib;
using System.Text.RegularExpressions;
using System.IO.Enumeration;
using System.Diagnostics;
using System.Text.Json;

public class VoiceSettings
{
    public int Volume { get; set; }
    public string[]? VoicePath { get; set; }

    public bool[]? Sleep { get; set; }
}

public class PlayVoiceWMP
{
    WindowsMediaPlayer _mp;
    double _playTime;
    int _volume;
    string[]? _voicePath;
    string _soundFile;
    bool[]? _sleep;

    public PlayVoiceWMP()
    {
        int hour = DateTime.Now.Hour;
        LoadSettings();
        if (_sleep is not null && _sleep[hour] == true) Environment.Exit(0);

        if (_voicePath is null || _voicePath[hour] is null) _soundFile = "noFile";
        else _soundFile = _voicePath[hour];

        _mp = new WindowsMediaPlayer();
        MpSet(_soundFile);
    }

    //setting.jsonを参照し設定を読み込む
    private void LoadSettings()
    {
        string settingFile = "setting.json";
        string jsonstr = File.ReadAllText(settingFile);
        VoiceSettings voiceSettings = JsonSerializer.Deserialize<VoiceSettings>(jsonstr)!;

        this._volume = voiceSettings.Volume;

        if (voiceSettings.VoicePath is not null)
        {
            this._voicePath = voiceSettings.VoicePath;
        }

        if (voiceSettings.Sleep is not null)
        {
            this._sleep = voiceSettings.Sleep;
        }
    }

    //Windows Media Playerに読み込んだ設定値をセット
    private void MpSet(string soundFile)
    {
        _mp.settings.volume= _volume;
        _mp.URL = soundFile;

        //無効なURLが設定された場合プログラムを終了する
        _mp.MediaError += Mp_MediaError;

        while (_mp.openState != WMPOpenState.wmposMediaOpen)
        {
            Thread.Sleep(100);
        }
        _playTime = _mp.currentMedia.duration;
    }

    //無効なURLが設定された場合の処理
    private void Mp_MediaError(object pMediaObject)
    {
        switch (pMediaObject)
        {
            case "noFile":
                Debug.WriteLine("setting.jsonにファイルの設定がありません");
                break;
            default:
                Debug.WriteLine("設定されたvoiceファイルが存在しません");
                break;
        }
        Environment.Exit(0);
    }

    public void Play()
    {
        _mp.controls.play();
        Thread.Sleep(1000 * ( (int)_playTime + 1 ) );
    }
}
