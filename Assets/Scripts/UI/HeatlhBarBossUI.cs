using UnityEngine;
using UnityEngine.UI;

public class HeatlhBarBossUI : MonoBehaviour
{
    [SerializeField] private Image _fillAmount;
    [SerializeField] private Text _nameBoss;

    private void OnEnable()
    {
        EventManager.TakeDamageBoss += ShowCurrentHealth;
    }

    private void OnDisable()
    {
        EventManager.TakeDamageBoss -= ShowCurrentHealth;
    }

    public void ShowNameBoss(string name)
    {
        _nameBoss.text = name;
    }


    private void ShowCurrentHealth(float _currentHealth)
    {
        _fillAmount.fillAmount = _currentHealth;
    }
}
