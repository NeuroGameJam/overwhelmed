using UnityEngine;

namespace Assets.Scripts
{
    public class InfinityRunnerGame : MonoBehaviour
    {
        private GameManagerScript _gm;
        private float _speed = .5f;
        public GameObject EnemyCube;
        private readonly Vector3 _initialPosition = new Vector3(.5f, .08f, 0);

        // Use this for initialization
        public void Start () {
            EnemyCube.transform.localPosition = _initialPosition;
            _gm = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        }

        // Update is called once per frame
        public void Update ()
        {
            EnemyCube.transform.Translate(-_speed * Time.deltaTime, 0,0);
            if (EnemyCube.transform.localPosition.x <= -0.5)
            {
                var newPosition = new Vector3(_initialPosition.x + Random.Range(0f,2f), _initialPosition.y, _initialPosition.z);
                EnemyCube.transform.localPosition = newPosition;
                _gm.AddCubeOnInfinityRunner();
            }
        }
    }
}
