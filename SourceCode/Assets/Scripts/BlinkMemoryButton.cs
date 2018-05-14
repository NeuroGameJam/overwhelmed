using System.Collections;
using UnityEngine;
using UnityEngine.Collections;

public class BlinkMemoryButton : MonoBehaviour
{

    private AudioSource _audio;
    private Material _onBlue;

	// Use this for initialization
	void Start ()
	{
	    _audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Blink()
    {
        _audio.Play();
      //  StartCoroutine(Button());
        //Invoke para o apagar passado 1 segundo
    }

    IEnumerator Button()
    {
        gameObject.transform.localScale += new Vector3(0, 0.5f, 0);
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.localScale -= new Vector3(0, 0.5f, 0);

    }
}
