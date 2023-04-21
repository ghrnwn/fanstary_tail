using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Entity
{
	[Header("Effect")]
	public GameObject shotFX;
	public GameObject dieFX;

	public GameObject dieBeforeFX;
	public GameObject dieAfterFX;

	[Header("Object")]
	public GameObject bullet; 
	public GameObject target;

	[Header("ETC")]
	public Transform muzzle;

	public float shootingDelay = 5;
	public float aimheight = 1.2f;
	public float aimRadius = 50;
	
	private bool aim = false;
	private bool shoot = false;
	[SerializeField] private bool die = false;
	private bool dieAfter = false;

	private Rigidbody r;
	private Animator a;
	[SerializeField] private float dieTime = 10;

    private void Awake()
    {
		hp = maxHp;
		a = GetComponent<Animator>();
		r = GetComponent<Rigidbody>();
		target = GameObject.FindWithTag("Player");
    }
	private void Update()
	{
		if (!die)
		{
			CheckPlayer();
			if (hp == 0)
			{
				die = true;
				Destroy(transform.parent.gameObject);

				GameObject dieff = Instantiate(dieFX, transform.position, Quaternion.identity);
				Destroy(dieff, dieff.GetComponent<ParticleSystem>().main.duration);

				transform.parent = null;
				this.enabled = true;
				r.useGravity = true;
				r.constraints = RigidbodyConstraints.None;
				r.AddExplosionForce(500, transform.root.position, 1);

				GetComponent<Collider>().isTrigger = false;

				GameObject dieffbf = Instantiate(dieBeforeFX, transform);
				dieffbf.transform.localPosition = new Vector3(0, 0, 0);

				return;
			}
			if (aim == true) Aiming();
		}
        else
        {
			if (dieTime > 0) 
			{
				if (dieTime > 5)
				{
					r.AddTorque(Vector3.right * 1000 * Time.deltaTime);
					r.AddTorque(Vector3.up * 1000 * Time.deltaTime);
				}
				dieTime -= Time.deltaTime; 
			}
			else if (!dieAfter)
			{
				dieAfter = true;
				GameObject dieffAft = Instantiate(dieAfterFX, transform.position, Quaternion.identity);
				Destroy(dieffAft, dieffAft.GetComponent<ParticleSystem>().main.duration);
				Destroy(gameObject);
			}
        }
	}
	private void FixedUpdate()
	{
		if (!shoot && aim)
		{
			Invoke("Shot", shootingDelay);
			shoot = true;
		}
	}
    public void Aiming()
	{
		Vector3 delta = target.transform.position - transform.position;
		Vector3 cross = Vector3.Cross(transform.forward, delta);

		float angle = Vector3.Angle(transform.forward, delta);
		
		r.AddTorque(cross * angle * 1);
	}
	void Shot()
	{
		if (die || target.GetComponent<Entity>().hp == 0) return;
		shoot = false;
		a.SetTrigger("Shot");
		GameObject newShotFX = Instantiate(shotFX, muzzle);
		Destroy(newShotFX, 2); 
		GameObject bullet_ = Instantiate(bullet, muzzle.position, muzzle.rotation);
		bullet.GetComponent<BulletAttack>().owner = gameObject;
	}

	void CheckPlayer()
	{
		Debug.DrawRay(muzzle.position, Vector3.Normalize(target.transform.position + new Vector3(0, aimheight, 0) - muzzle.position) * aimRadius);
		RaycastHit hit;
		if (Physics.Raycast(muzzle.position, target.transform.position + new Vector3(0, aimheight, 0) - muzzle.position, out hit, aimRadius, 1)
		 && hit.collider.gameObject.CompareTag("Player"))
		{
			aim = true;
		}
		else aim = false;
	}
}
