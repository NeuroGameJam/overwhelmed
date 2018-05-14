using System.Collections;
using System.Collections.Generic;
using NewtonVR;
using UnityEngine;

namespace Assets.Scripts
{
    public class MemoryGame : MonoBehaviour {

        public NVRButton BlueButton, GreenButton, YellowButton;
        private int _counter;
        private readonly string[] _order = new string[3];
        private GameManagerScript _gm;
        private GameObject g, b, y;
        private BoxCollider gCollider, bCollider, yCollider;
        private BlinkMemoryButton gScript, bScript, yScript;
        private AudioSource _audio;

        // Use this for initialization
        public void Start ()
        {
            _gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
            _audio = GetComponent<AudioSource>();
            b = BlueButton.gameObject;
            g = GreenButton.gameObject;
            y = YellowButton.gameObject;

            bCollider = b.GetComponent<BoxCollider>();
            gCollider = g.GetComponent<BoxCollider>();
            yCollider = y.GetComponent<BoxCollider>();

            gScript = g.GetComponent<BlinkMemoryButton>();
            bScript = b.GetComponent<BlinkMemoryButton>();
            yScript = y.GetComponent<BlinkMemoryButton>();

            StartNewGame();
        }

        private void StartNewGame()
        {
            _counter = 0;
            bCollider.enabled = false;
            gCollider.enabled = false;
            yCollider.enabled = false;
            var aux = new List<string> {"b", "g", "y"};
            for (var i = 0; i < 3; i++)
            {
                var number = Random.Range(0, aux.Count);
                _order[i] = aux[number];
                aux.RemoveAt(number);
            }
            StartCoroutine(WaitAfewSeconds());
            Invoke("CheckToSeeIfNoAnswer",13);
        }

        public void CheckToSeeIfNoAnswer()
        {
            //Time ended and failed, gotta end the game and save data
            if (_counter < 3)
            {
                EndGame();
                _gm.AddDataToMemoryGame(false);
                _audio.Play();
            }
        }

        private IEnumerator WaitAfewSeconds()
        {
            foreach (var s in _order)
            {
                switch (s)
                {
                    case "g":
                        gScript.Blink();
                        break;
                    case "b":
                        bScript.Blink();
                        break;
                    case "y":
                        yScript.Blink();
                        break;
                }
                yield return new WaitForSeconds(1);
            }
            bCollider.enabled = true;
            gCollider.enabled = true;
            yCollider.enabled = true;
        }

        // Update is called once per frame
        public void Update ()
        {
            var nextGuess = "";
            if (BlueButton.ButtonDown)
            {
                nextGuess = "b";
                BlueButton.gameObject.GetComponent<BlinkMemoryButton>().Blink();
            }
            if (GreenButton.ButtonDown)
            {
                nextGuess = "g";
                GreenButton.gameObject.GetComponent<BlinkMemoryButton>().Blink();
            }
            if (YellowButton.ButtonDown)
            {
                nextGuess = "y";
                YellowButton.gameObject.GetComponent<BlinkMemoryButton>().Blink();
            }
            if (nextGuess != "")
            {
                if (_order[_counter].Equals(nextGuess))
                {
                    if (_counter == 2)
                    {
                        _gm.AddDataToMemoryGame(true);
                        EndGame();
                    }
                }
                else
                {
                    _gm.AddDataToMemoryGame(false);
                    EndGame();
                    _audio.Play();
                }
                _counter++;
            }
        }

        private void EndGame()
        {
            Invoke("StartNewGame", Random.Range(15, 30));
            BlueButton.gameObject.GetComponent<BoxCollider>().enabled = false;
            GreenButton.gameObject.GetComponent<BoxCollider>().enabled = false;
            YellowButton.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
