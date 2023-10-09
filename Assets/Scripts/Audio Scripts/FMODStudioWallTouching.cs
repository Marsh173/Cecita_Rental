
using UnityEngine;


namespace FMODUnity
{
    public class FMODStudioWallTouching : MonoBehaviour
    {
        public LayerMask WallLayer;

        [Header("FMOD Settings")]
        [SerializeField] EventReference WallTouchingEventPath;
        FMOD.Studio.EventInstance Rubbing;

        private bool isTouchingLeftWall;
        private bool isTouchingRightWall;
        private bool isWalking;
        private bool hasChangedPosition = false;

        private void Start()
        {
            Rubbing = FMODUnity.RuntimeManager.CreateInstance(WallTouchingEventPath); //create FMOD instance to play event           
            isWalking = false;    
        }

        private void Update()
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Rubbing, transform, GetComponent<Rigidbody>()); //attach to gameobject in order to play

            isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

            Debug.DrawRay(transform.position, Vector3.right * 1f, Color.yellow);
            Debug.DrawRay(transform.position, Vector3.left * 1f, Color.green);
            isTouchingLeftWall = Physics.Raycast(transform.position, -transform.right, 1.0f, WallLayer); //if shoot a ray to the left, touched left wall
            isTouchingRightWall = Physics.Raycast(transform.position, transform.right, 1.0f, WallLayer); // if shoot a ray to the right, touched right wall

            //situation where touching Front or Back? 


            PlayRubbingSound();
        }


        void PlayRubbingSound()
        {
            FMOD.Studio.PLAYBACK_STATE fmodPbState;
            Rubbing.getPlaybackState(out fmodPbState);

            if (isTouchingLeftWall && isWalking) 
            {
                Debug.Log("Touched Left Wall && isWalking!");
                
                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {   
                    Rubbing.start();
                    Debug.Log("Play rubbing sound now...");
                }

               
            }
            else if (isTouchingRightWall && isWalking)
            {
                Debug.Log("Touched Right Wall");
               

                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                   
                    Rubbing.start();
                    Debug.Log("Play rubbing sound now...");
                }

            }
            else
            {
                
                Rubbing.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Debug.Log("Stopped...");
            }
            
        }

    }
}
