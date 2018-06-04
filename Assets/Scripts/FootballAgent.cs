using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballAgent : Agent
{
  public GameObject player;
  public FootballGoal goal;
  public Rigidbody ball;

  private Vector3 ballStartPosition;

  private void Start()
  {
    ballStartPosition = ball.transform.position - transform.position;
  }

  private void Update()
  {
    if(ball.isKinematic)
    {
      ball.transform.position = transform.position + (player.transform.rotation * ballStartPosition);
    }
  }

  public override void AgentReset()
  {
    base.AgentReset();

    goal.Reset();
    player.transform.rotation = Quaternion.identity;

    ball.isKinematic = true;
    ball.transform.position = transform.position + ballStartPosition;
  }

  public override void CollectObservations()
  {
    AddVectorObs(player.transform.rotation.y);
    AddVectorObs(ball.transform.position);
    AddVectorObs(goal.transform.position);
  }

  public override void AgentAction(float[] vectorAction, string textAction)
  {
    if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
    {
      player.transform.Rotate(new Vector3(0, 1, 0), vectorAction[0]);

      if (player.transform.rotation.eulerAngles.y < -90)
      {
        SetReward(-0.1f);
      }
      else if (player.transform.rotation.eulerAngles.y > 90)
      {
        SetReward(-0.1f);
      }

      if (vectorAction[1] > 0 && ball.isKinematic)
      {
        Shoot();
        SetReward(0.1f);
      }
    }
    else
    {
      Debug.LogError("Not implemented. Sorry.");
    }

    // Ball lost
    if (ball.isKinematic == false)
    {
      if (ball.transform.position.y < -1)
      {
        Done();
        SetReward(goal.HasScored ? 1f : -1f);
      }
    }

  }

  public void Shoot()
  {
    ball.isKinematic = false;

    ball.AddForce(player.transform.rotation * new Vector3(-1000, 0, 0));
  }
}
