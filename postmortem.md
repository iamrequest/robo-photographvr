# Postmortem


## Photo Mode

There were 2 methods of taking photos in this game: via an interactable camera that can be grabbed in game, and via a time-stopping photo mode. The first would be similar to the camera in Breath of the Wild, and the latter would be similar to typical photo modes accessed through a pause menu (eg: Code Vein, Spider Man 2019, etc).

### Interactable Camera

I used a physical object to represent a camera here, which worked pretty well. If I were to do it again, I'd have it take photos in a non-square resolution so that users could rotate the camera to take landscape/portrait photos. It'd be neat to compare the camera's up direction to world up to determine the picture's orientation when it gets taken.

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
