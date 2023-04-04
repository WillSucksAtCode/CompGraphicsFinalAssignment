# CompGraphicsFinalAssignment

*Farhan* - When it came down to part 2 (Texturing) I utilized two softwares to create custom models and textures to be implemented into the game. I used Blender to create custom 3D models and also UV unwrapped in blender as well using blender to smart UV unwrap for me. After creating the model and UV unwrapping, The model was exported as an FBX file and then brought into a different software, Substance 3D Painter. In Substance Painter, i added the textures/materials to the model and then exported the textures as specific 2D images for each slot in Unity: Albedo, Metallic, Normal Map, Height, and Occlusion. 

![Screenshot 2023-04-04 020557](https://user-images.githubusercontent.com/72412425/229908673-f128242d-c9eb-4b8b-b1cc-5d62eeae767d.png)

As you can see in the image above, there's images in each slot that were specifically created thanks to Substance Painter and the image below is how the model and the textures look

![Screenshot 2023-04-04 020624](https://user-images.githubusercontent.com/72412425/229908876-6cc06f44-1175-49df-815c-21566a4ce6fa.png)

First and foremost, Will and I couldn't get LUT to work in the game because we are using URP and LUT just doesn't want to co-operate. Although we couldn't get it to work we still tried our best by using the script and shader that was provided to us in the lecture. I used the basic Warm and Cool LUT that I created in photoshop which I then saved as a png and added it to the LUT shader material. 

*William* - For Part 4: Visual Effects, I added decals to our scene to create a more ominous feel to the scene by adding a bloody hand to our facility door. Decals are done by layering an extra texture on top of an already existing one. We do this by getting two textures and then applying the decal texture in the surf function using the albedo. In Unity’s render pipeline, we implement it using a decal material on a decal projector. The image below is how it looks like. 

![image](https://user-images.githubusercontent.com/72412425/229915701-0d262dba-be34-485e-9698-b36d6739a617.png)

*Farhan* - I used Unity's built in particle system to create the rain particles that is in our scene as well as particles for the guns to act as a muzzle flash. The rain particles are looped and are relased in a square shape around the player. I then chose "Random between two constants" in the linear section of velocity of its lifetime. The constants' values were only changed in the Y-Axis to -25, and -35. This way we can have different variations to how it falls. For the gun particles, I used the standard cone emission and were slightly modified for each gun to fit their firing styles.

![Screenshot 2023-04-04 015210](https://user-images.githubusercontent.com/72412425/229916901-ddf8eee8-3268-4751-913e-a09a6c3ff71f.png) 
![Screenshot 2023-04-04 041525](https://user-images.githubusercontent.com/72412425/229916955-15e11315-8129-476d-94a3-a26d243ad595.png)

Moving on to the Additional Post Process effects that were added, I added bloom while Farhan added motion blur. I implemented bloom as a way to add a sense of moonlight reflecting off the trees. Bloom is achieved by blurring the view of the camera, brightening that and then adding it to the original view, and that can be achieved with the blend mode One. In Unity’s rendering pipeline it is simply done using the post processing effect of the same name, with the addition of a subtle blue tint.

![1](https://user-images.githubusercontent.com/72412425/229922214-c4d463e5-b270-484a-b76a-b63d47410faa.png)

I also added in Fog to our scene to once again add more mysterious and ominous feel to the game. I did this by going to Window -> Rendering -> Lighting. In the lighting tab there is a "Other settings" which allows us to add fog and play with the fog's settings such as it's color and density. 

![Screenshot 2023-04-04 023946](https://user-images.githubusercontent.com/72412425/229922986-21539f64-fbe1-4374-9a01-5dcd1565dca8.png)

*Farhan* - I implemented motion blur which was done by the post processing motion blur effect in unity's render pipeline. Motion blur is the process of blurring the player's view in the direction of movement. It basically gets the frames over a set amount of time and blends them together to get a blur effect.

![Screenshot 2023-04-04 011329](https://user-images.githubusercontent.com/72412425/229923034-f2cc7891-4570-4136-b14b-6fe2ca746af6.png)

