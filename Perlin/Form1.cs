using System.Windows.Forms;

namespace Perlin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GenTerrain()
        {
            int width = int.Parse(txtWidth.Text);
            int height = int.Parse(txtHeight.Text);

            Bitmap i = new Bitmap(width, height);

            int seed = txtSeed.Text.GetHashCode();

            LayeredNoise noise = new LayeredNoise(4, seed);

            LayeredNoise forest = new LayeredNoise(4, seed + 10000);
            LayeredNoise beach = new LayeredNoise(4, seed + 20000);

            Random whiteNoiseGen = new(seed + 20000);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double value = noise.GetAt(x, y);


                    if (value < 0) // sea
                    {
                        value = Math.Clamp(value * -2.0, 0.0, 1.0);
                        int red = 100 - (int)(value * 100);
                        int green = 180 - (int)(value * 128);

                        int blue = 200 - (int)(value * 55);

                        i.SetPixel(x, y, Color.FromArgb(255, red, green, blue));
                    }
                    else if (value + 0.04*beach.GetAt(x, y) < 0.05) // beach
                    {
                        int n = (int) (50.0 * whiteNoiseGen.NextDouble());
                        i.SetPixel(x, y, Color.FromArgb(255, 200-n, 180-n, 100-n));
                    }
                    else if (value > 0.35) // mountain peak
                    {
                        double lval = Math.Clamp((value - 0.33) * 100.0, 0.0, 1.0);
                        int intv = 240 + 15 * (int)lval;

                        i.SetPixel(x, y, Color.FromArgb(255, intv, intv, intv));
                    }
                    else if (value > 0.3) // mountain
                    {
                        double lval = Math.Clamp((value - 0.3) * 10.0, 0.0, 1.0);
                        int intv = 70 + 80 * (int)lval;

                        i.SetPixel(x, y, Color.FromArgb(255, intv, intv, intv));
                    }
                    else
                    {
                        // grass

                        int intv = (int)(value * 170) + 80;

                        double whiteNoise = whiteNoiseGen.NextDouble();

                        double forestValue = forest.GetAt(x, y);

                        if (value > 0.1 && value < 0.2 && forestValue > 0.07 && whiteNoise > 0.2)
                        {
                            i.SetPixel(x, y, Color.FromArgb(255, 0, intv - (int)(60.0 * whiteNoise + 40.0 * forestValue), 0));
                        }
                        else
                        {
                            i.SetPixel(x, y, Color.FromArgb(255, 0, intv, 0));
                        }


                    }
                }
            }

            for (int n = 0; n < 5; n++) // towns
            {
                int x = whiteNoiseGen.Next() % width;
                int y = whiteNoiseGen.Next() % height;
                double elev = noise.GetAt(x, y);
                if (elev < 0.05 || elev > 0.32)
                {
                    n -= 1;
                    continue;
                }

                for (int j = 0; j < 10; j++) // building
                {
                    int dx = whiteNoiseGen.Next() % 24;
                    int dy = whiteNoiseGen.Next() % 24;

                    int w = whiteNoiseGen.Next() % 5 + 1;
                    int h = whiteNoiseGen.Next() % 5 + 1;

                    for (int px = x+dx; px < x+dx+w; px++)
                    {
                        for (int py = y+dy; py < y+dy+h; py++)
                        {
                            if (px >= width || py >= height)
                            {
                                continue;
                            }
                            i.SetPixel(px, py, Color.FromArgb(255, 91, 61, 34));
                        }
                    }
                }
            }

            for (int n = 0; n < 10; n++)
            {
                int x = whiteNoiseGen.Next() % width;
                int y = whiteNoiseGen.Next() % height;

                double elev = noise.GetAt(x, y);
                if (elev < 0.32)
                {
                    continue;
                }


                int length = 0;

                while (elev > 0)
                {
                    i.SetPixel(x, y, Color.DarkBlue);
                    length += 1;

                    double up = noise.GetAt(x, y - 1);
                    double down = noise.GetAt(x, y + 1);
                    double left = noise.GetAt(x - 1, y);
                    double right = noise.GetAt(x + 1, y);

                    double biasY = up - down;
                    double biasX = left - right;

                    double vLength = Math.Sqrt(biasX * biasX + biasY * biasY);

                    double xdir = biasX / vLength;
                    double ydir = biasY / vLength;

                    double threshold = 1.0 / Math.Sqrt(2);

                    if (xdir > threshold)
                    {
                        i.SetPixel(x+1, y, Color.DarkBlue);
                    } else if (xdir < -threshold)
                    {
                        i.SetPixel(x-1, y, Color.DarkBlue);
                    }
                    
                    if (ydir > threshold)
                    {
                        i.SetPixel(x, y+1, Color.DarkBlue);
                    } else if (ydir < -threshold)
                    {
                        i.SetPixel(x, y-1, Color.DarkBlue);
                    }

                    if (whiteNoiseGen.NextDouble() < xdir)
                    {
                        x += 1;
                    }
                    else if (whiteNoiseGen.NextDouble() < -xdir)
                    {
                        x -= 1;
                    }
                    if (whiteNoiseGen.NextDouble() < ydir)
                    {
                        y += 1;
                    }
                    else if (whiteNoiseGen.NextDouble() < -ydir)
                    {
                        y -= 1;
                    }

                    if (x >= width-1 || y >= height-1 || x <= 0 || y <= 0)
                    {
                        break;
                    }
                    if (length > 1000)
                    {
                        break;
                    }

                    elev = noise.GetAt(x, y);
                }
            }

            pctBox.Image = i;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GenTerrain();
        }

        private void btnRandomSeed_Click(object sender, EventArgs e)
        {
            Random r = new();
            txtSeed.Text = r.Next().ToString();
            GenTerrain();
        }

        private void txtSeed_TextChanged(object sender, EventArgs e)
        {
            GenTerrain();
        }
    }
}
