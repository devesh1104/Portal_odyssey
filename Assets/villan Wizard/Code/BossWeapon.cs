using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
  
	public int attackDamage = 20;
	public int enragedAttackDamage = 40;
	private Animator animator;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;

	public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D[] players = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);
		foreach (Collider2D playerCollider in players)
			{
				PlayerHealth playerHealth = playerCollider.GetComponent<PlayerHealth>();
				if (playerHealth != null)
				{
					playerHealth.TakeDamage(attackDamage);
				}
			}
	}

	public void EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<PlayerHealth>().TakeDamage(enragedAttackDamage);
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
