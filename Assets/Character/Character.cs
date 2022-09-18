using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public Slider HpSlider;
    public Slider MpSlider;

    public CharacterStat MaxHp;
    public CharacterStat MaxMp;

    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Vitality;

    private int iLevel;

    private float m_fHp;
    private float m_fMp;

    private CharacterStat m_pAttack = new(5500);
    private CharacterStat m_pDefense = new(5000);
    private CharacterStat m_pHit;
    private CharacterStat m_pDodge;
    private CharacterStat m_pMovespeed = new(1);

    public CharacterStat Attack
    {
        get
        {
            return m_pAttack;
        }
    }

    public CharacterStat Movespeed
    {
        get
        {
            return m_pMovespeed;
        }
    }

    private void Start()
    {
        m_fHp = MaxHp.Value;

        UpdateHpSlider();
    }

    private void Update()
    {
        UpdateHpSlider();

        if (m_fHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D pCollision)
    {
        Character pCharacter = pCollision.gameObject.GetComponent<Character>();
        if (pCharacter == null) return;

        int iDamageTaken = CalculateDamageTaken(pCharacter);
        FloatingDamage.Create(transform.position, iDamageTaken);

        TakingDamage(iDamageTaken);
    }

    private void UpdateHpSlider()
    {
        if (HpSlider == null) return;

        HpSlider.value = m_fHp;
        HpSlider.maxValue = MaxHp.Value;
    }

    private void UpdateMpSlider()
    {
        if (MpSlider == null) return;

        MpSlider.value = m_fMp;
        MpSlider.maxValue = MaxMp.Value;
    }

    private int CalculateDamageTaken(Character pEnemy)
    {
        float fEnemyAttack = pEnemy.Attack.Value;
        float fPlayerDefense = m_pDefense.Value;
        return (int)(fEnemyAttack - fPlayerDefense);
    }

    private void TakingDamage(float fDamageTaken)
    {
        if (m_fHp - fDamageTaken > 0)
        {
            m_fHp -= fDamageTaken;
        }
        else
        {
            m_fHp = 0;
        }
    }
}
