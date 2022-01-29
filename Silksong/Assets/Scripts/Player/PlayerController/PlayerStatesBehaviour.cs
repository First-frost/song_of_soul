using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ҫ�����޸���ֵ �޸ĺ�smb��ö�ٽ���ʧ
public enum EPlayerState
{
    None = 0,
    Idle = 1,
    Run = 2,
    Jump = 4,
    Fall = 8,
    NormalAttack = 16,
    Sprint=32,

}
public class PlayerStatesBehaviour 
{
   public PlayerController playerController { get; set; }
   public PlayerJump playerJump;
   public PlayerFall playerFall;
   public PlayerSprint playerSprint;
    public void init()
    {
        playerJump = new PlayerJump(playerController);
        playerFall = new PlayerFall(playerController);
        playerSprint = new PlayerSprint(playerController);
    }

    public PlayerStatesBehaviour(PlayerController playerController)
    {
        this.playerController = playerController;
        init();
    }
    public void StatesEnterBehaviour(EPlayerState playerStates)
    {
        switch (playerStates)
        {
            /* case EPlayerState.None:
                   break*/
            case EPlayerState.Idle:
                break;
            case EPlayerState.Run:
                break;
            case EPlayerState.Jump:
                playerJump.JumpStart();
                break;
            case EPlayerState.Fall:
                break;
            case EPlayerState.NormalAttack:
                playerController.CheckFlipPlayer(1f);
                break;
            case EPlayerState.Sprint:
                playerSprint.SprintStart();
                break;
            default:
                break;
        }
    }
    //activeΪ�����stateʱ��һ֡��ʼ��Ҳ����û�а�start���зֿ�
    public  void StatesActiveBehaviour(EPlayerState playerStates)
    {
        switch (playerStates)
        {
            /* case EPlayerState.None:
                 break;*/
            case EPlayerState.Idle:
                //PlayerController.CheckIsGroundedAndResetAirJumpCount();
                playerController.CheckAddItem();
                playerController.CheckHorizontalMove(0.4f);
                break;
            case EPlayerState.Run:
                // PlayerController.CheckIsGroundedAndResetAirJumpCount();
                playerController.CheckAddItem();
                playerController.CheckFlipPlayer(1f);
                playerController.CheckHorizontalMove(0.4f);
                break;
            case EPlayerState.Jump:
                // PlayerController.IsGrounded = false;
                playerController.CheckFlipPlayer(1f);
                playerController.CheckHorizontalMove(0.5f);
                break;
            case EPlayerState.Fall:
                //PlayerController.CheckIsGroundedAndResetAirJumpCount();
                playerController.CheckFlipPlayer(1f);
                playerController.CheckHorizontalMove(0.5f);
                playerFall.checkMaxFallSpeed();
                break;
            case EPlayerState.NormalAttack:
                playerController.CheckHorizontalMove(0.5f);
                break;
            case EPlayerState.Sprint:

                break;
            default:
                break;
        }
    }
    public  void StatesExitBehaviour(EPlayerState playerStates)
    {
        switch (playerStates)
        {

            case EPlayerState.Idle:

                break;
            case EPlayerState.Run:

                break;
            case EPlayerState.Jump:

                break;
            case EPlayerState.Fall:

                break;
            case EPlayerState.NormalAttack:

                break;
            case EPlayerState.Sprint:
                playerSprint.SprintEnd();                  
                break;
            default:
                break;
        }
    }


}


/*public abstract class StatesBehaviour
{
    public abstract void StatesEnterBehaviour(EPlayerState playerStates);
    public abstract void StatesActiveBehaviour(EPlayerState playerStates);
    public abstract void StatesExitBehaviour(EPlayerState playerStates);
}*/

public abstract class PlayerAction
{
    protected PlayerController playerController;
    public PlayerAction(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}

public class PlayerJump:PlayerAction
{
    public PlayerJump(PlayerController playerController) : base(playerController) { }

    private float jumpStartHeight;

    private int currentJumpCountLeft;
    public int CurrentJumpCountLeft
    {
        get { return currentJumpCountLeft; }
        set
        {
            currentJumpCountLeft = value;
            playerController. PlayerAnimator.SetInteger(playerController.animatorParamsMapping.JumpLeftCountParamHash, currentJumpCountLeft);
        }
    }

    public void resetJumpCount() => CurrentJumpCountLeft = playerController.playerInfo.maxJumpCount;

    public void JumpStart()
    {

       playerController.RB.gravityScale = 0;

       if(playerController.isGroundedBuffer()==false)//ֻ�п�����Ծ����Ծ������������Ծ������IsGround set������ȥ
         --CurrentJumpCountLeft;

        playerController.RB.velocity = new Vector2(playerController.RB.velocity.x, playerController.playerInfo.jumpUpSpeed);
        jumpStartHeight = playerController.transform.position.y;

        playerController.StopCoroutine(JumpUpCheck());
        playerController.StartCoroutine(JumpUpCheck());
    }

    public IEnumerator JumpUpCheck()
    {
        bool hasQuickSlowDown=false;
        bool hasNormalSlowDown = false;

        float normalSlowDistance = 0.5f*playerController.playerInfo.jumpUpSpeed * Constants.JumpUpSlowDownTime;//s=0.5*velocity*time
        while(true)
        {
            yield return null;//ÿ��update��ѭ��һ��
            //EPlayerState state = playerController.PlayerAnimatorStatesControl.CurrentPlayerState;
            if (playerController.RB.velocity.y<0.01f)//��Ծ�������̽���
            {
                if(playerController.PlayerAnimatorStatesControl.CurrentPlayerState!=EPlayerState.Sprint)
                playerController.RB.gravityScale = playerController.playerInfo.normalGravityScale;

                break;
            }

            float jumpHeight = playerController.transform.position.y - jumpStartHeight; 

            if(jumpHeight>playerController.playerInfo.jumpMinHeight-0.5f)//�ﵽ��С�߶Ⱥ����ͣ��
            {

                if ( hasQuickSlowDown == false && PlayerInput.Instance.jump.Held == false )//��ɲ
                {
                    hasQuickSlowDown = true;
                    float jumpSlowDownTime = Constants.JumpUpStopTime;
                    float acce = playerController.RB.velocity.y / jumpSlowDownTime;
                    float gScale = -acce / Physics2D.gravity.y;
                    // Debug.Log(gScale);
                    playerController.RB.gravityScale = gScale;
                }
                if(!hasNormalSlowDown && !hasQuickSlowDown && jumpHeight > playerController.playerInfo.jumpMaxHeight - normalSlowDistance )//��ͣ
                {
                    hasNormalSlowDown = true;
                    float jumpSlowDownTime = Constants.JumpUpSlowDownTime;
                    float acce = playerController.RB.velocity.y / jumpSlowDownTime;
                    float gScale = -acce / Physics2D.gravity.y;
                    // Debug.Log(gScale);
                    playerController.RB.gravityScale = gScale;
                }
            }
        }

    }

}

public class PlayerFall:PlayerAction
{

    public PlayerFall(PlayerController playerController) : base(playerController) { }

    public void checkMaxFallSpeed()
    {
        if(playerController.RB.velocity.y<-playerController.playerInfo.maxFallSpeed)
        {
            playerController.RB.velocity =new Vector2(playerController.RB.velocity.x, -playerController.playerInfo.maxFallSpeed);
        }
    }
}

public class PlayerSprint : PlayerAction
{
    public PlayerSprint(PlayerController playerController):base(playerController){}

    private bool sprintReady;
    public bool SprintReady
    {
        get { return sprintReady; }
        set
        {
            sprintReady = value;
            playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SprintReadyParamHash,sprintReady);
        }
    }

    private int airSprintLeftCount;
    public int AirSprintLeftCount
    {
        get { return airSprintLeftCount; }
        set
        {
            airSprintLeftCount = value;
            playerController.PlayerAnimator.SetInteger(playerController.animatorParamsMapping.AirSprintLeftCountParamHash,airSprintLeftCount);
        }
    }

    public void resetAirSprintLeftCount()
    {
        AirSprintLeftCount = playerController.playerInfo.maxAirSprintCount;
    }
    public void SprintStart()
    {
        playerController.RB.gravityScale = 0;
        int x = playerController.playerInfo.playerFacingRight ? 1 : -1;
        playerController.RB.velocity = new Vector2(playerController.playerInfo.sprintSpeed * x, 0);

        if (playerController.isGroundedBuffer() == false)
            AirSprintLeftCount--;

        playerController.gravityLock = true;
    }

    public void SprintEnd()
    {
        playerController.RB.gravityScale = playerController.playerInfo.normalGravityScale;
        playerController.RB.velocity = Vector2.zero;
        playerController.StartCoroutine(sprintCdCount());

        playerController.gravityLock = false;
    }

    public IEnumerator  sprintCdCount()
    {
        SprintReady = false;
        yield return new WaitForSeconds(Constants.SprintCd);
        SprintReady = true;
    }

}