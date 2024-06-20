using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandSettings : MonoBehaviour
{
    #region Attributes

    [Header("Size Settings")]
    
    [SerializeField] float minSize = 0.3f;
    [SerializeField] float maxSize = 0.5f;
    
    [SerializeField] bool isRandSize = false;

    [Space]

    [Header("Speed Settings")]
    
    [SerializeField] float minSpeed = 0.4f;
    [SerializeField] float maxSpeed = 0.45f;

    [SerializeField] bool isRandSpeed = false;

    Monsters monster;

    #endregion

    #region Methods

    #region Unity Callbacks

    private void Awake()
    {
        monster = GetComponent<Monsters>();
    }

    private void Start()
    {
        if (isRandSize) transform.localScale = GenerateRandSize(minSize, maxSize);
        if (isRandSpeed) monster.SPEED = GenerateRandSpeed(minSpeed, maxSpeed);
    }

    #endregion

    // Size
    public Vector3 GenerateRandSize(float minSize, float maxSize)
    {
        Vector3 newScale = new Vector3();
        newScale.x = Random.Range(minSize, maxSize);
        newScale.y = newScale.x - 0.05f;

        return newScale;
    }

    // Speed
    public float GenerateRandSpeed(float minSpeed, float maxSpeed)
    {
        float newSpeed = Random.Range(minSpeed, maxSpeed);
        newSpeed = GameManager.instance.playerTransform.GetComponent<PlayerMove>().SPEED * newSpeed;
        return newSpeed;
    }


    #endregion
}
