using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupScript : MonoBehaviour
{
    private GameObject Owner;
    public int Uses;
    public Sprite Image;

    public PowerupScript()
    {
        Owner = null;
        Uses = 1;
        Image = null;

    }

    public virtual GameObject GetOwner()
    {
        return Owner;
    }

    public virtual void SetOwner(GameObject O)
    {
        Owner = O;
    }
    public virtual void SetNumUses(int i)
    {
        Uses = i;
    }
    public virtual void SetImage(Sprite i)
    {
        Image = i;
    }
    public virtual void Use() // each powerup will have it's own function call
    {

    }
}
