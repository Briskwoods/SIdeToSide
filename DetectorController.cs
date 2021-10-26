using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : MonoBehaviour
{
    public bool disable = false;

    [SerializeField] private CharacterDetector[] detectors;

    private void Start()
    {
        detectors = this.gameObject.GetComponentsInChildren<CharacterDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (disable)
        {
            case true:
                for (int i = 0; i<detectors.Length; i++) {
                    detectors[i].enabled = false;
                    detectors[i].gameObject.GetComponent<SphereCollider>().enabled = false;
                }
                disable = false;
                break;
            case false: break;
        }
    }
}
