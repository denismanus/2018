using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorable  {
    void ExchangeColor(GameObject player);
    bool ColorComparison(Color playerColor);
}
