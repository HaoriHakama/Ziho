using System;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Drawing.Text;
using System.DirectoryServices.ActiveDirectory;
using System.Diagnostics;
using System.Text.Json;

//setting.json
public class VoiceSettings
{
    public string TaskName { get; set; }
    public int Volume { get; set; }
    public string? FolderPath { get; set; }
    public string[] VoicePath { get; set; }
    public bool[] Sleep { get; set; }

    public VoiceSettings()
    {
        this.TaskName = "ZihoHH";
        this.VoicePath = new string[24];
        this.Sleep = new bool[24];
    }
}
public class ChangeSettings
{
    string _settingFile = "setting.json";
    string[] _fileList = new string[24];
    VoiceSettings _voiceSettings;

    public ChangeSettings()
    {
        string jsonstr = File.ReadAllText(this._settingFile);
        this._voiceSettings = JsonSerializer.Deserialize<VoiceSettings>(jsonstr)!;
        if (this._voiceSettings.VoicePath is not null)
        {
            for (int i = 0; i < 24; i++)
            {
                if (this._voiceSettings.VoicePath[i] is not null) _fileList[i] = this._voiceSettings.VoicePath[i];
                else _fileList[i] = "noFile";
            }
        }
    }

    internal void SetVolume(int volume)
    {
        _voiceSettings.Volume = volume;
    }

    internal void SetSleepMode(int start, int end)
    {
        if (start == end)
        {
            for (int i = 0; i < 24; i++)
            {
                _voiceSettings.Sleep[i] = false;
            }
            _voiceSettings.Sleep[start] = true;
        }
        else if (start < end)
        {
            for (int i = 0; i < 24; i++)
            {
                _voiceSettings.Sleep[i] = false;
            }
            for (int i=start; i<=end; i++)
            {
                _voiceSettings.Sleep[i] = true;
            }
        }
        else
        {
            for (int i=0; i<24; i++)
            {
                _voiceSettings.Sleep[i] = true;
            }
            for (int i=end; i<=start; i++)
            {
                _voiceSettings.Sleep[i] = false;
            }
        }
    }

    internal void DleteSleepMode()
    {
        for (int i = 0; i < 24; i++)
        {
            _voiceSettings.Sleep[i] = false;
        }
    }

    internal void SetTaskName(string taskName)
    {
        _voiceSettings.TaskName = taskName;
    }

    internal string GetFileList(int hour)
    {
        return _fileList[hour];
    }

    internal void SetFileList(int hour, string filePath)
    {
        _fileList[hour] = filePath;
    }

    //フォルダを参照して_fileListを作成する関数
    internal void BrowseFileList(string path)
    {
        string[] fileList = new string[25];
        var filesMp3 = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories).ToList();
        var filesWav = Directory.GetFiles(path, "*.wav", SearchOption.AllDirectories).ToList();
        filesMp3.AddRange(filesWav);

        string[] files = filesMp3.ToArray();

        if (files.Length == 0) return;

        //各ファイルの数字の部分を抽出
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            Match match = Regex.Match(fileName, "[0-2][0-9]|[0-9]"); //先に二桁の判定が行われる(検証済み)
            int h = int.Parse(match.Value);
            fileList[h] = file;
        }

        //午後(12～24時)がない場合
        for (int i = 12; i <= 24; i++)
        {
            if (fileList[i - 12] is not null & fileList[i] is null) fileList[i] = fileList[i - 12];
        }

        //0時のファイルが存在せず24時のファイルが存在する場合
        if (fileList[0] is null && fileList[24] is not null) fileList[0] = fileList[24];

        for (int i = 0; i < 24; i++)
        {
            if (fileList[i] is not null)
            {
                _fileList[i] = fileList[i];
            }
            else
            {
                _fileList[i] = "noFile(ブラウズファイルリスト)";
            }
        }
    }

    //setting.jsonに音声設定を保存する
    internal void SaveConfigFileList()
    {
        for (int i = 0; i < 24; i++)
        {
            if (_fileList[i] == "noFile" || _fileList[i] is null)
            {
                _voiceSettings.VoicePath[i] = "noFile";
            }
            else
            {
                _voiceSettings.VoicePath[i] = _fileList[i];
            }
        }
        string jsonStr = JsonSerializer.Serialize(_voiceSettings);
        File.WriteAllText(_settingFile, jsonStr);
    }

    public void DeleteConfigFileList()
    {
        for (int i = 0; i < 24; i++)
        {
            _fileList[i] = "noFIle";
            _voiceSettings.VoicePath[i] = "noFile";
        }
        string jsonStr = JsonSerializer.Serialize(_voiceSettings);
        File.WriteAllText(_settingFile, jsonStr);
    }

    internal void SetZiho()
    {
        DateTime dt = DateTime.Now;
        dt.AddHours(1);
        string t = dt.ToString("HH");
        string exeFile = Environment.CurrentDirectory + "/PlayVoice.exe";
        string command = $"/k schtasks /create /tn \"{_voiceSettings.TaskName}\" /tr \"{exeFile}\" /sc hourly /st {t}:00 /f";
        ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", command);
        Process.Start(psi);
    }

    internal void DeleteZiho()
    {
        string command = $"/k schtasks /delete /tn \"{_voiceSettings.TaskName}\" /f";
        ProcessStartInfo psi2 = new ProcessStartInfo("cmd.exe", command);
        Process.Start(psi2);
    }
}
