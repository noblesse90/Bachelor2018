﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField] private Button btn;

    [SerializeField] private Text currencytxt;
    [SerializeField] private Text lifetxt;
    [SerializeField] private Text wavetxt;

    private int currency;
    private int life;
    private int wave;

    // Use this for initialization
    void Start()
    {
        Currency = 1000;
        Life = 500;

        btn = btn.GetComponent<Button>();
        btn.onClick.AddListener(WaveManager.Instance.NextWave);
    }

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
            this.lifetxt.text = value.ToString() + " <color=red><3</color>";
        }
    }

    public int Wave
    {
        get
        {
            return wave;
        }

        set
        {
            wave = value;
            this.wavetxt.text = "Current Wave: " + value.ToString();
        }
    }

    public Button Btn
    {
        get
        {
            return btn;
        }
    }
}
