namespace hVSm;

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}

public class Game
{
    private static Random _random = new Random();
    private IGameElement?[,] map = new IGameElement[15, 15];
    private Hero _hero;

    private List<Type> _monsterTypes = new List<Type> { typeof(Wolf), typeof(Orc), typeof(Dragonnet) };
    private Dictionary<(int x, int y), Monster> _monsters = new();

    public Game(Hero hero)
    {
        _hero = hero;
        InitMap();
    }

    public void ShowMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == null)
                {
                    Console.Write("    ");
                }
                else
                {
                    Console.Write($" {map[i, j]} ");
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine();
        _hero.ShowInventory();
    }


    public void MoveHero(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Up:
                if (_hero.Position.x <= 0) Console.WriteLine("Can't move up, out of bounds!");
                else
                {
                    Console.WriteLine("Moving up");
                    map[_hero.Position.x, _hero.Position.y] = null;
                    _hero.Position = (_hero.Position.x - 1, _hero.Position.y);
                    map[_hero.Position.x, _hero.Position.y] = _hero;
                }
                break;
            case MoveDirection.Down:
                if (_hero.Position.x >= map.GetLength(0) - 1) Console.WriteLine("Can't move down, out of bounds!");
                else
                {
                    Console.WriteLine("Moving down");
                    map[_hero.Position.x, _hero.Position.y] = null;
                    _hero.Position = (_hero.Position.x + 1, _hero.Position.y);
                    map[_hero.Position.x, _hero.Position.y] = _hero;
                }
                break;
            case MoveDirection.Left:
                if (_hero.Position.y <= 0) Console.WriteLine("Can't move left, out of bounds!");
                else
                {
                    Console.WriteLine("Moving left");
                    map[_hero.Position.x, _hero.Position.y] = null;
                    _hero.Position = (_hero.Position.x, _hero.Position.y - 1);
                    map[_hero.Position.x, _hero.Position.y] = _hero;
                }
                break;
            case MoveDirection.Right:
                if (_hero.Position.y >= map.GetLength(1) - 1) Console.WriteLine("Can't move right, out of bounds!");
                else
                {
                    Console.WriteLine("Moving right");
                    map[_hero.Position.x, _hero.Position.y] = null;
                    _hero.Position = (_hero.Position.x, _hero.Position.y + 1);
                    map[_hero.Position.x, _hero.Position.y] = _hero;
                }
                break;
        }

        if (_monsters.ContainsKey(_hero.Position))
        {
            Figth();
        }
    }

    private void InitMap()
    {
        PlacePlant();
        PlaceHero();
        PlaceMonster();
    }

    private void PlacePlant()
    {
        Plant plant = new Plant();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = plant;
            }
        }
    }

    private void PlaceHero()
    {
        _hero.Position = (0, 0);
        map[_hero.Position.x, _hero.Position.y] = _hero;
    }
    private void PlaceMonster()
    {
        int numberOfMonsters = _random.Next(8, 15);
        for (int i = 0; i < numberOfMonsters; i++)
        {
            int typeOfMonster = _random.Next(0, 3);
            Monster monster = (Monster)Activator.CreateInstance(_monsterTypes[typeOfMonster])!;
            (int x, int y) position = (0, 0);
            do
            {
                (int x, int y) test_position = (_random.Next(1, map.GetLength(0)), _random.Next(1, map.GetLength(1)));
                if (_monsters.Count == 0) position = test_position;
                else
                {
                    foreach (var monster_pos in _monsters.Keys)
                    {
                        if (monster_pos.x <= test_position.x + 1 && monster_pos.x >= test_position.x - 1 &&
                            monster_pos.y <= test_position.y + 1 && monster_pos.y >= test_position.y - 1)
                        {
                            position = (0, 0);
                            break;
                        }
                        else
                        {
                            position = test_position;
                        }
                    }
                }
            } while (position.Equals((0, 0)));

            _monsters[position] = monster;

        }

    }

    private void Figth()
    {
        // Too hard to if the hero do not have the initiative
        // bool HeroTurn = _random.Next(0, 2) == 0 ? true : false;
        bool HeroTurn = true;
        while (_hero.IsAlive && _monsters[_hero.Position].IsAlive)
        {
            DisplayFight();
            if (HeroTurn)
            {
                Console.WriteLine("Your turn! Press any key to attack...");
                Console.ReadKey();
                DisplayFight(HeroAttack: true);
                System.Threading.Thread.Sleep(500);
                DisplayFight();
                _hero.Hit(_monsters[_hero.Position]);
                HeroTurn = false;

            }
            else
            {
                Console.WriteLine($"It's the {_monsters[_hero.Position].GetType().Name}'s turn! Press any key to continue...");
                Console.ReadKey();
                DisplayFight(MonsterAttack: true);
                System.Threading.Thread.Sleep(500);
                DisplayFight();
                _monsters[_hero.Position].Hit(_hero);
                HeroTurn = true;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        if (_hero.IsAlive)
        {
            Console.Clear();
            _hero.Rest();
            _monsters.Remove(_hero.Position);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("You have been defeated! Game Over.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }

    private void DisplayFight(bool HeroAttack = false, bool MonsterAttack = false)
    {
        Console.Clear();
        Console.WriteLine($"A fight starts between the Hero and a {_monsters[_hero.Position].GetType().Name}!");
        Console.WriteLine();
        Console.WriteLine();
        if (HeroAttack) Console.WriteLine($"       {_hero}");
        else if (MonsterAttack) Console.WriteLine($"                                   {_monsters[_hero.Position]}");
        else Console.WriteLine();
        if (HeroAttack) Console.WriteLine($"                     VS            {_monsters[_hero.Position]}");
        else if (MonsterAttack) Console.WriteLine($"       {_hero}            VS");
        else Console.WriteLine($"       {_hero}            VS            {_monsters[_hero.Position]}");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();

    }

    private void Checkwin()
    {
        if (_monsters.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Congratulations! You have defeated all the monsters!");
            Console.WriteLine("Here is your loot:");
            _hero.ShowInventory();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
