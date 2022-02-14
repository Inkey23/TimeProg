
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
            this.LoadButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Grafik1 = new System.Windows.Forms.Button();
            this.GrafikTable = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.CheckRate = new System.Windows.Forms.CheckBox();
            this.CheckHorizontal = new System.Windows.Forms.CheckBox();
            this.CheckVertical = new System.Windows.Forms.CheckBox();
            this.CheckTemperature = new System.Windows.Forms.CheckBox();
            this.CheckZoom = new System.Windows.Forms.CheckBox();
            this.CheckMinutes = new System.Windows.Forms.CheckBox();
            this.CheckHour = new System.Windows.Forms.CheckBox();
            this.CheckJump = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GrafikTable)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadButton.Location = new System.Drawing.Point(1240, 517);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(244, 45);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Загрузить скрипт";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(1240, 621);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(244, 45);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Экспортировать данные";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Grafik1
            // 
            this.Grafik1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Grafik1.Location = new System.Drawing.Point(1240, 568);
            this.Grafik1.Name = "Grafik1";
            this.Grafik1.Size = new System.Drawing.Size(244, 45);
            this.Grafik1.TabIndex = 2;
            this.Grafik1.Text = "Построить график";
            this.Grafik1.UseVisualStyleBackColor = true;
            this.Grafik1.Click += new System.EventHandler(this.Grafik1_Click);
            // 
            // GrafikTable
            // 
            this.GrafikTable.BackColor = System.Drawing.Color.PaleTurquoise;
            this.GrafikTable.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalLeft;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.Title = "Время (сек) ";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            chartArea1.Name = "ChartArea1";
            this.GrafikTable.ChartAreas.Add(chartArea1);
            this.GrafikTable.Cursor = System.Windows.Forms.Cursors.Default;
            this.GrafikTable.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.GrafikTable.Legends.Add(legend1);
            this.GrafikTable.Location = new System.Drawing.Point(0, 0);
            this.GrafikTable.Name = "GrafikTable";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Горизонтальная пл-ть";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Вертикальная пл-ть";
            series2.YValuesPerPoint = 6;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Температура";
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.GrafikTable.Series.Add(series1);
            this.GrafikTable.Series.Add(series2);
            this.GrafikTable.Series.Add(series3);
            this.GrafikTable.Size = new System.Drawing.Size(1496, 678);
            this.GrafikTable.TabIndex = 3;
            this.GrafikTable.Text = "GrafikTable";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            title1.Name = "Title1";
            title1.Text = "Циклограмма v1.2";
            title1.TextStyle = System.Windows.Forms.DataVisualization.Charting.TextStyle.Shadow;
            this.GrafikTable.Titles.Add(title1);
            this.GrafikTable.Click += new System.EventHandler(this.chart1_Click);
            // 
            // CheckRate
            // 
            this.CheckRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckRate.AutoSize = true;
            this.CheckRate.Location = new System.Drawing.Point(1241, 428);
            this.CheckRate.Name = "CheckRate";
            this.CheckRate.Size = new System.Drawing.Size(117, 17);
            this.CheckRate.TabIndex = 5;
            this.CheckRate.Text = "Строить скорость";
            this.CheckRate.UseVisualStyleBackColor = true;
            // 
            // CheckHorizontal
            // 
            this.CheckHorizontal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckHorizontal.AutoSize = true;
            this.CheckHorizontal.Checked = true;
            this.CheckHorizontal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckHorizontal.Location = new System.Drawing.Point(1241, 451);
            this.CheckHorizontal.Name = "CheckHorizontal";
            this.CheckHorizontal.Size = new System.Drawing.Size(204, 17);
            this.CheckHorizontal.TabIndex = 6;
            this.CheckHorizontal.Text = "Показывать горизонтальную пл-ть";
            this.CheckHorizontal.UseVisualStyleBackColor = true;
            // 
            // CheckVertical
            // 
            this.CheckVertical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckVertical.AutoSize = true;
            this.CheckVertical.Checked = true;
            this.CheckVertical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckVertical.Location = new System.Drawing.Point(1241, 474);
            this.CheckVertical.Name = "CheckVertical";
            this.CheckVertical.Size = new System.Drawing.Size(193, 17);
            this.CheckVertical.TabIndex = 7;
            this.CheckVertical.Text = "Показывать вертикальную пл-ть";
            this.CheckVertical.UseVisualStyleBackColor = true;
            // 
            // CheckTemperature
            // 
            this.CheckTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckTemperature.AutoSize = true;
            this.CheckTemperature.Checked = true;
            this.CheckTemperature.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckTemperature.Location = new System.Drawing.Point(1241, 494);
            this.CheckTemperature.Name = "CheckTemperature";
            this.CheckTemperature.Size = new System.Drawing.Size(156, 17);
            this.CheckTemperature.TabIndex = 8;
            this.CheckTemperature.Text = "Показывать температуру";
            this.CheckTemperature.UseVisualStyleBackColor = true;
            // 
            // CheckZoom
            // 
            this.CheckZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckZoom.AutoSize = true;
            this.CheckZoom.Location = new System.Drawing.Point(1241, 405);
            this.CheckZoom.Name = "CheckZoom";
            this.CheckZoom.Size = new System.Drawing.Size(249, 17);
            this.CheckZoom.TabIndex = 9;
            this.CheckZoom.Text = "Режим масшатабирования кнопками мыши";
            this.CheckZoom.UseVisualStyleBackColor = true;
            // 
            // CheckMinutes
            // 
            this.CheckMinutes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckMinutes.AutoSize = true;
            this.CheckMinutes.Location = new System.Drawing.Point(1241, 323);
            this.CheckMinutes.Name = "CheckMinutes";
            this.CheckMinutes.Size = new System.Drawing.Size(146, 17);
            this.CheckMinutes.TabIndex = 10;
            this.CheckMinutes.Text = "Ось времени в минутах";
            this.CheckMinutes.UseVisualStyleBackColor = true;
            // 
            // CheckHour
            // 
            this.CheckHour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckHour.AutoSize = true;
            this.CheckHour.Location = new System.Drawing.Point(1241, 346);
            this.CheckHour.Name = "CheckHour";
            this.CheckHour.Size = new System.Drawing.Size(133, 17);
            this.CheckHour.TabIndex = 11;
            this.CheckHour.Text = "Ось времени в часах";
            this.CheckHour.UseVisualStyleBackColor = true;
            // 
            // CheckJump
            // 
            this.CheckJump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckJump.AutoSize = true;
            this.CheckJump.Location = new System.Drawing.Point(1241, 369);
            this.CheckJump.Name = "CheckJump";
            this.CheckJump.Size = new System.Drawing.Size(209, 30);
            this.CheckJump.TabIndex = 12;
            this.CheckJump.Text = "Убрать скачок на 5760 \r\n(необходимо перезагрузить скрипт)";
            this.CheckJump.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1496, 678);
            this.Controls.Add(this.CheckJump);
            this.Controls.Add(this.CheckHour);
            this.Controls.Add(this.CheckMinutes);
            this.Controls.Add(this.CheckZoom);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Grafik1);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.CheckTemperature);
            this.Controls.Add(this.CheckVertical);
            this.Controls.Add(this.CheckHorizontal);
            this.Controls.Add(this.CheckRate);
            this.Controls.Add(this.GrafikTable);
            this.Name = "Form1";
            this.Text = "AcuitasCyclogram_v1.2";
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
        private System.Windows.Forms.CheckBox CheckZoom;
        private System.Windows.Forms.CheckBox CheckMinutes;
        private System.Windows.Forms.CheckBox CheckHour;
        private System.Windows.Forms.CheckBox CheckJump;
    }
}

