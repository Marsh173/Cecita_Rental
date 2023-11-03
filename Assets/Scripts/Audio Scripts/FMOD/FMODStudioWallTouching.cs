
using UnityEngine;


namespace FMODUnity
{
    public class FMODStudioWallTouching : MonoBehaviour
    {
        public LayerMask WallLayer;

        [Header("FMOD Settings")]
        [SerializeField] EventReference WallTouchingLeftEventPath;
        [SerializeField] EventReference WallTouchingRightEventPath;
        [SerializeField] EventReference WallHitEventPath;
        FMOD.Studio.EventInstance Rubbing_Left;
        FMOD.Studio.EventInstance Rubbing_Right;
        FMOD.Studio.EventInstance Wall_Hit;

        private bool isTouchingLeftWall;
        private bool isTouchingRightWall;
        private bool isWalking;
        private bool isHitWall;
        private bool hitSoundPlayed;

        private void Start()
        {
            Rubbing_Left = FMODUnity.RuntimeManager.CreateInstance(WallTouchingLeftEventPath); //create FMOD instance to play event           
            Rubbing_Right = FMODUnity.RuntimeManager.CreateInstance(WallTouchingRightEventPath);
            Wall_Hit = FMODUnity.RuntimeManager.CreateInstance(WallHitEventPath);
            isWalking = false;
            hitSoundPlayed = false;
        }

        private void Update()
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Rubbing_Left, transform, GetComponent<Rigidbody>()); //attach to gameobject in order to play
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Rubbing_Right, transform, GetComponent<Rigidbody>());
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Wall_Hit, transform, GetComponent<Rigidbody>());

            isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

            Debug.DrawRay(transform.position, Vector3.right * 1f, Color.yellow);
            Debug.DrawRay(transform.position, Vector3.left * 1f, Color.green);
            isTouchingLeftWall = Physics.Raycast(transform.position, -transform.right, 1.0f, WallLayer); //if shoot a ray to the left, touched left wall
            isTouchingRightWall = Physics.Raycast(transform.position, transform.right, 1.0f, WallLayer); // if shoot a ray to the right, touched right wall

            //situation where touching Front or Back? 
            
            isHitWall = Physics.Raycast(transform.position, transform.forward, 1f, WallLayer);

           
            PlayHitSound();
            PlayRubbingSound();
        }


        void PlayRubbingSound()
        {
            

            if (isTouchingLeftWall && isWalking) 
            {
                //Debug.Log("Touched Left Wall && isWalking!");

                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                Rubbing_Left.getPlaybackState(out fmodPbState);

                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    Rubbing_Left.start();
                    //Debug.Log("Play rubbing sound now...");
                }

               
            }
            else if (isTouchingRightWall && isWalking)
            {
                //Debug.Log("Touched Right Wall");
                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                Rubbing_Right.getPlaybackState(out fmodPbState);

                if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {

                    Rubbing_Right.start();
                    //Debug.Log("Play rubbing sound now...");
                }

            }
            
            else
            {

                Rubbing_Left.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Rubbing_Right.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                
                
                //Debug.Log("Stopped...");
            }
            
        }


        float timeInHitWall = 0.0f;
        float durationOfHitSound = 0.04f; // Adjust the time as needed

        void PlayHitSound()
        {
            if (isHitWall && isWalking)
            {
                FMOD.Studio.PLAYBACK_STATE fmodPbState;
                Wall_Hit.getPlaybackState(out fmodPbState);

                timeInHitWall += Time.deltaTime;

                if (!hitSoundPlayed)
                {
                    if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        Wall_Hit.start();

                        //Debug.Log(timeInHitWall);

                        if (timeInHitWall >= durationOfHitSound)
                        {
                            hitSoundPlayed = true;
                            Wall_Hit.release();
                            //Debug.Log("Hit Sound played");
                        }


                    }
                }
                else
                {
                    Wall_Hit.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
                
            }
            else
            {
                
                Wall_Hit.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                timeInHitWall = 0.0f;
                hitSoundPlayed = false;
                //Debug.Log("away from wall");
            }
        }
    }
}
