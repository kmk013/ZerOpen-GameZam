using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Animal {

    private Image saturnImage;
    private Text saturnText;
    private float saturn = 100;

    private Image gasImage;
    private Image needleImage;
    private Image runImage;
    private float speed;

    public GameObject gas;
    public GameObject needle;

    private void Awake()
    {
        size = 2;
    }

    private void Start()
    {
        saturnImage = GameObject.Find("SaturnBar").GetComponent<Image>();
        saturnText = GameObject.Find("SaturnText").GetComponent<Text>();
        gasImage = GameObject.Find("GasButton").GetComponent<Image>();
        needleImage = GameObject.Find("NeedleButton").GetComponent<Image>();
        runImage = GameObject.Find("RunButton").GetComponent<Image>();

        GameManager.Instance.obj_list.Add(this.gameObject);
    }

    private void Update()
    {
        if (saturn <= 0)
            SceneManager.LoadScene(3);

        if (saturn / 100 > 2)
            size = (int)saturn / 100;
        
        saturnText.text = "X" + ((int)saturn / 100).ToString();
        saturnImage.fillAmount = saturn % 100 / 100;

        PlayerMove();
        ScalingPlayer();
        PlayerCommand();
    }

    private void FixedUpdate()
    {
        saturn -= 0.15f;

        if (gasImage.fillAmount < 1)
            gasImage.fillAmount += 0.005f;

        if (needleImage.fillAmount < 1)
            needleImage.fillAmount += 0.005f;

        if (runImage.fillAmount < 1)
            runImage.fillAmount += 0.01f;
    }

    private void LateUpdate()
    {
        LookAtMouse();
        CameraMove();
    }

    void PlayerCommand()
    {
        if (Input.GetKeyDown("1") && gasImage.fillAmount == 1)
            CreateGas();

        if (Input.GetMouseButtonDown(0) && needleImage.fillAmount == 1)
            CreateNeedle();

        if (Input.GetKeyDown("3") && runImage.fillAmount == 1)
            Run();
        else if (runImage.fillAmount == 1)
            speed = 240;

        if (Input.GetMouseButtonDown(1))
            MinusSaturn();
    }

    void CreateGas()
    {
        Instantiate(gas, transform.position, Quaternion.identity);
        gasImage.fillAmount = 0;
        saturn -= saturn * 0.1f + 10;
        GameObject.Find("GasSound").GetComponent<AudioSource>().Play();
    }

    void CreateNeedle()
    {
        Instantiate(needle, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        needleImage.fillAmount = 0;
        saturn -= 20;
        GameObject.Find("NeedleSound").GetComponent<AudioSource>().Play();
    }

    void Run()
    {
        speed *= 1.5f;
        runImage.fillAmount = 0;
        saturn -= saturn * 0.2f + 15;
        GameObject.Find("RunSound").GetComponent<AudioSource>().Play();
    }

    void MinusSaturn()
    {
        saturn -= 5;
    }

    void LookAtMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void PlayerMove()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(dx, dy, 0) * speed * Time.deltaTime;

        transform.localPosition += dir;
    }

    void CameraMove()
    {
        Camera.main.transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position, 5 * Time.deltaTime);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);

        if (Camera.main.transform.position.x > ((GameManager.Instance.mapSizeX / 2) - Camera.main.orthographicSize * 2))
            Camera.main.transform.position = new Vector3(((GameManager.Instance.mapSizeX / 2) - Camera.main.orthographicSize * 2), Camera.main.transform.position.y, Camera.main.transform.position.z);

        if (Camera.main.transform.position.x < ((-GameManager.Instance.mapSizeX / 2) + Camera.main.orthographicSize * 2))
            Camera.main.transform.position = new Vector3(((-GameManager.Instance.mapSizeX / 2) + Camera.main.orthographicSize * 2), Camera.main.transform.position.y, Camera.main.transform.position.z);

        if (Camera.main.transform.position.y > ((GameManager.Instance.mapSizeY / 2) - Camera.main.orthographicSize))
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, ((GameManager.Instance.mapSizeY / 2) - Camera.main.orthographicSize), Camera.main.transform.position.z);

        if (Camera.main.transform.position.y < ((-GameManager.Instance.mapSizeY / 2) + Camera.main.orthographicSize))
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, ((-GameManager.Instance.mapSizeY / 2) + Camera.main.orthographicSize), Camera.main.transform.position.z);

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Mob" && collider.gameObject.GetComponent<Animal>().size < size)
        {
            GameManager.Instance.obj_list.Remove(collider.gameObject);
            Destroy(collider.gameObject);
            MobSpawn.Instance.spawnCount++;

            saturn += ((float)collider.gameObject.GetComponent<Animal>().size / (float)size) * 100;
        }

        if(collider.gameObject.tag == "Mob" && collider.gameObject.GetComponent<Animal>().size >= size)
        {
            SceneManager.LoadScene(2);
        }

        if(collider.gameObject.name == "Hunter")
        {
            SceneManager.LoadScene(2);
        }
    }
}
