﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : Node
{
    Vector3 steering;
    Vector3 desiredVelo;
    float distanceFromFlee;
    int force = 1000;

    public override Result Execute(Enemy owner)
    {
        Debug.Log("Flee");
        Debug.DrawLine(owner.transform.position, owner.enemyRB.velocity + owner.transform.position, Color.yellow); //Debug purposes

        distanceFromFlee = (owner.transform.position - owner.playerReference.position).magnitude; //Calculates the distance between the sheep and position
        desiredVelo = (owner.transform.position - owner.playerReference.position).normalized * owner.maxVelo; //Get the desired velocity for flee by minusing the target positions (in this case the player) from the attached objects position
        desiredVelo.y = 0;

        if (distanceFromFlee > owner.slowingRadius)  
        {
            desiredVelo = Vector3.zero; //Slows down the enemy aka arrival
        }

        steering = desiredVelo - owner.enemyRB.velocity; //Sets the steering behaviour by minusing
        owner.enemyRB.velocity = Vector3.ClampMagnitude(owner.enemyRB.velocity, 3); //Clamps the magnitude of the enemy
        owner.enemyRB.AddForce(steering * owner.force); //Moves the character based on the set steering behaviour

        Vector3 directionSheepFace = (owner.transform.position - owner.playerReference.transform.position) * force;
        directionSheepFace.y = owner.transform.position.y;
        owner.transform.LookAt(directionSheepFace);

        return previousResult = Result.success;
    }
}