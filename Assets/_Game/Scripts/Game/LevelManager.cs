using System;
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

    public void LoadLevel(Action onLoadStarted = null, Action<Level> onLoadCompleted = null)
    {
        UnloadLevel();

        onLoadStarted?.Invoke();

        StartCoroutine(LoadLevelCoroutine(1.0f, onLoadCompleted));
    }

    public void UnloadLevel()
    {
        if (level != null)
        {
            level.Despawn();
        }
    }

    public void ResetLevelIndex()
    {
        levelIndex = 0;
    }

    private IEnumerator LoadLevelCoroutine(float loadingTime, Action<Level> onLoadCompleted)
    {
        level = Instantiate(ResourceManager.Instance.LevelPrefabArray[levelIndex]);

        yield return new WaitForSeconds(loadingTime);

        onLoadCompleted?.Invoke(level);
    }
}