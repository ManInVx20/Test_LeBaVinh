using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private Level level;
    private int levelIndex;

    private void Awake()
    {
        levelIndex = 0;
    }

    public bool TryLoadNextLevel()
    {
        if (levelIndex + 1 < ResourceManager.Instance.LevelPrefabArray.Length)
        {
            levelIndex += 1;

            return true;
        }

        return false;
    }

    public int GetCurrentLevel()
    {
        return levelIndex + 1;
    }

    public void LoadLevel()
    {
        UnloadLevel();

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