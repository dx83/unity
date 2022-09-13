using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class AnimationClipLoader : MonoBehaviour
{
    private static Dictionary<string, Dictionary<string, AnimationClip>> animationClips = 
        new Dictionary<string, Dictionary<string, AnimationClip>>();

    public static void Load()
    {
        var loadCall = Addressables.LoadAssetsAsync<AnimationClip>(
            "AnimationClip", (result) => {
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
    
    public static Dictionary<string, AnimationClip> GetAnimationClip(string name)
    {
        if (animationClips.ContainsKey(name))
            return animationClips[name];

        return null;
    }
}
