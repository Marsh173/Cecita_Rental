
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

/***
 * Broadcast Script
 * 
 */
namespace FMODUnity
{
    public class FMODStudioChangeMusic : MonoBehaviour
    {
        FMOD.ChannelGroup mcg;
        FMOD.Studio.Bus Masterbus;

        [Header("FMOD Settings")]
        [SerializeField] EventReference BeautifulBroadcastEventPath;
        [SerializeField] EventReference AwfulBroadcastEventPath;

        FMOD.Studio.EventInstance beautiful_broadcast; //Play on Start, Fadeout on trigger enter for x seconds.
        FMOD.Studio.EventInstance awful_broadcast; //Play on Trigger Enter, fade in for x seconds

        [Header("Override Attenuation (for both track)")]
        public bool OverrideAttenuation = false;
        [SerializeField] [Range(0f, 30f)] float minDistance = 1.0f;
        [SerializeField] [Range(0f, 50f)] float maxDistance = 20.0f;

        float originalMinDistance = 1.0f;
        float originalMaxDistance = 20.0f;



        void Start()
        {
            // Find all enclosed rooms in the scene
            EnclosedRoomDetector[] enclosedRooms = FindObjectsOfType<EnclosedRoomDetector>();

            // Subscribe to the event of each enclosed room
            foreach (var room in enclosedRooms)
            {
                room.onPlayerEnteredRoom.AddListener(PlayerEnteredRoomHandler);
            }

            OnEnable();
            beautiful_broadcast = FMODUnity.RuntimeManager.CreateInstance(BeautifulBroadcastEventPath);
            beautiful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, originalMinDistance);
            beautiful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, originalMaxDistance);

            awful_broadcast = FMODUnity.RuntimeManager.CreateInstance(AwfulBroadcastEventPath);
            awful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, originalMinDistance);
            awful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, originalMaxDistance);

            beautiful_broadcast.start();

            Masterbus = FMODUnity.RuntimeManager.GetBus("Bus:/");
        }

        
        void Update()
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(beautiful_broadcast, transform, GetComponent<Rigidbody>()); 
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(awful_broadcast, transform, GetComponent<Rigidbody>());

            //DeathReset();

            if (OverrideAttenuation)
            {
                SetAttenuationDistances(minDistance, maxDistance);
            }
        }

        private void PlayerEnteredRoomHandler(GameObject roomObject)
        {
            Debug.Log("Event triggered at " + roomObject.name);

            if (roomObject.name == "startroom")
            {
                //activate all speakers
                if (gameObject.CompareTag("Broadcast-Startroom") || gameObject.CompareTag("Broadcast-Saferoom1") || gameObject.CompareTag("Broadcast-Saferoom2"))
                {
                    // Activate the speaker
                    Debug.Log("reset broadcast for checkpoint room");
                    FMOD.Studio.PLAYBACK_STATE fmodPbState;
                    beautiful_broadcast.getPlaybackState(out fmodPbState);

                    if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        awful_broadcast.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                        beautiful_broadcast.start();
                       
                    }

                }
               
            }
            else if (roomObject.name == "saferoom1")
            {
                //activate all speakers
                if (gameObject.CompareTag("Broadcast-Saferoom1") || gameObject.CompareTag("Broadcast-Saferoom2"))
                {
                    // Activate the speaker
                    Debug.Log("reset broadcast for checkpoint room");
                    FMOD.Studio.PLAYBACK_STATE fmodPbState;
                    beautiful_broadcast.getPlaybackState(out fmodPbState);

                    if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        awful_broadcast.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                        beautiful_broadcast.start();

                    }

                }
            }
            else if (roomObject.name == "saferoom2")
            {
                //activate all speakers
                if (gameObject.CompareTag("Broadcast-Saferoom2"))
                {
                    // Activate the speaker
                    Debug.Log("reset broadcast for checkpoint room");
                    FMOD.Studio.PLAYBACK_STATE fmodPbState;
                    beautiful_broadcast.getPlaybackState(out fmodPbState);

                    if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        awful_broadcast.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                        beautiful_broadcast.start();

                    }

                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                //fade out beautiful music, fade in awful music
                //Debug.Log("enter trigger");

                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                awful_broadcast.getPlaybackState(out fmodPbState);

                FMOD.Studio.PLAYBACK_STATE fmodPbStateSpeaker;
                beautiful_broadcast.getPlaybackState(out fmodPbStateSpeaker);

                if (fmodPbStateSpeaker == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    if(fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        beautiful_broadcast.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                        awful_broadcast.start();
                        //awful_broadcast.release();
                        //Debug.Log("Broadcast Turned off");
                    }
                    
                }

                
            }
        }

        private void DeathReset()
        {
            if (Respawn.dead)
            {
                Debug.Log("reset broadcast");
                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                beautiful_broadcast.getPlaybackState(out fmodPbState);

                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    awful_broadcast.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    beautiful_broadcast.start();
                    //beautiful_broadcast.release();
                    // Debug.Log("Play awful sound now...");
                }
            }
        }


        public void SetAttenuationDistances(float newMinDistance, float newMaxDistance)
        {
            originalMinDistance = newMinDistance;
            originalMaxDistance = newMaxDistance;

            // Update the minimum and maximum attenuation distances for the FMOD event
            beautiful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, originalMinDistance);
            beautiful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, originalMaxDistance);

            awful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, originalMinDistance);
            awful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, originalMaxDistance);

            //Debug.Log("Override Attenuation");
        }

        private void OnEnable()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public void OnSceneUnloaded(Scene unloadedScene)
        {
            // Stop the audio playback here
            beautiful_broadcast.release();
            awful_broadcast.release();
            //Debug.Log("Unloaded");
            FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out mcg);
            mcg.stop();
            Masterbus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(FMODStudioChangeMusic))]
                public class FMODStudioChangeMusicEditor : Editor
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
                        FMODStudioChangeMusic targetScript = (FMODStudioChangeMusic)target;

                        GUIStyle labelStyle = new GUIStyle();
                        labelStyle.fontSize = 50;
                        labelStyle.alignment = TextAnchor.MiddleCenter; // Center the text
                        labelStyle.normal.textColor = Color.red;


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
                        Handles.Label(targetScript.transform.position, "SOUND!", labelStyle);
            }
                }
        #endif


    }
}

