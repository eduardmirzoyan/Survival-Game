using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackTimerUI : MonoBehaviour
{
    [SerializeField] private Image timerFillImage;

    public void UpdateTimer(float cooldown, float duration)
    {
        timerFillImage.fillAmount = 1 - cooldown / duration;
    }
}
