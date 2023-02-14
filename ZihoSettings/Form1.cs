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
            fbd.ShowNewFolderButton = false; //���[�U�[�͐V�����t�H���_�����Ȃ�
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
                refFolderPath = fbd.SelectedPath;
            }
        }

        //�ʕύX�{�^���������ꂽ���̏���
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
        //�{�^��2�`25�͌ʕύX�A�e�L�X�g�{�b�N�X3�`26���Ή�
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

        //�Q�ƃt�H���_�ɑ΂��āA�e�����̉����t�@�C���������T��
        private void AutoBrowse()
        {
            if (refFolderPath is not null)
            {
                this.fileNames.BrowseFileList(refFolderPath);
            }
        }

        //���ׂẴe�L�X�g�{�b�N�X�Ɉꊇ��fileNames��\��(�R���X�g���N�^or�����T��)
        private void ShowALLFileNames()
        {
            for (int h = 0; h < 24; h++)
            {
                soundFileTextBox[h].Text = this.fileNames.GetFileList(h);
            }
        }

        //�����T��
        private void button26_Click(object sender, EventArgs e)
        {
            AutoBrowse();
            ShowALLFileNames();
        }

        //�����ݒ��ۑ�
        private void button27_Click(object sender, EventArgs e)
        {
            fileNames.SaveConfigFileList();
        }

        //�����ݒ���폜
        private void button28_Click(object sender, EventArgs e)
        {
            fileNames.DeleteConfigFileList();
            ShowALLFileNames();
        }

        //�^�X�N�X�P�W���[���Ɏ����ݒ�
        private void button29_Click(object sender, EventArgs e)
        {
            ChangeSettings.SetZiho();
        }

        //�^�X�N�X�P�W���[�����玞����폜
        private void button30_Click(object sender, EventArgs e)
        {
            ChangeSettings.DeleteZiho();
        }
    }
}