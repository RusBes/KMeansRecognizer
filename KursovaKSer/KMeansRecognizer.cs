using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KursovaKSer
{
	public class Recognizer
	{
		public int ClusterCount
		{
			get { return _algorithmProcessor.ClusterCount; }
			set { _algorithmProcessor.ClusterCount = value; }
		}
		
		public double Epsilon
		{
			get { return _algorithmProcessor.Epsilon; }
			set { _algorithmProcessor.Epsilon = value; }
		}

		/// <summary>
		/// Every recognizable number have <see cref="ClusterCount"/> centers
		/// </summary>
		public Dictionary<int, Point[]> Centers { get; private set; }

		private KMeansAlgorithm _algorithmProcessor;

		public Recognizer(KMeansAlgorithm algorithm)
		{
			_algorithmProcessor = algorithm;
			Centers = new Dictionary<int, Point[]>();
			Epsilon = 0.0001;
			ClusterCount = 6;
		}

		public void Teach(Bitmap image, int realNumber)
		{
			var inputPoints = ImageHelper.BitmapToPointFormat(image);
			_algorithmProcessor.WorkingArea = new Area(new Point(0, image.Height), new Point(image.Width, 0));
			var clusters = _algorithmProcessor.Run(inputPoints.ToList());
			Centers[realNumber] = clusters.Select(cl => cl.Center).ToArray();
		}

		public RecognizingResult Recognize(Bitmap image)
		{
			if (Centers.Count == 0)
			{
				throw new Exception("Teaching required");
			}
			var points = ImageHelper.BitmapToPointFormat(image);
			var clusters = _algorithmProcessor.Run(points.ToList());

			var recognizedCenters = clusters.Select(cl => cl.Center).ToArray();
			var distancesDict = new Dictionary<int, double>();
			foreach (var kvp in Centers)
			{
				distancesDict[kvp.Key] = getEvklidDist(recognizedCenters, kvp.Value);
			}
			var probabilities = distancesToProbabilities(distancesDict);
			return new RecognizingResult(probabilities, clusters);
		}

		private Dictionary<int, double> distancesToProbabilities(Dictionary<int, double> distances)
		{
			var res = new Dictionary<int, double>(distances);
			var sum = res.Select(kvp => 1 / kvp.Value).Sum();
			foreach (var key in distances.Keys)
			{
				res[key] = (100 / res[key]) / sum;
			}
			return res;
		}

		private double getEvklidDist(Point[] points1, Point[] points2)
		{
			var error = 0d;
			for (int i = 0; i < points2.Length; i++)
			{
				var minDistance = _algorithmProcessor.GetDistance(points1[0], points2[0]);
				var indMinDistance = 0;
				for (int j = 1; j < points1.Length; j++)
				{
					var dist = _algorithmProcessor.GetDistance(points1[j], points2[i]);
					if (dist < minDistance)
					{
						minDistance = dist;
						indMinDistance = j;
					}
				}
				error += Math.Pow(minDistance, 2);
			}
			return Math.Sqrt(error);
		}

		public struct RecognizingResult
		{
			public Dictionary<int, double> Probabilities { get; }

			public List<Cluster> Clusters { get; }

			public RecognizingResult(Dictionary<int, double> probabilities, List<Cluster> clusters)
			{
				Probabilities = probabilities;
				Clusters = clusters;
			}
		}
	}
}
