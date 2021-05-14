using UnityEngine;

public class LevelUnlocker
{
    public void UnlockLevel(int levelIndex)
    {
        var currentUnlockedLevels = PlayerPrefs.GetInt("maxLevelReached", 0);
        if (currentUnlockedLevels < levelIndex)
        {
            PlayerPrefs.SetInt("maxLevelReached", levelIndex);
        }
    }

}
