using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FakeAchievementUI : MonoBehaviour
{
    public event Action<FakeAchievementUI> OnAnimationDone = delegate { };
    
    public float AnimationDuration = 2f;
    public float WaitTime = 2f;
    
    public Image icon;
    public Text title;
    
    public void Initialize(FakeAchievement achievement, int index)
    {
        title.text = achievement.Definition.Name;
        icon.sprite = achievement.Definition.Icon;
        gameObject.SetActive(true);
        StartCoroutine(Animation(index));
    }

    private IEnumerator Animation(int index)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        Vector3 startPosition = Vector3.zero;
        Vector3 targetPosition = startPosition + Vector3.up * rectTransform.sizeDelta.y * index;

        // Reveal
        float timer = 0f;
        while (timer < AnimationDuration)
        {
            timer = Mathf.Clamp(timer + Time.deltaTime, 0, AnimationDuration);
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, timer / AnimationDuration);
            yield return null;
        }
        
        // Wait
        yield return new WaitForSeconds(WaitTime);
        
        // Hide
        while (timer > 0)
        {
            timer = Mathf.Clamp(timer - Time.deltaTime, 0, AnimationDuration);
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, timer / AnimationDuration);
            yield return null;
        }
        
        OnAnimationDone(this);
    }
}