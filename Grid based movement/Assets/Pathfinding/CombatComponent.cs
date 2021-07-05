using UnityEngine;

public class CombatComponent : MonoBehaviour
{
	public int health;
	public int damage;
	public int minAttackRange, maxAttackRange;
	public void Attack(CombatComponent target)
	{
		Debug.Log($"{gameObject.name} attacked {target.gameObject.name}!");
		target.Hit(damage);
	}

	public void Hit(int damage)
	{
		health -= damage;
	}
}
