using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHudUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image healthFillImage;

    public void UpdateHealth(float curr, float max)
    {
        healthFillImage.fillAmount = curr / max;
    }
}
