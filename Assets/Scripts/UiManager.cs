using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager instantia;

    public static UiManager Instance
    {
        get
        {
            if(instantia == null)
            {
                instantia = FindObjectOfType<UiManager>();

            }
            return instantia;
        }
    }
    [SerializeField] TextMeshProUGUI CoinText;
    public void SetCoin(int coinvalues)
    {
        CoinText.text = coinvalues.ToString();
    }

}
