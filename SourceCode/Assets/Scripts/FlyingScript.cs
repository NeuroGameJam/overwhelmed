using NewtonVR;
using UnityEngine;

namespace Assets.Scripts
{
    public class FlyingScript : MonoBehaviour {

        public NVRSlider Slider;
        private GameManagerScript _gm;

        public void Start()
        {
            _gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
        public void Update()
        {
            transform.localPosition = new Vector3(Slider.CurrentValue, 0);
        }

        //Hit the enemy cube
        public void OnTriggerEnter(Collider other)
        {
            _gm.AddDataToFlyingFails();
        }
    }
}
