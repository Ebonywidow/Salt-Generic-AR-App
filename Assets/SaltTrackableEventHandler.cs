using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SaltTrackableEventHandler : MonoBehaviour, ITrackableEventHandler {

    protected TrackableBehaviour mTrackableBehaviour;

    protected virtual void Start() {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if ( mTrackableBehaviour )
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    /// 
    public GameObject GO_VideoPlayer;
    public GameObject GO_Shaker;
    public GameObject GO_FilmRays;
    private GameObject go_OneShotAudio;

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus) {

        if ( newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED ) {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            //Debug.Log("Trackable " + mTrackableBehaviour. + " gameobject found");
            OnTrackingFound();
        } else if ( previousStatus == TrackableBehaviour.Status.TRACKED &&
                   newStatus == TrackableBehaviour.Status.NOT_FOUND ) {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        } else {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    protected virtual void OnTrackingFound() {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
        var animatorComponents = GetComponentsInChildren<Animator>(true);


        GO_Shaker.SetActive(true);
        // Enable animators:
        foreach ( var component in animatorComponents ) {
            Debug.Log("This is the animator component name " + component.name);

            if ( component == true ) {
                component.enabled = true;
                if ( component.name == "Penguin" ) {
                    component.Play("Penguin");
                } else if ( component.name == "Trex" ) {
                    component.Play("Trex");
                } else if ( component.name == "Train" ) {
                    component.Play("Train");
                } else if ( component.name == "SaltShakerAni5" ) {
                    component.Play("Take 001");
                }
            }
        }

        // Enable rendering:
        foreach ( var component in rendererComponents )
            component.enabled = true;

        // Enable colliders:
        foreach ( var component in colliderComponents )
            component.enabled = true;

        // Enable canvas':
        foreach ( var component in canvasComponents )
            component.enabled = true;
    }


    protected virtual void OnTrackingLost() {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
        var animatorComponents = GetComponentsInChildren<Animator>(true);
        //var soundComponents = GetComponentInChildren<AudioClip>(true);

        go_OneShotAudio = GameObject.Find("One shot audio");
        GameObject gameObject_VP = GameObject.Find("VideoPlayer");

        GO_Shaker.SetActive(false);
        GO_FilmRays.SetActive(false);
        //var videoAudio = GO_VideoPlayer.GetComponent<AudioSource>();
        // Enable animators:
        foreach ( var component in animatorComponents ) {
            component.StopPlayback();
            component.enabled = false;
        }

        // Disable rendering:
        foreach ( var component in rendererComponents )
            component.enabled = false;

        // Disable colliders:
        foreach ( var component in colliderComponents )
            component.enabled = false;

        // Disable canvas:
        foreach ( var component in canvasComponents )
            component.enabled = false;

        // Disable sound:
        if ( go_OneShotAudio != null ) {
            Destroy(go_OneShotAudio);
        }

        // Sound fix for audio that is playing as part of a videoplayer attached to a gameobject (03-06-2018)
        if ( gameObject_VP != null ) {
            gameObject_VP.SetActive(false);
            Debug.Log("Videoplayer stop fix");
        }

        // Sound fix for audio that is still playing when tracking is lost (03-06-2018)
        if ( mTrackableBehaviour.gameObject.GetComponentInChildren<AudioSource>() != null ) {
            mTrackableBehaviour.gameObject.GetComponentInChildren<AudioSource>().Stop();
            //videoPlayer.Stop();
        }
    }

}
