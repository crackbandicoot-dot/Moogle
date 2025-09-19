using System.Diagnostics;
using System.Drawing.Design;
using System.IO;

namespace MoogleController;

public partial class MoogleController : Form
{
    private Process? serverProcess;
    private Button btnOn;
    private Button btnOff;

    public MoogleController()
    {
        InitializeComponent();

        // Set up the form
        this.Text = "Moogle Server Controller";
        this.Size = new Size(300, 200);
      

        // Create On button
        btnOn = new Button();
        btnOn.Text = "Start";
        btnOn.Location = new Point(50, 50);
        btnOn.Size = new Size(80, 40);
        btnOn.Click += BtnOn_Click;
        this.Controls.Add(btnOn);

        // Create Off button
        btnOff = new Button();
        btnOff.Text = "Finish";
        btnOff.Location = new Point(150, 50);
        btnOff.Size = new Size(80, 40);
        btnOff.Click += BtnOff_Click;
        this.Controls.Add(btnOff);
    }

    private void BtnOn_Click(object sender, EventArgs e)
    {
        if (serverProcess == null || serverProcess.HasExited)
        {
            try
            {
                serverProcess = new Process();
                serverProcess.StartInfo.FileName = "dotnet";
                serverProcess.StartInfo.Arguments = "watch run --project MoogleUI";
                serverProcess.StartInfo.WorkingDirectory = @"C:\Users\PC\Desktop\QuickAcces\Moogle";
                serverProcess.StartInfo.UseShellExecute = false;
                serverProcess.StartInfo.CreateNoWindow = true;
                serverProcess.Start();
                MessageBox.Show("Server started.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start server: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Server is already running.");
        }
    }

    private void BtnOff_Click(object sender, EventArgs e)
    {
        if (serverProcess != null && !serverProcess.HasExited)
        {
            try
            {
                serverProcess.Kill();
                serverProcess.WaitForExit();
                serverProcess = null;
                MessageBox.Show("Server stopped.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to stop server: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Server is not running.");
        }
    }
}
