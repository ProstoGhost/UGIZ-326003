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

namespace PhotoEnhancer
{
    public partial class MainForm : Form
    {
        private Label dragDropLabel; // Объявляем Label для отображения сообщения

        Panel parametersPanel;
        List<NumericUpDown> parameterControls;

        Photo originalPhoto;
        Photo resultPhoto;

        public MainForm()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += MainForm_DragEnter;
            this.DragDrop += MainForm_DragDrop;
            this.DragLeave += MainForm_DragLeave;

            dragDropLabel = new Label
            {
                Text = "Перетащите изображение сюда",
                Font = new Font("Play", 32),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(150, 0, 0, 0), // Полупрозрачный черный фон
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false,
                Dock = DockStyle.Fill // Заполняем весь размер формы
            };
            this.Controls.Add(dragDropLabel);
        }

        private void filtersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            applyButton.Visible = true;
            replaceButton.Visible = true;

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
            resultPictureBox.Visible = true;
        }

        public void AddFilter(IFilter filter)
        {
            if (filter == null) return;

            filtersComboBox.Items.Add(filter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.tiff", // Фильтр для выбора изображений
                Title = "Выберите изображение"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Получаем путь к выбранному файлу
                string filePath = openFileDialog.FileName;

                try
                {
                    // Загружаем изображение в объект Bitmap
                    Bitmap bmp = (Bitmap)Image.FromFile(filePath);

                    // Преобразуем Bitmap в Photo (если это необходимо)
                    originalPhoto = Convertors.BitmapToPhoto(bmp);

                    // Отображаем изображение в PictureBox
                    originalPictureBox.Image = bmp;
                }
                catch(Exception ex) {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Проверяем, что обработанное изображение существует
            if (resultPhoto == null)
            {
                MessageBox.Show("Обработанное изображение не найдено. Примените фильтр к изображению.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Создаем объект SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.tiff", // Фильтр для сохранения изображений
                Title = "Сохранить изображение"
            };
            // Показываем диалоговое окно и проверяем результат
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Получаем путь для сохранения файла
                string filePath = saveFileDialog.FileName;

                try
                {
                    // Сохраняем обработанное изображение
                    Bitmap resultBitmap = Convertors.PhotoToBitmap(resultPhoto);
                    resultBitmap.Save(filePath);
                    MessageBox.Show("Изображение успешно сохранено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // Проверяем, что перетаскиваемый объект является файлом изображения
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && IsImageFile(files[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                    dragDropLabel.Visible = true; // Показываем Label
                    dragDropLabel.BringToFront(); // Перемещаем Label в самый верхний слой
                }
            }
        }
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string filePath = files[0];

                    try
                    {
                        // Загружаем изображение в объект Bitmap
                        Bitmap bmp = (Bitmap)Image.FromFile(filePath);

                        // Преобразуем Bitmap в Photo (если это необходимо)
                        originalPhoto = Convertors.BitmapToPhoto(bmp);

                        // Отображаем изображение в PictureBox
                        originalPictureBox.Image = bmp;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            dragDropLabel.Visible = false; // Скрываем Label после завершения перетаскивания
        }
        private void MainForm_DragLeave(object sender, EventArgs e)
        {
            dragDropLabel.Visible = false; // Скрываем Label, если перетаскивание покинуло форму
        }
        private bool IsImageFile(string filePath)
        {
            // Проверяем расширение файла на соответствие изображениям
            string[] validExtensions = { ".jpg", ".jpeg", ".tiff" };
            string extension = Path.GetExtension(filePath).ToLower();
            return validExtensions.Contains(extension);
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            var filter = filtersComboBox.SelectedItem as IFilter;

            if (filter == null) return;

            var parameters = new double[parameterControls.Count];

            for (int i = 0; i < parameters.Length; i++)
                parameters[i] = (double)parameterControls[i].Value;

            //resultPhoto = filter.Process(originalPhoto, parameters);
            //resultPictureBox.Image = Convertors.PhotoToBitmap(resultPhoto);

            originalPhoto = filter.Process(originalPhoto, parameters);
            originalPictureBox.Image = Convertors.PhotoToBitmap(originalPhoto);

            resultPhoto = originalPhoto;
            resultPictureBox.Image = originalPictureBox.Image;
            resultPictureBox.Visible = false;
            
        }
    }
}
 