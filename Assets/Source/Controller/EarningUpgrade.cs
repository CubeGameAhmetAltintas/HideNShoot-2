[System.Serializable]
public class EarningUpgrade : UpgradeModel
{
    public int StartEarning;
    public int CurrentEarning
    {
        get
        {
            return StartEarning + (Level * 2);
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
        PlayerDataModel.Data.EarningLevel = Level;
        PlayerDataModel.Data.Save();
    }

    public int GetEarning(int passedScoreView)
    {
        return CurrentEarning * passedScoreView;
    }
}