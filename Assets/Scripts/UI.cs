using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI terrifiedCountText;
    public TextMeshProUGUI shakesCountText;
    public TextMeshProUGUI levelText;
    public int shakesCount = 0;
    public Slider slider;
    public GameObject shopPanel;
    public Button[] hatShopButtons;
    public GameObject[] hats;
    private int terrifiedCount; 
    private int level = 0;

    private void Start()
    {
        for(int i = 0; i < hatShopButtons.Length; i++)
        {
            hatShopButtons[i].interactable = false;
        }
    }

    public void UpdateTerrifiedCountText()
    {
        terrifiedCount++;
        terrifiedCountText.text = terrifiedCount.ToString();
    }

    public void UpdateShakesCountText()
    {
        shakesCount++;
        shakesCountText.text = shakesCount.ToString();
    }

    public void UpdateLevelText()
    {
        level++;
        levelText.text = level.ToString();
        slider.maxValue += 50;
    }

    public void UpdateSlider()
    {
        slider.value = terrifiedCount;
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        for (int i = 0; i < hatShopButtons.Length; i++)
        {
            hatShopButtons[i].interactable = ((shakesCount / 10 >= i + 1) && (level > i)) || (hats[i].GetComponent<Hat>().isPurchased);
        }
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    private void Update()
    {
        if(slider.value == slider.maxValue)
        {
            UpdateLevelText();
            terrifiedCount = 0;
            slider.value = 0;
        }
    }
}
