namespace ObstacleWarningSystem
{
    public interface IObstacle
    {
        int X { get; }
        int Y { get; }
        bool IsObstacleAt(int x, int y);
    }
}
