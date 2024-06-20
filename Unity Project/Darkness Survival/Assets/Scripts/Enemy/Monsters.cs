using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Monsters : MonoBehaviour, IDamageable
{
    ///////////////////////
    //       Stats       //
    ///////////////////////

    Transform targetDestination;
    GameObject targetGameobject;
    Character targetCharacter;
    Rigidbody2D rigidbody2d;
    ColorChange colorChange;

    [Header("Stats")]

    [SerializeField] float maxHp = 4f;
    [SerializeField] float minHp = 4f;
    float hp;

    [SerializeField] float damage = 12f;
    [SerializeField] float speed = 2.5f * 0.4f;
    [SerializeField] int soulsReward = 30;

    // Changeable stats //

    float currentDamage;
    float damageMultiplier = 0.7f;
    float currentSpeed;

    //////////////////////

    Color defaultColor;
    private bool isSwappingSide = false;
    bool isSpirit;
    float swapTimer;

    // Settings //

    [Space]
    [Space]
    [Header("Settings")]
    [Space]

    [SerializeField] Color spiritColor;
    [SerializeField] float colorChangeTime = 1f;
    [SerializeField] float slowDownSpeed = 0.3f;
    [SerializeField] float sideSwapDelay = 4f;
    [SerializeField] ObjectsDetection objectsDetection;
    [SerializeField] float attackReloadTime = 1f;
    [SerializeField] float knockBackTime = 0.5f;

    SimpleFlash simpleFlash;

    bool isKnockedBack = false;
    float knockBackEndTime;

    //////////////
    /// Boss
    //////////////

    [SerializeField] StatusBar hpBar;
    bool isBoss = false;
    Vector3 offset;

    //////////////

    float timer;

    //private Vector2[] path;
    //private int targetIndex;

    ///////////////////////
    //      Methods      //
    ///////////////////////

    public float SPEED
    {
        get { return speed; }
        set { speed = value; }
    }

    public float HP
    {
        get { return hp; }
        set { maxHp = hp; }
    }

    public float MAX_HP
    {
        get { return maxHp; }
    }

    // - Awake 

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        defaultColor = GetComponent<SpriteRenderer>().color;
        colorChange = GetComponent<ColorChange>();
        simpleFlash = GetComponent<SimpleFlash>();
    }

    // - Start

    private void Start()
    {
        timer = -1f;
        swapTimer = 0f;
        isSpirit = false;
        currentDamage = damage;
        currentSpeed = speed;
        hp = Random.Range(minHp, maxHp);
        maxHp = hp;

        if(hpBar != null)
        {
            hpBar.SetState(hp, maxHp);
            isBoss = true;
            offset = hpBar.transform.position - transform.position;
        }
    }


    // - Set Target

    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;
        speed = targetGameobject.GetComponent<PlayerMove>().SPEED * speed;
    }

    // - Updates 

    private void Update()
    {
        if (isBoss)
        {
            hpBar.transform.position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z);
        }


        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        //////////////////////////////////
        // Check and change spirit mode //
        //////////////////////////////////

        if (colorChange != null)
        {
            if (objectsDetection != null)
            {
                if (objectsDetection.IsDetected() && !isSpirit)
                {
                    isSpirit = true;
                    SpiritSettings(isSpirit);
                }
                if (!objectsDetection.IsDetected() && isSpirit)
                {
                    isSpirit = false;
                    SpiritSettings(isSpirit);
                }
            }
            else
            {
                Debug.Log(" Error :: Monsters -> ObjectTrigger can not be found! Object Detection fail.");
            }
        }

        ////////////////////////////////


        ////////////////////////////////
        // Check and change look side //
        ////////////////////////////////

        bool isLeft = targetDestination.position.x < transform.position.x;
        bool needsToSwapSide = (isLeft && transform.localScale.x > 0f) || (!isLeft && transform.localScale.x < 0f);

        if (needsToSwapSide)
        {
            if (!isSwappingSide)
            {
                currentSpeed *= slowDownSpeed;
                isSwappingSide = true;
            }

            swapTimer += Time.deltaTime;
        }

        if (swapTimer >= sideSwapDelay)
        {
            currentSpeed = speed;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            swapTimer = 0f;
            isSwappingSide = false;
        }

        ////////////////////////////////
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;

        if (!isKnockedBack)
        {
            rigidbody2d.velocity = direction * currentSpeed;
        }
        else
        {
            if (Time.time >= knockBackEndTime)
            {
                isKnockedBack = false;
            }
        }
    }


    // - Collisions

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameobject)
        {
            Attack();
        }
    }

    // - Gameplay methods 

    private void Attack()
    {
        if(timer > 0f)
        {
            return;
        }

        if (targetCharacter == null)
        {
            targetCharacter = targetGameobject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(currentDamage);
        timer = attackReloadTime;
    }

    // - Take Damage and knockback
    public void TakeDamage(float damage, Vector2 knockBack = default)
    {
        if (knockBack != Vector2.zero)
        {
            rigidbody2d.AddForce(knockBack, ForceMode2D.Impulse);
            isKnockedBack = true;
            knockBackEndTime = Time.time + knockBackTime;
        }
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (isBoss) hpBar.SetState(hp, maxHp);

        // Damage message

        Vector3 messagePosition = new Vector3(
                   transform.position.x,
                   transform.position.y + 0.8f,
                   transform.position.z
                   );

        if (simpleFlash != null) simpleFlash.Flash();

        MessageSystem.instance.PostMessage(damage.ToString(), messagePosition);

        // Check if dead 

        if (hp <= 0)
        {
            //// Heal message 

            //if (check demonic absorption)
            //{
            //    float healAmount = 6.5f;
            //    MessageSystem.instance.PostMessage(healAmount.ToString(), messagePosition, Color.green);
            //}

            if (simpleFlash != null) simpleFlash.Flash();

            // Additional Damage message
            //MessageSystem.instance.PostMessage(damage.ToString(), messagePosition);

            targetGameobject.GetComponent<Level>().AddExperience(soulsReward);
            GetComponent<DropOnDestroy>().CheckDrop();

            if(isBoss) Destroy(hpBar.gameObject);
            Destroy(gameObject);
        }
    }


    // Method for objects colliding 

    private Coroutine activeCoroutine;

    public void SpiritSettings(bool isSpirit)
    {
        // If Coroutine is active, we need to stop it
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        // Check condition and change settings 

        if (isSpirit)
        {
            currentDamage *= damageMultiplier;
            activeCoroutine = StartCoroutine(colorChange.ChangeColor(spiritColor, colorChangeTime));
        }
        else
        {
            currentDamage = damage;
            activeCoroutine = StartCoroutine(colorChange.ChangeColor(defaultColor, colorChangeTime));
        }
    }
}
