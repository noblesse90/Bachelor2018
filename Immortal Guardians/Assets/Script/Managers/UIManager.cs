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
    
    // MANABAR
    [SerializeField]private Image _manaBar;
    [SerializeField] private Text _manaBarText;
    
    // ABILITIES
    [SerializeField]private Image _leftClickIcon;
    [SerializeField]private Image _rightClickIcon;
    [SerializeField] private Image _firstAbilityIcon;

    public Image ManaBar
    {
        get { return _manaBar; }
        set { _manaBar = value; }
    }
    
    private bool _canBasicAttack = true, _canGCDAttack = true;
    private float _basicAttackTimer, _gcdTimer, _gcd;

    public bool CanBasicAttack
    {
        get { return _canBasicAttack; }
        set { _canBasicAttack = value; }
    }

    public bool CanGcdAttack
    {
        get { return _canGCDAttack; }
        set { _canGCDAttack = value; }
    }

   


    // Use this for initialization
    private void Start()
    {
        Currency = 500;
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
        
        
        _gcd = 0.5f;
    }

    private void BasicTowerTestMetode()
    {
        if(!(Currency - TowerManager.Instance.GetBasicTowerCost < 0))
        {
            GManager.Instance.BuildMode = true;
            TowerManager.Instance.CurrentTower = null;
            _towerStatsUi.SetActive(false);
            BuildingMode.Instance.TowerType = "BasicTower";
            GManager.Instance.TowerToBuild = "BasicTower";
        }  
    }

    private void CanonTowerTestMetode()
    {
        if (!(Currency - TowerManager.Instance.GetCannonTowerCost < 0))
        {
            GManager.Instance.BuildMode = true;
            TowerManager.Instance.CurrentTower = null;
            _towerStatsUi.SetActive(false);
            BuildingMode.Instance.TowerType = "CanonTower";
            GManager.Instance.TowerToBuild = "CanonTower";
        }    
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
        
        // Fill manabar
        UiFillBars();
        
        // gcd timer
        Timers();
    }

    public int Currency
    {
        get
        {
            return _currency;
        }

        set
        {
            if (value > 99999)
            {
                value = 99999;
            }
            _currency = value;
            _currencyTxt.text = value.ToString();
            
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
            _life = value;
            _lifeTxt.text = value.ToString();
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
            _waveTxt.text = "Current Wave: " + value.ToString();
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
        _towerTypeText.text =  tc.TowerType + " (level " + tc.Level.ToString() + ")";
        _damageText.text = "Dmg: " + tc.Damage;
        _firerateText.text = "Firerate: " + (1 / tc.AttackCooldown).ToString("#.##") + "/s";
        _sellPriceText.text = "$" + (tc.TotalPrice / 2);
        _upgradePriceText.text = "$" + tc.UpgradePrice;
    }
    
    private void UiFillBars()
    {
        // Basic attack cooldown
        if (!_canBasicAttack)
        {
            _leftClickIcon.fillAmount = (_basicAttackTimer / _gcd);	
        }
        else
        {
            _leftClickIcon.fillAmount = 1;
        }

        if (!_canGCDAttack)
        {
            if (PlayerController.Instance.Mana >= PlayerController.Instance.RightClickCost)
            {
                _rightClickIcon.fillAmount = (_gcdTimer / _gcd);
            }
            else
            {
                _rightClickIcon.fillAmount = 0;
            }

            if (PlayerController.Instance.Mana >= PlayerController.Instance.FirstAbilityCost)
            {
                _firstAbilityIcon.fillAmount = (_gcdTimer / _gcd);
            }
            else
            {
                _firstAbilityIcon.fillAmount = 0;
            }
        }
        else
        {
            _rightClickIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.RightClickCost ? 1 : 0;
            _firstAbilityIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.FirstAbilityCost ? 1 : 0;
        }

		
        // MANABAR
        if (!NextWaveBtn.gameObject.activeInHierarchy && PlayerController.Instance.Mana <= PlayerController.Instance.MaxMana)
        {
            PlayerController.Instance.Mana += 5f * Time.deltaTime;
            _manaBar.fillAmount = PlayerController.Instance.Mana / PlayerController.Instance.MaxMana;
            ToStringManabar(PlayerController.Instance.Mana, PlayerController.Instance.MaxMana);
        }
    }

    private void ToStringManabar(float mana, float maxMana)
    {
        _manaBarText.text = mana.ToString("#") + "/" + maxMana;
    }
    
    private void Timers()
    {
        if (!_canBasicAttack)
        {
            _basicAttackTimer += Time.deltaTime;

            if(_basicAttackTimer >= _gcd)
            {
                _canBasicAttack = true;
                _basicAttackTimer = 0;
            }
        }

        if (!_canGCDAttack)
        {
            _gcdTimer += Time.deltaTime;

            if (_gcdTimer >= _gcd)
            {
                _canGCDAttack = true;
                _gcdTimer = 0;
            }
        }
    }
}
