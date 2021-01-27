using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public int maxHealth = 50;
    [SyncVar]
    public int currentHealth = 50;

    [SyncVar]
    public float moveSpeed = 10f;
    [SyncVar]
    public float jumpForce = 20f;
    [SyncVar]
    public float rotationSpeed = 10f;
    public CharacterController controller;

    [SyncVar]
    private Vector3 moveDirection;
    public LayerMask groundMask;

    public float gravityScale = 1f;
    public GameObject playerCamera;
    Transform cameraProp;
    public GameObject playerMinmap;
    public float lerpFactor;

    public Animator animator;
    public AudioSource audioSource;

    public GameObject quadCross;
    private MeshRenderer quadCrossAffichage;

    [SyncVar]
    public bool isDriving;
    private GameObject boat;
    private Vector3 distanceBoat;
    [SyncVar]
    public float boatVitesse = 2;
    [SyncVar]
    public float boatRotate = 0.5f;
    private bool canShoot;
    public GameObject CannonBallPrefab;
    public GameObject trashInstant;

    public HealthBar healthBar;
    public WeaponAttack weapon;

    public GameObject chest;

    private Vector3 startPoint;

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        cameraProp = playerCamera.transform.parent;

        // TEST
        UnlockSword();
    }

    // Start is called before the first frame update
    void Start()
    {
        isDriving = false;
        canShoot = true;
        controller = GetComponent<CharacterController>();

        //crossIcon minimap
        quadCrossAffichage = quadCross.GetComponent<MeshRenderer>();
        quadCrossAffichage.enabled = false;

        chest.SetActive(false);

        startPoint = transform.position;

        if (this.isLocalPlayer)
        {
            currentHealth = maxHealth;
            playerCamera.SetActive(true);
            playerMinmap.SetActive(true);
        } else
        {
            playerCamera.SetActive(false);
            playerMinmap.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            if (!isDriving && !PauseMenu.gameIsPaused)
            {

                Vector3 right = cameraProp.right;
                Vector3 forward = cameraProp.forward;

                Vector3 move = (((forward - new Vector3(0, forward.y, 0)) * z) + ((right - new Vector3(0, right.y, 0)) * x)) * moveSpeed * Time.deltaTime;
                transform.GetChild(0).transform.LookAt(Vector3.Lerp(((move.normalized) + transform.GetChild(0).transform.position), transform.GetChild(0).transform.position, lerpFactor));


                moveDirection = new Vector3(0, moveDirection.y, 0) + move;

                if (controller.isGrounded)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        moveDirection.y = jumpForce;
                    }
                }

                moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale) * Time.deltaTime;
                controller.Move(moveDirection);

                if (x != 0 || z != 0) animator.SetBool("walking", true);
                else animator.SetBool("walking", false);

                if (Input.GetKey(KeyCode.G))
                {
                    quadCrossAffichage.enabled = true;
                    quadCross.transform.parent = null;
                }

                if(Input.GetMouseButtonDown(0)) 
                {
                    animator.SetTrigger("attack");
                }

                if(Input.GetKey(KeyCode.LeftShift))
                {
                    animator.SetBool("running", true);
                    moveSpeed = 40;
                } else
                {
                    animator.SetBool("running", false);
                    moveSpeed = 20;
                }
            }
            else
            {
                if (boat != null)
                {                   
                    boat.transform.Rotate(new Vector3(0, x * rotationSpeed * boatRotate * Time.deltaTime, 0));
                    transform.rotation = boat.transform.rotation;
                    boat.transform.Translate(new Vector3(0, 0, z * moveSpeed * boatVitesse * Time.deltaTime));
                    transform.position = boat.transform.position + distanceBoat;
                    if (Input.GetKey(KeyCode.Space) && canShoot)
                    {
                        StartCoroutine("Shoot");
                    }
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    isDriving = false;
                    transform.GetChild(0).gameObject.SetActive(true);
                }
            }
           
        }
        if (Input.GetKey(KeyCode.H))
        {
            quadCrossAffichage.enabled = true;
        }
        if (Input.GetKey(KeyCode.P))
        {
            bool res = estAuBonEndroit();
            if (res)
            {
                if (this.isLocalPlayer)
                {
                    chest.transform.position = chest.transform.position + new Vector3(0, -0.4f, 0);
                    chest.transform.eulerAngles = new Vector3(0, chest.transform.eulerAngles.y + 270, 0);
                    chest.SetActive(true);
                    chest.transform.parent = null;
                    GameObject canvasFin = GameObject.Find("Canvas").transform.GetChild(1).GetChild(1).gameObject;
                    canvasFin.SetActive(true);
                }
                else
                {
                    GameObject canvasFin = GameObject.Find("Canvas").transform.GetChild(1).GetChild(2).gameObject;
                    canvasFin.SetActive(true);
                }
            }
        }
        
    }

    void TakeDamage(int damages)
    {
        Debug.Log("Lose " + damages + " health");
        if(currentHealth - damages > 0)
        {
            currentHealth -= damages;
        } else
        {
            currentHealth = 0;
            Respawn();
        }
        healthBar.UpdateHealth();
    }

    public void UnlockSword()
    {
        weapon.gameObject.SetActive(true);
    }

    public void Attack(PlayerController playerController)
    {
        playerController.TakeDamage(10);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Boat")
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                isDriving = true;
                boat = other.gameObject;
                transform.GetChild(0).gameObject.SetActive(false);
                distanceBoat = transform.position - boat.transform.position;

            }
        }

        if (other.tag == "Water")
        {
            Debug.Log("Water");
            Respawn();
        }
    }

    private bool estAuBonEndroit()
    {
        float playerX = this.transform.position.x;
        float playerZ = this.transform.position.z;
        Transform crossElement = GameObject.Find("CrossElement").transform;

        for (int i = 0; i < crossElement.childCount; i++) {
            GameObject firstCross = crossElement.GetChild(i).gameObject;
            float firstX = firstCross.transform.position.x;
            float firstZ = firstCross.transform.position.z;
            int precision = 10;
            if ((playerX > firstX - precision) && (playerX < firstX + precision) && (playerZ > firstZ - precision) && (playerZ < firstZ + precision))
                return true;
        }

        /*GameObject secondCross = GameObject.Find("CrossElement").transform.GetChild(1).gameObject;
        float secondX = secondCross.transform.position.x;
        float secondZ = secondCross.transform.position.z;
        if ((playerX > secondX - precision) && (playerX < secondX + precision) && (playerZ > secondZ - precision) && (playerZ < secondZ + precision))
            return true;*/

        return false;
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        Vector3 position = boat.transform.position + boat.transform.forward.normalized * 57 + boat.transform.up.normalized * 4;

        GameObject cannonBall = Instantiate(CannonBallPrefab, trashInstant.transform);
        cannonBall.transform.position = position;
        Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddForce(boat.transform.forward.normalized * 80 + boat.transform.forward.normalized * 0.1f, ForceMode.Impulse);
        Destroy(cannonBall, 5f);
        yield return new WaitForSeconds(.3f);
        canShoot = true;
    }

    //NE TELEPORTE QUE SI ON APPUYE SUR "ECHAP" !!!
    private void Respawn()
    {
        Debug.Log("Respawn");
        Debug.Log(startPoint);
        transform.position = startPoint;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void unlockMap()
    {
        Debug.Log("alo");
        quadCrossAffichage.enabled = true;
    }
}
