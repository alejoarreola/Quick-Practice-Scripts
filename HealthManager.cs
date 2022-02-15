using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    #region Public Variables
    public Slider HealthBarSlider;                                  // To reference the UI's health bar
    public Text HealthText;                                // To reference the health text box

    //Variables for damage indicator
    //public Image DamageImage;                                       // To reference the image asset for damage
    public float damageFlashSpeed = 1f;                             // Speed at which the damage image fades
    public Color damageFlashColor = new Color(1f, 0f, 0f, 0.1f);    // Color of damage image

    //variables for healing indicator
    //public Image HealImage;                                         // To reference the image asset for healing
    public float healFlashSpeed = 1f;                               // Speed at which the heal image fades
    public Color healFlashColor = new Color(1f, 0f, 0f, 0.1f);      // Color of heal image
    #endregion //public variables

    #region Private Variables
    int maxHealth = 10;                 // Max health 
    int minimumHealth = 0;              // Minimum health (0 by default)
    float currentHealth = 10;           // Current health (matches max by default)
    bool damaged = false;               // To be be True while damage is taken
    bool healed = false;                // To be True while healing is happening
    #endregion //private variables

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If there's a health slider, update its values
        if (HealthBarSlider != null) // Checks if variable is set to null, to avoid "null ref exception" if we don't need health slider bar (or forgot to link)
        {
            UpdateHealthBar();
        }

        // If there's a health text box, update its value
        if (HealthText != null)
        {
            UpdateHealthText("Int");
        }

        //DamageFlash();          // Checks if damage image should flash and, if so, flashes
        //HealFlash();            // Checks if heal image should flash and, if so, flashes
        LockMinMaxHealth();     // Checks if current health exceeds health contraints then adjusts as needed
    }

    // Sets max health value to the provided value (look into using C# "properties" later)
    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }

    // Returns the maximum possible health
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    //updates the current health value to the provided value
    public void SetCurrentHealth(int newCurrentHealth)
    {
        currentHealth = newCurrentHealth;
    }

    // Returns the current health value
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // Ensures health never goes over/under the max/min health
    public void LockMinMaxHealth()
    {
        // Stops health from going above/below min/max health
        currentHealth = Mathf.Clamp(currentHealth, minimumHealth, maxHealth);
    }

    // Subtracts the supplied damage amount from current health
    public void TakeDamage(int damagageAmount)
    {
        // Flags "damaged" as true so other functions know damage is taken
        damaged = true;

        // Only reduces health if there is health to reduce
        if (currentHealth > minimumHealth)
        {
            currentHealth -= damagageAmount;
        }
    }

    // Increases current health by the specified amount
    public void Heal(int healAmount)
    {
        // Flags "healed" as true so other functions know healing has occured
        healed = true; 

        // Only increases current health if not at max health
        if (currentHealth < maxHealth)
        {
            currentHealth += healAmount;
        }
    }

    public void UpdateHealthBar()
    {
        HealthBarSlider.maxValue = maxHealth;
        HealthBarSlider.value = currentHealth;
    }

    // Updates health text box to show current health
    // Chooses how to display the output, with options meant to make manage versatile from game to game
    public void UpdateHealthText(string howDisplay) 
    {
        if (howDisplay == "Int")
        {
            // Shows integer version of health
            HealthText.text = Math.Truncate(currentHealth).ToString();
        }

        else if (howDisplay == "Float")
        {
            // Shows float value of health
            HealthText.text = currentHealth.ToString();
        }

        else if (howDisplay == "PercentFloat")
        {
            // Displays float percentage of current health
            HealthText.text = ((currentHealth / maxHealth) * 100).ToString() + "%";
        }

        else if (howDisplay == "PercentInt")
        {
            // Displays percent of current health in integer value
            HealthText.text = Math.Truncate((currentHealth / maxHealth) * 100).ToString() + "%";
        }
        
        else
        {
            // Debug handling for what happens when the value is not one of the above options
            Debug.Log("The value supplied to HealthManager.UpdateHealthText is not one of the available options");
        }
    }

    #region Color Flash
    // Flashes a damage indicator and fades it over time
    public void DamageFlash()
{
    /* When the player takes damage, indicator will flash red
     * on first iteration through this function after being flagged as damaged, screen turns red
     * and damaged is set to false
     * each subsequent iteration through this function fades the red flash until completely gone
     * the length of flash is controlled by damageFlashSpeed
     * if function is called in Update(), this will happen on over time
     * if function is called manually, it will only fade during iterations of function call
     */

    if (damaged)
    {
         transform.GetComponent<Renderer>().material.color = damageFlashColor; // Apply flash color
    }

    else // Not the first loop as damaged
    {
        // Fade out color
        damageFlashColor = Color.Lerp(damageFlashColor, Color.clear, damageFlashSpeed * Time.deltaTime);
    }

    // Reset damaged flag
    damaged = false;
}

// Flash heal indicator and fades over time. Same functionality as DamageFlash
public void HealFlash()
{
    if (healed)
    {
        transform.GetComponent<Renderer>().material.color = healFlashColor; // Apply flash color
    }

    else // Not the first loop as healed
    {
        // Fade out color
        healFlashColor = Color.Lerp(healFlashColor, Color.clear, healFlashSpeed * Time.deltaTime);
    }

    // Reset damaged flag
    healed = false;
}
    #endregion

    /* Flashes a damage indicator and fades it over time
    public void DamageFlash()
    {
        /* When the player takes damage, indicator will flash red
         * on first iteration through this function after being flagged as damaged, screen turns red
         * and damaged is set to false
         * each subsequent iteration through this function fades the red flash until completely gone
         * the length of flash is controlled by damageFlashSpeed
         * if function is called in Update(), this will happen on over time
         * if function is called manually, it will only fade during iterations of function call
         

        if (damaged)
        {
            DamageImage.color = damageFlashColor; // Apply flash color
        }

        else // Not the first loop as damaged
        {
            // Fade out color
            DamageImage.color = Color.Lerp(DamageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }

        // Reset damaged flag
        damaged = false;
    }

    // Flash heal indicator and fades over time. Same functionality as DamageFlash
    public void HealFlash()
    {
        if (healed)
        {
            HealImage.color = healFlashColor; // Apply flash color
        }

        else // Not the first loop as healed
        {
            // Fade out color
            HealImage.color = Color.Lerp(HealImage.color, Color.clear, healFlashSpeed * Time.deltaTime);
        }

        // Reset damaged flag
        healed = false;
    }
    */

    #region Damage Over Time
    // Damage Over Time as specified by variables passed to it
    public void DamageOverTime(int damageAmount, int damageTime)
    {
        StartCoroutine(DamageOverTimeCoroutine(damageAmount, damageTime));
    }

    IEnumerator DamageOverTimeCoroutine(float damageAmount, float damageTime)
    {
        damaged = true;
        float amountDamaged = 0;
        float damagePerLoop = damageAmount / damageTime * Time.deltaTime;
        while (amountDamaged < damageAmount)
        {
            currentHealth -= damagePerLoop;
            Debug.Log(currentHealth.ToString());
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion

    #region Heal Over Time
    // Heal Over Time as specified by variables passed to it
    public void HealOverTime(int healAmount, int duration)
    {
        StartCoroutine(HealOverTimeCoroutine(healAmount, duration));
    }

    IEnumerator HealOverTimeCoroutine(float healAmount, float duration)
    {
        healed = true;
        float amountHealed = 0;
        float healPerLoop = healAmount / duration * Time.deltaTime;
        while (amountHealed < healAmount)
        {
            currentHealth += healPerLoop;
            amountHealed += healPerLoop;
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion
}
