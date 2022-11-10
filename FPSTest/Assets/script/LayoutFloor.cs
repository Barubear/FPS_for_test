using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutFloor : MonoBehaviour
{
    public Vector3 cellCount = Vector3.zero;
    public Vector3 floorSize = Vector3.zero;
    public List<Transform> floorTansforms = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("reset floor Layout")]
    private void resetLayout() {
        floorTansforms.Clear();
        var childrenTransforms = this.GetComponentsInChildren<Transform>();
        for (int i = 1; i< childrenTransforms.Length; i++)
        {
            if (childrenTransforms[i].gameObject.CompareTag("floor")) {
                floorTansforms.Add(childrenTransforms[i]);
            }
            
        }
        int currentIndx = 0;
        for(int x = 0; x < cellCount.x; x++)
        {
            for(int z = 0;z < cellCount.z; z++)
            {
                Vector3 localPos = new Vector3(x * floorSize.x, 0, z * floorSize.z);
                floorTansforms[currentIndx].localPosition = localPos;
                currentIndx++;
            }

        }
    
    }
}
