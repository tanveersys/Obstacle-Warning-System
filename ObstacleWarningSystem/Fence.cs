namespace ObstacleWarningSystem
{
    public class Fence : IObstacle
    {
        public string direction { get; }
        private readonly int mapSize;

        public int X { get; }
        public int Y { get; }

        public Fence(string direction, int mapSize)
        {
            this.direction = direction;
            this.mapSize = mapSize;

            switch (direction)
            {
                case "north":
                    X = mapSize / 2;
                    Y = 0;
                    break;
                case "east":
                    X = mapSize - 1;
                    Y = mapSize / 2;
                    break;
                default:
                    X = -1;
                    Y = -1;
                    break;
            }
        }

        public bool IsObstacleAt(int x, int y)
        {
            switch (direction)
            {
                case "north":
                    return y == 0;
                case "east":
                    return x == (mapSize - 1);
                default:
                    return false;
            }
        }
    }


}
