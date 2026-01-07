using System.Collections;
using UnityEngine;

[System.Serializable]
public class Event
{
    public string name;
    public int chance;
    public int time;
}

public class Complications : MonoBehaviour
{
    public Event[] events;

    public float Min;
    public float Max;

    public GameObject Walls;
    private bool isEventActive;

    public UIGame uIGame;

    Event RandomEvent()
    {
        float total = 0f;
        foreach (var item in events)
        {
            total += item.chance;
        }

        float rand = Random.Range(0f, total);

        foreach (var item in events)
        {
            if(rand <= item.chance)
                return item;
            rand -= item.chance;
        }

        return null;
    }

    void Start()
    {
        StartCoroutine(Loop());
        Debug.Log("вызыван");
    }

    IEnumerator Loop()
    {
        while (true)
        {
            float delay = Random.Range(Min, Max);
            yield return new WaitForSeconds(delay);

            if(isEventActive)
                continue;

            var evnt = RandomEvent();
            if(evnt != null) {
                StartCoroutine(ActiveEvent(evnt));
            }
            Debug.Log(evnt.name);
        }
    }

    IEnumerator ActiveEvent(Event evnt)
    {
        isEventActive = true;

        switch (evnt.name)
        {
            case "Без cтен":
                Walls.SetActive(false);
                uIGame.ActiveComplications(true, evnt.name, evnt.time);
                break;
        }

        yield return new WaitForSeconds(evnt.time);

        switch (evnt.name)
        {
            case "Без cтен":
                Walls.SetActive(true);
                uIGame.ActiveComplications(false, evnt.name, evnt.time);
                break;
        }

        isEventActive = false;
    }
}