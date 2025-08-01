using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExpSlider
{
    void setPlayerMaxExp(float maxExp);
    void setPlayerExp(float exp);
    float getPlayerExp();
}
