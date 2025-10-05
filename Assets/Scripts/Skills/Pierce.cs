using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pierce : MonoBehaviour
{
    private int pierceCount = 2;
    private HashSet<GameObject> pierced = new();


    public bool AlreadyPierced(GameObject go)
    {
        return pierced.Contains(go);
    }

    public void Pierced(GameObject go)
    {
        pierced.Add(go);
    }

    public bool CanPierce()
    {
        return pierced.Count < pierceCount;
    }



}
