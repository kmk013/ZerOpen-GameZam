using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Animal {

    public Image saturnImage;
    public Text saturnText;
    private float saturn = 100;

    public Image gasImage;
    public Image needleImage;
    public Image runImage;
    private float speed;

    public GameObject gas;
    public GameObject needle;

    private void Awake()
    {
        size = 2;
        speed = GetComponent<PlayerMove>().speed;
    }

    private void Start()
    {
        Hunter.instance.objs_mob.Add(this.gameObject);
    }

    private void Update()
    {
        if (saturn <= 0)
            SceneManager.LoadScene(3);

        if (saturn / 100 > 2)
            size = (int)saturn / 100;

        ScalingPlayer();

        saturnText.GetComponent<Text>().text = "X" + ((int)saturn / 100).ToString();
        saturnImage.GetComponent<Image>().fillAmount = saturn % 100 / 100;

        saturn -= 2 * Time.deltaTime;

        if (Input.GetKeyDown("1") && gasImage.GetComponent<Image>().fillAmount == 1)
        {
            CreateGas();
            gasImage.GetComponent<Image>().fillAmount = 0;
            saturn -= saturn * 0.1f + 10;
            GameObject.Find("GasSound").GetComponent<AudioSource>().Play();
        }

        if (Input.GetMouseButtonDown(0) && needleImage.GetComponent<Image>().fillAmount == 1)
        {
            CreateNeedle();
            needleImage.GetComponent<Image>().fillAmount = 0;
            saturn -= 20;
            GameObject.Find("NeedleSound").GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown("3") && runImage.GetComponent<Image>().fillAmount == 1)
        {
            Run();
            runImage.GetComponent<Image>().fillAmount = 0;
            saturn -= saturn * 0.2f + 15;
            GameObject.Find("RunSound").GetComponent<AudioSource>().Play();
        }
        else if (runImage.GetComponent<Image>().fillAmount == 1)
        {
            GetComponent<PlayerMove>().speed = speed;
        }

        if (Input.GetMouseButtonDown(1))
        {
            MinusSaturn();
        }

        if (gasImage.GetComponent<Image>().fillAmount < 1)
        {
            gasImage.GetComponent<Image>().fillAmount += 0.1f * Time.deltaTime;
        }
        if (needleImage.GetComponent<Image>().fillAmount < 1)
        {
            needleImage.GetComponent<Image>().fillAmount += 0.1f * Time.deltaTime;
        }
        if (runImage.GetComponent<Image>().fillAmount < 1)
        {
            runImage.GetComponent<Image>().fillAmount += 0.2f * Time.deltaTime;
        }
    }

    void CreateGas()
    {
        Instantiate(gas, transform.position, Quaternion.identity);
    }

    void CreateNeedle()
    {
        Instantiate(needle, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    void Run()
    {
        GetComponent<PlayerMove>().speed *= 1.5f;
    }

    void MinusSaturn()
    {
        saturn -= 5;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Mob" && collider.gameObject.GetComponent<Animal>().size < size)
        {
            Hunter.instance.objs_mob.Remove(collider.gameObject);
            Destroy(collider.gameObject.transform.parent.gameObject);
            MobSpawn.instance.spawnCount++;

            saturn += ((float)collider.gameObject.GetComponent<Animal>().size / (float)size) * 100;
        }

        if(collider.gameObject.tag == "Mob" && collider.gameObject.GetComponent<Animal>().size >= size)
        {
            SceneManager.LoadScene(2);
        }

        if(collider.gameObject.name == "Eagle")
        {
            SceneManager.LoadScene(2);
        }
    }
}
