using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : MonoBehaviour, IExpSlider
{
    public Slider sli;

    public void setPlayerMaxExp(float maxExp)
    {
        sli.maxValue = maxExp;
    }
    public void setPlayerExp(float exp)
    {
        sli.value = exp;
    }
    public float getPlayerExp()
    {
        return sli.value;
    }
}
