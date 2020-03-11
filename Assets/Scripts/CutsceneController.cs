using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    //Cutscene UI Objects
    public GameObject cutscene1;
    public GameObject cutscene2;
    public GameObject cutscene3;
    public GameObject cutscene4;

    //PlayableDirector for Timeline Control
    //public UnityEvent continueScene;
    public PlayableDirector director;

    //Variables for cutscenes because this is all in a timeline rather than separate animations
    public bool cutsceneViewed_1 = false;
    public bool cutsceneViewed_2 = false;
    public bool cutsceneViewed_3 = false;
    public bool cutsceneViewed_4 = false;

    //MutEx for scene starting
    public bool currentlyViewing = false;

    //Cutscene Extras
    public GameObject new_friend;
    public Animator PanelAnim;
    public bool transitioning;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!cutsceneViewed_1 && !currentlyViewing)
            {
                currentlyViewing = true;
                //Enable object that handles cutscene on UI
                cutscene1.SetActive(true);
                ResumeTimeline();
            }

            if (cutsceneViewed_1 && !cutsceneViewed_2 && !currentlyViewing)
            {
                currentlyViewing = true;
                cutscene2.SetActive(true);
                new_friend.SetActive(true);
                ResumeTimeline();
            }

            if (cutsceneViewed_2 && !cutsceneViewed_3 && !currentlyViewing)
            {
                currentlyViewing = true;
                cutscene3.SetActive(true);
                ResumeTimeline();
            }

            if (cutsceneViewed_3 && !cutsceneViewed_4 && !currentlyViewing)
            {
                currentlyViewing = true;
                cutscene4.SetActive(true);
                ResumeTimeline();
            }
        }
    }

    //Signal Receivers for cutscene completion and allowing for access to the next scene
    public void ReceiveSignal_1()
    {
        cutsceneViewed_1 = true;
        currentlyViewing = false;
    }

    public void ReceiveSignal_2()
    {
        cutsceneViewed_2 = true;
        currentlyViewing = false;
    }

    public void ReceiveSignal_3()
    {
        cutsceneViewed_3 = true;
        currentlyViewing = false;
    }

    public void ReceiveSignal_4()
    {
        cutsceneViewed_4 = true;
        currentlyViewing = false;
    }


    //Wrote these two after flipping through the Unity Forums for a solution to pausing the Timeline more than once.
    public void StopTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void ResumeTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    private void Update()
    {
        if (cutsceneViewed_1 && cutsceneViewed_2 && cutsceneViewed_3 && cutsceneViewed_4 && !transitioning)
        {
            transitioning = true;
            StartCoroutine(SceneTransitionAway());
        }
    }

    //This function is called by the end of an animation
    IEnumerator SceneTransitionAway()
    {
        //Enable Fade Out Canvas
        PanelAnim.SetTrigger("TransitionOUT");
        //Wait
        yield return new WaitForSeconds(3.2f);
        //Transition Scenes
        SceneManager.LoadScene("EndScene");
        yield return null;
    }
}
