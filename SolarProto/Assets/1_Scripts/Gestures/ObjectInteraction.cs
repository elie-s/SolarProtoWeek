using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Material selectedMat;
    Material tempMat;
    MeshRenderer meshRend,_meshRend;
    Transform _hitObj;

    public GameObject interactionPanel;


    void Update()
    {
        if (_hitObj != null)
        {
            _meshRend = _hitObj.GetComponent<MeshRenderer>();
            _meshRend.material = tempMat;
            
            _hitObj = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform hitObj = hit.transform;

            if (hitObj.tag == "Building")
            {
                meshRend = hitObj.GetComponent<MeshRenderer>();
                if (meshRend != null)
                {
                    tempMat = meshRend.material;
                    meshRend.material = selectedMat;
                }
                _hitObj = hitObj;
                if (Input.GetMouseButtonDown(0))
                {
                    interactionPanel.SetActive(true);
                }
            }
            else
            {
                interactionPanel.SetActive(false);
            }

        }


        
    }
}
