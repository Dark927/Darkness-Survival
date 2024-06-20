using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] GameObject[] cards;
    [SerializeField] float animationTime = 0.5f;

    private void OnEnable()
    {
        StartCoroutine(AnimateCards());
    }

    private IEnumerator AnimateCards()
    {
        foreach (GameObject card in cards)
        {
            card.GetComponent<Button>().interactable = false; 
            card.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        float timer = 0;

        while (timer <= animationTime)
        {
            foreach (GameObject card in cards)
            {
                card.transform.localScale = Vector3.Lerp(card.transform.localScale, Vector3.one, timer / animationTime);

                if(card.transform.localScale.x >= 0.8f)
                {
                    card.GetComponent<Button>().interactable = true;
                }
            }

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        foreach (GameObject card in cards)
        {
            card.transform.localScale = Vector3.one;
            card.GetComponent<Button>().interactable = true;
        }
    }
}