using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controlls the ingame UI.
/// </summary>
public class IngameUIManager : MonoBehaviour
{
    // The slider image that shows how much happiness the village has.
    [SerializeField] private Image _HappinessBar;
    // The smiley face image that has to change.
    [SerializeField] private Image _HappinessFace;

    // Array of all the possible images to display emotion of the current happiness level.
    [SerializeField] private Sprite[] _EmotionSprites = null;

    // Current happiness level.
    private int _HappinessLevel = 0;

    private void Awake()
    {
        ResourceManager.OnHappinessChanged += CheckHappinessEmote;
    }

    private void Start()
    {
        CheckHappinessEmote(ResourceManager.GetResourceAmount(ResourceType.happiness));
    }

    private void FixedUpdate()
    {
        UpdateHappinessUI(ResourceManager.GetResourceAmount(ResourceType.happiness));
    }

    // This function adjusts the slider according to the amount of happiness.
    private void UpdateHappinessUI(float happiness)
    {
        float fillAmount = _HappinessBar.fillAmount;
        _HappinessBar.fillAmount = Mathf.Lerp(fillAmount, (happiness / 100), Time.deltaTime * 1.4f);
    }

    // This function checks if the happiness emote has to change.
    private void CheckHappinessEmote(float currentHappiness)
    {
        if (currentHappiness < 25)
        {
            if(_HappinessLevel != 1)
            {
                _HappinessFace.sprite = _EmotionSprites[0];

                _HappinessLevel = 1;
            }
        }
        else if (currentHappiness >= 25 && currentHappiness < 50)
        {
            if (_HappinessLevel != 2)
            {
                _HappinessFace.sprite = _EmotionSprites[1];

                _HappinessLevel = 2;
            }
        }
        else if (currentHappiness >= 50 && currentHappiness < 75)
        {
            if (_HappinessLevel != 3)
            {
                _HappinessFace.sprite = _EmotionSprites[2];

                _HappinessLevel = 3;
            }
        }
        else if(currentHappiness >= 75)
        {
            if (_HappinessLevel != 4)
            {
                _HappinessFace.sprite = _EmotionSprites[3];

                _HappinessLevel = 4;
            }
        }
    }
}
