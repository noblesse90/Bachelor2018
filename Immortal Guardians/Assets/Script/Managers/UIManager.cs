using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    [SerializeField] private Button _nextWaveBtn;
    [SerializeField] private Button _sellTowerBtn;
    [SerializeField] private Button _upgradeBtn;

    [SerializeField] private Text _currencyTxt;
    [SerializeField] private Text _lifeTxt;
    [SerializeField] private Text _waveTxt;

    private int _currency;
    private int _life;
    private int _wave;

    // Tower Stats
    [SerializeField] private Text _towerTypeText;
    [SerializeField] private Text _damageText;
    [SerializeField] private Text _firerateText;
    [SerializeField] private Text _sellPriceText;
    [SerializeField] private Text _upgradePriceText;
    [SerializeField] private GameObject _towerStatsUi;

    // TESTS
    [SerializeField] private Button _basicTowerTestBtn;
    [SerializeField] private Button _canonTowerTestBtn;


    // Use this for initialization
    private void Start()
    {
        Currency = 200;
        Life = 500;

        _nextWaveBtn = _nextWaveBtn.GetComponent<Button>();
        _nextWaveBtn.onClick.AddListener(WaveManager.Instance.NextWave);

        _sellTowerBtn = _sellTowerBtn.GetComponent<Button>();
        _sellTowerBtn.onClick.AddListener(TowerManager.Instance.SellTower);

        _upgradeBtn = _upgradeBtn.GetComponent<Button>();
        _upgradeBtn.onClick.AddListener(TowerManager.Instance.UpgradeTower);

        _basicTowerTestBtn = _basicTowerTestBtn.GetComponent<Button>();
        _basicTowerTestBtn.onClick.AddListener(BasicTowerTestMetode);

        _canonTowerTestBtn = _canonTowerTestBtn.GetComponent<Button>();
        _canonTowerTestBtn.onClick.AddListener(CanonTowerTestMetode);

    }

    private void BasicTowerTestMetode()
    {
        if(!(Currency - 10 < 0))
        {
            GManager.Instance.BuildMode = true;
            TowerManager.Instance.CurrentTower = null;
            _towerStatsUi.SetActive(false);
        }
        BuildingMode.Instance.TowerType = "BasicTower";
    }

    private void CanonTowerTestMetode()
    {
        if (!(Currency - 20 < 0))
        {
            GManager.Instance.BuildMode = true;
            TowerManager.Instance.CurrentTower = null;
            _towerStatsUi.SetActive(false);
        }
        BuildingMode.Instance.TowerType = "CanonTower";
    }

    private void Update()
    {
        if(TowerManager.Instance.CurrentTower == null)
        {
            _sellTowerBtn.transform.gameObject.SetActive(false);
            _upgradeBtn.transform.gameObject.SetActive(false);
        }
        else
        {
            if(!(TowerManager.Instance.CurrentTower.GetComponent<TowerController>().Level > 3))
            {
                _upgradeBtn.transform.gameObject.SetActive(true);
            }
            else
            {
                _upgradeBtn.transform.gameObject.SetActive(false);
            }
            _sellTowerBtn.transform.gameObject.SetActive(true);
        }
    }

    public int Currency
    {
        get
        {
            return _currency;
        }

        set
        {
            this._currency = value;
            this._currencyTxt.text = value.ToString() + " <color=lime>$</color>";
        }
    }

    public int Life
    {
        get
        {
            return _life;
        }

        set
        {
            this._life = value;
            this._lifeTxt.text = value.ToString() + " <color=red><3</color>";
        }
    }

    public int Wave
    {
        get
        {
            return _wave;
        }

        set
        {
            _wave = value;
            this._waveTxt.text = "Current Wave: " + value.ToString();
        }
    }

    public Button NextWaveBtn
    {
        get
        {
            return _nextWaveBtn;
        }
    }

    public GameObject TowerStatsUi
    {
        get
        {
            return _towerStatsUi;
        }

        set
        {
            _towerStatsUi = value;
        }
    }

    public void SetTowerStats(TowerController tc)
    {
        this._towerTypeText.text =  tc.TowerType + " (level " + tc.Level.ToString() + ")";
        this._damageText.text = "Dmg: " + tc.Damage.ToString();
        this._firerateText.text = "Firerate: " + (1 / tc.AttackCooldown).ToString("#.##") + "/s";
        this._sellPriceText.text = "$" + (tc.TotalPrice / 2).ToString();
        this._upgradePriceText.text = "$" + tc.UpgradePrice.ToString();
    }
}
