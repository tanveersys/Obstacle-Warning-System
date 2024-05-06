namespace ObstacleWarningSystem
{
    public class Camera : IObstacle
    {
        public string direction { get; }
        private readonly int mapSize;

        public int X { get; }
        public int Y { get; }

        public Camera(string direction, int mapSize)
        {
            this.direction = direction;
            this.mapSize = mapSize;

            switch (direction)
            {
                case "north":
                    X = mapSize / 2;
                    Y = mapSize - 1;
                    break;
                case "south":
                    X = mapSize / 2;
                    Y = 0;
                    break;
                case "east":
                    X = 0;
                    Y = mapSize / 2;
                    break;
                case "west":
                    X = mapSize - 1;
                    Y = mapSize / 2;
                    break;
                default:
                    X = 0;
                    Y = 0;
                    break;
            }
        }

        public bool IsObstacleAt(int x, int y)
        {
            switch (direction)
            {
                case "north":
                    return y > 0;
                case "south":
                    return y < (mapSize - 1);
                case "east":
                    return x < (mapSize - 1);
                case "west":
                    return x > 0;
                default:
                    return false;
            }
        }
    }

}
