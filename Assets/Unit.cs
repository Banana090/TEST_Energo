using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float m_MoveSpeed;
    [SerializeField] protected float m_Radius;
    [SerializeField] protected float m_Damage;
    [SerializeField] protected float m_MaxHealth;
    [Space, SerializeField] protected Healthbar m_Healhbar;

    [HideInInspector] public int m_TeamID;

    public Transform m_Transform { get; protected set; }
    public float m_Health { get; protected set; }

    protected bool m_IsInFireMode;

    public virtual void Initialize()
    {
        m_Transform = transform;
        m_Health = m_MaxHealth;
        m_Healhbar.Initialize();
    }

    public virtual void TakeDamage(float amount)
    {
        m_Health -= amount;
        if (m_Health <= 0)
        {
            m_Health = 0;
            m_Healhbar.SetActive(false);
            Die();
        }

        m_Healhbar.SetHealth(m_Health / m_MaxHealth);
    }

    public virtual void Die()
    {
        GameController.RemoveUnit(this);
        Destroy(gameObject);
    }

    public virtual void Tick()
    {

    }

    public virtual void UpdateColor(Color color)
    {
        GetComponentInChildren<SpriteRenderer>().color = color;
    }
}
