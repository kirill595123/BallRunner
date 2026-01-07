using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Open(GameObject panel)
    {
        panel.SetActive(true);
        Vector3 pos = panel.GetComponent<RectTransform>().localScale;
        panel.GetComponent<RectTransform>().localScale = new Vector2(0.05f, 0.05f);
        panel.GetComponent<RectTransform>().DOScale(pos, 0.1f).SetEase(Ease.InSine);
    }

    public void Close(GameObject panel)
    {
        Vector3 pos = panel.GetComponent<RectTransform>().localScale;
        panel.GetComponent<RectTransform>().DOScale(new Vector3(0.05f, 0.05f, 1f), 0.1f).SetEase(Ease.OutSine).OnComplete(() =>{
                panel.SetActive(false);
                panel.GetComponent<RectTransform>().localScale = pos;
            }
        );
    }
}
