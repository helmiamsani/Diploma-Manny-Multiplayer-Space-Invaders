using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ReceiveDamage : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 10;

    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private string targetTag;

    [SerializeField]
    private int damageToTarget;

    [SerializeField]
    private bool destroyOnDeath;

    private Vector2 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = this.maxHealth;
        this.initialPosition = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == this.targetTag)
        {
            this.TakeDamage(damageToTarget);

            Destroy(collider.gameObject);
        }
    }

    void TakeDamage(int amount)
    {
        if (this.isServer)
        {
            this.currentHealth -= amount;
            if (this.currentHealth <= 0)
            {
                if (this.destroyOnDeath)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    this.currentHealth = this.maxHealth;
                }
            }
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        this.transform.position = this.initialPosition;
    }
}
