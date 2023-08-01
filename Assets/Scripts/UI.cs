using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI terrifiedCountText;
    public TextMeshProUGUI shakesCountText;
    public TextMeshProUGUI levelText;
    public Slider slider;
    private int terrifiedCount;
    private int shakesCount;
    private int level;

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
    }

    public void UpdateSlider()
    {
        slider.value = terrifiedCount;
    }
}
