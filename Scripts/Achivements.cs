using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Achivement
{
    public string Name;
    public int Amount;
    public string Tipe;
}

public class Achivements : MonoBehaviour
{
    public Achivement[] achivements;

    public GameObject AchivementPrefab;
    public Transform Parent;

    void Start()
    {
        foreach (var item in achivements)
        {
            var Clone = Instantiate(AchivementPrefab, Parent);

            Clone.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.Name;
            var Description = Clone.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            var ProgresText = Clone.transform.Find("ProgresText").GetComponent<TextMeshProUGUI>();
            var Progres = Clone.transform.Find("Progres").GetComponent<Slider>();
            var Сompleted = Clone.transform.Find("Completed");

            if (item.Tipe == "Coins")
            {
                Description.text = "Накопить " + item.Amount + " за всю игру";
                if (GameManager.gameManager.Total_coins >= item.Amount)
                {
                    Clone.transform.Find("Progres").gameObject.SetActive(false);
                    Сompleted.gameObject.SetActive(true); 
                    ProgresText.gameObject.SetActive(false);
                }
                else
                {
                    Progres.maxValue = item.Amount;
                    Progres.value = GameManager.gameManager.Total_coins;
                    ProgresText.text = GameManager.gameManager.Total_coins.ToString();
                }
            }
        }
    }
}