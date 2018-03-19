using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
	public Sprite Art;
	
	public int Health;
	public int Money;
	public int Defaultspeed;

}
