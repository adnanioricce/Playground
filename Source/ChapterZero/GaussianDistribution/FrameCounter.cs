using System.Collections.Generic;
using System.Linq;

namespace GaussianDistribution
{
    public class FrameCounter
    {
        public FrameCounter()
        {

        }
        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }
        private const int MAXIMUM_SAMPLES = 100;
        private Queue<float> _sampleBuffer = new Queue<float>();
        public bool Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;
            _sampleBuffer.Enqueue(CurrentFramesPerSecond);
            if(_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(avg => avg);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }
            TotalFrames++;
            TotalSeconds += deltaTime;
            return true;
        }
    }
}
