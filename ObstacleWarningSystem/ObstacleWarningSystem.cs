namespace ObstacleWarningSystem
{
    public class ObstacleWarningSystem
    {
        private readonly List<IObstacle> obstacles;
        private readonly int mapSize;

        public ObstacleWarningSystem(int mapSize)
        {
            this.obstacles = new List<IObstacle>();
            this.mapSize = mapSize;
        }
        public void ProcessCommand(string input)
        {
            string[] parts = input.Split(' ');
            string command = parts[0];

            switch (command.ToLower())
            {
                case "add":
                    AddObstacle(parts);
                    break;
                case "check":
                    CheckSafety(parts);
                    break;
                case "display":
                    DisplayObstacles();
                    break;
                case "help":
                    PrintHelp();
                    break;
                case "exit":
                    Exit();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
        private void AddObstacle(string[] parts)
        {
            if (parts.Length < 4)
            {
                Console.WriteLine("Invalid command syntax.");
                return;
            }

            int x, y;
            if (!int.TryParse(parts[2], out x) || !int.TryParse(parts[3], out y))
            {
                Console.WriteLine("Invalid coordinates.");
                return;
            }

            switch (parts[1])
            {
                case "guard":
                    if (parts.Length == 4 && int.TryParse(parts[2], out int xPos) && int.TryParse(parts[3], out int yPos))
                    {
                        if (IsValidLocation(xPos, yPos))
                        {
                            obstacles.Add(new Guard(xPos, yPos));
                            Console.WriteLine("Successfully added guard obstacle.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid location. Please enter coordinates within map boundaries.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid guard command format. Use: add guard x y");
                    }
                    break;
                case "fence":
                    if (parts.Length == 4 && (parts[2] == "north" || parts[2] == "east"))
                    {
                        obstacles.Add(new Fence(parts[2], mapSize));
                        Console.WriteLine("Successfully added fence obstacle.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid fence command format. Use: add fence north|east");
                    }
                    break;
                case "sensor":
                    Console.WriteLine("Sensor functionality not yet implemented.");
                    break;
                case "camera":
                    if (parts.Length == 4 && (parts[2] == "north" || parts[2] == "south" || parts[2] == "east" || parts[2] == "west"))
                    {
                        obstacles.Add(new Camera(parts[2], mapSize));
                        Console.WriteLine("Successfully added camera obstacle.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid camera command format. Use: add camera north|south|east|west");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid obstacle type.");
                    break;
            }
        }
        private void CheckSafety(string[] parts)
        {
            if (parts.Length < 3)
            {
                Console.WriteLine("Invalid command syntax.");
                return;
            }

            int x, y;
            if (!int.TryParse(parts[1], out x) || !int.TryParse(parts[2], out y))
            {
                Console.WriteLine("Invalid coordinates.");
                return;
            }

            CheckLocation(x, y);
        }
        private void CheckLocation(int x, int y)
        {
            if (IsValidLocation(x, y))
            {
                List<string> safeDirections = new List<string>();
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx != 0 || dy != 0)
                        {
                            int newX = x + dx;
                            int newY = y + dy;
                            if (IsValidLocation(newX, newY) && IsSafeLocation(newX, newY))
                            {
                                switch (dx)
                                {
                                    case -1:
                                        safeDirections.Add("West");
                                        break;
                                    case 1:
                                        safeDirections.Add("East");
                                        break;
                                }
                                switch (dy)
                                {
                                    case -1:
                                        safeDirections.Add("North");
                                        break;
                                    case 1:
                                        safeDirections.Add("South");
                                        break;
                                }
                            }
                        }
                    }
                }

                if (safeDirections.Count > 0)
                {
                    Console.WriteLine("You can safely take any of the following directions:");
                    Console.WriteLine(string.Join(", ", safeDirections));
                }
                else
                {
                    Console.WriteLine("This location is not safe. No safe directions found.");
                }
            }
        }
        private bool IsSafeLocation(int x, int y)
        {
            return IsValidLocation(x, y) && obstacles.All(o => !o.IsObstacleAt(x, y));
        }
        private bool IsValidLocation(int x, int y)
        {
            return x >= 0 && x < mapSize && y >= 0 && y < mapSize;
        }
        public void DisplayObstacles()
        {
            Console.WriteLine("Valid obstacles:");
            foreach (var obstacle in obstacles)
            {
                if (obstacle is Guard)
                    Console.WriteLine($"Guard at ({obstacle.X}, {obstacle.Y})");
                else if (obstacle is Fence)
                    Console.WriteLine($"Fence at ({obstacle.X}, {obstacle.Y}) with orientation {((Fence)obstacle).direction}");
                else if (obstacle is Sensor)
                    Console.WriteLine($"Sensor at ({obstacle.X}, {obstacle.Y})");
                else if (obstacle is Camera)
                    Console.WriteLine($"Camera at ({obstacle.X}, {obstacle.Y}) with direction {((Camera)obstacle).direction}");
            }
        }
        private void PrintHelp()
        {
            Console.WriteLine("Valid commands are: add guard, add fence, add sensor, add camera, check, display, help, exit");
        }
        private void Exit()
        {
            Console.WriteLine("Thank you for using the Obstacle Warning System.");
            Environment.Exit(0);
        }
    }
}
