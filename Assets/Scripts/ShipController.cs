using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon;

public class ShipController : Photon.PunBehaviour{

    public float AccelerateForce;
    public float DecelerateForce;
    public float torqueForce;

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    public GameObject shot;
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public Transform shotSpawn3;
    public Transform shotSpawn4;
    public float fireRate;
    public float speed;
    private float nextFire;

    private float slowTime;
    private string bulletType = "Bullet";

	public GameObject Spacefield;
	public GameObject SpacefieldFront;

	private float accelerateTime;
	private float prevAcceleration = 5;
	private float acceleratetAmount = 1;

    private bool ai = false;
    private float randomX;
    private float randomY;
    float change = 0f;

    // Use this for initialization
    void Start() {
		if (GetComponent<PhotonView>().GetComponent<IMAI>() != null)
        {
            ai =  true;
            randomX = Random.Range(-1f, 1f);
            randomY = Random.Range(-1f, 1f);
        }
    }

    // Update is called once per frame
    void Update() {

        if (!ai)
        {
            float x = Input.GetAxis("Accelerate") * Time.deltaTime * 10.0f * acceleratetAmount;
            var y = Input.GetAxis("Rotate") * Time.deltaTime * 150.0f;

            if (accelerateTime > 0)
            {
                accelerateTime -= Time.deltaTime;
                if (accelerateTime <= 0)
                {
                    SetAcceleration(prevAcceleration);
                    acceleratetAmount = 1;
                }
            }

            if (slowTime > 0)
            {
                slowTime -= Time.deltaTime;
                x = (float)(x * 0.65);
                y = (float)(y * 0.65);
            }
            transform.Rotate(0, 0, y);
            transform.Translate(0, x, 0);

            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                CmdFire1();
            }
            Camera.main.transform.eulerAngles = new Vector3(0, 0, 0);
            GetComponentsInChildren<Camera>()[1].transform.eulerAngles = new Vector3(0, 0, 0);
            RandomMatchmaker rm = GameObject.FindObjectOfType<RandomMatchmaker>();
            rm.playerPos = gameObject.transform.position;
        } else
        {
            if (change == 0)
            {
                if (Random.Range(0f, 1f) >= 0.25f)
                {
                    randomX = Random.Range(0.7f, 1f);
                } else
                {
                    randomX = Random.Range(-1f, 1f);
                }
                randomY = Random.Range(-1f, 1f);
                change = 100f;
            } else
            {
                change--;
            }

            float x = randomX * Time.deltaTime * 10.0f * acceleratetAmount;
            var y = randomY * Time.deltaTime * 150.0f;

            if (accelerateTime > 0)
            {
                accelerateTime -= Time.deltaTime;
                if (accelerateTime <= 0)
                {
                    SetAcceleration(prevAcceleration);
                    acceleratetAmount = 1;
                }
            }

            if (slowTime > 0)
            {
                slowTime -= Time.deltaTime;
                x = (float)(x * 0.65);
                y = (float)(y * 0.65);
            }
            transform.Rotate(0, 0, y);
            transform.Translate(0, x, 0);

            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                CmdFire1();
            }

            //Caused bug that player position affected by AI
            //RandomMatchmaker rm = GameObject.FindObjectOfType<RandomMatchmaker>();
            //rm.playerPos = gameObject.transform.position;
        }
    }

    void FixedUpdate() {
        Rigidbody rb = GetComponent<Rigidbody>();
        /*if (Input.GetButton("Accelerate")) {
            rb.AddForce(transform.up * AccelerateForce);
        }
        if (Input.GetButton("Decelerate")) {
            rb.AddForce(transform.up * DecelerateForce);
        }
        rb.AddTorque(transform.forward * Input.GetAxis("Rotate") * torqueForce * Time.deltaTime);
        */
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, xMin, xMax),
            Mathf.Clamp(rb.position.y, yMin, yMax)
		);

    }
		

    void CmdFire1()
    {
        if (this.bulletType == "Bullet")
        {
            var shot1 = (GameObject)PhotonNetwork.Instantiate("Bullet", shotSpawn1.position, shotSpawn1.rotation, 0);
            if (ai)
            {
                shot1.GetComponent<IMAIBULLET>().ai = true;
            }
            shot1.GetComponent<Rigidbody>().velocity = shot1.transform.up * 20 * acceleratetAmount;

            var shot2 = (GameObject)PhotonNetwork.Instantiate("Bullet", shotSpawn2.position, shotSpawn2.rotation, 0);
            if (ai)
            {
                shot2.GetComponent<IMAIBULLET>().ai = true;
            }
            shot2.GetComponent<Rigidbody>().velocity = shot2.transform.up * 20 * acceleratetAmount;
            
        }
        else if (this.bulletType == "Invisible Bullet")
        {
            var shot1 = (GameObject)PhotonNetwork.Instantiate("Invisible Bullet", shotSpawn1.position, shotSpawn1.rotation, 0);
            if (ai)
            {
                shot1.GetComponent<IMAIBULLET>().ai = true;
            }
            shot1.GetComponent<Rigidbody>().velocity = shot1.transform.up * 20 * acceleratetAmount;

            var shot2 = (GameObject)PhotonNetwork.Instantiate("Invisible Bullet", shotSpawn2.position, shotSpawn2.rotation, 0);
            if (ai)
            {
                shot2.GetComponent<IMAIBULLET>().ai = true;
            }
            shot2.GetComponent<Rigidbody>().velocity = shot2.transform.up * 20 * acceleratetAmount;
        }
        else if (this.bulletType == "Fire Bullet")
        {
            var shot3 = (GameObject)PhotonNetwork.Instantiate("Fire Bullet", shotSpawn3.position, shotSpawn3.rotation, 0);
            if (ai)
            {
                shot3.GetComponent<IMAIBULLET>().ai = true;
            }
            shot3.GetComponent<Rigidbody>().velocity = shot3.transform.up * 12 * acceleratetAmount;
        }
        else if (this.bulletType == "Ereki Bullet")
        {
            var shot4 = (GameObject)PhotonNetwork.Instantiate("Ereki Bullet", shotSpawn4.position, shotSpawn4.rotation, 0);
            if (ai)
            {
                shot4.GetComponent<IMAIBULLET>().ai = true;
            }
            shot4.GetComponent<Rigidbody>().velocity = shot4.transform.up * 30 * acceleratetAmount;
        } 
    }

    [PunRPC]
    public void SlowEffect(float time)
    {
		slowTime = time;
    }

    [PunRPC]
    public void setBullet(string bullet)
    {
        this.bulletType = bullet;
        if (bullet == "Fire Bullet")
        {
            this.fireRate = 2;
        } else
        {
            this.fireRate = 1;
        }
    }

    [PunRPC]
	public void RecoverSpeed()
	{
		var x = Input.GetAxis("Accelerate") * Time.deltaTime * 5.0f;
		var y = Input.GetAxis("Rotate") * Time.deltaTime * 100.0f;
		x = x * 2;
		y = y * 2;
		speed = speed * 2;
	}

	public void SetAcceleration(float acceleration) {
		acceleratetAmount = 1.5f;
		accelerateTime = 5f;
	}
}