using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [Header("References")]
    private Rigidbody knifeRb;
    [SerializeField] private Tomb tomb;
    [SerializeField] private ParticleSystem woodFX;

    [Space]

    [Header("Variables")]
    private float _newPosX;
    private float _newPosY;
    private float _starPosX;
    private float _startPosY;
    private ParticleSystem.EmissionModule woodFXEmission;
    [SerializeField] Vector2 _clampValues = new Vector2(-5f, 5f);
    [SerializeField] private float movementSpeed;
    [SerializeField] float _lerpSpeed = 5.0f;
    [SerializeField] float hitDamage;
    private bool isMoving = false;
   

    void Start()
    {
      
        woodFXEmission = woodFX.emission;
        knifeRb = GetComponent<Rigidbody>();
        _starPosX = transform.position.x;
        _startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = Input.GetMouseButton(0);
        if(isMoving)
        {
           
            _newPosX = Mathf.Clamp(transform.position.x+Input.GetAxis("Mouse X") * movementSpeed, _clampValues.x, _clampValues.y);
            _newPosY = Mathf.Clamp(transform.position.y + Input.GetAxis("Mouse Y") * movementSpeed, _clampValues.x, _clampValues.y);
        }
    }
    private void FixedUpdate()
    {
        if(isMoving)
        {
            
            knifeRb.MovePosition(new Vector3(Mathf.Lerp(transform.position.x, _newPosX, _lerpSpeed * Time.deltaTime),
                Mathf.Lerp(transform.position.y, _newPosY, _lerpSpeed * Time.deltaTime), 0.0f));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        woodFXEmission.enabled = false;
       
    }
    private void OnCollisionStay(Collision collision)
    {
       TombCollision tombCollision = collision.collider.GetComponent<TombCollision>();
        if(tombCollision!=null)
        {
            // Hit Collider
            woodFXEmission.enabled = true;
            woodFX.transform.position = collision.contacts[0].point;
            tombCollision.HitCollider(hitDamage);
            tomb.Hit(tombCollision.index, hitDamage);
          
        }
    }
}
