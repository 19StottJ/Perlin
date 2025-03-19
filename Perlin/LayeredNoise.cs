using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perlin
{
    internal class LayeredNoise
    {
        int[] xoff;
        int[] yoff;
        Noise[] noises;
        int layers;

        public LayeredNoise(int layers, int seed)
        {
            this.layers = layers;
            xoff = new int[layers];
            yoff = new int[layers];
            noises = new Noise[layers];

            Random r = new(seed);

            for (int j = 0; j < layers; j++)
            {
                int size = 10 * (int) (Math.Pow(2.0, (double) j));
                xoff[j] = r.Next() % size;
                yoff[j] = r.Next() % size;
                noises[j] = new Noise(size, r.Next());
            }
        }

        public double GetAt(int x, int y)
        {
            double value = 0.0;
            double totAmp = 0.0;
            for (int j = 0; j < layers; j++)
            {

                double amp = Math.Pow(2.0, (double) j);
                totAmp += amp;
                int dx = xoff[j];
                int dy = yoff[j];
                value += amp * noises[j].ValueAtPoint(x + dx, y + dy);
            }
            value /= totAmp;

            if (value < 0)
            {
                value = -Math.Sqrt(-value); // make bigger
            }
            else
            {
                value = Math.Sqrt(value);
            }
            return value;
        }
    }
}
