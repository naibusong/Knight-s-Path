using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateBar : MonoBehaviour
{
    public Image healthImage;
    public Image delayHealthImage;


    private void Update()
    {
        if(delayHealthImage.fillAmount > healthImage.fillAmount)
        {
            delayHealthImage.fillAmount -= Time.deltaTime;
        }
    }
    public void OnHealthChange(float percentage)//����Ѫ���ٷֱ� Current/Max
    {
        healthImage.fillAmount = percentage;
        
    }
}
