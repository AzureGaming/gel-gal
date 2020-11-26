using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelArea : MonoBehaviour {
    int type;

    public void Init(int gelType) {
        type = gelType;
    }

    public int GetType() {
        return type;
    }
}
