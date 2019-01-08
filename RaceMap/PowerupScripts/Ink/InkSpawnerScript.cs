using UnityEngine;


public class InkSpawnerScript : PowerupScript
{
    public GameObject Ink;
    private GameObject Inst;

    public InkSpawnerScript()
    {
        Uses = 2;
        SetImage(Image);
    }
    public override void Use()
    {
        if (Uses > 0)
        {
           Inst = Instantiate(Ink, GetOwner().transform.position - (transform.forward * 3), transform.rotation);
            Inst.name = GetOwner().name + " Ink";
            Uses--;
        }

    }
}
