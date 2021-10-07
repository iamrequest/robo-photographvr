# Robo PhotographVR

You're a robot let loose with a camera in a lovely Autumn park. What will you choose to capture in film?

Take pictures of yourself or your surroundings with a handheld camera! Or, enter photo mode to fly around the scene, taking photos from a third-person perspective. Once you're happy with your pictures, you can use the Save Machine to export your pics to a PNG file!

This was my submission to "[Ludum Dare 49](https://ldjam.com/events/ludum-dare/49/robo-photographvr)", a 3 day game jam.
The goal for this project was to experiment with a full-body playable character (via FinalIK + HurricaneVR + Hexabody), and photo mode options (both in the context of VR).

You can download the game for free [here, on itch.io](https://request.itch.io/robo-photographvr), or you can check out my other projects at [request.moe](https://request.moe). You can also [read the postmortem for this project](./postmortem.md), if you're interested!

You're free to do whatever with this code, but if you do use it, it'd be real cool of you to link back to this page or the itch.io page (or both). Thanks!

## Setup

  1. Clone this repo
  2. Install the following assets from the Unity Asset Store:
     * [SteamVR Plugin](https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647)
     * [Hurricane VR](https://assetstore.unity.com/packages/tools/physics/hurricane-vr-physics-interaction-toolkit-177300)
     * [Hexabody VR Player Controller](https://assetstore.unity.com/packages/tools/physics/hexabody-vr-player-controller-185521)
     * [Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)

## Project Settings 

- Unity 2019.4.24f1
- Universal Render Pipeline (7.5.3)
- OpenVR Desktop (2.0.5)
- SteamVR (2.7.3, sdk 1.14.15)
- TextMesh Pro (2.1.4)
- Visual Effect Graph (7.5.3)
- Hurricane VR (2.3)
- Odin Inspector (3.0.9)
- Hexabody VR Player Controller (1.25)

## Third Party Assets

Type|Asset|Author|License
-|-|-|-
Font|[Press Start 2P Font](https://www.fontspace.com/press-start-2p-font-f11591)|[codeman38]()|[SIL Open Font License (OFL)](https://www.fontspace.com/help#license-17)
Shader|[Skybox Gradient Shader](https://www.youtube.com/watch?v=f6zUot73-gg&list=WL&index=9)|[Alex Strook](https://twitter.com/AlexStrook)|[]()
BGM|[The Forest of the Yellow Witch](https://opengameart.org/content/the-forest-of-the-yellow-witch-low-intensity-adventure-loopable)|[Request](https://twitter.com/requestmoe)|[CC0 1.0 Universal](https://creativecommons.org/publicdomain/zero/1.0/)
Textures|[UI Pack: RPG Expansion](https://www.kenney.nl/assets/ui-pack-rpg-expansion)|[Kenney](https://twitter.com/KenneyNL)|[CC0 1.0 Universal](https://creativecommons.org/publicdomain/zero/1.0/)
Textures|[Game Icons](https://kenney.nl/assets/game-icons)|[Kenney](https://twitter.com/KenneyNL)|[CC0 1.0 Universal](https://creativecommons.org/publicdomain/zero/1.0/)
Textures|[UI Pack: Space Expansion](https://kenney.nl/assets/ui-pack-space-expansion)|[Kenney](https://twitter.com/KenneyNL)|[CC0 1.0 Universal](https://creativecommons.org/publicdomain/zero/1.0/)

## Unity Assets

Type|Asset|Asset URL|Author
-|-|-|-
VR Base Framework|SteamVR Plugin|https://assetstore.unity.com/packages/tools/integration/steamvr-plugin-32647|Valve
VR Interactions|Hurricane VR|https://assetstore.unity.com/packages/tools/physics/hurricane-vr-physics-interaction-toolkit-177300|Cloudwalkin Games 
VR Player Controller|Hexabody|https://assetstore.unity.com/packages/tools/physics/hexabody-vr-player-controller-185521|Cloudwalkin Games 
Inspector|Odin Inspector and Serializer|https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041|Sirenix
