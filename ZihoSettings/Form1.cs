namespace ZihoSettings
{
    public partial class Form1 : Form
    {
        static string? refFolderPath;
        ChangeSettings fileNames;
        TextBox[] soundFileTextBox;
        public Form1()
        {
            InitializeComponent();
            this.fileNames = new ChangeSettings();
            this.soundFileTextBox = new TextBox[24] { textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20, textBox21, textBox22, textBox23, textBox24, textBox25, textBox26 }; ;
            ShowALLFileNames();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = false; //ユーザーは新しいフォルダを作れない
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
                refFolderPath = fbd.SelectedPath;
            }
        }

        //個別変更ボタンが押された時の処理
        private void IndivisualChange(int hour)
        {
            string initialDirectory;
            if (refFolderPath is null) initialDirectory = @"C:";
            else initialDirectory = refFolderPath;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = initialDirectory;
            ofd.Filter = "mp3 wav|*.mp3;*.wav";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                soundFileTextBox[hour].Text = ofd.FileName;
                this.fileNames.SetFileList(hour, ofd.FileName);
            }
        }
        //ボタン2～25は個別変更、テキストボックス3～26が対応
        private void button2_Click(object sender, EventArgs e)
        {
            IndivisualChange(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IndivisualChange(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IndivisualChange(2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IndivisualChange(3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IndivisualChange(4);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IndivisualChange(5);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            IndivisualChange(6);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            IndivisualChange(7);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            IndivisualChange(8);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            IndivisualChange(9);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            IndivisualChange(10);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            IndivisualChange(11);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            IndivisualChange(12);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            IndivisualChange(13);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            IndivisualChange(14);
        }   

        private void button17_Click(object sender, EventArgs e)
        {
            IndivisualChange(15);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            IndivisualChange(16);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            IndivisualChange(17);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            IndivisualChange(18);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            IndivisualChange(19);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            IndivisualChange(20);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            IndivisualChange(21);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            IndivisualChange(22);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            IndivisualChange(23);
        }

        //参照フォルダに対して、各時刻の音声ファイルを自動探索
        private void AutoBrowse()
        {
            if (refFolderPath is not null)
            {
                this.fileNames.BrowseFileList(refFolderPath);
            }
        }

        //すべてのテキストボックスに一括でfileNamesを表示(コンストラクタor自動探索)
        private void ShowALLFileNames()
        {
            for (int h = 0; h < 24; h++)
            {
                soundFileTextBox[h].Text = this.fileNames.GetFileList(h);
            }
        }

        //自動探索
        private void button26_Click(object sender, EventArgs e)
        {
            AutoBrowse();
            ShowALLFileNames();
        }

        //音声設定を保存
        private void button27_Click(object sender, EventArgs e)
        {
            fileNames.SaveConfigFileList();
        }

        //音声設定を削除
        private void button28_Click(object sender, EventArgs e)
        {
            fileNames.DeleteConfigFileList();
            ShowALLFileNames();
        }

        //タスクスケジューラに時報を設定
        private void button29_Click(object sender, EventArgs e)
        {
            ChangeSettings.SetZiho();
        }

        //タスクスケジューラから時報を削除
        private void button30_Click(object sender, EventArgs e)
        {
            ChangeSettings.DeleteZiho();
        }
    }
}