using UnityEngine;

public abstract class Command
{
      public abstract void Execute(Animator anim);
}

public class PerformJump : Command
{
      public override void Execute(Animator anim)
      {
            anim.SetTrigger("isJumping");
      }
}

public class PerformPunch : Command
{
      public override void Execute(Animator anim)
      {
            anim.SetTrigger("isPunching");
      }
}

public class PerformKick : Command
{
      public override void Execute(Animator anim)
      {
            anim.SetTrigger("isKicking");
      }
}

public class DoNothing : Command
{
      public override void Execute(Animator anim)
      {
            
      }
}