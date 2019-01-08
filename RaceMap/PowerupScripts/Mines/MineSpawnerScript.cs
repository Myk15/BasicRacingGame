using UnityEngine;

public class MineSpawnerScript : PowerupScript
{
    public GameObject Mine;
    private GameObject Inst;

    public MineSpawnerScript()
    {
        Uses = 3;
        SetImage(Image);

    }
    public override void Use()
    {
  
        
        if (Uses > 0)
        {
            
            Inst = Instantiate(Mine, GetOwner().transform.position - (transform.forward * 3), transform.rotation);
            Inst.name = GetOwner().name + " Mine";
            Uses--;
        }
        
    }
}
