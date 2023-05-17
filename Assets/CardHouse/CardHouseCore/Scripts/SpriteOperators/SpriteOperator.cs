using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class SpriteOperator : MonoBehaviour
{
    public string FavoredState;
    protected SpriteRenderer SpriteTarget;
    Dictionary<Object, string> Votes = new Dictionary<Object, string>();

    void Awake()
    {
        SpriteTarget = GetComponent<SpriteRenderer>();
    }

    public void Activate(string name)
    {
        Activate(name, this);
    }

    public void Activate(string name, Object voter)
    {
        Votes[voter] = name;

        if (SpriteTarget == null)
            return;
        
        var allVotes = Votes.Values.ToList();
        if (allVotes.Contains(FavoredState))
        {
            ChangeSprite(FavoredState);
        }
        else if (allVotes.All(x => x == name))
        {
            ChangeSprite(name);
        }
    }

    protected abstract void ChangeSprite(string name);
}
