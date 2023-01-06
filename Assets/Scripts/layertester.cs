using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layertester : MonoBehaviour
{
    [SerializeField] GameObject impostor;
    [SerializeField] LayerMask groundLayer;
    public GameObject tracker;
    bool susBool;
    string activeObjectName;
    // Start is called before the first frame update
    void Start()
    {
        ObjectPicking.layerChanger += SwitchLayer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if(Input.GetKeyDown(KeyCode.B)) {gameObject.layer = i; print(i); i++;}

        if (susBool) {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, groundLayer) && activeObjectName.Equals(gameObject.name)) {
                GameObject g = (GameObject)Instantiate(tracker, hit.point, Quaternion.identity, this.transform);
                print("isworking bro u stupid");
                Destroy(g,0.02f);
            }
        }
    }

    void SwitchLayer(bool isEnabled, string name_) {
        activeObjectName = name_;
        if (isEnabled && name_.Equals(gameObject.name)) {
            gameObject.layer = 7;
            if (impostor) impostor.layer = 7;

        } else if(!isEnabled && name_.Equals(gameObject.name)) {
            gameObject.layer = 6;
            if (impostor) impostor.layer = 6;
        }

        susBool = isEnabled;
    }



    private void OnDestroy() {
        ObjectPicking.layerChanger -= SwitchLayer;
    }
}
