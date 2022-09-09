using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    // [SerializeField] public bool enemyIsAlive = true;
    // [SerializeField] public bool enemyIsDead = false;
    public bool deathCheckTrigger = false;

    [SerializeField] private Animator aiAnimations;
    private bool isHurtHasPlayed;

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        
        hitPoints -= damage;

        if(hitPoints == 0)
        {
            //enemyIsDead = true;
            GetComponent<Animator>().SetTrigger("killed" );
            Destroy(gameObject, 2f);
            deathCheckTrigger = true;
        }

        else if(hitPoints == 25 && !isHurtHasPlayed)
        {
            GetComponent<Animator>().SetTrigger("Hurt");
        }

    }

    public void DeathCheck(bool enemyIsDead)
    {
        Debug.Log("DeathCheck method is running");

        if (deathCheckTrigger)
        {
            enemyIsDead = true;
        }

        else if (!deathCheckTrigger)
        {
            //enemyIsDead = false;
            return;
        }
    }

    public void HurtCheck()
    {
        GetComponent<Animator>().ResetTrigger("Hurt");
        isHurtHasPlayed = true;
    }
}
