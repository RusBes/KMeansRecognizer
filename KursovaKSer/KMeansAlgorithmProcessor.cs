using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KursovaKSer
{
	public class KMeansAlgorithm
	{
		public int ClusterCount { get; set; }
		
		public double Epsilon { get; set; }

		public int MaxIterationCount { get; set; }

		public List<Point> Points { get; set; }

		private Area _workingArea;
		private bool _workingAreaAssigned;
		/// <summary>
		/// If not set, return rectangle formed by <see cref='Points'/>
		/// </summary>
		public Area WorkingArea
		{
			get
			{
				if (_workingAreaAssigned)
				{
					return _workingArea;
				}
				if(Points == null)
				{
					return new Area();
				}
				var topLeft = new Point(
					Points.Select(p => p.X).Min(),
					Points.Select(p => p.Y).Max());
				var botRight = new Point(
					Points.Select(p => p.X).Max(),
					Points.Select(p => p.Y).Min());
				return new Area(topLeft, botRight);
			}
			set
			{
				_workingArea = value;
				_workingAreaAssigned = true;
			}
		}

		private Random _rnd = new Random();

		public KMeansAlgorithm() : this(6) { }

		public KMeansAlgorithm(int clusterCount)
		{
			MaxIterationCount = 1000;
			Epsilon = 0.0001;
			ClusterCount = 6;
		}

		public List<Cluster> Run()
		{
			if(Points == null || Points.Count == 0)
			{
				throw new Exception("No points assigned");
			}
			return Run(Points);
		}

		public List<Cluster> Run(List<Point> inputPoints)
		{
			var area = WorkingArea;
			var centers = getRandomPoints(area.TopLeft.X, area.BotRight.X, area.BotRight.Y, area.TopLeft.Y);
			var clusters = centers.Select(center => new Cluster(center)).ToList();
			double error = 1;
			for (int i = 0; i < MaxIterationCount && error > Epsilon; i++)
			{
				clusters.ForEach(cl => cl.Points.Clear());
				AssignPointsToClusters(clusters, inputPoints);
				if (clusters.Any(cl => !cl.Points.Any())) // start over if empty clusters exist
				{
					return Run(inputPoints);
				}
				var newCenters = recalcCenters(clusters);
				error = getError(centers, newCenters);
				centers = newCenters;
				for (int j = 0; j < clusters.Count; j++)
				{
					clusters[j].Center = centers[j];
				}
			}
			return clusters;
		}

		private List<Point> getRandomPoints(int minX, int maxX, int minY, int maxY)
		{
			var res = new List<Point>();
			for (int i = 0; i < ClusterCount; i++)
			{
				res.Add(new Point(_rnd.Next(minX, maxX), _rnd.Next(minY, maxY)));
			}
			return res;
		}

		private void AssignPointsToClusters(List<Cluster> clusters, List<Point> points)
		{
			foreach (var p in points)
			{
				var cluster = getNearestCluster(p, clusters);
				cluster.Points.Add(p);
			}
		}

		private Cluster getNearestCluster(Point p, List<Cluster> clusters)
		{
			var minDist = GetDistance(p, clusters[0].Center);
			var nearestCluster = clusters[0];
			for (int j = 1; j < clusters.Count; j++)
			{
				var dist = GetDistance(p, clusters[j].Center);
				if (dist < minDist)
				{
					minDist = dist;
					nearestCluster = clusters[j];
				}
			}
			return nearestCluster;
		}

		public double GetDistance(Point p1, Point p2)
		{
			return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
		}

		private List<Point> recalcCenters(List<Cluster> clusters)
		{
			var res = new List<Point>();
			for (int i = 0; i < clusters.Count; i++)
			{
				res.Add(new Point(
					clusters[i].Points.Sum(p => p.X) / clusters[i].Points.Count,
					clusters[i].Points.Sum(p => p.Y) / clusters[i].Points.Count));
			}
			return res;
		}
		
		private double getError(List<Point> centers1, List<Point> centers2)
		{
			var distances = new double[centers1.Count];
			for (int i = 0; i < centers1.Count; i++)
			{
				distances[i] = GetDistance(centers1[i], centers2[i]);
			}
			return distances.Max();
		}
	}
}
