using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AdaptiveInput
{
    public partial class MainForm : Form
    {
        public string lessonFolder;
        Timer scanForLessonTimer = new Timer();
        int lastHash = 0;

        public MainForm()
        {
            InitializeComponent();

            lessonFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\AdaptiveInput\\Lessons\\";
            if (Directory.Exists(lessonFolder) == false) Directory.CreateDirectory(lessonFolder);

            ScanForLessonTimer_Tick(null, null);

            scanForLessonTimer.Interval = 5000;
            scanForLessonTimer.Tick += ScanForLessonTimer_Tick;
            scanForLessonTimer.Start();
        }

        private void ScanForLessonTimer_Tick(object sender, EventArgs e)
        {
            var files = Directory.GetDirectories(lessonFolder);

            string fileString = String.Join("", files);
            var currentHash = fileString.GetHashCode();

            if (lastHash != currentHash)
            {
                goBtn.Enabled = false;
                if (lessonLbx.SelectedItem != null) lessonLbx.SelectedItem.ToString();
                lessonLbx.Items.Clear();

                foreach (var file in files)
                {
                    var prefix = Path.GetDirectoryName(file);
                    lessonLbx.Items.Add(file.Substring(prefix.Length + 1));
                }
                lastHash = currentHash;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void lessonBtn_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", lessonFolder);
        }

        private void lessonLbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            goBtn.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sf = new SettingsForm();
            var result = sf.ShowDialog();
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            begin();
        }

        private void beginLesson(string path)
        {
            List<string> imageFiles = new List<string>();
            imageFiles.AddRange(Directory.GetFiles(path, "*.jpg"));
            imageFiles.AddRange(Directory.GetFiles(path, "*.gif"));
            imageFiles.AddRange(Directory.GetFiles(path, "*.jpeg"));
            imageFiles.AddRange(Directory.GetFiles(path, "*.png"));
            imageFiles.AddRange(Directory.GetFiles(path, "*.tif"));
            imageFiles.AddRange(Directory.GetFiles(path, "*.tiff"));

            List<Image> images = new List<Image>();

            foreach (var f in imageFiles)
            {
                Bitmap bm = (Bitmap)Image.FromFile(f, true);
                Bitmap tmp = new Bitmap(bm.Width, bm.Height);
                Graphics grPhoto = Graphics.FromImage(tmp);
                grPhoto.DrawImage(bm, new Rectangle(0, 0, tmp.Width, tmp.Height), 0, 0, tmp.Width, tmp.Height, GraphicsUnit.Pixel);
                images.Add(bm);
            }


            if (images.Count > 0)
            {
                var inw = new InputWindow(images);
                inw.Show();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var abx = new About();
            abx.ShowDialog();
        }
        
        private void begin()
        {
            if (lessonLbx.SelectedItem != null)
            {
                var lessonPath = Path.Combine(lessonFolder, lessonLbx.SelectedItem.ToString());
                beginLesson(lessonPath);
            }
        }

        private void lessonLbx_DoubleClick(object sender, EventArgs e)
        {
            begin();
        }

        private void lessonLbx_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Space:
                    begin();
                    break;
            }
        }
    }
}
