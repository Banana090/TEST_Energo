using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : Unit
{
    protected Unit m_Target;

    public override void Tick()
    {
        if (m_Target == null)
            m_Target = GameController.GetRandomTeam(m_TeamID).GetRandomMember();
        else if (!m_IsInFireMode)
        {
            Vector3 Direction = (m_Target.m_Transform.position - m_Transform.position).normalized;
            m_Transform.position += Direction * m_MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(m_Transform.position, m_Target.m_Transform.position) <= m_Radius)
                m_IsInFireMode = true;
        }
        else
        {
            m_Target.TakeDamage(m_Damage * Time.deltaTime);
        }
    }
}
