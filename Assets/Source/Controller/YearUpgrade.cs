[System.Serializable]
public class YearUpgrade : UpgradeModel
{
    public int StartYear;
    public int CurrentYear
    {
        get
        {
            return StartYear + Level;
        }
    }

    public override void Initialize(int level)
    {
        base.Initialize(level);
    }

    public override void Upgrade()
    {
        base.Upgrade();

        UpdatePrice();
        Level++;
        //PlayerDataModel.Data.YearLevel = Level;
        PlayerDataModel.Data.Save();
    }
}
