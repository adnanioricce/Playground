using System;

namespace PerlinNoise
{
    //TODO:Add a perlin noise implementation
    public class PerlinNoise
    {
        public static float[][] GeneratePerlinNoise(float[][] baseNoise,int octaveCount)
        {
            int width = baseNoise.Length;
            int height = baseNoise[0].Length;

            float[][][] smoothNoise = new float[octaveCount][][]; //an array of 2D arrays containing

            float persistance = 0.5f;

            //generate smooth noise
            for (int i = 0; i < octaveCount; i++)
            {
                smoothNoise[i] = GenerateSmoothNoise(baseNoise, i);
            }

            float[][] perlinNoise = Utilities.GetEmptyArray<float>(width, height);
            float amplitude = 1.0f;
            float totalAmplitude = 0.0f;

            //blend noise together
            for (int octave = octaveCount - 1; octave > 0; octave--)
            {
                amplitude *= persistance;
                totalAmplitude += amplitude;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        perlinNoise[i][j] += smoothNoise[octave][i][j] * amplitude;
                    }
                }
            }

            //normalisation
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    perlinNoise[i][j] /= totalAmplitude;
                }
            }

            return perlinNoise;
        }
        public static float[][] GeneratePerlinNoise(int width,int height,int octaveCount)
        {
            float[][] noise = GenerateWhiteNoise(width, height);
            return GeneratePerlinNoise(noise, octaveCount);
        }
        private static float[][] GenerateWhiteNoise(int width, int height)
        {
            var random = new Random(0);
            float[][] noise = new float[2][]
            {
            new float[width],
            new float[height]
            };
            for (int i = 0; i < noise[0].Length; i++)
            {
                for (int j = 0; j < noise[1].Length; j++)
                {
                    noise[i][j] = random.Next() % 1;
                }
            }
            return noise;
        }
        private static float[][] GenerateSmoothNoise(float[][] noise, int octave)
        {
            int width = noise[0].Length;
            int height = noise[1].Length;
            float[][] smoothNoise = Utilities.GetEmptyArray<float>(width, height);
            int samplePeriod = 1 << octave;
            float sampleFrequency = 1.0f / samplePeriod;
            for (int i = 0; i < width; i++)
            {
                int sample_i0 = (i / samplePeriod) * samplePeriod;
                int sample_i1 = (sample_i0 + samplePeriod) % width; //wrap around
                float horizontalBlend = (i - sample_i0) * sampleFrequency;
                for (int j = 0; j < height; j++)
                {
                    //calculate the vertical sampling indices
                    int sample_j0 = (j / samplePeriod) * samplePeriod;
                    int sample_j1 = (sample_j0 + samplePeriod) % height; //wrap around
                    float vertical_blend = (j - sample_j0) * sampleFrequency;

                    //blend the top two corners
                    float top = Interpolate(noise[sample_i0][sample_j0],
                       noise[sample_i1][sample_j0], horizontalBlend);

                    //blend the bottom two corners
                    float bottom = Interpolate(noise[sample_i0][sample_j1],
                       noise[sample_i1][sample_j1], horizontalBlend);

                    //final blend
                    smoothNoise[i][j] = Interpolate(top, bottom, vertical_blend);
                }
            }
            return smoothNoise;
        }
        public static float Interpolate(float x0,float x1,float alpha)
        {
            return x0 * (1 - alpha) + alpha * x1;
        }
    
        private float[][] GetEmptyArray(int firstArrayLength, int secondArrayLength)
        {
            return new float[2][]
            {
                new float[firstArrayLength],
                new float[secondArrayLength]
            };
        }
    }
    
}
