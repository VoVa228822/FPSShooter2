using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayGun : MonoBehaviour
{
    [SerializeField] float SwayClamp = 0.09f;

    [SerializeField] float smoothing = 3f;

    Vector3 origin;

    private void Start()
    {
        origin = transform.localPosition;
    }
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        input.x = Mathf.Clamp(input.x, -SwayClamp, SwayClamp);
        input.y = Mathf.Clamp(input.y, -SwayClamp, SwayClamp);

        Vector3 Target = new Vector3(-input.x,-input.y, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, Target + origin, Time.deltaTime * smoothing);    
    }
}
