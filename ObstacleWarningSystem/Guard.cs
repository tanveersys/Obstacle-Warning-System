namespace ObstacleWarningSystem
{
    public class Guard : IObstacle
    {
        public int X { get; }
        public int Y { get; }
        public Guard(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool IsObstacleAt(int x, int y)
        {
            return this.X == x && this.Y == y;
        }
    }
}
