using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshPro;
    public void OnInit(float damge)
    {
        m_TextMeshPro.text = damge.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
