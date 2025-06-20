using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealthBarUI : MonoBehaviour
{
   public Transform cam;

   void lateUpdate()
   {
      transform.LookAt(transform.position + cam.forward);
   }
}