using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] float speed = 0.3f;
    [SerializeField] string magnetPickUpLayer = "PickUpRadius";
    Coroutine activeCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character c = collision.GetComponentInParent<Character>();

        bool isMagnetPickUp = collision.gameObject.layer == LayerMask.NameToLayer(magnetPickUpLayer);


        if (c != null)
        {
            // Heal check

            if (GetComponent<HealPickUpObject>() != null)
            {
                if (isMagnetPickUp)
                    return;

                if (c.isDamaged)
                {
                    GetComponent<IPickUpObject>().OnPickUp(c);
                    Destroy(gameObject);
                }
                return;
            }
            
            if (isMagnetPickUp)
            {
                // PickUp object with magnet effects

                if (activeCoroutine == null)
                {
                    activeCoroutine = StartCoroutine(MoveToCharacter(c.transform));
                }
            }
        }

        IEnumerator MoveToCharacter(Transform character)
        {
            Vector3 endPosition = character.position + new Vector3(0f, 0.3f, 0f);

            Vector3 startScale = gameObject.transform.localScale;
            Vector3 endScale = Vector3.zero;

            while (Vector3.Distance(gameObject.transform.position, endPosition) > 0.05f)
            {
                endPosition = character.position + new Vector3(0f, 0.3f, 0f);

                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endPosition, speed * Time.deltaTime);

                float distanceToCharacter = Vector3.Distance(gameObject.transform.position, endPosition);
                float t = Mathf.InverseLerp(0.5f, 0f, distanceToCharacter);
                gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, t);

                speed += 0.1f;

                yield return null;
            }

            GetComponent<IPickUpObject>().OnPickUp(character.GetComponent<Character>());
            Destroy(gameObject);
        }
    }
}