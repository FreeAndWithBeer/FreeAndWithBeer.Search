﻿namespace SoftwareBotany.Sunlight
{
    public partial class Vector
    {
        public IVectorStatistics GetStatistics() { return new Statistics(this); }

        private class Statistics : IVectorStatistics
        {
            internal Statistics(Vector vector)
            {
                _wordCount = vector._wordCountPhysical;

                bool lastWordCompressed = false;

                for (int i = 0; i < vector._wordCountPhysical; i++)
                {
                    Word word = vector._words[i];

                    if (!word.IsCompressed && lastWordCompressed && i < vector._wordCountPhysical - 1)
                    {
                        if (word.Population == 1)
                            _oneBitPackableWordCount++;
                        else if (word.Population == 2)
                            _twoBitPackableWordCount++;
                    }

                    lastWordCompressed = word.IsCompressed;
                }
            }

            private readonly int _wordCount;
            public int WordCount { get { return _wordCount; } }

            private readonly int _oneBitPackableWordCount;
            public int OneBitPackableWordCount { get { return _oneBitPackableWordCount; } }

            private readonly int _twoBitPackableWordCount;
            public int TwoBitPackableWordCount { get { return _twoBitPackableWordCount; } }
        }
    }

    public interface IVectorStatistics
    {
        int WordCount { get; }
        int OneBitPackableWordCount { get; }
        int TwoBitPackableWordCount { get; }
    }
}