using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHeader : MonoBehaviour
{
    private RectTransform _buttonTransform;
    protected RectTransform ButtonTransform
    {
        get
        {
            if (_buttonTransform == null)
                _buttonTransform = this.GetComponent<RectTransform>();
            return _buttonTransform;
        }
    }

    
}
