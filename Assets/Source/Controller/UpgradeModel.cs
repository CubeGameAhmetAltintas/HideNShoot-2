[System.Serializable]
public class UpgradeModel
{
    public int Level;
    public int StartPrice;
    public int AddPricePerLevel;
    public int Price;

    public virtual void Initialize(int level)
    {
        Level = level;
        Price = StartPrice;
        AddPricePerLevel = 400;
        for (int i = 0; i < level; i++)
        {
            UpdatePrice();
        }
    }

    public virtual void Upgrade()
    {

    }

    protected virtual void UpdatePrice()
    {
        Price += AddPricePerLevel;
        AddPricePerLevel = 500;
    }
}
