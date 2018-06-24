using System.Collections.Generic;
using System.Drawing;

namespace KursovaKSer
{
	public class Cluster
	{
		public Point Center { get; set; }

		public List<Point> Points { get; }

		public Cluster()
		{
			Points = new List<Point>();
		}

		public Cluster(Point center) : this()
		{
			Center = center;
		}

		public Cluster(Point center, List<Point> points)
		{
			Center = center;
			Points = points;
		}
	}
}
