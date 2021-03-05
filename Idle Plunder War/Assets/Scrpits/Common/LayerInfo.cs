using UnityEngine;

public class LayerInfo 
{
    public static int Post = LayerMask.NameToLayer("Post");
    public static int Ground = LayerMask.NameToLayer("Ground");

    public static int Player = LayerMask.NameToLayer("Player");
    public static int Enemy = LayerMask.NameToLayer("Enemy");
    public static int Dead = LayerMask.NameToLayer("Dead");
    public static int Building = LayerMask.NameToLayer("Building");
    public static int Treasure = LayerMask.NameToLayer("Treasure");
}
