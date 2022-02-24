using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstStepsArea : MonoBehaviour
{
    //dialogue assets
    [HideInInspector]
    public RPGTalk myTalk;
    public TextAsset myText;

    //cutscene camera target
    public Animator starTargetAnim;

    public CinemachineVirtualCamera cm;
    private float defaultDeadZoneHeight;

    private void Awake()
    {
        myTalk = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<RPGTalk>();
        defaultDeadZoneHeight = cm.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight;
        cm.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
    }

    private void Start()
    {
        Invoke("First", 2);
    }

    public void StartTalking(string key, UnityEngine.Events.UnityAction call)
    {
        PlayerController.Instance.myState = PlayerController.playerState.Interacting;
        PlayerController.Instance.SetCursor("interact");
        PlayerController.Instance.StopWalkAnimation();
        TooltipUI.HideTooltip_Static();

        myTalk.txtToParse = myText;

        myTalk.callback.RemoveAllListeners();
        if(call != null)
        {
            myTalk.callback.AddListener(call);
        }
        
        myTalk.NewTalk(key);
    }

    public void First()
    {
        StartTalking("begin", AfterFirst);
    }

    public void AfterFirst()
    {
        starTargetAnim.Play("starTargetDown");
        Invoke("Second", 4);
    }

    public void Second()
    {
        StartTalking("intro", PlayerController.EndInteraction);
        PlayerController.RefocusCam();
        cm.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = defaultDeadZoneHeight;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Body")
        {
            TookFirstSteps();
        }
    }

    public void TookFirstSteps()
    {
        PlayerController.Instance.lockToBody = false;

        StartTalking("walked", PlayerController.EndInteraction);
        Destroy(gameObject);
    }
}
