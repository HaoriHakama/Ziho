using System;

public class InvisibleForm : Form
{
	public InvisibleForm()
	{
        WindowState= FormWindowState.Minimized;
        ShowInTaskbar = false;
        this.Load += Form_Load;
    }

    private void Form_Load(object sender, EventArgs e)
    {
        var mmp = new PlaySound();
        mmp.Play();
    }

    

    
}
