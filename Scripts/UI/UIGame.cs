using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIGame : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI CoinsText;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject ComplicationPanel;
    public bool BoolActiveComplications;
    public bool ComlicBool;
    public float TimeComplication;
    public GameObject Player;
    private bool _isPause;

    public Complications complications;
    public items items;
    public Health health;

    void Update()
    {
        ScoreText.text = GameManager.gameManager.Score.ToString();
        CoinsText.text = items.CoinsSpawned.Count + "/" + GameManager.gameManager.MaxCoins;

        if (ComlicBool)
        {
            TimeComplication -= Time.deltaTime;
            ComplicationPanel.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = Mathf.CeilToInt(TimeComplication).ToString();

            if(TimeComplication <= 0)
            {
                ComlicBool = false;
            }
        }

        if (items.CoinsSpawned.Count >= GameManager.gameManager.MaxCoins)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        GameOverPanel.transform.localPosition = new UnityEngine.Vector2(0, -800);
        GameOverPanel.GetComponent<RectTransform>().DOAnchorPos(new UnityEngine.Vector2(0, 0), 0.2f).OnComplete(() =>
            Time.timeScale = 0f
        );
    }

    public void Pause()
    {
        if (!_isPause)
        {
            PausePanel.SetActive(true);
            PausePanel.transform.localPosition = new UnityEngine.Vector2(0, -800);
            PausePanel.GetComponent<RectTransform>().DOAnchorPos(new UnityEngine.Vector2(0, 0), 0.2f).OnComplete(() =>
                Time.timeScale = 0f
            );

            _isPause = true;
        }
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        PausePanel.GetComponent<RectTransform>().DOAnchorPos(new UnityEngine.Vector2(0, -800), 0.4f).OnComplete(() =>{
            PausePanel.SetActive(false);
            PausePanel.transform.localPosition = new UnityEngine.Vector2(0, 0);
        });

        _isPause = false;
    }

    public void Restart(GameObject panel)
    {
        Time.timeScale = 1f;
        panel.GetComponent<RectTransform>().DOAnchorPos(new UnityEngine.Vector2(0, -800), 0.4f).OnComplete(() =>{
            panel.SetActive(false);
            panel.transform.localPosition = new UnityEngine.Vector2(0, 0);
            health.RenderHealth();
        });

        Player.transform.position = new UnityEngine.Vector3(0, 0.5f, 0);
        Player.GetComponent<Rigidbody>().linearVelocity = UnityEngine.Vector3.zero;

        _isPause = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void X2Button()
    {
        Debug.Log("мне лень добовлять рекламу!!");
    }

    public void ActiveComplications(bool Active, string name, int time)
    {
        BoolActiveComplications = Active;

        TimeComplication = time;
        ComplicationPanel.SetActive(BoolActiveComplications);
        ComplicationPanel.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;

        if (BoolActiveComplications)
        {
            ComlicBool = true;
        }
    }
}