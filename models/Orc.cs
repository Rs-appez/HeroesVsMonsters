namespace hVSm;

public class Orc : Monster
{
    public Orc() : base(BonusStrength: 1)
    {
        Loot.Add("Gold", Dice.RollDice(6));
    }

    public override string ToString()
    {
        return "🧌";
    }

}
