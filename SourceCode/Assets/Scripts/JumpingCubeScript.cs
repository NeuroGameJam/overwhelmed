using NewtonVR;
using UnityEngine;

namespace Assets.Scripts
{
    public class JumpingCubeScript : MonoBehaviour {

        public NVRButton Button;
        private GameManagerScript _gm;
        private Rigidbody _rb;
        private bool _jumping;
        private AudioSource _audio;

        // Use this for initialization
        public void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            _audio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        public void Update()
        {
            if (Button.ButtonDown && !_jumping)
            {
                _rb.AddForce(Vector3.up * 3, ForceMode.Impulse);
                _jumping = true;
                _audio.Play();
                _gm.AddDataToInfinityRunner(true);
            }
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Floor")
            {
                _jumping = false;
            }
        }

        //Hit the enemy cube
        public void OnTriggerEnter(Collider other)
        {
            _gm.AddDataToInfinityRunner(false);
        }
    }
}
