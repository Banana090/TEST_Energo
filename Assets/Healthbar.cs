using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public RectTransform m_Red;

    private float m_Width;

    public void Initialize()
    {
        m_Width = m_Red.rect.width;
    }

    public void SetHealth(float amount)
    {
        m_Red.sizeDelta = new Vector2((1.0f - amount) * -m_Width, m_Red.sizeDelta.y);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
