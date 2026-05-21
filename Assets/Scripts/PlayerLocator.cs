using UnityEngine;

public static class PlayerLocator
{
    private static Transform player;

    public static void SetPlayer(Transform t)
    {
        player = t;
    }

    public static Transform GetPlayer()
    {
        if (player != null && !player.gameObject.scene.isLoaded)
        {
            player = null;
        }
        return player;
    }
}