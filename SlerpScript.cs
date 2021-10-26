// Animates the position in an arc between sunrise and sunset.

using UnityEngine;
using System.Collections;

public class SlerpScript : MonoBehaviour
{
    public Transform sunrise; //Starting Position
    public Transform sunset;  //Ending Position

    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    //This is how far the ball will go in the air before getting to the targets
    [Range(0.1f, 1f)]
    public float Height;

    // The time at which the animation started.
    private float startTime;

    void Start()
    {
        // Note the time at the start of the animation.
        //In your case, there is when the player has been given instructions to jump into hole
        startTime = Time.time;

        StartCoroutine(MovingIntoHole());
    }


    IEnumerator MovingIntoHole()
    {
        while (Vector3.Distance(transform.position, sunset.position) > 0.1f)
        {
            yield return new WaitForSeconds(0.017f); //Introduce a small delay so that the coroutine does not hang

            // The center of the arc
            Vector3 center = (sunrise.position + sunset.position) * 0.5F;

            // move the center a bit downwards to make the arc vertical
            center -= new Vector3(0, Height, 0);

            // Interpolate over the arc relative to center
            Vector3 riseRelCenter = sunrise.position - center;
            Vector3 setRelCenter = sunset.position - center;

            // The fraction of the animation that has happened so far is
            // equal to the elapsed time divided by the desired time for
            // the total journey.
            float fracComplete = (Time.time - startTime) / journeyTime;

            //transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position = Vector3.Lerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;
        }
    }
}
