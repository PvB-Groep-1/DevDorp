using UnityEngine;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{
    [SerializeField] private Image _HappinessBar;

    private void FixedUpdate()
    {
        UpdateHappinessUI(ResourceManager.TotalHappiness);
    }

    private void UpdateHappinessUI(float happiness)
    {
        float fillAmount = _HappinessBar.fillAmount;
        _HappinessBar.fillAmount = Mathf.Lerp(fillAmount, (happiness / 100), Time.deltaTime * 1.4f);
    }
}
