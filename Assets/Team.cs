using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public int m_ID;
    public List<Unit> m_Members;
    public Color m_Color;

    public bool m_IsLost;

    public Team(int id, Color color)
    {
        m_ID = id;
        m_Color = color;
        m_Members = new List<Unit>();

        m_IsLost = false;
    }

    public void AddMember(Unit member)
    {
        m_Members.Add(member);
        member.UpdateColor(m_Color);
        member.m_TeamID = m_ID;
    }

    public void RemoveMember(Unit member)
    {
        if (m_Members.Contains(member))
            m_Members.Remove(member);
    }

    public Unit GetRandomMember()
    {
        if (m_Members == null) return null;
        if (m_Members.Count <= 0) return null;
        return m_Members[Random.Range(0, m_Members.Count)];
    }

    public bool CheckLoseCondition()
    {
        m_IsLost = m_Members.Count <= 0;
        return m_IsLost;
    }
}
