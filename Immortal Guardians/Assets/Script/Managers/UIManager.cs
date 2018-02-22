using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField] private Button nextWave;
    [SerializeField] private Button sellTower;
    [SerializeField] private Button Upgrade;

    [SerializeField] private Text currencytxt;
    [SerializeField] private Text lifetxt;
    [SerializeField] private Text wavetxt;

    private int currency;
    private int life;
    private int wave;

    // Tower Stats
    [SerializeField] private Text towerTypeText;
    [SerializeField] private Text damageText;
    [SerializeField] private Text firerateText;
    [SerializeField] private Text sellPriceText;
    [SerializeField] private Text upgradePriceText;
    [SerializeField] private GameObject ui;

    // TESTS
    [SerializeField] private Button basicTowerTest;
    [SerializeField] private Button canonTowerTest;


    // Use this for initialization
    void Start()
    {
        Currency = 200;
        Life = 500;

        nextWave = nextWave.GetComponent<Button>();
        nextWave.onClick.AddListener(WaveManager.Instance.NextWave);

        sellTower = sellTower.GetComponent<Button>();
        sellTower.onClick.AddListener(TowerManager.Instance.SellTower);

        Upgrade = Upgrade.GetComponent<Button>();
        Upgrade.onClick.AddListener(TowerManager.Instance.UpgradeTower);

        basicTowerTest = basicTowerTest.GetComponent<Button>();
        basicTowerTest.onClick.AddListener(basicTowerTestMetode);

        canonTowerTest = canonTowerTest.GetComponent<Button>();
        canonTowerTest.onClick.AddListener(canonTowerTestMetode);

    }

    private void basicTowerTestMetode()
    {
        if(!(Currency - 10 < 0))
        {
            GManager.Instance.BuildMode = true;
            TowerManager.Instance.CurrentTower = null;
            ui.SetActive(false);
        }
        BuildingMode.Instance.TowerType = "BasicTower";
    }

    private void canonTowerTestMetode()
    {
        if (!(Currency - 20 < 0))
        {
            GManager.Instance.BuildMode = true;
            TowerManager.Instance.CurrentTower = null;
            ui.SetActive(false);
        }
        BuildingMode.Instance.TowerType = "CanonTower";
    }

    private void Update()
    {
        if(TowerManager.Instance.CurrentTower == null)
        {
            sellTower.transform.gameObject.SetActive(false);
            Upgrade.transform.gameObject.SetActive(false);
        }
        else
        {
            if(!(TowerManager.Instance.CurrentTower.GetComponent<TowerController>().Level > 3))
            {
                Upgrade.transform.gameObject.SetActive(true);
            }
            else
            {
                Upgrade.transform.gameObject.SetActive(false);
            }
            sellTower.transform.gameObject.SetActive(true);
        }
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

    public Button NextWave
    {
        get
        {
            return nextWave;
        }
    }

    public GameObject Ui
    {
        get
        {
            return ui;
        }

        set
        {
            ui = value;
        }
    }

    public void setTowerStats(TowerController tc)
    {
        this.towerTypeText.text =  tc.TowerType + " (level " + tc.Level.ToString() + ")";
        this.damageText.text = "Dmg: " + tc.Damage.ToString();
        this.firerateText.text = "Firerate: " + (1 / tc.AttackCooldown).ToString("#.##") + "/s";
        this.sellPriceText.text = "$" + (tc.TotalPrice / 2).ToString();
        this.upgradePriceText.text = "$" + tc.UpgradePrice.ToString();
    }
}
