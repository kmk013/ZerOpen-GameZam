using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>{

    public AudioSource gasSound;
    public AudioSource needleSound;
    public AudioSource runSound;

    public int mapSizeX = 3560;
    public int mapSizeY = 3520;
}
