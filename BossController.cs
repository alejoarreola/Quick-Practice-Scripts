using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetHealth()
    {
        GetComponent<HealthManager>().SetMaxHealth(100);
        GetComponent<HealthManager>().SetCurrentHealth(50);
    }
}
