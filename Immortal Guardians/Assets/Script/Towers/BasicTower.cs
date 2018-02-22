using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour {

    private float range;
    private int damage;
    private float firerate;
    private int price;
    private int level;

    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    // Use this for initialization
    void Awake () {
        Range = 10f;
        damage = 10;
        firerate = 1f;
        GetComponent<TowerController>().AttackCooldown = firerate;
        price = 10;
        level = 1;
    }
	
	public void Upgrade()
    {
        
    }
}
