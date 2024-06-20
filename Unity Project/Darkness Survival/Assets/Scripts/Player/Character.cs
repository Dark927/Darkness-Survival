using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float maxHp = 100f;
    [SerializeField] float armor = 1f;
    
    float defaultArmor = 1f;
    float defaultMaxHp;

    [Space]
    [Header("Settings")]
    [SerializeField] float currentHp;
    [SerializeField] StatusBar hpBar;

    [HideInInspector] public bool isDamaged;
    [HideInInspector] public Level level;
    [HideInInspector] public LightEnergy lightEnergy;

    public bool offControl = false;

    // Attack attributes 

    Animator animator;
    AttackControl attackControl;
    private Renderer rend;

    [SerializeField] ControlCyberSword cyberSword;

    bool isShortAttack = false;
    bool isHeavyAttack = false;


    // Materials 

    [Space]
    [Header("Materials")]
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material attackGlowMaterial;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = defaultMaterial;
        level = GetComponent<Level>();
        animator = GetComponent<Animator>();
        lightEnergy = GetComponent<LightEnergy>();
    }

    // Start Parameters 

    private void Start()
    {
        defaultArmor = armor;
        defaultMaxHp = maxHp;

        // Set HP states 

        hpBar.SetState(currentHp, maxHp);

        if (currentHp < maxHp)
            isDamaged = true;
        else
            isDamaged = false;

        // Set attack state

        attackControl = animator.GetBehaviour<AttackControl>();
    }

    /////////////////////////
    // Getters and setters //
    /////////////////////////
    
    public float MAX_HP
    {
        get { return defaultMaxHp; }
        set { maxHp = value; }
    }

    public float ARMOR
    {
        get { return defaultArmor; }
        set { armor = value; }
    }

    /////////////////////////
    
    // Take Damage function 

    public void TakeDamage(float damage)
    {
        if (offControl)
            return;

        ApplyArmor(ref damage);

        currentHp -= damage;
        isDamaged = true;

        if (currentHp <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            offControl = true;
        }
        hpBar.SetState(currentHp, maxHp);
    }

    private void ApplyArmor(ref float damage)
    {
        damage -= armor;

        if (damage < 0)
        {
            damage = 0;
        }
    }

    // Heal Function 

    public void Heal(float amount)
    {
        if (offControl)
            return;

        currentHp += amount;
        if (currentHp >= maxHp)
        {
            currentHp = maxHp;
            isDamaged = false;
        }

        hpBar.SetState(currentHp, maxHp);
    }

    // Main Attack Function

    private void Update()
    {
        // Check if btn is clicked and attack not started yet

        // Input.GetKeyDown(KeyCode.Space)      - Space btn
        // Input.GetMouseButtonDown(0)          - Left mouse btn

        if (!offControl)
        {
            if (!GameManager.instance.isAndroid)
            {
                isShortAttack = Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0);
                isHeavyAttack = Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1);
            }

            if (!attackControl.IsAttacking)
            {
                if (isShortAttack || isHeavyAttack)
                {
                    // Start attack animation 
                    animator.SetTrigger("isAttack");
                    animator.SetFloat("AttackType", (isShortAttack ? -1f : 1f));

                }
            }
            isShortAttack = false;
            isHeavyAttack = false;
        }
    }

    private void ActivateHit(bool isFirstHit)
    {
        if (cyberSword == null)
        {
            Debug.Log(" Error :: cyberSword is null !");
            return;
        }

        cyberSword.Hit(isFirstHit);
    }

    public void FirstHit()
    {
        ActivateHit(true);
    }

    public void SecondHit()
    {
        ActivateHit(false);
    }

    // Activating glow material and default 

    public void onGlow()
    {
        rend.material = attackGlowMaterial;
    }

    public void offGlow()
    {
        rend.material = defaultMaterial;
    }


    // --- Android ---

    public void SetShortAttack()
    {
        isShortAttack = true;
    }

    public void SetHeavyAttack()
    {

        isHeavyAttack = true;
    }
}
