using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    public void SetBullet(Vector3 playerposition, float rotation)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
        transform.position = playerposition;
    }


    private void FixedUpdate()
    {
        if(gameObject.activeInHierarchy)
        {
            transform.Translate(0f, _moveSpeed * Time.deltaTime, 0f);
        }

        if (!IsVisibleOnCamera())
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsVisibleOnCamera()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0f || viewportPosition.x > 1f || viewportPosition.y < 0f || viewportPosition.y > 1f)
        {
            return false;
        }

        return true;
    }
}
