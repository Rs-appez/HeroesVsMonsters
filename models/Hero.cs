namespace hVSm;

public abstract class Hero : Entity
{
    public Dictionary<string, int> Inventory { get; private set; } = new();

    public Hero(int BonusStamina = 0, int BonusStrength = 0) : base(BonusStamina, BonusStrength)
    {
    }
    public override void Hit(Entity target)
    {
        if (target is Monster)
        {
            if (target.OnDeath == null || !target.OnDeath.GetInvocationList().Contains((Action<Entity>)Loot))
                target.OnDeath += Loot;
        }

        Console.WriteLine("Hero attacks!");
        base.Hit(target);

    }
    private void Loot(Entity entity)
    {
        if (entity is ILootable lootable)
        {
            Dictionary<string, int> loot = lootable.Loot;
            foreach (var item in loot)
            {
                if (item.Value <= 0) continue;

                if (Inventory.ContainsKey(item.Key))
                {
                    Inventory[item.Key] += item.Value;
                }
                else
                {
                    Inventory[item.Key] = item.Value;
                }

            }
            Console.WriteLine("Hero loots the following items:");
            foreach (var item in loot)
            {
                Console.WriteLine($"{item.Value} x {item.Key}");
            }
        }
    }

    public void Rest()
    {
        RestetLife();
        Console.WriteLine("Hero rests and restores life to full.");
    }

    public void ShowInventory()
    {
        Console.WriteLine("Hero's Inventory:");
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
            return;
        }
        foreach (var item in Inventory)
        {
            Console.WriteLine($"{item.Value} x {item.Key}");
        }
    }
}
