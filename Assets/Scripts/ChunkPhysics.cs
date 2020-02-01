using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPhysics : MonoBehaviour
{
    public List<GameObject> Chunks = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        PopChunks();   
    }

    void PopChunks()
    {
        if (Chunks.Count > 2)
        {
            Chunks = Chunks.Distinct().OrderByDescending(x => x.transform.localScale.x).ToList();
            var newScale = Chunks.First().transform.localScale + Chunks[1].transform.localScale * 0.3f;
            Destroy(Chunks[1]);
            Chunks.First().transform.localScale = newScale;
            Chunks.Clear();
        }
    }
}
