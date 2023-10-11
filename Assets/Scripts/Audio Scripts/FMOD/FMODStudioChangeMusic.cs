
using UnityEngine;


namespace FMODUnity
{
    public class FMODStudioChangeMusic : MonoBehaviour
    {

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
            beautiful_broadcast = FMODUnity.RuntimeManager.CreateInstance(BeautifulBroadcastEventPath);
            beautiful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, originalMinDistance);
            beautiful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, originalMaxDistance);

            awful_broadcast = FMODUnity.RuntimeManager.CreateInstance(AwfulBroadcastEventPath);
            awful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, originalMinDistance);
            awful_broadcast.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, originalMaxDistance);

            beautiful_broadcast.start();
        }

        
        void Update()
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(beautiful_broadcast, transform, GetComponent<Rigidbody>()); 
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(awful_broadcast, transform, GetComponent<Rigidbody>());

            if (OverrideAttenuation)
            {
                SetAttenuationDistances(minDistance, maxDistance);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                //fade out beautiful music, fade in awful music

                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                awful_broadcast.getPlaybackState(out fmodPbState);

                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    beautiful_broadcast.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    awful_broadcast.start();
                    awful_broadcast.release();
                    Debug.Log("Play awful sound now...");
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

            Debug.Log("Override Attenuation");
        }



    }
}

