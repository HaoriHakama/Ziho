using System;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Drawing.Text;
using System.DirectoryServices.ActiveDirectory;
using System.Diagnostics;

//App.configを取得・編集するクラス
public class FileNames
{
	private string[] _fileList;
	Configuration config;

    public FileNames()
	{
        this.config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        this._fileList = new string[24];
        for (int i = 0; i < 24; i++)
        {
            string key = "key" + i.ToString();
            string? file = ConfigurationManager.AppSettings[key];
            if (file is not null) _fileList[i] = file;
            else _fileList[i] = "noFile";
        }
    }

    public string[]? FileList { get; }

	internal string GetFileList(int hour)
	{
		return this._fileList[hour];
	}

	internal void SetFileList(int hour, string filePath)
	{
		_fileList[hour] = filePath;
	}

	//フォルダを参照して_fileListを作成する関数
	internal void BrowseFileList(string path)
	{
		string[] fileList = new string[25];
		string[] files = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);
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
		for (int i=12; i<=24; i++)
		{
			if (fileList[i - 12] is not null & fileList[i] is null) fileList[i] = fileList[i - 12];
		}

		//0時のファイルが存在せず24時のファイルが存在する場合
		if (fileList[0] is null && fileList[24] is not null) fileList[0] = fileList[24];

		for (int i=0; i<24; i++)
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

	//AppConfigに音声設定を保存する
	internal void SaveConfigFileList()
	{
		for (int i=0; i<24; i++)
		{
			if (_fileList[i] == "noFile" || _fileList[i] is null)
			{
				SetConfigFileList(i, "noFile");
			}
			else
			{
                SetConfigFileList(i, _fileList[i]);
            }
		}
        config.Save();
    }

    private void SetConfigFileList(int i, string filePath)
	{
		string key = "key" + i.ToString();
		config.AppSettings.Settings[key].Value = filePath;
	}

	public void DeleteConfigFileList()
	{
		for (int i=0; i<24; i++)
		{
            string key = "key" + i.ToString();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = "noFile";
            _fileList[i] = "noFile";
        }
	}
}
