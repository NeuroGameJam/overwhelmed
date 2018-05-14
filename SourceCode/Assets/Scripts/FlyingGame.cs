using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class FlyingGame : MonoBehaviour {

        private float _speed = .5f;
        private GameManagerScript _gm;
        public List<GameObject> EnemyCubes;

        // Use this for initialization
        public void Start()
        {
            _gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }

        // Update is called once per frame
        public void Update()
        {
            foreach (var enemyCube in EnemyCubes)
            {
                enemyCube.transform.Translate(0, -_speed*Time.deltaTime, 0);
                if (enemyCube.transform.localPosition.y <= 0)
                {
                    var newPosition = new Vector3(Random.Range(0f, 1f), Random.Range(1f, 3f), 0);
                    enemyCube.transform.localPosition = newPosition;
                    _gm.AddCubeOnFlyingGame();
                }
            }
        }
    }
}
