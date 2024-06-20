using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteExplosionAfterTime : MonoBehaviour
{
    bool isEnd = false;

    private void Update()
    {
        if (isEnd)
            Destroy(gameObject);
    }

    public void isEnded()
    {
        isEnd = true;
    }
}
