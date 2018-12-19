using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBlockController : BlockController {
    [SerializeField] List<GameObject> demolishBlocks;

    private void DemolishLinkedBlocks() {
        demolishBlocks.ForEach(delegate(GameObject block) {
            if (block != null) block.GetComponent<BlockController>().DestroyBlock();
        });
    }

    private void OnCollisionEnter2D()
    {
        if (tag == "Breakable")
        {
            DemolishLinkedBlocks();
            HandleHit();
        }
    }
}
