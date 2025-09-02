namespace hVSm;

public abstract class Monster : Entity, ILootable
{

    public Dictionary<string, int> Loot { get; protected set; } = new();


    public Monster(int BonusStamina = 0, int BonusStrength = 0) : base(BonusStamina, BonusStrength)
    {
    }



}
