
namespace TimeProg
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Grafik1 = new System.Windows.Forms.Button();
            this.GrafikTable = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CheckRate = new System.Windows.Forms.CheckBox();
            this.CheckHorizontal = new System.Windows.Forms.CheckBox();
            this.CheckVertical = new System.Windows.Forms.CheckBox();
            this.CheckTemperature = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GrafikTable)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(12, 881);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(207, 45);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Загрузить скрипт";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(1592, 881);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(209, 45);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Сохранить в файл";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Grafik1
            // 
            this.Grafik1.Location = new System.Drawing.Point(827, 881);
            this.Grafik1.Name = "Grafik1";
            this.Grafik1.Size = new System.Drawing.Size(275, 45);
            this.Grafik1.TabIndex = 2;
            this.Grafik1.Text = "Построить график";
            this.Grafik1.UseVisualStyleBackColor = true;
            this.Grafik1.Click += new System.EventHandler(this.Grafik1_Click);
            // 
            // GrafikTable
            // 
            this.GrafikTable.BackColor = System.Drawing.Color.PaleTurquoise;
            this.GrafikTable.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea1.AxisX.Title = "Время (сек) ";
            chartArea1.Name = "ChartArea1";
            this.GrafikTable.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.GrafikTable.Legends.Add(legend1);
            this.GrafikTable.Location = new System.Drawing.Point(16, 12);
            this.GrafikTable.Name = "GrafikTable";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Горизонтальная пл-ть";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Вертикальная пл-ть";
            series2.YValuesPerPoint = 6;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Температура";
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.GrafikTable.Series.Add(series1);
            this.GrafikTable.Series.Add(series2);
            this.GrafikTable.Series.Add(series3);
            this.GrafikTable.Size = new System.Drawing.Size(1785, 863);
            this.GrafikTable.TabIndex = 3;
            this.GrafikTable.Text = "GrafikTable";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            title1.Name = "Title1";
            title1.Text = "Циклограмма v0.4";
            title2.Name = "Title2";
            title2.Text = "Путь к файлу";
            this.GrafikTable.Titles.Add(title1);
            this.GrafikTable.Titles.Add(title2);
            this.GrafikTable.Click += new System.EventHandler(this.chart1_Click);
            // 
            // CheckRate
            // 
            this.CheckRate.AutoSize = true;
            this.CheckRate.Location = new System.Drawing.Point(1569, 190);
            this.CheckRate.Name = "CheckRate";
            this.CheckRate.Size = new System.Drawing.Size(117, 17);
            this.CheckRate.TabIndex = 5;
            this.CheckRate.Text = "Строить скорость";
            this.CheckRate.UseVisualStyleBackColor = true;
            // 
            // CheckHorizontal
            // 
            this.CheckHorizontal.AutoSize = true;
            this.CheckHorizontal.Checked = true;
            this.CheckHorizontal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckHorizontal.Location = new System.Drawing.Point(1569, 130);
            this.CheckHorizontal.Name = "CheckHorizontal";
            this.CheckHorizontal.Size = new System.Drawing.Size(15, 14);
            this.CheckHorizontal.TabIndex = 6;
            this.CheckHorizontal.UseVisualStyleBackColor = true;
            // 
            // CheckVertical
            // 
            this.CheckVertical.AutoSize = true;
            this.CheckVertical.Checked = true;
            this.CheckVertical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckVertical.Location = new System.Drawing.Point(1569, 145);
            this.CheckVertical.Name = "CheckVertical";
            this.CheckVertical.Size = new System.Drawing.Size(15, 14);
            this.CheckVertical.TabIndex = 7;
            this.CheckVertical.UseVisualStyleBackColor = true;
            // 
            // CheckTemperature
            // 
            this.CheckTemperature.AutoSize = true;
            this.CheckTemperature.Checked = true;
            this.CheckTemperature.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckTemperature.Location = new System.Drawing.Point(1569, 160);
            this.CheckTemperature.Name = "CheckTemperature";
            this.CheckTemperature.Size = new System.Drawing.Size(15, 14);
            this.CheckTemperature.TabIndex = 8;
            this.CheckTemperature.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1813, 938);
            this.Controls.Add(this.CheckTemperature);
            this.Controls.Add(this.CheckVertical);
            this.Controls.Add(this.CheckHorizontal);
            this.Controls.Add(this.CheckRate);
            this.Controls.Add(this.GrafikTable);
            this.Controls.Add(this.Grafik1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LoadButton);
            this.Name = "Form1";
            this.Text = "AcuitasCyclogram_v0.4";
            ((System.ComponentModel.ISupportInitialize)(this.GrafikTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button Grafik1;
        private System.Windows.Forms.CheckBox CheckRate;
        private System.Windows.Forms.CheckBox CheckHorizontal;
        private System.Windows.Forms.CheckBox CheckVertical;
        private System.Windows.Forms.CheckBox CheckTemperature;
        public System.Windows.Forms.DataVisualization.Charting.Chart GrafikTable;
    }
}

