# LOVE/D
LOVE/D is a Chrome's T-Rex Runner and Geometry Dash clone game made in Unity. It is mostly unfinished and I intended it as a game development excercise. The goal of this excercise was to finally finish a game I started making. The goal wasn't really met but that's the reason I'm putting it on GitHub. However, I don't consider this a complete failure because I still managed to learn new things from it.

[PROBLEMS & BUGS]
1. The code is a complete mess. Some scripts interacting with other scripts they should know nothing about, scripts doing things you wouldn't expect them to be doing on based on their name, nested if statements that could be reworked into something much better to look at, no documentation for future readability and many more. This is mostly because of how this small project was made: adding new features whenever I felt like it without caring for future sustainability. What was important to me when adding them was to see them in motion as soon as possible.

2. Hard to judge jump distance when the level speed changes so frequently. Having Fast-fall button does correct this to an extent but it is still not ideal. Changing the level speed to increase only at certain times and remain consistent for at least 10-20 seconds could work as well.

3. The forward dash power-up doesn't feel great since there is not enough visual feedback for it. If done in the air, the fall afterwards can feel really jarring. 

4. Not enough variety in platforms and RNG can sometimes screw you over really bad. If you are close to the edge of the screen and over 60 seconds on the timer and certain obstacle combinations come at you, there might be no possible way for you to avoid them no matter what you do.

5. If you collide with a dash power-up while currently using one, the screen won't change colors and the zoom won't work. This is true for any dash power-ups you pick after that one as well. This won't be fixed unless you completely restart the game.

6. This happened only once out of many playtests but it's still a problem (it's just hard to replicate and locate). When you die in the game and there is a hole in the instantiated ground that hole won't be filled up even after respawning and starting a new run.

7. The obstacle patterns are hardcoded into the the script which makes them hard to understand upon fist glance and impractical to use. Files should have been used to store the obstacle patterns. That would have simplified the creating process of new patterns and the usgage of them in the scripts.

8. Times aren't saved anywhere to incentivise the player to beat their previous record.

9. No sound effects whatsoever or music.

10. No way to exit the game from the game itself.

11. Way too many unnecessary colliders and collision detections for this kind of game.

12. And more that aren't coming to mind while I am writing this...

[HELPFUL RESOURCES]
1. Sebastian Lague's 2D Platformer Controller series: https://www.youtube.com/playlist?list=PLFt_AvWsXl0f0hqURlhyIoAabKPgRsqjz
2. Blackthornprod's Dash Move video: https://www.youtube.com/watch?v=w4YV8s9Wi3w
3. Blackthornprod's Hold Jump Key To Jump Higher video: https://www.youtube.com/watch?v=j111eKN8sJw (although I didn't end up using this approach to jumping)
