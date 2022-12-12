using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicWeapon : MonoBehaviour
{
    public int maxClipSize = 8;
    private int ammoRemaining = 0;
    float maxRange = 100.0f;

    public bool playerController = true;
    public Text ammoRemainingLabel;

    public Transform shootPoint;

    public GameObject impactEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ammoRemaining = maxClipSize;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (ammoRemainingLabel != null)
        {
            ammoRemainingLabel.text = ammoRemaining.ToString();
        }
    }

    void Fire()
    {
        ammoRemaining -= 1;
        UpdateUI();

        RaycastHit hitInfo;

        var ray = new Ray(shootPoint.position, shootPoint.forward);
        bool hit = Physics.Raycast(ray, out hitInfo, maxRange);

        if (hit)
        {
            var impactEffect = Instantiate(impactEffectPrefab);
            impactEffect.transform.position = hitInfo.point;

            var direction = Vector3.Reflect(shootPoint.forward, hitInfo.normal);

            impactEffect.transform.forward = direction;

            Destroy(impactEffect, 4);

            var damage = hitInfo.collider.GetComponentInParent<DamageTaking>();

            if (damage != null) {
                damage.TakeDamage();
            }
        }

    }

    void Reload()
    {
        ammoRemaining = maxClipSize;
        UpdateUI();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerController == false) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (ammoRemaining > 0)
            {
                Fire();
            }
            else
            {
                Reload();
            }
        }


    }
}
