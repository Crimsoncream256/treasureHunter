using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public AudioSource _audio;
    public AudioClip _doorInfo;
    public AudioClip _doorOpen;
    public AudioClip _doorClose;
    private bool _isOpen;
    private bool _isOpeningMotion;

    public float _volume = 0.5f;

    private Vector3 _openRot = new Vector3 (0,0,90);
    private Vector3 _closeRot = new Vector3(90, 0, 0);
    private Vector3 _openMove = new Vector3(5.0f, 5.0f, 55.0f);
    private Vector3 _closeMove = new Vector3(0f, 5.0f, 55.0f);

    void Start()
    {
        _audio = gameObject.AddComponent<AudioSource>();
        _audio.volume = _volume;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("‚È‚ñ‚©“ü‚Á‚½");
        if (col.gameObject.tag == "Player" && !_isOpeningMotion)
        {

            _audio.PlayOneShot(_doorInfo);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {

            if (!_isOpen)
            {
                _isOpeningMotion = true;
                _audio.PlayOneShot(_doorOpen);
                DOTween.Sequence()
                    .Append(this.transform.DOMove(_openMove, 1f))
                    .Join(this.transform.DORotate(_openRot, 1f));
                _isOpen = true;
                _isOpeningMotion = false;
            } else if (_isOpen)
            {
                _isOpeningMotion = true;
                _audio.PlayOneShot(_doorClose);
                DOTween.Sequence()
                    .Append(this.transform.DOMove(_closeMove, 1f))
                    .Join(this.transform.DORotate(_closeRot, 1f));
                _isOpen = false;
                _isOpeningMotion = false;
            }

        }
    }
}
