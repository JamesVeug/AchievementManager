using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// An example class to simulate an achievement.
/// Ideally you would inherit IAchievement and plug in the correct services (SteamSDK) to unlock achievements in the cloud.
/// </summary>
public class FakeAchievement : IAchievement
{
    public static event Action<FakeAchievement> OnAchievementAchieved = delegate { };
    
    public bool Achieved { get; private set; }
    public AchievementDefinition Definition { get; }
    
    public FakeAchievement(AchievementDefinition definition)
    {
        Definition = definition;
    }
    
    public async Task MarkAsAchieved()
    {
        if (Achieved)
        {
            return;
        }
        
        Achieved = true;

        await Task.Yield();
        PlayerPrefs.SetInt(Definition.ID, 1); // Move this to your own save system

        OnAchievementAchieved?.Invoke(this);
        Debug.Log("Flagged Achievement " + Definition.ID + " as achieved: " + Achieved);
    }

    public async Task Refresh()
    {
        await Task.Yield();

        Achieved = PlayerPrefs.GetInt(Definition.ID, 0) == 1; // Move this to your own save system
        Debug.Log("Updated Achievement " + Definition.ID + " done: " + Achieved);
    }

    public async Task ResetAchievement()
    {
        await Task.Yield();
        
        Achieved = false;
        PlayerPrefs.SetInt(Definition.ID, 0); // Move this to your own save system

        Debug.Log("Flagged Achievement " + Definition.ID + " as not achieved: " + Achieved);
    }
}