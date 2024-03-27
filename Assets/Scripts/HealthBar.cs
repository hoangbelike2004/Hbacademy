using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image m_Image;
    [SerializeField] Vector3 OffSet;
    float hp, maxhp;
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        m_Image.fillAmount = Mathf.Lerp(m_Image.fillAmount, hp / maxhp,Time.deltaTime*5f);
        transform.position = target.position + OffSet;
    }
    public void OnInit(float maxhp,Transform target)
    {
        this.maxhp = maxhp;
        this.target = target;
        hp = maxhp;
        m_Image.fillAmount = 1;
    }
    public void SetHp(float Hp)
    {
        this.hp = Hp;
        //m_Image.fillAmount = hp / maxhp;
    }
}
