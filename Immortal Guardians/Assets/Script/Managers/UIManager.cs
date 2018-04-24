using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : Singleton<UIManager> {
  
    [Header("Wave button")]
    [SerializeField] private Button _nextWaveBtn;
    
    [Header("Display Text")]
    [SerializeField] private GameObject _lifeTxt;
    [SerializeField] private GameObject _currencyTxt;
    [SerializeField] private GameObject _enemyCountTxt;
    [SerializeField] private GameObject _waveTxt;
    
    private int _currency;
    private int _life;
    private int _enemyCount;
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
    
    // UNDO TOWER BUTTON
    [Header("Undo Tower Button")]
    [SerializeField] private Button _undoTower;

    // TOWER BUILD BUTTONS
    [Header("Tower Build Buttons")]
    [SerializeField] private GameObject _towerButtons;
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
    [SerializeField] private Image _secondAbilityIcon;
    [SerializeField] private Image _thirdAbilityIcon;
    [SerializeField] private Image _forthAbilityIcon;
    
    // ABILITIES SPRITES
    [Header("Melee")]
    [SerializeField] private Sprite _leftClickSpriteMelee;
    [SerializeField] private Sprite _rightClickSpriteMelee;
    [SerializeField] private Sprite _firstAbilitySpriteMelee;
    [SerializeField] private Sprite _secondAbilitySpriteMelee;
    [SerializeField] private Sprite _thirdAbilitySpriteMelee;
    [SerializeField] private Sprite _forthAbilitySpriteMelee;
    
    [Header("Ranged")]
    [SerializeField] private Sprite _leftClickSpriteRanged;
    [SerializeField] private Sprite _rightClickSpriteRanged;
    [SerializeField] private Sprite _firstAbilitySpriteRanged;
    [SerializeField] private Sprite _secondAbilitySpriteRanged;
    [SerializeField] private Sprite _thirdAbilitySpriteRanged;
    [SerializeField] private Sprite _forthAbilitySpriteRanged;
    
    
    // TOGGLE EFFECT
    [Header("Toggle Effect")] 
    [SerializeField] private GameObject _toggleEffect;
    
    // PAUSE MENU BUTTON
    [Header("Pause Menu")] 
    [SerializeField] private Button _pause;
    [SerializeField] private GameObject _pauseWindow;
    [SerializeField] private GameObject _settingsWindow;

    // HELP MENU
    [Header("Help Menu")]
    [SerializeField] private GameObject _helpWindow;
    [SerializeField] private GameObject _uiHelp;
    
    // END SCREEN
    [Header("ENDSCREEN")] 
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _loseText;
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _quitBtn;
    
    //
    [Header("Grid")]
    [SerializeField] private GameObject _grid;

    public Image ManaBar
    {
        get { return _manaBar; }
        set { _manaBar = value; }
    }

    private float _manaPerSecond;

    public float ManaPerSecond
    {
        get { return _manaPerSecond; }
        set { _manaPerSecond = value; }
    }

    private bool _canBasicAttack = true,
        _canGCDAttack = true,
        _canGoldOverTime = true,
        _canTrap = true,
        _canBuff = true,
        _canTurretMode = true,
        _canPull = true,
        _canLightningSword = true;


    private float _basicAttackTimer,
        _gcdTimer,
        _gcd,
        _goldTimer,
        _goldCooldown,
        _trapTimer,
        _trapCooldown,
        _buffTimer,
        _buffCooldown,
        _turretTimer,
        _turretCooldown,
        _pullTimer,
        _pullCooldown,
        _lSwordTimer,
        _lSwordCooldown;

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

    public float Gcd
    {
        get { return _gcd; }
        set { _gcd = value; }
    }

    public bool CanTrap
    {
        get { return _canTrap; }
        set { _canTrap = value; }
    }

    public bool CanBuff
    {
        get { return _canBuff; }
        set { _canBuff = value; }
    }

    public bool CanTurretMode
    {
        get { return _canTurretMode; }
        set { _canTurretMode = value; }
    }

    public bool CanPull
    {
        get { return _canPull; }
        set { _canPull = value; }
    }

    public bool CanLightningSword
    {
        get { return _canLightningSword; }
        set { _canLightningSword = value; }
    }


    private void Start()
    {
        Currency = 150;
        Life = 50;

        _manaPerSecond = 5f;

        _nextWaveBtn.onClick.AddListener(WaveManager.Instance.NextWave);

        _sellTowerBtn.onClick.AddListener(TowerManager.Instance.SellTower);

        _upgradeBtn.onClick.AddListener(TowerManager.Instance.UpgradeTower);
        
        _undoTower.onClick.AddListener(TowerManager.Instance.UndoTower);

        _basicTowerTestBtn.onClick.AddListener(BasicTowerTestMetode);

        _canonTowerTestBtn.onClick.AddListener(CanonTowerTestMetode);
        
        _pause.onClick.AddListener(Pause);
        
        
        
        _gcd = 0.5f;
        _goldCooldown = 5;
        _trapCooldown = 30;
        _buffCooldown = 60;
        _turretCooldown = 120;

        _pullCooldown = 60;
        _lSwordCooldown = 5;
        
        _enemyCountTxt.GetComponent<TextMeshProUGUI>().text = "0";
        _waveTxt.GetComponent<TextMeshProUGUI>().text = "Current Wave: 0";
    }

    private void Update()
    {
        if (GManager.Instance.GameStarted)
        {
            // Fill manabar
            UiFillBars();
        
            // gcd timer
            Timers();
            
            GoldOverTime();
        }  
    }
    
    // PAUSE MENU
    public void Pause()
    {
        if (_pauseWindow.activeInHierarchy || _settingsWindow.activeInHierarchy
            || _helpWindow.activeInHierarchy || _uiHelp.activeInHierarchy)
        {
            _pauseWindow.SetActive(false);
            _settingsWindow.SetActive(false);
            StartGame();
        }
        else
        {
            _pauseWindow.SetActive(true);
            StopGame(); 
        }
    }
    
    // HELP MENU
    public void OpenHelp()
    {
        _helpWindow.SetActive(true);
        StopGame();
    }

    public void CloseHelp()
    {
        if (_helpWindow.activeInHierarchy || _uiHelp.activeInHierarchy)
        {
            _helpWindow.SetActive(false);
            _uiHelp.SetActive(false);
            StartGame();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
    }

    
    
    public void Quit()
    {
        Application.Quit();
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
                
                _secondAbilityIcon.GetComponent<Image>().sprite = _secondAbilitySpriteMelee;
                _secondAbilityIcon.transform.parent.GetComponent<Image>().sprite = _secondAbilitySpriteMelee;
                
                _thirdAbilityIcon.GetComponent<Image>().sprite = _thirdAbilitySpriteMelee;
                _thirdAbilityIcon.transform.parent.GetComponent<Image>().sprite = _thirdAbilitySpriteMelee;
                
                _forthAbilityIcon.GetComponent<Image>().sprite = _forthAbilitySpriteMelee;
                _forthAbilityIcon.transform.parent.GetComponent<Image>().sprite = _forthAbilitySpriteMelee;
                
                break;
            
            case PlayerController.Class.Ranged:
                _leftClickIcon.GetComponent<Image>().sprite = _leftClickSpriteRanged;
                _leftClickIcon.transform.parent.GetComponent<Image>().sprite = _leftClickSpriteRanged;
                
                _rightClickIcon.GetComponent<Image>().sprite = _rightClickSpriteRanged;
                _rightClickIcon.transform.parent.GetComponent<Image>().sprite = _rightClickSpriteRanged;
                
                _firstAbilityIcon.GetComponent<Image>().sprite = _firstAbilitySpriteRanged;
                _firstAbilityIcon.transform.parent.GetComponent<Image>().sprite =
                    _firstAbilitySpriteRanged;
                
                _secondAbilityIcon.GetComponent<Image>().sprite = _secondAbilitySpriteRanged;
                _secondAbilityIcon.transform.parent.GetComponent<Image>().sprite = _secondAbilitySpriteRanged;
                
                _thirdAbilityIcon.GetComponent<Image>().sprite = _thirdAbilitySpriteRanged;
                _thirdAbilityIcon.transform.parent.GetComponent<Image>().sprite = _thirdAbilitySpriteRanged;
                
                _forthAbilityIcon.GetComponent<Image>().sprite = _forthAbilitySpriteRanged;
                _forthAbilityIcon.transform.parent.GetComponent<Image>().sprite = _forthAbilitySpriteRanged;
                
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
            if (BuildingMode.Instance.Tower != null)
            {
                BuildingMode.Instance.Tower.SetActive(false);
            }
            BuildingMode.Instance.Tower = null;
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
            if (BuildingMode.Instance.Tower != null)
            {
                BuildingMode.Instance.Tower.SetActive(false);
            }
            BuildingMode.Instance.Tower = null;
            GManager.Instance.TowerToBuild = "CanonTower";
        }    
    }
    
    public int Life
    {
        get{ return _life; }

        set
        {
            _life = value;
            if (_life <= 0)
            {
                _lifeTxt.GetComponent<TextMeshProUGUI>().text = 0.ToString();
                return;
            }
            _lifeTxt.GetComponent<TextMeshProUGUI>().text = _life.ToString();
        }
    }

    public int Currency
    {
        get{ return _currency; }

        set
        {
            if (value > 99999)
            {
                _currency = 99999;
            }
            else if (value <= 0)
            {
                _currency = 0;
            }
            else
            {
                _currency = value;
            }
            _currencyTxt.GetComponent<TextMeshProUGUI>().text = _currency.ToString();
            
        }
    }

    public void SetGrid(bool b)
    {
        if (b)
        {
            _grid.SetActive(true);
            _towerButtons.SetActive(true);
            GManager.Instance.TowerMode = true;
        }
        else
        {
            _grid.SetActive(false);
            _towerButtons.SetActive(false);
            GManager.Instance.TowerMode = false;
            GManager.Instance.BuildMode = false;
            GManager.Instance.DeselectTower();
        }
    }

    public void EnemyCount(int spawned, int died)
    {
        _enemyCount = spawned - died;
        _enemyCountTxt.GetComponent<TextMeshProUGUI>().text = _enemyCount.ToString();
    }


    public int Wave
    {
        set
        {
            _wave = value;
            if (value == 10)
            {
                _waveTxt.GetComponent<TextMeshProUGUI>().text = "Final Wave";
                return;
            }
            _waveTxt.GetComponent<TextMeshProUGUI>().text = "Current Wave: " + value;
        }
    }

    public Button NextWaveBtn
    {
        get{ return _nextWaveBtn; }
    }

    public GameObject TowerStatsUi
    {
        get{ return _towerStatsUi; }

        set{ _towerStatsUi = value; }
    }
    
    public Button UpgradeBtn
    {
        get { return _upgradeBtn; }
        set { _upgradeBtn = value; }
    }
    
    public Button UndoTower
    {
        get { return _undoTower; }
        set { _undoTower = value; }
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

        _sellPriceText.GetComponent<TextMeshProUGUI>().text = "$" + Mathf.FloorToInt(tc.TotalPrice * 0.75f);
        _upgradePriceText.GetComponent<TextMeshProUGUI>().text = "$" + tc.UpgradePrice;
    }

    public void SelectTower()
    {
        if(!(TowerManager.Instance.CurrentTower.GetComponent<TowerController>().Level > 3))
        {
            _upgradeBtn.transform.gameObject.SetActive(true);
        }
        else
        {
            _upgradeBtn.transform.gameObject.SetActive(false);
        }
        
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

        if (!_canBuff)
        {
            _firstAbilityIcon.fillAmount = (_buffTimer / _buffCooldown);
        }

        if (!_canTrap)
        {
            _thirdAbilityIcon.fillAmount = (_trapTimer / _trapCooldown);
        }

        if (!_canTurretMode)
        {
            _forthAbilityIcon.fillAmount = (_turretTimer / _turretCooldown);
        }

        if (!_canPull)
        {
            _thirdAbilityIcon.fillAmount = (_pullTimer / _pullCooldown);
        }

        if (!_canLightningSword && !PlayerController.Instance.LSwordActive)
        {
            _forthAbilityIcon.fillAmount = (_lSwordTimer / _lSwordCooldown);
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

            if (_canBuff)
            {
                if (PlayerController.Instance.Mana >= PlayerController.Instance.FirstAbilityCost)
                {
                    _firstAbilityIcon.fillAmount = (_gcdTimer / _gcd);
                }
                else
                {
                    _firstAbilityIcon.fillAmount = 0; 
                }
            }
            
            if (PlayerController.Instance.Mana >= PlayerController.Instance.SecondAbilityCost)
            {
                _secondAbilityIcon.fillAmount = (_gcdTimer / _gcd);
            }
            else
            {
                _secondAbilityIcon.fillAmount = 0; 
            }
            
            if (PlayerController.Instance.GetClass == PlayerController.Class.Ranged)
            {
                if (_canTrap)
                {
                    if (PlayerController.Instance.Mana >= PlayerController.Instance.ThirdAbilityCost)
                    {
                        _thirdAbilityIcon.fillAmount = (_gcdTimer / _gcd);
                    }
                    else
                    {
                        _thirdAbilityIcon.fillAmount = 0; 
                    } 
                }

                if (_canTurretMode)
                {
                    if (PlayerController.Instance.Mana >= PlayerController.Instance.ForthAbilityCost)
                    {
                        _forthAbilityIcon.fillAmount = (_gcdTimer / _gcd);
                    }
                    else
                    {
                        _forthAbilityIcon.fillAmount = 0; 
                    }
                }
            }
            else if (PlayerController.Instance.GetClass == PlayerController.Class.Melee)
            {
                if (_canPull)
                {
                    if (PlayerController.Instance.Mana >= PlayerController.Instance.ThirdAbilityCost)
                    {
                        _forthAbilityIcon.fillAmount = (_gcdTimer / _gcd);
                    }
                    else
                    {
                        _thirdAbilityIcon.fillAmount = 0; 
                    }
                }

                if (_canLightningSword)
                {
                    if (PlayerController.Instance.Mana >= PlayerController.Instance.ForthAbilityCost)
                    {
                        _forthAbilityIcon.fillAmount = (_gcdTimer / _gcd);
                    }
                    else
                    {
                        _forthAbilityIcon.fillAmount = 0;
                    }
                }

                if (PlayerController.Instance.LSwordActive)
                {
                    _forthAbilityIcon.fillAmount = 1;
                }
            }            
        }
        else
        {    
            _rightClickIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.RightClickCost ? 1 : 0;

            if (_canBuff)
            {
                _firstAbilityIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.FirstAbilityCost ? 1 : 0;
            }
            
            if (PlayerController.Instance.OrbitingSwordBool || PlayerController.Instance.MultishotBool)
            {
                _secondAbilityIcon.fillAmount = 1;
            }
            else
            {
                _secondAbilityIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.SecondAbilityCost ? 1 : 0;
            }
            
            if (PlayerController.Instance.GetClass == PlayerController.Class.Ranged)
            {
                if (_canTrap)
                {
                    _thirdAbilityIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.ThirdAbilityCost ? 1 : 0;
                }

                if (_canTurretMode)
                {
                    _forthAbilityIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.ForthAbilityCost ? 1 : 0;
                }
            }
            else if (PlayerController.Instance.GetClass == PlayerController.Class.Melee)
            {
                if (_canPull)
                {
                    _thirdAbilityIcon.fillAmount = PlayerController.Instance.Mana >= PlayerController.Instance.ThirdAbilityCost ? 1 : 0;
                }

                if (_canLightningSword)
                {
                    _forthAbilityIcon.fillAmount =
                        PlayerController.Instance.Mana >= PlayerController.Instance.ForthAbilityCost ? 1 : 0;
                }
                
                if (PlayerController.Instance.LSwordActive)
                {
                    _forthAbilityIcon.fillAmount = 1;
                }
            }

            

            
        }

		
        // MANABAR
        if (!NextWaveBtn.gameObject.activeInHierarchy && PlayerController.Instance.Mana <= PlayerController.Instance.MaxMana)
        {
            PlayerController.Instance.Mana += _manaPerSecond * Time.deltaTime;
        }
        _manaBar.fillAmount = PlayerController.Instance.Mana / PlayerController.Instance.MaxMana;
        ToStringManabar(PlayerController.Instance.Mana, PlayerController.Instance.MaxMana);
        
        // TOGGLE
        if (PlayerController.Instance.GetClass == PlayerController.Class.Melee)
        {
            _toggleEffect.SetActive(PlayerController.Instance.OrbitingSwordBool);
        }
        else
        {
            _toggleEffect.SetActive(PlayerController.Instance.MultishotBool);
        }
        
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
        // ---- BASIC ATTACK COOLDOWN ----
        if (!_canBasicAttack)
        {
            _basicAttackTimer += Time.deltaTime;

            if(_basicAttackTimer >= _gcd)
            {
                _canBasicAttack = true;
                _basicAttackTimer = 0;
            }
        }
        // ---- GCD ----
        if (!_canGCDAttack)
        {
            _gcdTimer += Time.deltaTime;

            if (_gcdTimer >= _gcd)
            {
                _canGCDAttack = true;
                _gcdTimer = 0;
            }
        }
        // ---- GOLD GENERATION ----
        if (!_canGoldOverTime)
        {
            _goldTimer += Time.deltaTime;

            if (_goldTimer >= _goldCooldown)
            {
                _canGoldOverTime = true;
                _goldTimer = 0;
            }
        }

        // ---- RANGED ----
        if (!_canTrap)
        {
            _trapTimer += Time.deltaTime;

            if (_trapTimer >= _trapCooldown)
            {
                _canTrap = true;
                _trapTimer = 0;
            }
        }
        
        if (!_canTurretMode)
        {
            _turretTimer += Time.deltaTime;

            if (_turretTimer >= _turretCooldown)
            {
                _canTurretMode = true;
                _turretCooldown = 0;
            }
        }
        
        // ---- MELEE ----
        if (!_canPull)
        {
            _pullTimer += Time.deltaTime;

            if (_pullTimer >= _pullCooldown)
            {
                _canPull = true;
                _pullTimer = 0;
            }
        }
        
        if (!_canLightningSword && !PlayerController.Instance.LSwordActive)
        {
            _lSwordTimer += Time.deltaTime;

            if (_lSwordTimer >= _lSwordCooldown)
            {
                _canLightningSword = true;
                _lSwordTimer = 0;
            }
        }
        
        // ---- RANGED AND MELEE ----
        if (!_canBuff)
        {
            _buffTimer += Time.deltaTime;

            if (_buffTimer >= _buffCooldown)
            {
                _canBuff = true;
                _buffTimer = 0;
            }
        }
        
    }

    public IEnumerator LoseScreenFade()
    {
        _loseText.SetActive(true);
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 1.5f)
        {
            _loseText.GetComponent<TextMeshProUGUI>().color = new Color(1,0,0,i);
            _loseText.transform.localScale = new Vector3(1+i, 1+i, 0);
            yield return null;
        }
       
        _restartBtn.gameObject.SetActive(true);
        _quitBtn.gameObject.SetActive(true);
        StopGame();
    }
    
    public IEnumerator WinScreenFade()
    {
        _winText.SetActive(true);
        for (float i = 0; i < 1.0f; i += Time.deltaTime / 1.5f)
        {
            _winText.GetComponent<TextMeshProUGUI>().color = new Color(0,1,0,i);
            _winText.transform.localScale = new Vector3(1+i, 1+i, 0);
            yield return null;
        }
       
        _restartBtn.gameObject.SetActive(true);
        _quitBtn.gameObject.SetActive(true);
        StopGame();
    }

    public void GoldOverTime()
    {
        if (WaveManager.Instance.SpawnMode && _canGoldOverTime)
        {
            Currency += 3;
            _canGoldOverTime = false;
        }
       
    }
    
    private void StopGame()
    {
        Time.timeScale = 0;
        CameraZoom.Instance.Zoom = false;
        _nextWaveBtn.interactable = false;
        _basicTowerTestBtn.interactable = false;
        _canonTowerTestBtn.interactable = false;
        GManager.Instance.Paused = true;
        GManager.Instance.BuildMode = false;
        GManager.Instance.DeselectTower();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        CameraZoom.Instance.Zoom = true;
        _nextWaveBtn.interactable = true;
        _basicTowerTestBtn.interactable = true;
        _canonTowerTestBtn.interactable = true;
        GManager.Instance.Paused = false;
    }
}
