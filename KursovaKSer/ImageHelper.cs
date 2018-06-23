using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaKSer
{
	public static class ImageHelper
	{
		public static unsafe byte[,,] BitmapToBytes(Bitmap bmp)
		{
			int width = bmp.Width,
				height = bmp.Height;
			byte[,,] res = new byte[3, height, width];
			BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
				PixelFormat.Format24bppRgb);
			try
			{
				byte* curpos;
				fixed (byte* _res = res)
				{
					byte* _r = _res, _g = _res + width * height, _b = _res + 2 * width * height;
					for (int h = 0; h < height; h++)
					{
						curpos = ((byte*)bd.Scan0) + h * bd.Stride;
						for (int w = 0; w < width; w++)
						{
							*_b = *(curpos++);
							++_b;
							*_g = *(curpos++);
							++_g;
							*_r = *(curpos++);
							++_r;
						}
					}
				}
			}
			finally
			{
				bmp.UnlockBits(bd);
			}
			return res;
		}

		public static unsafe Bitmap BytesToBitmap(byte[,,] rgb)
		{
			if ((rgb.GetLength(0) != 3))
			{
				throw new ArrayTypeMismatchException("Size of first dimension for passed array must be 3 (RGB components)");
			}

			int width = rgb.GetLength(2),
				height = rgb.GetLength(1);

			Bitmap result = new Bitmap(width, height, PixelFormat.Format24bppRgb);

			BitmapData bd = result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
				PixelFormat.Format24bppRgb);

			try
			{
				byte* curpos;
				fixed (byte* _rgb = rgb)
				{
					byte* _r = _rgb, _g = _rgb + width * height, _b = _rgb + 2 * width * height;
					for (int h = 0; h < height; h++)
					{
						curpos = ((byte*)bd.Scan0) + h * bd.Stride;
						for (int w = 0; w < width; w++)
						{
							*(curpos++) = *_b; ++_b;
							*(curpos++) = *_g; ++_g;
							*(curpos++) = *_r; ++_r;
						}
					}
				}
			}
			finally
			{
				result.UnlockBits(bd);
			}

			return result;
		}
		
		public static Bitmap LoadBitmap(string path)
		{
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return new Bitmap(fs);
			}
		}



		public static byte[,] Monochromize(byte[,,] input, int limit = 126)
		{
			var res = new byte[input.GetLength(1), input.GetLength(2)];
			for (int i = 0; i < input.GetLength(1); i++)
			{
				for (int j = 0; j < input.GetLength(2); j++)
				{
					var averageComponent = (input[0, i, j] + input[1, i, j] + input[1, i, j]) / 3;
					res[i, j] = (byte)(averageComponent > limit ? 255 : 0);
				}
			}
			return res;
		}

		public static double[,] Normalize(byte[,] input)
		{
			var res = new double[input.GetLength(0), input.GetLength(1)];
			for (int i = 0; i < input.GetLength(0); i++)
			{
				for (int j = 0; j < input.GetLength(1); j++)
				{
					res[i, j] = input[i, j] == 0 ? 1 : 0;
				}
			}
			return res;
		}

		public static Point[] BitmapToPointFormat(Bitmap bmp)
		{
			var res = new List<Point>();
			var normalized = Normalize(Monochromize(BitmapToBytes(bmp)));
			for (int i = 0; i < normalized.GetLength(0); i++)
			{
				for (int j = 0; j < normalized.GetLength(1); j++)
				{
					if(normalized[i, j] > 0)
					{
						res.Add(new Point(j, normalized.GetLength(0) - i));
					}
				}
			}
			return res.ToArray();
		}
	}
}
