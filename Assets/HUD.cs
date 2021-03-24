using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    private static HUD m_Singleton;
    public static HUD Singleton
    {
        get
        {
            if (m_Singleton == null)
                m_Singleton = FindObjectOfType<HUD>();
            return m_Singleton;
        }
    }

    public GameObject m_Background;
    public GameObject m_StartButton;
    public TextMeshProUGUI m_TimeText;
    public TextMeshProUGUI m_VictoryText;

    public void OnStartMatch()
    {
        GameController.StartMatch();
        m_Background.SetActive(false);
        m_StartButton.SetActive(false);
    }

    public void DeclareVictory(Team team)
    {
        m_Background.SetActive(true);

        m_VictoryText.text = $"Team {team.m_ID} wins";
        m_VictoryText.color = team.m_Color;

        m_TimeText.text = (GameController.m_MatchStartAt - System.DateTime.Now).ToString("c");

        m_VictoryText.gameObject.SetActive(true);
        m_TimeText.gameObject.SetActive(true);
    }
}
