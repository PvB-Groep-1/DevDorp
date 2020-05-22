using UnityEngine;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{
    [SerializeField] private Image _HappinessBar;
    [SerializeField] private Image _HappinessFace;

    [SerializeField] private Sprite[] _EmotionSprites = null;

    [SerializeField] private ParticleSystem _SadParticles, _HappyParticles;

    private int _HappinessLevel = 0;

    private void Awake()
    {
        ResourceManager.OnHappinessChanged += CheckHappinessEmote;
    }

    private void Start()
    {
        CheckHappinessEmote(ResourceManager.GetResourceAmount());
    }

    private void FixedUpdate()
    {
        UpdateHappinessUI(ResourceManager.GetResourceAmount());

        if (Input.GetKeyDown(KeyCode.J))
        {
            ResourceManager.DecreaseHappiness(30);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            ResourceManager.IncreaseHappiness(30);
        }
    }

    private void UpdateHappinessUI(float happiness)
    {
        float fillAmount = _HappinessBar.fillAmount;
        _HappinessBar.fillAmount = Mathf.Lerp(fillAmount, (happiness / 100), Time.deltaTime * 1.4f);
    }

    private void CheckHappinessEmote(float currentHappiness)
    {
        if (currentHappiness < 25)
        {
            if(_HappinessLevel != 1)
            {
                _SadParticles.Play();

                _HappinessFace.sprite = _EmotionSprites[0];

                _HappinessLevel = 1;
            }
        }
        else if (currentHappiness >= 25 && currentHappiness < 50)
        {
            if (_HappinessLevel != 2)
            {
                if (_HappinessLevel < 2)
                    _HappyParticles.Play();
                else if (_HappinessLevel > 2)
                    _SadParticles.Play();

                _HappinessFace.sprite = _EmotionSprites[1];

                _HappinessLevel = 2;
            }
        }
        else if (currentHappiness >= 50 && currentHappiness < 75)
        {
            if (_HappinessLevel != 3)
            {
                if (_HappinessLevel < 3)
                    _HappyParticles.Play();
                else if (_HappinessLevel > 3)
                    _SadParticles.Play();

                _HappinessFace.sprite = _EmotionSprites[2];

                _HappinessLevel = 3;
            }
        }
        else if(currentHappiness >= 75)
        {
            if (_HappinessLevel != 4)
            {
                _HappyParticles.Play();

                _HappinessFace.sprite = _EmotionSprites[3];

                _HappinessLevel = 4;
            }
        }
    }
}
