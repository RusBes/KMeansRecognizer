using System.Drawing;

namespace KursovaKSer
{
	public struct Area
	{
		public Point TopLeft { get; set; }

		public Point BotRight { get; set; }

		public Area(Point topLeft, Point botRight)
		{
			TopLeft = topLeft;
			BotRight = botRight;
		}
	}
}
