using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {
    
    // GAME STARTED BOOL
    private bool _start = false;

    public bool StartGame
    {
        get { return _start; }
        set { _start = value; }
    }

    [Header("Wave button")]
    [SerializeField] private Button _nextWaveBtn;
    
    [Header("Display Text")]
    [SerializeField] private GameObject _currencyTxt;
    [SerializeField] private GameObject _lifeTxt;
    [SerializeField] private GameObject _waveTxt;

    private int _currency;
    private int _life;
    private int _wave;

    // Tower Stats
    [Header("Tower UI")]
    [SerializeField] private GameObject _towerTypeText;
    [SerializeField] private GameObject _damageText;
    [SerializeField] private GameObject _firerateText;
    [SerializeField] private GameObject _slowText;
    [SerializeField] private GameObject _poisonText;
    [SerializeField] private GameObject _towerStatsUi;
    [SerializeField] private Button _sellTowerBtn;
    [SerializeField] private Button _upgradeBtn;
    [SerializeField] private GameObject _sellPriceText;
    [SerializeField] private GameObject _upgradePriceText;

    // TOWER BUILD BUTTONS
    [Header("Tower Build Buttons")]
    [SerializeField] private Button _basicTowerTestBtn;
    [SerializeField] private Button _canonTowerTestBtn;
    
    // MANABAR
    [Header("Player Manabar")]
    [SerializeField]private Image _manaBar;
    [SerializeField] private GameObject _manaBarText;
    
    // ABILITIES
    [Header("Ability Images(gameobjects)")]
    [SerializeField] private Image _leftClickIcon;
    [SerializeField] private Image _rightClickIcon;
    [SerializeField] private Image _firstAbilityIcon;
    
    // ABILITIES SPRITES
    [Header("Melee")]
    [SerializeField] private Sprite _leftClickSpriteMelee;
    [SerializeField] private Sprite _rightClickSpriteMelee;
    [SerializeField] private Sprite _firstAbilitySpriteMelee;
    [Header("Ranged")]
    [SerializeField] private Sprite _leftClickSpriteRanged;
    [SerializeField] private Sprite _rightClickSpriteRanged;
    [SerializeField] private Sprite _firstAbilitySpriteRanged;
    
    // TOGGLE EFFECT
    [Header("Toggle Effect")] 
    [SerializeField] private GameObject _toggleEffect;
    
    // PAUSE MENU BUTTON
    [Header("Pause Menu")] 
    [SerializeField] private Button _pause;
    [SerializeField] private GameObject _pauseWindow;
    [SerializeField] private GameObject _settingsWindow;
   

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


    private void Start()
    {
        Currency = 500;
        Life = 500;

        _nextWaveBtn.onClick.AddListener(WaveManager.Instance.NextWave);

        _sellTowerBtn.onClick.AddListener(TowerManager.Instance.SellTower);

        _upgradeBtn.onClick.AddListener(TowerManager.Instance.UpgradeTower);

        _basicTowerTestBtn.onClick.AddListener(BasicTowerTestMetode);

        _canonTowerTestBtn.onClick.AddListener(CanonTowerTestMetode);
        
        _pause.onClick.AddListener(Pause);
        
        _gcd = 0.5f;

        _waveTxt.GetComponent<TextMeshProUGUI>().text = "Current Wave: 0";
    }

    private void Update()
    {
        if (_start)
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
    }
    
    // PAUSE MENU
    public void Pause()
    {
        if (_pauseWindow.activeInHierarchy || _settingsWindow.activeInHierarchy)
        {
            _pauseWindow.SetActive(false);
            _settingsWindow.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            _pauseWindow.SetActive(true);
            Time.timeScale = 0;
            CameraZoom.Instance.Zoom = false;
        }
    }
    
    public void ClassIcons()
    {
        switch (PlayerController.Instance.GetClass)
        {
            case PlayerController.Class.Melee:
                _leftClickIcon.GetComponent<Image>().sprite = _leftClickSpriteMelee;
                _leftClickIcon.transform.parent.GetComponent<Image>().sprite = _leftClickSpriteMelee;
                _rightClickIcon.GetComponent<Image>().sprite = _rightClickSpriteMelee;
                _rightClickIcon.transform.parent.GetComponent<Image>().sprite = _rightClickSpriteMelee;
                _firstAbilityIcon.GetComponent<Image>().sprite = _firstAbilitySpriteMelee;
                _firstAbilityIcon.transform.parent.GetComponent<Image>().sprite = _firstAbilitySpriteMelee;
                break;
            case PlayerController.Class.Ranged:
                _leftClickIcon.GetComponent<Image>().sprite = _leftClickSpriteRanged;
                _leftClickIcon.transform.parent.GetComponent<Image>().sprite = _leftClickSpriteRanged;
                _rightClickIcon.GetComponent<Image>().sprite = _rightClickSpriteRanged;
                _rightClickIcon.transform.parent.GetComponent<Image>().sprite = _rightClickSpriteRanged;
                _firstAbilityIcon.GetComponent<Image>().sprite = _firstAbilitySpriteRanged;
                _firstAbilityIcon.transform.parent.GetComponent<Image>().sprite =
                    _firstAbilitySpriteRanged;
                break;
                    
        }
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
            _currencyTxt.GetComponent<TextMeshProUGUI>().text = value.ToString();
            
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
            _lifeTxt.GetComponent<TextMeshProUGUI>().text = value.ToString();
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
            _waveTxt.GetComponent<TextMeshProUGUI>().text = "Current Wave: " + value;
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
        // TOWER TYPE
        _towerTypeText.GetComponent<TextMeshProUGUI>().text = tc.TowerType + " (level " + tc.Level.ToString() + ")";
        // DAMAGE
        _damageText.GetComponent<TextMeshProUGUI>().text = "Dmg: " + tc.Damage;
        // FIRE RATE
        if (1 / tc.AttackCooldown < 1)
        {
            _firerateText.GetComponent<TextMeshProUGUI>().text = "Firerate: " + "0" + (1 / tc.AttackCooldown).ToString("#.##") + "/s";
        }
        else
        {
            _firerateText.GetComponent<TextMeshProUGUI>().text = "Firerate: " + (1 / tc.AttackCooldown).ToString("#.##") + "/s";
        }
        // SLOW
        if (tc.Slow <= 0)
        {
            _slowText.GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            _slowText.GetComponent<TextMeshProUGUI>().text = "Slow: " + tc.Slow*100 + "%";
        }
        // POISON
        if (tc.Poison <= 0)
        {
            _poisonText.GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            _poisonText.GetComponent<TextMeshProUGUI>().text = "Poison: " + tc.Poison;
        }
        
        _sellPriceText.GetComponent<TextMeshProUGUI>().text = "$" + (tc.TotalPrice / 2);
        _upgradePriceText.GetComponent<TextMeshProUGUI>().text = "$" + tc.UpgradePrice;
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

            if (PlayerController.Instance.Mana >= PlayerController.Instance.ScatterShotCost)
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
            _firstAbilityIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.ScatterShotCost ? 1 : 0;
        }

		
        // MANABAR
        if (!NextWaveBtn.gameObject.activeInHierarchy && PlayerController.Instance.Mana <= PlayerController.Instance.MaxMana)
        {
            PlayerController.Instance.Mana += 5f * Time.deltaTime;
        }
        _manaBar.fillAmount = PlayerController.Instance.Mana / PlayerController.Instance.MaxMana;
        ToStringManabar(PlayerController.Instance.Mana, PlayerController.Instance.MaxMana);
        
        // TOGGLE
        _toggleEffect.SetActive(PlayerController.Instance.OrbitingSwordBool);
    }

    private void ToStringManabar(float mana, float maxMana)
    {
        Mathf.Floor(mana);
        if (mana <= 0)
        {
            _manaBarText.GetComponent<TextMeshProUGUI>().text = "0/" + maxMana;
        }
        else
        {
          _manaBarText.GetComponent<TextMeshProUGUI>().text = mana.ToString("#") + "/" + maxMana;
        }
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
