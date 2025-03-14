using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] private ScreenManager screenManager;
    private List<GameObject> players;
    void Start()
    {
        var playerControllers = screenManager.Players;
        players = new List<GameObject>();
        foreach (var playerController in playerControllers)
        {
            var parent = playerController.gameObject;
            players.Add(parent);
        }
    }

    void FixedUpdate()
    {
        var heights = new List<float>();
        var grounded = new List<bool>();
        foreach (var player in players)
        {
            var origin = player.transform.position;
            var raycastDir = new Vector3(0, -1, 0);
            var radius = 0.5f;
            var point1 = origin + new Vector3(0, radius, 0);
            var point2 = origin + new Vector3(0, -radius, 0);
            RaycastHit hit;
            if (Physics.CapsuleCast(point1, point2, radius, raycastDir, out hit, 1))
            {
                heights.Add(hit.distance);
                grounded.Add(true);
            }
            else
            {
                heights.Add(0);
                grounded.Add(false);
            }
        }
        var highest = Mathf.Max(heights.ToArray());
        var highestIndex = heights.IndexOf(highest);
        var highestGrounded = grounded[highestIndex];
        if (!highestGrounded && grounded.Contains(true))
        {
            while (!highestGrounded)
            {
                highest = Mathf.Max(heights.FindAll(h => h != highest).ToArray());
                highestIndex = heights.IndexOf(highest);
                highestGrounded = grounded[highestIndex];
                
            }
        }
        if (highestGrounded)
        {
            foreach (var player in players)
            {
                player.transform.position = new Vector3(player.transform.position.x, highest, player.transform.position.z);
            }
        } else
        {
            foreach (var player in players)
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.1f, player.transform.position.z);
            }
        }
    }
}
