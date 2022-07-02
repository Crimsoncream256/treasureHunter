using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;
    public float _jumpForce = 100.0f;

    private Animator anim = null;

    [SerializeField] bool randomizePitch = true;
    public AudioSource _audio;
    public AudioClip _footstep1;
    public AudioClip _footstep2;
    public float _volume = 0.5f;
    [SerializeField] AudioClip[] clips;
    [SerializeField] float pitchRange = 0.1f;

    [SerializeField] bool _ground = true;

    public bool _isMoving = false;

    public float moveSpeed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        _audio = gameObject.AddComponent<AudioSource>();
        _audio.volume = _volume;
        _audio.PlayOneShot(_footstep1);
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // �L�����N�^�[�̌�����i�s������
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);

        }

        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)&& _ground)
        {
            Debug.Log("�����Ă�");
            _isMoving = true;
            anim.SetBool("moving", true);
            //_audio.PlayOneShot(_footstep1);
            //_audio.PlayOneShot(_footstep2);
            //Footstepsound();
        } else {
            _isMoving = false;
            anim.SetBool("moving", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_ground)
            {
                //jumpForce�̕���������ɗ͂�������
                rb.AddForce(transform.up * _jumpForce);
                _ground = false;
            }

        }
    }

    /*
    IEnumerator Footstepsound()
    {
        _audio.PlayOneShot(_footstep1);
        yield return new WaitForSeconds(0.5f);
        _audio.PlayOneShot(_footstep2);
        yield return new WaitForSeconds(0.5f);
    }
    */

    public void PlayFootstepSE()
    {
        if (randomizePitch)
            _audio.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);

        _audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }


    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("�Ȃ񂩓�����");
        if (col.gameObject.tag == "Ground")
        {
            //Debug.Log("���߂�");
            if (!_ground)
                _ground = true;
        }
    }
}
