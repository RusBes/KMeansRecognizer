using Cyotek.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KursovaKSer
{
	public partial class Form1 : Form
	{
		private Series seriesInputDataTeaching;
		private Series seriesIdealCentersTeaching;

		private Series seriesInputDataRecongnizing;
		private Series seriesIdealCentersRecongnizing;
		private Series seriesRealCentersRecongnizing;

		public Form1()
		{
			InitializeComponent();

			updateClusterCountFromForm();

			seriesInputDataTeaching = new Series("InputData");
			seriesInputDataTeaching.ChartType = SeriesChartType.Point;
			seriesIdealCentersTeaching = new Series("IdealCenters");
			seriesIdealCentersTeaching.ChartType = SeriesChartType.Point;
			chartTeaching.Series.Add(seriesInputDataTeaching);
			chartTeaching.Series.Add(seriesIdealCentersTeaching);

			seriesInputDataRecongnizing = new Series("InputData");
			seriesInputDataRecongnizing.ChartType = SeriesChartType.Point;
			seriesIdealCentersRecongnizing = new Series("IdealCenters");
			seriesIdealCentersRecongnizing.ChartType = SeriesChartType.Point;
			seriesRealCentersRecongnizing = new Series("RealCenters");
			seriesRealCentersRecongnizing.ChartType = SeriesChartType.Point;
			seriesRealCentersRecongnizing.MarkerStyle = MarkerStyle.Star5;
			seriesRealCentersRecongnizing.MarkerSize = 10;
			chartRecognizing.Series.Add(seriesInputDataRecongnizing);
			chartRecognizing.Series.Add(seriesIdealCentersRecongnizing);
			chartRecognizing.Series.Add(seriesRealCentersRecongnizing);
		}

		private Model _model = new Model();

		private void butFullTeaching_Click(object sender, EventArgs e)
		{
			var files = Directory.GetFiles(@"E:\University\5 KURS\Neural Network Data\Numbers\").
				Where(path => Path.GetFileNameWithoutExtension(path).Last() == '1');
			var dict = new Dictionary<int, Bitmap>();
			foreach (var filePath in files)
			{
				var realNum = getRealNumFromFileName(filePath);
				dict[realNum] = ImageHelper.LoadBitmap(filePath);
			}
			_model.Teach(dict);
		}

		private void butTeachSingle_Click(object sender, EventArgs e)
		{
			var ofd = new OpenFileDialog();
			if (ofd.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			var bmp = ImageHelper.LoadBitmap(ofd.FileName);
			var inputImagePoints = ImageHelper.BitmapToPointFormat(bmp);
			var realNum = getRealNumFromFileName(ofd.FileName);
			_model.Teach(bmp, realNum);
			var outputCenters = _model.Centers[realNum];
			_model.Centers[realNum] = outputCenters;

			ShowPoints(inputImagePoints, seriesInputDataTeaching);
			ShowPoints(outputCenters, seriesIdealCentersTeaching);
		}

		private int getRealNumFromFileName(string filePath)
		{
			var file = Path.GetFileNameWithoutExtension(filePath);
			return int.Parse(file[file.Length - 2].ToString());
		}

		private void ShowPoints(Point[] points, Series series)
		{
			series.Points.Clear();
			foreach (var p in points)
			{
				series.Points.AddXY(p.X, p.Y);
			}
		}

		private void butRecognize_Click(object sender, EventArgs e)
		{
			var ofd = new OpenFileDialog();
			if (ofd.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			var image = ImageHelper.LoadBitmap(ofd.FileName);
			var probabilities = _model.Recognize(image);
			if(probabilities == null)
			{
				MessageBox.Show("Спочатку потрібно навчити систему розпізнавання!");
				return;
			}
			var recognizedNum = probabilities.First(x => x.Value == probabilities.Values.Max()).Key;

			showResults(probabilities, recognizedNum, image);
		}

		private void showResults(Dictionary<int, double> probabilities, int recognizedNum, Bitmap image)
		{
			foreach (var probability in probabilities)
			{
				lbResults.Items.Add($"Вірогідність\t{probability.Key}\t=\t{Math.Round(probability.Value, 2)}%");
			}
			lbResults.Items.Add($"Скоріше за все на рисунку зображена картинка цифри\t{recognizedNum}");
			lbResults.Items.Add("");

			ShowPoints(ImageHelper.BitmapToPointFormat(image), seriesInputDataRecongnizing);
			ShowPoints(_model.Centers[recognizedNum], seriesIdealCentersRecongnizing);
			ShowPoints(_model.LastRecognizedCenters, seriesRealCentersRecongnizing);
		}

		private void nudClusterCount_ValueChanged(object sender, EventArgs e)
		{
			updateClusterCountFromForm();
		}

		private void updateClusterCountFromForm()
		{
			_model.ClusterCount = Convert.ToInt32(nudClusterCount.Value);
		}
	}
	

	/// <summary>
	/// To be able drag control from toolbox
	/// </summary>
	public class MyImageBox : ImageBox
	{

	}
}
