using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] hats;
    public UI ui;
    private Hat currentHat;
    private int currenthatIndex;

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
                if (!currentHat.isPurchased)
                {
                    ui.shakesCount -= (currenthatIndex + 1) * 10;
                    ui.shakesCountText.text = ui.shakesCount.ToString();
                }
                hats[i].GetComponent<Hat>().isPurchased = true;
                
            }
        }
        gameObject.SetActive(false);
    }

    public void DeleteDescription(GameObject description)
    {
        Destroy(description);
    }
}
