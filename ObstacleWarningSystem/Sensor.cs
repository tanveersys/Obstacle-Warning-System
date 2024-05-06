namespace ObstacleWarningSystem
{
    public class Sensor : IObstacle
    {
        private bool isActive;
        public int X { get; }
        public int Y { get; }
        public Sensor(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.isActive = true;
        }

        public bool IsActive()
        {
            return isActive;
        }

        public bool IsObstacleAt(int x, int y)
        {
            return this.X == x && this.Y == y && isActive;
        }
    }
}
