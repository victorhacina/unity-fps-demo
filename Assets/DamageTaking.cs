using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTaking : MonoBehaviour
{

    public int maxHealth = 3;
    public float respawnDelay = 2.0f;

    int _currentHealth;
    int currentHealth {
        get {
            return _currentHealth;
        }

        set { _currentHealth = value;
            if (_currentHealth <= 0) {
                Die();
            }
        }
    }

    public void Die() {
        var respawner = new GameObject("Respawner for:" + gameObject.name)
            .AddComponent<Respawner>();

        respawner.target = gameObject;
        respawner.delay = respawnDelay;

        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount = 1) {
        currentHealth -= amount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
