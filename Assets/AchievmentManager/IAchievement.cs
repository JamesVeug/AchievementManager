using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IAchievement
{
    bool Achieved { get; }
    AchievementDefinition Definition { get; }
    Task MarkAsAchieved();
    Task Refresh();
    Task ResetAchievement();
}
