using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackTimerUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image timerFillImage;

    public void UpdateValue(float cooldown, float duration)
    {
        timerFillImage.fillAmount = 1 - cooldown / duration;
    }

    public void SetVisibilty(bool show)
    {
        canvasGroup.alpha = show ? 1f : 0f;
    }
}
