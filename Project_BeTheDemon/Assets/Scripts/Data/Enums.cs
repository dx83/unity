
public enum InGameState
{
    Start = 0,
    End,
    Run,
    Attack,
    TakeHit,
    Idle,
    Death,
    Restart,
    None
}


public enum HeroAni { Idle, Run, Dash, Attack, TakeHit, Death }
public enum CollideObj { Monster = 0, Skill, Item, Stop, None }


public enum MonsterAni { Idle = 0, Attack, Hurt, Death }
public enum MonsterState { Idle = 0, Attack, Hurt, Death }
