using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int playerID = 0;

    public GameObject healthBar;

    public GameObject dashBar;
    private float healthBarFullWidth;

    private float dashBarFullWidth;

    public float currentCooldown = 0f;

    private Rigidbody myRigidbody;

    public float movementForce;

    public float dashForce;

    public float maxDash;

    public Image image;

    public float cooldownAmmount = 1.0f;

    private int health;
    public int maxHealth = 100;

	void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        health = maxHealth;

        GetComponentInChildren<GrenadeLauncher>().PlayerID = playerID;

        healthBarFullWidth = healthBar.transform.localScale.x;

        dashBarFullWidth = dashBar.transform.localScale.x;
	}

    public void Damage(int damage)
    {
        health -= damage;

        healthBar.transform.localScale = new Vector3(healthBarFullWidth * ((float)health / maxHealth), healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if (health < 0) Destroy(gameObject);
    }
	
	void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis(string.Format("HorizontalP{0}", playerID)), Input.GetAxis(string.Format("VerticalP{0}", playerID)), .0f) * movementForce;
        
        Vector3 dashMovement = new Vector3(Input.GetAxis(string.Format("HorizontalP{0}", playerID)), Input.GetAxis(string.Format("VerticalP{0}", playerID)), .0f) * dashForce;

        myRigidbody.AddForce(movement * Time.deltaTime, ForceMode.Force);

        if (currentCooldown >= 0){
            currentCooldown -= Time.deltaTime;

            dashBar.transform.localScale = new Vector3(dashBarFullWidth * (1.0f - ((float)currentCooldown / cooldownAmmount)), dashBar.transform.localScale.y, dashBar.transform.localScale.z);
        }

        else if (Input.GetButtonDown(string.Format("DashP{0}", playerID )) == true){

            myRigidbody.AddForce(dashMovement, ForceMode.Force);
            currentCooldown = cooldownAmmount;

            //dashBar.transform.localScale = new Vector3(1.0f, Mathf.Clamp(currentCooldown / cooldownAmmount, .0001f, 1.0f), 1.0f);   
        }

        transform.localScale = new Vector3(1.0f, 1.0f, -Mathf.Sign(myRigidbody.velocity.x));
	}
}
