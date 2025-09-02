namespace hVSm;

public class Wolf : Monster
{
    public Wolf() : base()
    {
        Loot.Add("Leather", Dice.RollDice(4));
    }

    public override string ToString()
    {
        return "🐺";
    }

}
