public class DestroyBonus : IBonus {
    public override void Action()
    {
        Enemy[] golds = FindObjectsOfType<Enemy>();
        for (int i = 0; i < golds.Length; i++)
        {
            golds[i].Damage();
        }
    }
 
}
