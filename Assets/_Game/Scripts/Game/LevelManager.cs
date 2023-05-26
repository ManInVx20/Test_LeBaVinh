using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private Level level;
    private int levelIndex;

    private void Awake()
    {
        levelIndex = GameDataManager.Instance.GetGameData().LevelIndex;
    }

    public void NextLevel()
    {
        if (levelIndex < ResourceManager.Instance.LevelPrefabArray.Length)
        {
            levelIndex += 1;

            GameDataManager.Instance.GetGameData().LevelIndex = levelIndex;

            GameDataManager.Instance.WriteFile();
        }
    }

    public int GetCurrentLevel()
    {
        return levelIndex + 1;
    }

    public void LoadLevel()
    {
        if (level != null)
        {
            level.Despawn();
        }

        level = Instantiate(ResourceManager.Instance.LevelPrefabArray[levelIndex]);
    }

    public void UnloadLevel()
    {
        if (level != null)
        {
            level.Despawn();
        }
    }
}