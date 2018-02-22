using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField] private Text currencytxt;
    [SerializeField] private Text lifetxt;

    private int currency = 0;
    private int life = 0;

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;
            this.currencytxt.text = value.ToString() + " <color=lime>$</color>";
        }
    }

    public int Life
    {
        get
        {
            return life;
        }

        set
        {
            this.life = value;
            this.lifetxt.text = "<color=red><3</color> " + value.ToString();
        }
    }

    // Use this for initialization
    void Start () {
        Currency = 1000;
        Life = 500;
	}
}
