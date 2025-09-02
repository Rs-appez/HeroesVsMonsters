namespace hVSm;

public class Dragonnet : Monster
{
    public Dragonnet() : base(BonusStamina: 1)
    {
        Loot.Add("Gold", Dice.RollDice(6));
        Loot.Add("Leather", Dice.RollDice(4));
    }

    public override string ToString()
    {
        return "🐉";
    }
}
