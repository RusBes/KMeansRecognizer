namespace KursovaKSer
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			this.butTeachSingle = new System.Windows.Forms.Button();
			this.lbResults = new System.Windows.Forms.ListBox();
			this.butRecognize = new System.Windows.Forms.Button();
			this.chartTeaching = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.nudClusterCount = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.chartRecognizing = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.butFullTeaching = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.chartTeaching)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudClusterCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chartRecognizing)).BeginInit();
			this.SuspendLayout();
			// 
			// butTeachSingle
			// 
			this.butTeachSingle.Location = new System.Drawing.Point(12, 55);
			this.butTeachSingle.Name = "butTeachSingle";
			this.butTeachSingle.Size = new System.Drawing.Size(167, 37);
			this.butTeachSingle.TabIndex = 0;
			this.butTeachSingle.Text = "Навчити одну цифру";
			this.butTeachSingle.UseVisualStyleBackColor = true;
			this.butTeachSingle.Click += new System.EventHandler(this.butTeachSingle_Click);
			// 
			// lbResults
			// 
			this.lbResults.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lbResults.FormattingEnabled = true;
			this.lbResults.Location = new System.Drawing.Point(0, 211);
			this.lbResults.Name = "lbResults";
			this.lbResults.Size = new System.Drawing.Size(598, 121);
			this.lbResults.TabIndex = 5;
			// 
			// butRecognize
			// 
			this.butRecognize.Location = new System.Drawing.Point(12, 98);
			this.butRecognize.Name = "butRecognize";
			this.butRecognize.Size = new System.Drawing.Size(167, 37);
			this.butRecognize.TabIndex = 0;
			this.butRecognize.Text = "Розпізнати зображення";
			this.butRecognize.UseVisualStyleBackColor = true;
			this.butRecognize.Click += new System.EventHandler(this.butRecognize_Click);
			// 
			// chartTeaching
			// 
			chartArea1.Name = "ChartArea1";
			this.chartTeaching.ChartAreas.Add(chartArea1);
			this.chartTeaching.Location = new System.Drawing.Point(185, 12);
			this.chartTeaching.Name = "chartTeaching";
			this.chartTeaching.Size = new System.Drawing.Size(199, 193);
			this.chartTeaching.TabIndex = 6;
			this.chartTeaching.Text = "chart1";
			// 
			// nudClusterCount
			// 
			this.nudClusterCount.Location = new System.Drawing.Point(119, 141);
			this.nudClusterCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudClusterCount.Name = "nudClusterCount";
			this.nudClusterCount.Size = new System.Drawing.Size(60, 20);
			this.nudClusterCount.TabIndex = 7;
			this.nudClusterCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
			this.nudClusterCount.ValueChanged += new System.EventHandler(this.nudClusterCount_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 143);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Кількість кластерів";
			// 
			// chartRecognizing
			// 
			chartArea2.Name = "ChartArea1";
			this.chartRecognizing.ChartAreas.Add(chartArea2);
			this.chartRecognizing.Location = new System.Drawing.Point(390, 12);
			this.chartRecognizing.Name = "chartRecognizing";
			this.chartRecognizing.Size = new System.Drawing.Size(199, 193);
			this.chartRecognizing.TabIndex = 6;
			this.chartRecognizing.Text = "chart1";
			// 
			// butFullTeaching
			// 
			this.butFullTeaching.Location = new System.Drawing.Point(12, 12);
			this.butFullTeaching.Name = "butFullTeaching";
			this.butFullTeaching.Size = new System.Drawing.Size(167, 37);
			this.butFullTeaching.TabIndex = 0;
			this.butFullTeaching.Text = "Повне навчання";
			this.butFullTeaching.UseVisualStyleBackColor = true;
			this.butFullTeaching.Click += new System.EventHandler(this.butFullTeaching_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(598, 332);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nudClusterCount);
			this.Controls.Add(this.chartRecognizing);
			this.Controls.Add(this.chartTeaching);
			this.Controls.Add(this.lbResults);
			this.Controls.Add(this.butRecognize);
			this.Controls.Add(this.butFullTeaching);
			this.Controls.Add(this.butTeachSingle);
			this.Name = "Form1";
			this.Text = "Беселовський Руслан. Розпізнавання методом k-середніх";
			((System.ComponentModel.ISupportInitialize)(this.chartTeaching)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudClusterCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chartRecognizing)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button butTeachSingle;
		private System.Windows.Forms.ListBox lbResults;
		private System.Windows.Forms.Button butRecognize;
		private System.Windows.Forms.DataVisualization.Charting.Chart chartTeaching;
		private System.Windows.Forms.NumericUpDown nudClusterCount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataVisualization.Charting.Chart chartRecognizing;
		private System.Windows.Forms.Button butFullTeaching;
	}
}

