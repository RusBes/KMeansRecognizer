using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KursovaKSer
{
	class Model
	{
		public Point[] LastRecognizedCenters { get; private set; }

		public int ClusterCount
		{
			get { return _algorithm.ClusterCount; }
			set { _algorithm.ClusterCount = value; }
		}

		private KMeansAlgorithm _algorithm;

		private Recognizer _recognizer;

		/// <summary>
		/// Каждому числу соответствует набор центров размером <see cref="ClusterCount"/>
		/// </summary>
		public Dictionary<int, Point[]> Centers
		{
			get { return _recognizer.Centers; }
		}

		public Model()
		{
			_algorithm = new KMeansAlgorithm();
			_recognizer = new Recognizer(_algorithm);
		}

		public void Teach(Dictionary<int, Bitmap> source)
		{
			foreach (var kvp in source)
			{
				Teach(kvp.Value, kvp.Key);
			}
		}

		public void Teach(Bitmap image, int realNumber)
		{
			_recognizer.Teach(image, realNumber);
		}

		public Dictionary<int, double> Recognize(Bitmap image)
		{
			var result = _recognizer.Recognize(image);
			LastRecognizedCenters = result.Clusters.Select(cl => cl.Center).ToArray();
			return result.Probabilities;
		}
	}
}
