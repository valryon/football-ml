using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballAgent : Agent
{
  const float GOAL_SIZE = 2.9f;

  public GameObject player;
  public FootballGoal goal;
  public Rigidbody ball;

  private bool hasScored;
  private Vector3 ballStartPosition;
  private int rewardsGrantedForDirection;
  private bool shouldShoot;
  private float timeSinceReset;

  private void Start()
  {
    ballStartPosition = ball.transform.position - transform.position;
  }

  private void Update()
  {
    timeSinceReset += Time.deltaTime;

    if (ball.isKinematic)
    {
      ball.transform.position = transform.position + (player.transform.rotation * ballStartPosition);
    }
  }

  public override void AgentReset()
  {
    base.AgentReset();

    hasScored = false;
    goal.Reset();
    player.transform.rotation = Quaternion.identity;

    ball.isKinematic = true;
    ball.transform.position = transform.position + ballStartPosition;

    rewardsGrantedForDirection = 0;
    shouldShoot = false;
    timeSinceReset = 0f;
  }

  public override void CollectObservations()
  {
    AddVectorObs(player.transform.rotation.y);
    AddVectorObs(goal.transform.position.x);
    AddVectorObs(goal.transform.position.z);
    AddVectorObs(timeSinceReset);
  }

  public override void AgentAction(float[] vectorAction, string textAction)
  {
    if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
    {
      player.transform.Rotate(new Vector3(0, 1, 0), vectorAction[0]);

      float delta = GetDelta(transform.forward);
      float absDelta = Mathf.Abs(delta);

      // Right direction -> reward
      if (vectorAction[0] != 0)
      {
        // Move to goal
        bool rightDirection = (vectorAction[0] == -1f * Mathf.Sign(delta));
        if (rightDirection && rewardsGrantedForDirection < 15)
        {
          SetReward(0.05f);
          rewardsGrantedForDirection++;
        }

        // Reward finding goal
        if (absDelta < (GOAL_SIZE - 0.15f))
        {
          shouldShoot = true;
          SetReward(0.1f);
          rewardsGrantedForDirection = 99;
        }
      }

      if (vectorAction[1] > 0 && ball.isKinematic && timeSinceReset > 0.5f)
      {
        // How close to the right angle?
        if (absDelta < GOAL_SIZE)
        {
          SetReward(2);
        }
        else
        {
          // Don't shoot if goal not found
          SetReward(shouldShoot ? -2 : -1);
        }

        Shoot();
      }

      if (goal.HasScored && hasScored == false)
      {
        hasScored = true;
        SetReward(1f);
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
      }
    }

  }

  public void Shoot()
  {
    ball.isKinematic = false;

    ball.AddForce(player.transform.rotation * new Vector3(-1000, 0, 0));
  }

  private float GetDelta(Vector3 fwd)
  {
    return Vector3.Angle(Vector3.Normalize(goal.transform.position - player.transform.position), fwd) - 90f;
  }
}
