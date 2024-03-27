using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="AchievementDatabase", menuName = "AchievementDatabase")]
public class AchievementDatabase : ScriptableObject
{
    public List<AchievementDefinition> Data;
}

[Serializable]
public class AchievementDefinition
{
    public string ID;
    public string Name;
    public Sprite Icon;
    
    // public string SteamID; <--- helpful if you want to use steam achievements
}