using System;

namespace Structures
{
    [Serializable]
    public class Layer
    {

    }

    [Serializable]
    public class Level
    {
        public int height;
        public int width;
        public Layer[] layers;
    }
}