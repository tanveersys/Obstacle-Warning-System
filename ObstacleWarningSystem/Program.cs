namespace ObstacleWarningSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ObstacleWarningSystem system = new ObstacleWarningSystem(3);

            while (true)
            {
                Console.WriteLine("Enter command: ");
                string input = Console.ReadLine();
                system.ProcessCommand(input);
            }
        }
    }
}