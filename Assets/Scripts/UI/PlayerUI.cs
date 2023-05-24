using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image experienceBar;

    private PlayerData playerData;

    private void Start()
    {
        playerData = PlayerData.Instance;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _levelText.text = playerData.GetCurrentLevel().ToString();
        int currentLevel = PlayerData.Instance.GetCurrentLevel();
        float currentExperience = playerData.GetCurrentExperience();
        float experienceNeeded = playerData.GetExperienceNeededForLevel(currentLevel);

        float fillAmount = currentExperience / experienceNeeded;
        if (fillAmount >= 1f) // ≈сли fillAmount превышает 1, установим его равным 1
        {
            fillAmount = 0f;
        }

        experienceBar.fillAmount = fillAmount;
    }

}
