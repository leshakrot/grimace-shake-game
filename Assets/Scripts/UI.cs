using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;

public class UI : MonoBehaviour
{
    public static UI instance;

    public TextMeshProUGUI terrifiedCountText;
    public TextMeshProUGUI shakesCountText;
    public TextMeshProUGUI levelText;
    public GameObject buttonsDescription;
    public GameObject escButtonDescription;
    public int shakesCount = 0;
    public Slider slider;
    public GameObject shopPanel;
    public Button[] hatShopButtons;
    public GameObject[] hats;
    public int terrifiedCount; 
    public int level = 0;

    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < hatShopButtons.Length; i++)
        {
            bool isPurchased = PlayerPrefs.GetInt("Hat_" + i + "_Purchased", 0) == 1;
            bool isDescriptionDeleted = PlayerPrefs.GetInt("Description_" + hats[i].name + "_Deleted", 0) == 1;
            hatShopButtons[i].interactable = (((shakesCount / 10 >= i + 1) && (level > i))) || isPurchased;

            if (isDescriptionDeleted)
            {
                hats[i].GetComponent<ShopDescription>().description.SetActive(false);
            }
            
        }
        terrifiedCount = PlayerPrefs.GetInt("terrified");
        shakesCount = PlayerPrefs.GetInt("shakes");
        level = PlayerPrefs.GetInt("level");
        UpdateTerrifiedCountText();
        UpdateShakesCountText();
        UpdateLevelText();
        UpdateSlider();
    }

    public void AddTerrifiedCount()
    {
        terrifiedCount++;
        PlayerPrefs.SetInt("terrified", terrifiedCount);
        UpdateTerrifiedCountText();
    }

    public void UpdateTerrifiedCountText()
    {
        terrifiedCountText.text = terrifiedCount.ToString();
    }

    public void AddShakesCount()
    {
        shakesCount++;
        PlayerPrefs.SetInt("shakes", shakesCount);
        UpdateShakesCountText();
    }

    public void UpdateShakesCountText()
    { 
        shakesCountText.text = shakesCount.ToString();
    }

    public void AddLevel()
    {
        level++;
        PlayerPrefs.SetInt("level", level);
        slider.maxValue += 50;
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {   
        levelText.text = level.ToString();
    }

    public void UpdateSlider()
    {
        slider.value = terrifiedCount;
    }

    public void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        shopPanel.SetActive(true);
        for (int i = 0; i < hatShopButtons.Length; i++)
        {
            bool isPurchased = PlayerPrefs.GetInt("Hat_" + i + "_Purchased", 0) == 1;
            hatShopButtons[i].interactable = (((shakesCount / 10 >= i + 1) && (level > i))) || isPurchased;
        }
    }

    public void CloseShop()
    {
        if (!YandexGame.EnvironmentData.isMobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        shopPanel.SetActive(false);
    }

    private void Update()
    {
        if(slider.value == slider.maxValue)
        {
            AddLevel();
            terrifiedCount = 0;
            slider.value = 0;
        }

        if (Input.GetKeyDown(KeyCode.V)) OpenShop();
        if (Input.GetKeyDown(KeyCode.Escape)) CloseShop();
    }

    public void GetData()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            buttonsDescription.SetActive(false);
            escButtonDescription.SetActive(false);
        }
    }
}
