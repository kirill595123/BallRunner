using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Data
{
    public string Name;
    public string Description;
    public int Price;
    public int BasePrice;
    public int Max;      
    public int Count;   
    public string id;    
    public int ValuePerLevel;
}

public class Upgrade : MonoBehaviour
{
    public GameObject UpgradePrefub;
    public List<Data> UpdateData = new List<Data>();
    public GameObject Parent;
    public AudioClip Error;
    public AudioClip Up;

    void Start()
    {
        LoadFromGameManager();

        foreach (var item in UpdateData)
        {
            item.BasePrice = item.Price;
            item.Price = CalculatePrice(item.BasePrice, item.Count);

            GameObject clone = Instantiate(UpgradePrefub, Vector2.zero, Quaternion.identity);
            clone.transform.SetParent(Parent.transform, false);

            var nameText  = clone.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var priceText = clone.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            var descText  = clone.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            var maxText   = clone.transform.Find("Max").GetComponent<TextMeshProUGUI>();
            var button    = clone.transform.Find("Button").GetComponent<Button>();
            var buttonTxt = button.GetComponentInChildren<TextMeshProUGUI>();

            nameText.text  = item.Name;
            priceText.text = item.Price.ToString();
            descText.text  = item.Description;
            maxText.text   = item.Count + "/" + item.Max;

            if (item.Count >= item.Max)
            {
                button.interactable = false;
                buttonTxt.text = "MAX";
            }

            Data upgradeRef = item;
            GameObject cloneRef = clone;

            button.onClick.AddListener(() => OnUpgradeClick(upgradeRef, cloneRef));
        }
    }

    int CalculatePrice(int basePrice, int level)
    {
        return Mathf.FloorToInt(basePrice * Mathf.Pow(1.15f, level));
    }

    void OnUpgradeClick(Data data, GameObject clone)
    {
        var priceText = clone.transform.Find("Price").GetComponent<TextMeshProUGUI>();
        var maxText   = clone.transform.Find("Max").GetComponent<TextMeshProUGUI>();
        var button    = clone.transform.Find("Button").GetComponent<Button>();
        var buttonTxt = button.GetComponentInChildren<TextMeshProUGUI>();

        if (GameManager.gameManager.Money < data.Price)
        {
            AudioSource.PlayClipAtPoint(Error, transform.position);
            return;
        }

        if (data.Count >= data.Max)
        {
            AudioSource.PlayClipAtPoint(Error, transform.position);
            return;
        }

        GameManager.gameManager.Money -= data.Price;
        data.Count++;
        
        data.Price = CalculatePrice(data.BasePrice, data.Count);
        
        ApplyUpgradeEffect(data);
        SaveToGameManager();

        priceText.text = data.Price.ToString();
        maxText.text = data.Count + "/" + data.Max;

        if (data.Count >= data.Max)
        {
            button.interactable = false;
            buttonTxt.text = "MAX";
        }

        AudioSource.PlayClipAtPoint(Up, transform.position);
    }

    void ApplyUpgradeEffect(Data data)
    {
        if (data.id == "Money")
        {
            GameManager.gameManager.MaxMoney += data.ValuePerLevel;
        }
        else if (data.id == "Health")
        {
            GameManager.gameManager.Health += data.ValuePerLevel;
        }
    }

    void LoadFromGameManager()
    {
        foreach (var item in UpdateData)
        {
            if (item.id == "Money")
            {
                item.Count = GameManager.gameManager.MoneyUpgradeLevel;
                GameManager.gameManager.MaxMoney = 10 + item.Count * item.ValuePerLevel;
            }
            else if (item.id == "Health")
            {
                item.Count = GameManager.gameManager.HealthUpgradeLevel;
                GameManager.gameManager.Health = 1 + item.Count * item.ValuePerLevel;
            }
        }
    }

    void SaveToGameManager()
    {
        foreach (var item in UpdateData)
        {
            if (item.id == "Money")
            {
                GameManager.gameManager.MoneyUpgradeLevel = item.Count;
            }
            else if (item.id == "Health")
            {
                GameManager.gameManager.HealthUpgradeLevel = item.Count;
            }
        }
    }
}