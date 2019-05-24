using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpManager : MonoBehaviour {

    public Slider hpBar, mpBar;
    public Image[] balls;

    public int hpMax = 100;
    public int mpMax = 100;
    int hp = 100;
    int mp = 100;

    public void InitHp()
    {
        SetHp(0);
    }

    public void InitMp()
    {
        SetHp(0);
    }

    public void DoDamageHp(int point)
    {
        SetHp(hp - point);
    }

    public void DoSaveHp(int point)
    {
        SetHp(hp + point);
    }

    public void DoSaveMp(int point)
    {
        SetMp(mp + point);
    }

    public void SetHp(int point)
    {
        hp = Mathf.Clamp(point, 0, hpMax);
        if (hpBar)
        {
            float val = (float)hp / (float)hpMax; ;
            hpBar.value = val;
            for (int i = 0; i < 4; i++)
            {
                if (val > (i - 1f)/ 3f) balls[i].enabled = true;
                else balls[i].enabled = false;
            }
        }
    }

    public void SetMp(int point)
    {
        mp = Mathf.Clamp(point, 0, mpMax);
        if (mpBar)
            mpBar.value = (float)mp / (float)mpMax;
    }

}
