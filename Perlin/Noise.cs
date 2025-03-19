using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Perlin
{
    internal class Noise
    {
        int grid_size;
        int seed;

        public Noise(int size, int seed)
        {
            grid_size = size;
            this.seed = seed;
        }

        Dictionary<(int, int), (double, double)> vectors = new();

        (double, double) VectorAtPoint(int x, int y)
        {
            if (vectors.ContainsKey((x, y)))
            {
                return vectors[(x, y)];
            }

            Random r = new Random(seed + x + y * 1000);
            double xf = r.NextDouble();
            double yf = r.NextDouble();
            double length = Math.Sqrt(xf * xf + yf * yf);

            vectors.Add(
                (x, y),
                (xf / length, yf / length)
                );

            return (xf / length, yf / length);
        }

        double DotAtPoint(int x, int y, int gx, int gy)
        {
            double dx = (x - (double) gx * (double) grid_size) / (double)grid_size;
            double dy = (y - (double) gy * (double) grid_size) / (double)grid_size;

            (double gradx, double grady) = VectorAtPoint(gx, gy);


            double dotproduct = gradx * dx + grady * dy;

            return dotproduct; // from -1 to 1
        }

        double Lerp(double t, double a1, double a2)
        {
            return a1 + t * (a2 - a1);
        }

        public double ValueAtPoint(int x, int y)
        {
            int gx = x / grid_size;
            int gy = y / grid_size;

            double topLeft = DotAtPoint(x, y, gx, gy);
            double topRight = DotAtPoint(x, y, gx + 1, gy);
            double bottomLeft = DotAtPoint(x, y, gx, gy + 1);
            double bottomRight = DotAtPoint(x, y, gx + 1, gy + 1);

            double dx = ((double) x - ((double) gx * (double) grid_size)) / (double) grid_size;
            double dy = ((double) y - ((double) gy * (double) grid_size)) / (double) grid_size;

            double u = dx; // no fade
            double v = dy;

            double val = Lerp(v,
                Lerp(u, topLeft, topRight),
                Lerp(u, bottomLeft, bottomRight)
            );


            return val;
        }
    }
}
