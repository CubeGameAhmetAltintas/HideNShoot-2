using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelController : ControllerBaseModel
{
    public static LevelController Controller;
    public List<Level> Levels;
    public PathModel ActivePath;
    [SerializeField] RoadController roadController;
    [SerializeField] EnvironmentController environmentController;
    [SerializeField] PlayerController playerController;
    [SerializeField] Tool.Mesh.MeshModel roadMesh;
    Level loadedLevel;


    public override void Initialize()
    {
        base.Initialize();

        if (Controller == null)
        {
            Controller = this;
        }
        else
        {
            Destroy(Controller);
            Controller = this;
        }

        loadLevel();
    }

    private void loadLevel()
    {
        PlayerDataModel.Data.LevelIndex = PlayerDataModel.Data.LevelIndex >= Levels.Count ? 0 : PlayerDataModel.Data.LevelIndex;
        loadedLevel = Levels[PlayerDataModel.Data.LevelIndex];
        ActivePath = loadedLevel.Path;
        roadController.LoadLevel(loadedLevel);
        playerController.Init(loadedLevel); //??
        environmentController.LoadVisaulEnvironment();
    }

    public void NextLevel()
    {
        PlayerDataModel.Data.Level++;
        PlayerDataModel.Data.LevelIndex = PlayerDataModel.Data.LevelIndex + 1 < Levels.Count ? PlayerDataModel.Data.LevelIndex + 1 : 0;

        if (PlayerDataModel.Data.Level % 2 == 0)
        {
            environmentController.IncreaseEnvironmentId();
        }

        PlayerDataModel.Data.Save();
    }

    [EditorButton]
    public void E_SaveLevel()
    {
#if UNITY_EDITOR
        Level level = getActiveLevel();

        var path = EditorUtility.SaveFilePanel("Save Level", "Assets", "", "asset");
        if (path.Length > 0)
        {
            AssetDatabase.CreateAsset(level, path.Remove(0, path.IndexOf("Assets")));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        loadedLevel = null;
#endif

    }


    [EditorButton]
    public void CopyLevels()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            Level newLevel = ScriptableObject.CreateInstance<Level>();
            newLevel.Path = Levels[i].Path;
            newLevel.RoadDatas = Levels[i].RoadDatas;

#if UNITY_EDITOR
            AssetDatabase.CreateAsset(newLevel, "Assets/GameAssets/Levels/NewLevels/Level_" + i + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }
    }


    private Level getActiveLevel()
    {
        Level levelData = ScriptableObject.CreateInstance<Level>();
        RoadModel[] roads = FindObjectsOfType<RoadModel>();

        if (roads.Length == 0)
        {
            return null;
        }
        roads = roads.OrderBy(x => x.transform.position.z).ToArray();

        levelData.RoadDatas = new RoadDataModel[roads.Length];
        for (int i = 0; i < roads.Length; i++)
        {
            levelData.RoadDatas[i] = roads[i].GetDataModel();
            roads[i].SetDeactive();
        }

        return levelData;
    }

    [EditorButton]
    public void E_LoadLevel(Level level)
    {
        RoadController roadController = FindObjectOfType<RoadController>();
        roadController.E_LoadLevel(level);

        loadedLevel = level;
    }

    [EditorButton]
    public void E_SetRoad(int roadCount)
    {
        RoadController roadController = FindObjectOfType<RoadController>();
        roadController.E_SetRoad(roadCount);
    }
   

    [EditorButton]
    public void E_DeactiveItems()
    {
        ObstacleModel[] obstacles = FindObjectsOfType<ObstacleModel>();
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetDeactive();
        }


        MoneyModel[] moneys = FindObjectsOfType<MoneyModel>();
        for (int i = 0; i < moneys.Length; i++)
        {
            moneys[i].SetDeactive();
        }
    }


    [EditorButton]
    public void E_ResetLevel()
    {

        RoadModel[] roads = FindObjectsOfType<RoadModel>();

        if (roads.Length == 0)
        {
            return;
        }
        roads = roads.OrderBy(x => x.transform.position.z).ToArray();

        for (int i = 0; i < roads.Length; i++)
        {
            roads[i].SetDeactive();
        }

        ObstacleModel[] collectables = FindObjectsOfType<ObstacleModel>();
        for (int i = 0; i < collectables.Length; i++)
        {
            collectables[i].SetDeactive();
        }

        MoneyModel[] moneys = FindObjectsOfType<MoneyModel>();
        for (int i = 0; i < moneys.Length; i++)
        {
            moneys[i].SetDeactive();
        }
        roadMesh.Clear();
    }
}