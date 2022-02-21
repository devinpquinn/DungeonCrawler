using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.myState = PlayerController.playerState.Immobilized;
        FadeManager.FadeIn(1f);
    }
}
