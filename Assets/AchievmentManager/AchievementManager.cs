using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Main class that contains all achievements that the game offers
/// All achievements run async in case you need to plug them into any services like Steam SDK
/// If you're using this for client side achievements they work fine and save to player prefs.
/// </summary>
public class AchievementManager : MonoBehaviour
{
    public List<IAchievement> Achievements => m_achievements;
    private List<IAchievement> m_achievements = new List<IAchievement>();

    [SerializeField]
    private AchievementDatabase m_database;
    
    public void Awake()
    {
        for (int i = 0; i < m_database.Data.Count; i++)
        {
            IAchievement achievement = new FakeAchievement(m_database.Data[i]);
            m_achievements.Add(achievement);
            Task.Run(achievement.Refresh);
        }
    }

    public IAchievement GetAchievement(string id)
    {
        for (int i = 0; i < m_achievements.Count; i++)
        {
            IAchievement achievement = m_achievements[i];
            if (achievement.Definition.ID == id)
            {
                return achievement;
            }
        }
        
        Debug.Log("Achievement not found: " + id);
        return null;
    }
}
