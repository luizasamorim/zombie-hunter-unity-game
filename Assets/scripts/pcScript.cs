using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class pcScript : MonoBehaviour
{
    private Rigidbody rbd;
    public float vel = 7;
    private Quaternion rotIni;
    public float velRotY;
    public float countY = 0;
    public GameObject camera;
    public LayerMask npcMask;
    public float distance;
    private AudioSource audio;
    private int pills;
    private int munition;
    public GameObject pillScore;
    public GameObject munitionScore;
    public GameObject livesScore;
    public LayerMask pillMask;
    public LayerMask munitionMask;
    private TMP_Text pillText;
    private TMP_Text munitionText;
    private static TMP_Text livesText;
    private Animator animator;
    public GameObject center;
    private static int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        rotIni = transform.localRotation;
        velRotY = 10;
        distance = 100;
        audio = GetComponent<AudioSource>();
        pills = 0;
        munition = 5;
        pillText = pillScore.GetComponent<TMP_Text>();
        munitionText = munitionScore.GetComponent<TMP_Text>();
        livesText = livesScore.GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float fbward = Input.GetAxis("Vertical"); //forward and backward
        float sideways = Input.GetAxis("Horizontal");

        //detectando movimento
        if (fbward == 0)
            animator.SetBool("walking", false); //parado
        else
            animator.SetBool("walking", true); //andando

        rbd.velocity = transform.TransformDirection(new Vector3(sideways * vel, rbd.velocity.y, fbward * vel));

        countY += Input.GetAxisRaw("Mouse X") * Time.deltaTime * velRotY;

        Quaternion rotY = Quaternion.AngleAxis(countY, Vector3.up);

        transform.localRotation = rotIni * rotY;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            audio.Play();
            RaycastHit hit;
            munition--;
            munitionText.text = "Munição: " + munition;

            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, distance, npcMask))
            {
                Destroy(hit.collider.gameObject);
            }
        }

        //pills
        RaycastHit pillHit;
        if (Physics.Raycast(center.transform.position, center.transform.forward, out pillHit, 2f, pillMask))
        {
            Destroy(pillHit.collider.gameObject);
            pills ++;
            pillText.text = "Pílulas: " + pills + "/16";
            if (pills == 16)
            {
                SceneManager.LoadSceneAsync(0);
            }
        }

        //munition
        RaycastHit munitionHit;
        if (Physics.Raycast(center.transform.position, center.transform.forward, out munitionHit, 2f, munitionMask))
        {
            Destroy(munitionHit.collider.gameObject);
            munition += 5;
            munitionText.text = "Munição: " + munition;
        }

    }

    public static int takeLife() {
        lives--;
        livesText.text = "Vidas: " + lives + "/3";
        return lives;
    }
}
