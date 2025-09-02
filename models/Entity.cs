namespace hVSm;

public abstract class Entity : IGameElement
{
    public int Stamina { get; }
    private int _stamina_bonus;
    public int Strength { get; }
    private int _strength_bonus;
    private int _life;

    public bool IsAlive { get; private set; } = true;
    private int _currentLife;
    public (int x,int y) Position { get; set; }

    public Action<Entity>? OnDeath;

    public Entity(int BonusStamina = 0, int BonusStrength = 0)
    {
        _stamina_bonus = BonusStamina;
        _strength_bonus = BonusStrength;
        Stamina =  Dice.RollDices(6).OrderByDescending(x => x).Take(3).Sum();
        Strength =  Dice.RollDices(4).OrderByDescending(x => x).Take(3).Sum();
        _life = Stamina + Modifier(Stamina + _stamina_bonus);
        _currentLife = _life;
    }


    public virtual void Hit(Entity target)
    {

        int damage = Dice.RollDice(4) + Modifier(Strength);
        target.ReceiveDamage(damage);
    }

    public void ReceiveDamage(int damage)
    {
        _currentLife -= damage;
        Console.WriteLine($"{this.GetType().Name} received {damage} damage, remaining life: {_currentLife}");
        if (_currentLife <= 0)
        {
            Console.WriteLine($"{this.GetType().Name} has been defeated!");
            IsAlive = false;
            OnDeath?.Invoke(this);
        }
    }

    protected void RestetLife()
    {
        _currentLife = _life;
    }
    private int Modifier(int attribute)
    {
        switch (attribute)
        {
            case < 5:
                return -1;
            case < 10:
                return 0;
            case < 15:
                return 1;
            default:
                return 2;
        }
    }

}
