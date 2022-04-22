using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/CreateLevel", order = 1)]
public class Level : ScriptableObject
{
    public PathModel Path;
    public RoadDataModel[] RoadDatas;
    public WorldItemDataModel[] EnviromentDatas;
}

