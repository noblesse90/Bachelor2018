using UnityEngine;

// Enemy Scriptable object

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
	public Sprite Art;
	public Color Color;
	
	public int Health;
	public int Money;
	public int Defaultspeed;
	public bool Boss;
}
