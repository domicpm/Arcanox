using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthSlider
{
    void setPlayerMaxHealth(float health);
    void setPlayerHealth(float health);
    float getPlayerHealth();
}
