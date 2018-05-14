using NewtonVR;
using UnityEngine;

namespace Assets.Scripts
{
    public class TurnMeOffScript : MonoBehaviour {

        public NVRButton Button;
        private bool _isOn;
        private GameManagerScript _gm;
        private AudioSource _audio;
        private Material _material;
        private Color turnOffColor = new Color(.1f, 0, 0);


        // Use this for initialization
        public void Start () {
            _gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            _audio = GetComponent<AudioSource>();
            Invoke("TurnOn", 30);
            _material = GetComponent<Renderer>().material;
            _material.SetColor("_EmissionColor", turnOffColor);
        }
	
        // Update is called once per frame
        public void Update () {
            if (Button.ButtonDown)
            {
                if (_isOn)
                {
                    _isOn = false;
                    _audio.Play();
                    _material.SetColor("_EmissionColor", turnOffColor);
                    Invoke("TurnOn", Random.Range(5f,20f));
                    _gm.AddDataToButtonToTurnOff(true);
                }
                else
                {
                    _gm.AddDataToButtonToTurnOff(false);
                }
            }
        }

        public void TurnOn()
        {
            _isOn = true;
            _material.SetColor("_EmissionColor", new Color(1, 0, 0));
            _audio.Play();
            Invoke("TurnOff", 3);
        }

        public void TurnOff()
        {
            if (_isOn)
            {
                _isOn = false;
                _material.SetColor("_EmissionColor", turnOffColor);
                Invoke("TurnOn", Random.Range(5f, 20f));
                _gm.AddDataToButtonToTurnOff(false);
            }
        }
    }
}
