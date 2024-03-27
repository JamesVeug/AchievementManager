using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAchievementUIController : MonoBehaviour
{
    [SerializeField]
    private FakeAchievementUI m_achievementPrefab;
    
    private List<FakeAchievementUI> m_activeUIPool = new List<FakeAchievementUI>();
    private List<FakeAchievementUI> m_pool = new List<FakeAchievementUI>();
    
    private void Awake()
    {
        m_achievementPrefab.gameObject.SetActive(false);
        FakeAchievement.OnAchievementAchieved += OnAchievementAchieved;
    }
    
    private void OnDestroy()
    {
        FakeAchievement.OnAchievementAchieved -= OnAchievementAchieved;
    }

    private void OnAchievementAchieved(FakeAchievement obj)
    {
        FakeAchievementUI achievementUI = null;
        if (m_pool.Count > 0)
        {
            achievementUI = m_pool[0];
            m_pool.RemoveAt(0);
        }
        else
        {
            achievementUI = Instantiate(m_achievementPrefab, m_achievementPrefab.transform.parent);
            achievementUI.OnAnimationDone += OnAnimationDone;
        }
        
        m_activeUIPool.Add(achievementUI);
        achievementUI.Initialize(obj, m_activeUIPool.Count);
    }

    private void OnAnimationDone(FakeAchievementUI obj)
    {
        obj.gameObject.SetActive(false);
        m_activeUIPool.Remove(obj);
        m_pool.Add(obj);
    }
}
