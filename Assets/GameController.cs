using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Vector2 m_SpawnPoint1;
    public Vector2 m_SpawnPoint2;

    private static List<Unit> m_Units;
    private static Dictionary<int, Team> m_Teams;

    public static bool m_IsGameRunning;

    private static Transform m_UnitParent;
    private static Unit m_UnitPrefab;

    private static List<Unit> m_UnitsToRemove;
    public static DateTime m_MatchStartAt;

    public static Team GetRandomTeam(int except)
    {
        int index = UnityEngine.Random.Range(0, m_Teams.Count - 1);
        if (index >= except) index++;

        return m_Teams[index];
    }

    public static void DeclareVictory(int forTeam)
    {
        HUD.Singleton.DeclareVictory(m_Teams[forTeam]);
        m_IsGameRunning = false;
    }

    public static void RemoveUnit(Unit unit)
    {
        m_UnitsToRemove.Add(unit);
    }

    private static void RemoveUnits()
    {
        if (m_UnitsToRemove.Count <= 0)
            return;

        foreach (Unit unit in m_UnitsToRemove)
            ExecRemoveUnit(unit);

        m_UnitsToRemove.Clear();
    }

    private static void ExecRemoveUnit(Unit unit)
    {
        m_Units.Remove(unit);
        m_Teams[unit.m_TeamID].RemoveMember(unit);
        if (m_Teams[unit.m_TeamID].CheckLoseCondition())
        {
            int id = -1;

            foreach (Team team in m_Teams.Values)
            {
                if (!team.m_IsLost)
                {
                    if (id == -1)
                        id = team.m_ID;
                    else
                    {
                        id = -1;
                        break;
                    }    
                }
            }

            if (id != -1)
                DeclareVictory(id);
        }
    }

    private void Initilize()
    {
        m_Units = new List<Unit>();
        m_UnitsToRemove = new List<Unit>();
        m_Teams = new Dictionary<int, Team>();

        m_Teams.Add(0, new Team(0, Color.red));
        m_Teams.Add(1, new Team(1, Color.blue));

        m_UnitParent = new GameObject("Units").transform;
        m_UnitParent.parent = transform;
        m_UnitPrefab = Resources.Load<Unit>("Unit");

        for (int i = 0; i < 3; i++)
        {
            CreateUnit(0, m_SpawnPoint1);
            CreateUnit(1, m_SpawnPoint2);
        }
    }

    public static void StartMatch()
    {
        m_MatchStartAt = DateTime.Now;
        m_IsGameRunning = true;
    }

    public static void CreateUnit(int teamID, Vector2 spawnPoint)
    {
        Unit t = Instantiate(m_UnitPrefab, spawnPoint + UnityEngine.Random.insideUnitCircle * 2, Quaternion.identity, m_UnitParent);
        t.Initialize();
        m_Units.Add(t);
        m_Teams[teamID].AddMember(t);
    }

    private void Tick()
    {
        if (m_IsGameRunning)
        {
            foreach (Unit unit in m_Units)
                unit.Tick();

            RemoveUnits();
        }
    }

    private void Awake() => Initilize();
    private void Update() => Tick();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_SpawnPoint1, 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(m_SpawnPoint2, 2);
    }
}
