using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public enum MusicChoice {
    adventure,
    happy,
    suspense,
    drama
}

public class SimpleDialog : BaseNode {

    [Input] public string entry;
    [Output] public string exit;

    [TextArea(7, 20)]
    public string DialogText;
    public Sprite BackgroundImage;
    
    // 🎭 Actor Image Placeholder
    public Sprite ActorImage;
    
    // 🎬 Animate Actor Image (Checkbox)
    public bool animateActor;

    // 🎵 Dropdown to select background music
    public MusicChoice backgroundMusicChoice;

    // ✅ Define a default background to prevent null issues
    public static Sprite defaultBackground;

    public override string getDialogText() {
        return DialogText;
    }

    public override Sprite getSprite() {
        return BackgroundImage != null ? BackgroundImage : defaultBackground;
    }

    public AudioClip getBackgroundMusic() {
        return MusicManager.Instance.GetMusic(backgroundMusicChoice);
    }

    // 🎵 Play selected background music
    public void PlaySelectedMusic() {
        MusicManager.Instance.PlayMusic(backgroundMusicChoice);
    }
}
