using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleScene : MonoBehaviour
{
    public AchievementManager AchievementManager;

    public Button UnlockButton;
    public Button LockButton;
    public TMP_Text Text;

    public RectTransform Container;
    
    public void Start()
    {
        UnlockButton.gameObject.SetActive(false);
        LockButton.gameObject.SetActive(false);
        
        foreach (IAchievement achievement in AchievementManager.Achievements)
        {
            Button unlockButton = Instantiate(UnlockButton, Container);
            unlockButton.gameObject.SetActive(true);
            unlockButton.onClick.AddListener(() => UnlockAchievement(achievement));
            unlockButton.GetComponentInChildren<TMP_Text>().text = "Unlock " + achievement.Definition.Name;
            
            Button lockButton = Instantiate(LockButton, Container);
            lockButton.gameObject.SetActive(true);
            lockButton.onClick.AddListener(() => LockAchievement(achievement));
            lockButton.GetComponentInChildren<TMP_Text>().text = "Reset " + achievement.Definition.Name;
        }
    }

    private void Update()
    {
        string text = "Achievements: " + AchievementManager.Achievements.Count;
        foreach (IAchievement achievement in AchievementManager.Achievements)
        {
            text += "\n- " + achievement.Definition.Name + " - " + (achievement.Achieved ? "Achieved" : "Not Achieved");
        }
        Text.text = text;
    }

    private void LockAchievement(IAchievement achievement)
    {
        achievement.ResetAchievement();
    }

    private void UnlockAchievement(IAchievement achievement)
    {
        achievement.MarkAsAchieved();
    }
}
