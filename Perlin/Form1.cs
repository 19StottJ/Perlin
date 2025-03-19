namespace Perlin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pctBox_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Bitmap i = new Bitmap(500, 500);

            int seed = txtSeed.Text.GetHashCode();

            LayeredNoise noise = new LayeredNoise(4, seed);

            LayeredNoise forest = new LayeredNoise(5, seed + 10000);

            Random whiteNoiseGen = new(seed + 20000);
            
            for (int x = 0; x < 300; x++)
            {
                for (int y = 0; y < 300; y++)
                {
                    double value = noise.GetAt(x, y);


                    if (value < 0)
                    {
                        value = Math.Clamp(value * -2.0, 0.0, 1.0);
                        int red = 100 - (int)(value * 100);
                        int green = 180 - (int)(value * 128);

                        int blue = 200 - (int)(value * 55);

                        i.SetPixel(x, y, Color.FromArgb(255, red, green, blue));
                    }
                    else if (value > 0.4)
                    {
                        double lval = Math.Clamp((value - 0.33) * 100.0, 0.0, 1.0);
                        int intv = 240 + 15 * (int)lval;

                        i.SetPixel(x, y, Color.FromArgb(255, intv, intv, intv));
                    }
                    else if (value > 0.3)
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
                            i.SetPixel(x, y, Color.FromArgb(255, 0, intv - (int) (60.0 * whiteNoise + 40.0 * forestValue), 0));
                        } else {
                            

                            i.SetPixel(x, y, Color.FromArgb(255, 0, intv, 0));
                        }

                        
                    }
                }
            }

            for (int n = 0; n < 4; n++)
            {
                int x = whiteNoiseGen.Next() % 300;
                int y = whiteNoiseGen.Next() % 300;

                double height = noise.GetAt(x, y);
                if (height < 0.32)
                {
                    n -= 1;
                    continue;
                }

                double mX = 0.0;
                double mY = 0.0;

                while (height > 0)
                {
                    if (x > 300 || y > 300 || x < 0 || y < 0)
                    {
                        break;
                    }

                    i.SetPixel(x, y, Color.DarkBlue);


                    double up = noise.GetAt(x, y - 1);
                    double down = noise.GetAt(x, y + 1);
                    double left = noise.GetAt(x - 1, y);
                    double right = noise.GetAt(x + 1, y);

                    double lowest = Math.Min(Math.Min(up, down), Math.Min(left, right));

                    if (lowest == up)
                    {
                        y -= 1;
                        mY -= 1.0;
                    } else if (lowest == down)
                    {
                        y += 1;
                        mY -= 1.0;
                    } else if (lowest == left)
                    {
                        x -= 1;
                        mX += 1.0;
                    } else
                    {
                        x += 1;
                        mX += 1.0;
                    }
                    mX *= 0.9;
                    mY *= 0.9;

                    while (mX > 1.0 || mY > 1.0)
                    {
                        i.SetPixel(x, y, Color.DarkBlue);
                        if (mX > 1.0)
                        {
                            x += 1;
                            mX *= 0.7;
                        }
                        if (mY > 1.0)
                        {
                            y += 1;
                            mY *= 0.7;
                        }
                        if (x > 300 || y > 300 || x < 0 || y < 0)
                        {
                            break;
                        }
                    }

                    

                    height = noise.GetAt(x, y);
                }
            }

            pctBox.Image = i;
        }

        private void btnRandomSeed_Click(object sender, EventArgs e)
        {
            Random r = new();
            txtSeed.Text = r.Next().ToString();
        }
    }
}
