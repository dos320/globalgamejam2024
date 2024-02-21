using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomTag : MonoBehaviour
{
    [SerializeField]
    private List<string> tags = new List<string>();

    public bool hasTag(string tag)
    {
        return tags.Contains(tag);
    }

    public IEnumerable<string> getTags()
    {
        return tags;
    }

    public void rename(int index, string tagName)
    {
        tags[index] = tagName;
    }
   
}
