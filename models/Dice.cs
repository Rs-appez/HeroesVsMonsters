
static class Dice
{
    private static Random _random = new Random();

    public static List<int> RollDices(int numberOfDice = 1, int sidesPerDie = 6)
    {
        List<int> rolls = new List<int>();
        for (int i = 0; i < numberOfDice; i++)
        {
            rolls.Add(_random.Next(1, sidesPerDie + 1));
        }
        return rolls;
    }

    public static int RollDice(int sidesPerDie = 6)
    {
        return _random.Next(1, sidesPerDie + 1);
    }
}
