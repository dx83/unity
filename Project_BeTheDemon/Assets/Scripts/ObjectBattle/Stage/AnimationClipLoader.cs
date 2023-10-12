using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class AnimationClipLoader
{
    private Dictionary<string, Dictionary<string, AnimationClip>> animationClips =
        new Dictionary<string, Dictionary<string, AnimationClip>>();

    public void Load()
    {
        // Addressables Lable : MonsterClip
        var loadCall = Addressables.LoadAssetsAsync<AnimationClip>(
            "MonsterClip", (result) => {
                string[] str = result.name.Split('_');

                if (animationClips.ContainsKey(str[0]))
                {
                    animationClips[str[0]].Add(str[1], result);
                }
                else
                {
                    var clip = new Dictionary<string, AnimationClip>();
                    clip.Add(str[1], result);
                    animationClips.Add(str[0], clip);
                }
            });

        var clips = loadCall.WaitForCompletion();
    }

    public Dictionary<string, AnimationClip> GetAnimationClip(string name)
    {
        if (animationClips.ContainsKey(name))
            return animationClips[name];

        return null;
    }
}

public static class AniHash
{
    static int[] _mid;
    public static int[] MID
    {
        get
        {
            if (_mid == null)
            {
                _mid = new int[4];
                _mid[0] = Animator.StringToHash("Idle");
                _mid[1] = Animator.StringToHash("Attack");
                _mid[2] = Animator.StringToHash("Hurt");
                _mid[3] = Animator.StringToHash("Death");
            }
            return _mid;
        }
    }

    static int[] _hid;
    public static int[] HID
    {
        get
        {
            if (_hid == null)
            {
                _hid = new int[6];
                _hid[0] = Animator.StringToHash("Hero_Idle");
                _hid[1] = Animator.StringToHash("Hero_Run");
                _hid[2] = Animator.StringToHash("Hero_Dash");
                _hid[3] = Animator.StringToHash("Hero_Attack");
                _hid[4] = Animator.StringToHash("Hero_TakeHit");
                _hid[5] = Animator.StringToHash("Hero_Death");
            }
            return _hid;
        }
    }
}
