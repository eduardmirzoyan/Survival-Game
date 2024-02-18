using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHudUI : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;

    [SerializeField] private TextMeshProUGUI enemyCountLabel;
    [SerializeField] private TextMeshProUGUI killGoalLabel;

    public void UpdateHealth(float curr, float max)
    {
        healthFillImage.fillAmount = curr / max;
    }

    public void UpdateEnemyCount(int count, int cap)
    {
        enemyCountLabel.text = $"Enemies\n{count}/{cap}";
    }

    public void UpdateKillGoalLabel(int kills, int goal)
    {
        killGoalLabel.text = $"Enemies\n{kills}/{goal}";
    }
}
