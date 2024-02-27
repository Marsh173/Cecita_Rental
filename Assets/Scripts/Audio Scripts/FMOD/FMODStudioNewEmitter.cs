#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * This is a parent script for any other fmod related script to inherit from.
 * It contains the basic functionality of original EventEmitter with more leniency.
 */

namespace FMODUnity
{
    public enum ActivationMode
    {
        OnTriggerEnter,
        ObjectEnable,
        ObjectStart
    }

    public enum DeactivationMode
    {
        OnTriggerExit,
        ObjectDisable,
        ObjectDestroy
    }

    public class FMODStudioNewEmitter : MonoBehaviour
    {
        FMOD.ChannelGroup mcg;
        FMOD.Studio.Bus Masterbus;

        [Header("FMOD Settings")]
        [SerializeField] EventReference audioEventPath;
        [SerializeField] ActivationMode activationMode = ActivationMode.OnTriggerEnter;
        [SerializeField] DeactivationMode deactivationMode = DeactivationMode.OnTriggerExit;
        [SerializeField] bool NeedToResetAfterDeath = false;

        FMOD.Studio.EventInstance audioEvent; 
        
        [Header("Override Attenuation (for both track)")]
        public bool OverrideAttenuation = false;
        [SerializeField] [Range(0f, 30f)] float minDistance = 1.0f;
        [SerializeField] [Range(0f, 50f)] float maxDistance = 20.0f;

        float originalMinDistance = 1.0f;
        float originalMaxDistance = 20.0f;



        void Start()
        {
            OnEnable();
            audioEvent = FMODUnity.RuntimeManager.CreateInstance(audioEventPath);
            audioEvent.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, originalMinDistance);
            audioEvent.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, originalMaxDistance);
 
  
            Masterbus = FMODUnity.RuntimeManager.GetBus("Bus:/");

            if (activationMode == ActivationMode.ObjectStart)
            {
                PlayAudioEvent();
            }
        }

        
        void Update()
        {
            DeathReset();

            switch (activationMode)
            {
                case ActivationMode.ObjectEnable:
                    if (gameObject.activeSelf)
                    {
                        PlayAudioEvent();
                    }
                    break;
                    // Add additional activation modes here
            }

            if (OverrideAttenuation)
            {
                SetAttenuationDistances(minDistance, maxDistance);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(activationMode == ActivationMode.OnTriggerEnter && other.tag == "Player")
            {
                Debug.Log("enter trigger");
                PlayAudioEvent();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (deactivationMode == DeactivationMode.OnTriggerExit && other.tag == "Player")
            { 
                Debug.Log("exit trigger");
                StopAudioEvent();
            }
        }

        private void DeathReset()
        {
            if (NeedToResetAfterDeath && Respawn.dead)
            {
                //stop all music 
                Debug.Log("reset audio event");
                StopAudioEvent();
            }
        }

        private void OnDestroy()
        {
            if (deactivationMode == DeactivationMode.ObjectDestroy)
            {
                StopAudioEvent();
            }
        }

        private void PlayAudioEvent()
        {
            FMOD.Studio.PLAYBACK_STATE fmodPbState;
            audioEvent.getPlaybackState(out fmodPbState);

            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                audioEvent.start();
            }
        }

        private void StopAudioEvent()
        {
            FMOD.Studio.PLAYBACK_STATE fmodPbState;
            audioEvent.getPlaybackState(out fmodPbState);

            if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                audioEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }

        public void SetAttenuationDistances(float newMinDistance, float newMaxDistance)
        {
            originalMinDistance = newMinDistance;
            originalMaxDistance = newMaxDistance;

            // Update the minimum and maximum attenuation distances for the FMOD event
            audioEvent.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, originalMinDistance);
            audioEvent.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, originalMaxDistance);
        }

        private void OnEnable()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            if (deactivationMode == DeactivationMode.ObjectDisable)
            {
                StopAudioEvent();
            }
        }

        public void OnSceneUnloaded(Scene unloadedScene)
        {
            // Stop the audio playback here
            audioEvent.release();
          
            FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
            mcg.stop();
            Masterbus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(FMODStudioNewEmitter))]
                public class FMODStudioNewEmitterEditor : Editor
                {
                    private SerializedProperty maxDistanceProperty;
                    private SerializedProperty minDistanceProperty;

                    private void OnEnable()
                    {
                        maxDistanceProperty = serializedObject.FindProperty("maxDistance");
                        minDistanceProperty = serializedObject.FindProperty("minDistance");
                    }

                    public override void OnInspectorGUI()
                    {
                        // Draw default inspector fields
                        DrawDefaultInspector();

                        // Update serialized object
                        serializedObject.Update();

                        // Apply changes to the serialized object
                        serializedObject.ApplyModifiedProperties();
                    }

                    protected void OnSceneGUI()
                    {
                        FMODStudioNewEmitter targetScript = (FMODStudioNewEmitter)target;

                        GUIStyle labelStyle = new GUIStyle();
                        labelStyle.fontSize = 50;
                        labelStyle.alignment = TextAnchor.MiddleCenter; // Center the text
                        labelStyle.normal.textColor = Color.yellow;


                // Allow the max and min distances to be adjusted interactively in the Scene View
                float newMaxDistance = Handles.RadiusHandle(Quaternion.identity, targetScript.transform.position, targetScript.maxDistance);
                        float newMinDistance = Handles.RadiusHandle(Quaternion.identity, targetScript.transform.position, targetScript.minDistance);

                        if (newMaxDistance != targetScript.maxDistance || newMinDistance != targetScript.minDistance)
                        {
                            Undo.RecordObject(targetScript, "Change Max and Min Distances");
                            targetScript.maxDistance = newMaxDistance;
                            targetScript.minDistance = newMinDistance;
                        }

                        // Display the audio icon label
                        Handles.Label(targetScript.transform.position, "AHHHH!", labelStyle);
            }
                }
#endif


    }
}

