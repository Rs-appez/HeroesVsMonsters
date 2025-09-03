namespace hVSm;

class Program
{

    public static void Main(string[] args)
    {
        Console.Clear();
        Hero? hero;
        do
        {
            Console.WriteLine("Choose your hero:");
            Console.WriteLine("1. Human (Bonus: + 1 Strength | + 1 Stamina)");
            Console.WriteLine("2. Dwarf (Bonus: + 2 Stamina)");
            ConsoleKeyInfo key = Console.ReadKey(true);
            Console.Clear();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    hero = new Human();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    hero = new Dwarf();
                    break;
                default:
                    Console.WriteLine("Invalid choice, please select again.");
                    hero = null;
                    break;
            }

        } while (hero is null);

        Game game = new Game(hero);

        Console.WriteLine("Welcome to the game! Use arrow keys or ZQSD (or HJKL) to move. Press Esc, Ctrl+Q or X to quit.");
        game.ShowMap();

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.Clear();
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.Z:
                case ConsoleKey.K:
                    game.MoveHero(MoveDirection.Up);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                case ConsoleKey.J:
                    game.MoveHero(MoveDirection.Down);
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.Q when !keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control):
                case ConsoleKey.H:
                    game.MoveHero(MoveDirection.Left);
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                case ConsoleKey.L:
                    game.MoveHero(MoveDirection.Right);
                    break;
                case ConsoleKey.Escape:
                case ConsoleKey.X:
                case ConsoleKey.Q when keyInfo.Modifiers.HasFlag(ConsoleModifiers.Control):
                    return;
            }
            game.ShowMap();

        }
    }
}
