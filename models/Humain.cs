namespace hVSm;

public class Human : Hero
{
    public Human() : base(BonusStamina: 1, BonusStrength: 1)
    {
    }

    public override string ToString()
    {
        return "🧍";
    }

}
