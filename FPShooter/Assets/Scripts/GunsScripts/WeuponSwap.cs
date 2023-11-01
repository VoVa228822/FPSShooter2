using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeuponSwap : MonoBehaviour
{
    int SelectedWeopon = 0;

    public bool SwapGuns;

    [SerializeField] Animation AnimationOfSwap;

    [SerializeField] AnimationClip AnimationOfSwapClip;


    // Start is called before the f irst frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int LastSelectedWeapon = SelectedWeopon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectedWeopon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectedWeopon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectedWeopon = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectedWeopon = 3;

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(SelectedWeopon >= transform.childCount - 1)
            {
                SelectedWeopon = 0;
            }
            else
            {
                SelectedWeopon +=1;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(SelectedWeopon <= 0)
            {
                SelectedWeopon = transform.childCount -1;
            }
            else
            {
                SelectedWeopon -= 1;
            }
        }

        if (LastSelectedWeapon != SelectedWeopon)
        {
            SwapGuns = true;
            SelectWeapon();
        }
        else
        {
            SwapGuns = false;
        }

    }
    void SelectWeapon()
    {

        if(SelectedWeopon >= transform.childCount -1)
        {
            SelectedWeopon = transform.childCount -1;
        }

        AnimationOfSwap.Stop();
        AnimationOfSwap.Play(AnimationOfSwapClip.name);

        int i = 0;
        foreach(Transform _Weapons in transform)
        {
            if(i == SelectedWeopon)
            {
                _Weapons.gameObject.SetActive(true);
            }
            else
            {
                _Weapons.gameObject.SetActive(false);
            }
            i++;
        }
        
    }
}
