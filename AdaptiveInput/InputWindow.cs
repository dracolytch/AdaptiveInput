using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdaptiveInput
{
    public partial class InputWindow : Form
    {
        private enum SelectionMode { Row, Column };
        private List<Image> Images;
        private List<Panel> Panels;
        private List<int> Options;
        private int minActiveRow;
        private int maxActiveRow;
        private int minActiveColumn;
        private int maxActiveColumn;
        private SelectionMode mode = SelectionMode.Column;
        private int dimensions;
        private bool isDone = false;


        public InputWindow(List<Image> images)
        {
            InitializeComponent();
            Images = images;
            Panels = new List<Panel>();
            Options = new List<int>();
            Focus();
        }



        private void InputWindow_Load(object sender, EventArgs e)
        {
            buttonPanel.Location = new Point((Size.Width / 2) - (buttonPanel.Height / 2), buttonPanel.Location.Y);
            dimensions = (int)Math.Ceiling(Math.Sqrt(Images.Count));
            var h = buttonPanel.Height / dimensions;
            var imageIdx = 0;
            minActiveColumn = 0;
            minActiveRow = 0;
            maxActiveColumn = dimensions;
            maxActiveRow = dimensions;
            var midCol = maxActiveColumn / 2;

            for (var y = 0; y < dimensions; y++)
            {
                for (var x = 0; x < dimensions; x++)
                {
                    if (imageIdx < Images.Count)
                    {
                        Options.Add(imageIdx);
                        var pnl = new Panel();
                        pnl.Parent = buttonPanel;
                        pnl.Location = new Point(x * h, y * h);
                        pnl.Width = h;
                        pnl.Height = h;
                        pnl.Tag = imageIdx;
                        if (x < midCol) pnl.BackColor = Color.Green;
                        else pnl.BackColor = Color.Yellow;
                        Panels.Add(pnl);

                        var pbx = new PictureBox();
                        pbx.Parent = pnl;
                        pbx.Width = h - 20;
                        pbx.Height = h - 20;
                        pbx.Location = new Point(10, 10);
                        pbx.SizeMode = PictureBoxSizeMode.Zoom;
                        pbx.Image = Images[imageIdx];
                        pbx.Refresh();
                        pbx.Show();
                        pbx.Tag = imageIdx;
                        imageIdx++;
                    }
                }
            }
        }

        private int getRow(int id)
        {
            return id / dimensions;
        }

        private int getCol(int id)
        {
            return id % dimensions;
        }

        private void Select(int buttonNumber)
        {
            var midCol = minActiveColumn + ((maxActiveColumn - minActiveColumn) / 2);
            var midRow = minActiveRow + ((maxActiveRow - minActiveRow) / 2);

            switch (buttonNumber)
            {
                case 1:
                    if (mode == SelectionMode.Column) maxActiveColumn = midCol;
                    else maxActiveRow = midRow;
                    break;

                case 2:
                    if (mode == SelectionMode.Column) minActiveColumn = midCol;
                    else minActiveRow = midRow;
                    break;
            }

            // Switch Selection mode, and color
            if (mode == SelectionMode.Row) mode = SelectionMode.Column;
            else mode = SelectionMode.Row;

            foreach (var pnl in Panels)
            {
                var isActive = false;
                var row = getRow((int)pnl.Tag);
                var col = getCol((int)pnl.Tag);

                if (row < maxActiveRow && row >= minActiveRow)
                {
                    if (col < maxActiveColumn && col >= minActiveColumn)
                    {
                        isActive = true;
                        // Selected, re-color
                        if (mode == SelectionMode.Column)
                        {
                            if (col < midCol) pnl.BackColor = Color.Green;
                            else pnl.BackColor = Color.Yellow;
                        }
                        else
                        {
                            if (row < midRow) pnl.BackColor = Color.Green;
                            else pnl.BackColor = Color.Yellow;
                        }
                    }
                }

                if (isActive == false)
                {
                    pnl.BackColor = Color.Black;
                    foreach (var child in pnl.Controls.OfType<PictureBox>())
                    {
                        var gs = Graphics.FromImage(child.Image);
                        gs.FillRectangle(new SolidBrush(Color.FromArgb(192, Color.Black)), new Rectangle(0, 0, child.Image.Width, child.Image.Height));
                    }
                }
            }

            if ((maxActiveColumn - minActiveColumn == 1) && (maxActiveRow - minActiveRow == 1))
            {
                isDone = true;
                var id = (dimensions * minActiveRow) + minActiveColumn;
                Panels[id].BackColor = Color.White;
            }
        }

        private void InputWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (!isDone) Select(1);
                    break;

                case Keys.Enter:
                    if (!isDone) Select(2);
                    break;

                case Keys.Tab:
                    if (e.Shift) {
                    }
                    else {
                    }
                    break;

                case Keys.Back:
                    break;

                case Keys.Escape:
                    Close();
                    break;
            }
        }
    }
}
