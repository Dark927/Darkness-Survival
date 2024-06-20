using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    [SerializeField] GameObject damageMessage;

    List<TMPro.TextMeshPro> messagePool;

    [SerializeField] int objectCount = 15;
    int count;

    private void Start()
    {
        count = 0;
        messagePool = new List<TMPro.TextMeshPro>();
        for(int i = 0; i < objectCount; ++i)
        {
            Populate();
        }
    }

    public void Populate()
    {
        GameObject go = Instantiate(damageMessage, transform);
        messagePool.Add(go.GetComponent<TMPro.TextMeshPro>());
        go.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    public void PostMessage(string text, Vector3 worldPosition, Color? color = null)
    {
        messagePool[count].gameObject.SetActive(true);
        messagePool[count].transform.position = worldPosition;
        messagePool[count].text = text;

        Color messageColor = color ?? Color.red;
        messagePool[count].color = messageColor;

        count++;

        if(count >= objectCount)
        {
            count = 0;
        }
    }
}
