using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaKSer
{
	class Model
	{
		private Random _rnd = new Random();

		public Point[] LastRecognized;

		private int _clusterCount = 6;
		public int ClusterCount
		{
			get { return _clusterCount; }
			set { _clusterCount = value; }
		}

		private int _maxLearningCount = 1000;
		public int MaxLearningCount
		{
			get { return _maxLearningCount; }
			set { _maxLearningCount = value; }
		}

		/// <summary>
		/// Каждому числу соответствует набор центров размером <see cref="ClusterCount"/>
		/// </summary>
		public Dictionary<int, Point[]> Centers = new Dictionary<int, Point[]>();


		public void Teach(Dictionary<int, Bitmap> source)
		{
			foreach (var item in source)
			{
				var centers = Teach(item.Value);
				Centers[item.Key] = centers;
			}
		}

		public Point[] Teach(Bitmap image)
		{
			var inputPoints = ImageHelper.BitmapToPointFormat(image);
			var centers = GetStartPoints(image.Width, image.Height, _clusterCount);
			double eps = 0.000001;
			double error = 1;
			Dictionary<int, List<Point>> clusters;
			for (int i = 0; i < MaxLearningCount && error > eps; i++)
			{
				clusters = AssignPointsToClusters(centers, inputPoints);
				if (clusters.Any(kv => !kv.Value.Any())) // если есть пустые кластеры, начинаем сначала
				{
					centers = GetStartPoints(image.Width, image.Height, _clusterCount);
					error = 1;
					i = 0;
					continue;
				}
				var newCenters = getNewCenters(clusters);
				error = getError(centers, newCenters);
				centers = newCenters;
			}
			return centers;
		}

		private Dictionary<int, List<Point>> AssignPointsToClusters(Point[] clusterCenters, Point[] points)
		{
			var res = new Dictionary<int, List<Point>>();
			for (int i = 0; i < clusterCenters.Length; i++)
			{
				res[i] = new List<Point>();
			}
			for (int i = 0; i < points.Length; i++)
			{
				var minDist = getDistance(points[i], clusterCenters[0]);
				var clusterWithMinDist = 0; // index
				for (int j = 1; j < clusterCenters.Length; j++)
				{
					var dist = getDistance(points[i], clusterCenters[j]);
					if(dist < minDist)
					{
						minDist = dist;
						clusterWithMinDist = j;
					}
				}
				if(res[clusterWithMinDist] == null)
				{
					res[clusterWithMinDist] = new List<Point>();
				}
				res[clusterWithMinDist].Add(points[i]);
			}
			return res;
		}

		private Point[] getNewCenters(Dictionary<int, List<Point>> clusters)
		{
			var res = new List<Point>();
			for (int i = 0; i < clusters.Keys.Count; i++)
			{
				res.Add(new Point(
					clusters[i].Sum(p => p.X) / clusters[i].Count,
					clusters[i].Sum(p => p.Y) / clusters[i].Count));
			}
			return res.ToArray();
		}

		private double getError(Point[] centers1, Point[] centers2)
		{
			var distances = new double[centers1.Length];
			for (int i = 0; i < distances.Length; i++)
			{
				distances[i] = getDistance(centers1[i], centers2[i]);
			}
			return distances.Max();
		}

		private double getDistance(Point p1, Point p2)
		{
			return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
		}

		private Point[] GetStartPoints(int imageWidth, int imageHeight, int clusterCount)
		{
			var res = new Point[clusterCount];
			for (int i = 0; i < clusterCount; i++)
			{
				res[i] = new Point(_rnd.Next(0, imageWidth), _rnd.Next(0, imageHeight));
			}
			return res;
		}

		public Dictionary<int, double> Recognize(Bitmap image)
		{
			if(Centers.Count == 0)
			{
				return null;
			}

			var imageCenters = Teach(image);
			var errors = new List<double>();
			var probabilities = new Dictionary<int, double>();
			foreach (var item in Centers)
			{
				errors.Add(getErrorEvklid(imageCenters, item.Value));
				probabilities[item.Key] = errors.Last();
			}

			LastRecognized = imageCenters;
			return probabilities;
		}
		
		private double getErrorEvklid(Point[] points1, Point[] points2)
		{
			var pnts = new Point[points1.Length];
			Array.Copy(points1, pnts, pnts.Length);
			var error = 0d;

			for (int i = 0; i < points2.Length; i++)
			{
				var minDistance = getDistance(pnts[0], points2[0]);
				var indMinDistance = 0;
				for (int j = 1; j < pnts.Length; j++)
				{
					var dist = getDistance(pnts[j], points2[i]);
					if(dist < minDistance)
					{
						minDistance = dist;
						indMinDistance = j;
					}
				}
				error += Math.Pow(minDistance, 2);
			}
			return Math.Sqrt(error);
		}
	}
}
