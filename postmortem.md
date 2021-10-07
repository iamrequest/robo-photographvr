# Robo PhotographVR: Postmortem


## Photo Mode

There were 2 methods of taking photos in this game: via an interactable camera that can be grabbed in game, and via a time-stopping photo mode. The first would be similar to the camera in Breath of the Wild, and the latter would be similar to typical photo modes accessed through a pause menu (eg: Code Vein, Spider Man 2019, etc).

### Interactable Camera

I used an interactable in-game object to represent a camera here, which worked pretty well. If I were to do it again, I'd have it take photos in a non-square resolution so that users could rotate the camera to take landscape/portrait photos. It'd be neat to compare the camera's up direction to world up to determine the picture's orientation when it gets taken.

I had considered adding a VR interaction to let the player frame their own pictures with their hands (by extending their thumb and index finger). That might be better for its own experimentation, since it comes with some extra considerations:

* What is the aspect ratio of the picture? 
  * I'd have to consider the distance from each hand, as well as the eye to the hands. 
* Which eye am I starting from when considering direction to the hands? 
  * The dominant eye is typically the right one, but I'd need a settings menu to let players set this manually. Either that, or I'd have to use eye-tracking to determine if one eye is closed.
* How do I start this interaction, without accidentally triggering at the wrong time?
  * This interaction would probably start when the palms are facing opposite from each-other, the thumbs are facing opposite from eachother, thumb and index fingers are the only fingers extended.
    * Hopefully this is an isolated enough pose to prevent the player from accidentally triggering at the wrong time.
    * This would be much better with hand tracking, or index controllers. Not sure how well this'd work when the player has a single button for grips.
  * Maybe clicking one/either index finger in this pose would trigger the photo.

### Free-cam

This was a simple implementation, with some space to grow. To enable photo mode, I set Time.timescale to zero, disabled some key components (camera, VR IK) from the player controller, and enabled my photo mode camera rig. When disabling photo mode, I did the opposite. However, to prevent the player's avatar snapping back to the player's IRL pose, I had to smooth the time scale from 0 to 1. This seems to prevent most of the physics bugs that would happen as a result of the player moving around during stopped time.

In a game with actual consequences, it could be easy to abuse this mechanic, so some thought may need to be put into how often/when this can be implemented. For example, this mechanic could be pretty broken in Blade and Sorcery if the player could stop time, adjust their pose to block an incoming attack, and resume time - effectively allowing the player to block any attack with enough patience. This isn't too different from a pause buffer technique, but the fact that the player can move in real life during the pause (affecting the game state on un-pause) changes things significantly. 

Another consideration is letting the player freely roam in photo mode. How far can they stray from the player? Should they be allowed to move through walls in photo mode? This could potentially ruin some key moments (eg: letting the player peek through a wall or around a corner to spot an enemy, letting the player clip through walls to discover where hidden items are, etc). During this jam, I tested moving the photo mode rig with a character controller instead of just moving their transform around so that they would be bound by collision, but ultimately I wasn't happy with the results. It was too easy to clip through steep terrain, and it prevented the player from getting nice shots being positioned low to the ground. More work needed here if I were to implement it in a game, but I think the character controller route is the best option I've looked at.

### Photo Export

Exporting screenshots via an interactable in the game was really cool to me - it kinda reminds me of how you could print your photos from Pokemon Snap at Blockbuster. I would consider adding something like this in-game for future projects. It seems like a nice addition to your home base to make the area more interesting, especially if you could review the photos you took with friends after a mission or something.

I ran into an issue with the brightness/color of my exported images. The exported photos look much darker than what you see in game. Part of that issue is due to the fact that I capture the scene with post-processing, and then the player sees the image in-scene with that same post-processing enabled (effectively 2x the post-processing). 

I'm export my rendertextures as a PNG - however it seems like PNGs don't store gamma-corrected/color-corrected data. Something along those lines anyways, I don't know enough about image formats to diagnose the issue properly. Here's some reading material I found after the jam that might be helpful, if others run into this issue too:

* [Maybe there's an issue with the default color space being Linear instead of Gamma?](https://github.com/Hotrian/HeadlessOverlayToolkit/issues/7) I think SteamVR sets this during setup once you download it from the asset store, so I think it's probably important.
* [Maybe you can get around this by creating a new render texture in-code, and then blit the values over?](https://forum.unity.com/threads/writting-to-rendertexture-comes-out-darker.427631/#post-2764218) I'm already duplicating the render texture via the constructor, so I'm not sure if this would help. 
* I suspect the issue could also be related to the exported texture format (I use RGB24), but I'm not sure which one would be best.

## Full body playable character

This is something that turned out good enough, but there's lots of room for improvement. I was able to get calibration of both VRIK (finalIK) component, and hexabody calibrated in the same step, which seems to have worked well enough. 

### Hexabody

I've been meaning to try this outside of whitebox testing, it's pretty plug and play. If I had more time, I'd implement some interface to allow the user to toggle between sitting and standing - I wasn't sure how that would affect VRIK calibration, so I decided to leave it for next time. I'd also like to experiment with player scale after the calibration step. For example: We've calibrated the player to be x meters tall while standing, but in this game they're playing as an x centimeter tall creature. In this case, the player should get scaled down to match some target height. 

### FinalIK (VRIK)

I'm pretty impressed with how well this works out of the box (once you get hurricaneVR integration working). It works much better than my prototype using two-bone constraints using Unity's animation rigging package!

I'd like to work on getting leg animations working next - [the built-in procedural locomotion animation can look pretty silly at high speeds](https://twitter.com/requestmoe/status/1445571928920494094). This is something I briefly tried to get working, but ultimately didn't have enough time to get functional.

### HurricaneVR

Once I got HurricaneVR hooked up to my finalIK rig, it worked pretty smoothly. I suspect the hand bone alignment step went much smoother since I was using a mixamo model, but I'd like to try it out with my own model once I get more blender practice in (hands are hard to model!).

I did have a problem with the alignment of hand poses at different height scales, and it's much more noticeable at extreme player-calibrated heights. Because of this, it's possible for a shorter/taller player to see their hands clipping through some interactables, even though the original poses look fine. I suspect I have some of the finalIK hand transforms misaligned, but I'll need to play around a bit more to see which ones they are. I suspect this is a fixable issue.
