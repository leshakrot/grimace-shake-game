using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] hats;
    public UI ui;
    private Hat currentHat;
    private int currenthatIndex;

    public static Shop instance;

    private void Awake()
    {
        instance = this;
    }

    public void UnlockHat(int index)
    {
        for (int i = 0; i < hats.Length; i++)
        {
            if (i != index) hats[i].SetActive(false);
            if (i == index)
            {
                hats[i].SetActive(true);
                currentHat = hats[i].gameObject.GetComponent<Hat>();
                currenthatIndex = i;
                currentHat.isPurchased = PlayerPrefs.GetInt("Hat_" + i + "_Purchased", 0) == 1;
                if (!currentHat.isPurchased)
                {
                    ui.shakesCount -= (currenthatIndex + 1) * 10;
                    PlayerPrefs.SetInt("shakes", ui.shakesCount);
                    ui.shakesCountText.text = ui.shakesCount.ToString();
                    currentHat.isPurchased = true;
                }
                //hats[i].GetComponent<Hat>().isPurchased = true;       
                PlayerPrefs.SetInt("Hat_" + i + "_Purchased", currentHat.isPurchased ? 1 : 0);

            }
        }
        gameObject.SetActive(false);
    }

    public void DeleteDescription(GameObject description)
    {
        ShopDescription descComponent = description.GetComponent<ShopDescription>();

        if (descComponent != null)
        {
            descComponent.isDeleted = true;

            // Save the deletion status of the description
            PlayerPrefs.SetInt("Description_" + description.name + "_Deleted", descComponent.isDeleted ? 1 : 0);
        }

        Destroy(description);
    }
}
