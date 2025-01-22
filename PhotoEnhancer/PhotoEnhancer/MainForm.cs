using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    public partial class MainForm : Form
    {
        Panel parametersPanel;
        List<NumericUpDown> parameterControls;

        Photo originalPhoto;
        Photo resultPhoto;

        public MainForm()
        {
            InitializeComponent();

            var bmp = (Bitmap)Image.FromFile("cat.jpg");
            originalPhoto = Convertors.BitmapToPhoto(bmp);
            originalPictureBox.Image = bmp;
        }

        private void filtersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            applyButton.Visible = true;

            if(parametersPanel != null)
                this.Controls.Remove(parametersPanel);

            parametersPanel = new Panel();

            parametersPanel.Left = filtersComboBox.Left;
            parametersPanel.Top = filtersComboBox.Bottom + 13;
            parametersPanel.Width = filtersComboBox.Width;
            parametersPanel.Height = applyButton.Top - parametersPanel.Top - 13;
            //parametersPanel.BackColor = Color.Gray;

            this.Controls.Add(parametersPanel);

            var filter = filtersComboBox.SelectedItem as IFilter;

            if (filter == null) return;

            parameterControls = new List<NumericUpDown>();
            var parametersInfo = filter.GetParametersInfo();

            for(var i = 0; i < parametersInfo.Length; i++)
            {
                var label = new Label();
                label.Height = 28;
                label.Left = 0;
                label.Top = i * (label.Height + 10);
                label.Width = parametersPanel.Width - 50;               
                label.Text = parametersInfo[i].Name;
                label.Font = new Font(label.Font.FontFamily, 10);
                parametersPanel.Controls.Add(label);

                var inputBox = new NumericUpDown();
                inputBox.Left = label.Right + 5;
                inputBox.Top = label.Top;
                inputBox.Width = 45;
                inputBox.Height = label.Height;
                inputBox.Font = new Font(inputBox.Font.FontFamily, 10);
                inputBox.Minimum = (decimal)parametersInfo[i].MinValue;
                inputBox.Maximum = (decimal)parametersInfo[i].MaxValue;
                inputBox.Increment = (decimal)parametersInfo[i].Increment;
                inputBox.DecimalPlaces = 2;
                inputBox.Value = (decimal)parametersInfo[i].DefaultValue;
                parametersPanel.Controls.Add(inputBox);
                parameterControls.Add(inputBox);
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            var filter = filtersComboBox.SelectedItem as IFilter;

            if (filter == null) return;

            var parameters = new double[parameterControls.Count];

            for ( int i = 0; i < parameters.Length; i++)
                parameters[i] = (double)parameterControls[i].Value;

            resultPhoto = filter.Process(originalPhoto, parameters);
            resultPictureBox.Image = Convertors.PhotoToBitmap(resultPhoto);
        }

        public void AddFilter(IFilter filter)
        {
            if (filter == null) return;

            filtersComboBox.Items.Add(filter);
        }
    }
}
 