using UnityEngine;

public class ReadMe : MonoBehaviour
{
    /*
    welcome to my scuffed read me.

    My name is Benjamin Alexander and this is my project documentation

    Alot of important information will hopefully be added here before the end of impelmentation.
    if you are added to this project i aplogize before hand, but i wish you luck.

    Please keep in mind this documentation is designed to give additional information and was most likely written as i was writing new code.  Thus spelling and puncuation may not be on point.
    Listen just be happy i even took the time to write this.  Like im trying my best over here man.
    
    I will try and organize the majoriy of this information in blocks each new block will be prefaced by ***** that way it is obvious its a new section

    Please keep in mind most of this project is experimental and open to changes.  The information you read here most likely isnt the best way to implement these features.  
    This is just what my project provides.

    This project is a courseware designed to teach students about computer science.  Im making a platform to showcase a new way to approach learning that hasnt really been explored yet.  
    If you have questions or suggestions email me at 005886968@coyote.csusb.edu  Hopefully i still use this email.  

    ******************************************* VR  Controls *******************************************
    This section will be dedicated to information regarding VR controls and interfaces

    So most of the VR controls are on the top layer of the VR controller prefab.  
    See VRController1 Script.
    This script controls inputs and actions that players can perform or use in the game.

    The VR controls each need their own input module.  Both controls actually cant interact with UI at the same time.  Let me explain.
    Since each controll uses a different event camera to sned their actions we can not use both controls at the same time.  We have to choose one or the other.  
    The find pointer Canvas script will read the controll that was last used and set that as the event camera.  
    You can find this on the top most canvas object under the World space GO
    This does not apply to objects, only canvases.  
    At the time of development I tried to combine the cameras for both controls, but this obviously led to come problems.  There may be a better solution now.

    You can find the code for the Pointer under the VR controller -> camera Rig -> Conroller(left or right) -> CamCan -> PR_Pointer(L or R)
    This code controls our line renderer and send raycast information to our input module.  
    See https://www.youtube.com/c/VRwithAndrew/videos for some great tutorials

    If you would like to add actions, or change input controls click window -> steamVR Input
    Here you can add or change input buttons and actions.  make sure to click save and generate.
    Additionally you can open the binding UI to apply input actions to buttons on the controls you ahve.

    Almost all of the other components are provided by steam VR.  See their documentation for more information.
    
    Something i forgot to mention, when making a button you have to add 'public void OnPointerDown(PointerEventData eventData)'
    otherwise your controller wont be able to interact with it and push the button down.  It also needs to be under a canvas with the correct event camera attached to it.  If you dont do this you wont get any interaction with the button.

    Additional note Playerholder is my attempt at implementing non VR controls in the same scene as VR.  
    Ill probably ahve to make a separate scene for VR and NonVR.  If i fixed this and forgot to document it, then please go about your business as if you didnt see this.  

    **************************************** Sorting Algorithms ****************************************
    This section will be decicated to information regarding sorting algorithms

    Prepare to pull out some hair.  This is easily the most confusing section.  Hope you arent prone to punching walls.

    Probably reading the documentation and observing the file structure for bubble sort would be a good idea.  I feel this is the easiest code to understand, and the easiest algorithm to grasp.
    Plus this is probably my golden child.  Out of all the sorting algorithms i think this one was tested and worked on the most.  Just because i needed to get one perfect before finishing the others.  
    
    **************************************** Paradigm Tutorials ****************************************
    This section will be dedicated to information on paradigm tutorials    

    Probably the easiest section to understand.
    Just usign rich text i wrote out some explenations for the different features each paradigm offers.

    At the top layer of each feature we have a script that basically controls everything for that feature.  
    These scripts are really reusable.  You just have to change a couple lines in the scoperun function. 
    These functions will highlight different lines of text using a smei transparent 2d box.  Then as it runs it will print the output to a text box.  

    ******************************************* Optimization *******************************************
    This section will be decicated to optimization and other general things that may not be obvious  

    A couple simple things i did for optimization.  

    Lighting is baked.  nothing is realtime.  Its just not needed.  This is courseware.  its not supposed to be flashy or graphically impressive.  We dont care about that here.  

    The sorting room is pretty costly.  alot of coroutines might be running at the same time when you are in here.  Thus when the player is not insdie the room everything is disabled.
    When a gameobject is disabled the coroutine will stop.  Thus it will not be processing anything.  I doubt there will be too many problems with memory or computation speed.  

    If the player falls through the floor he will be spawned back in the spawn room.  If not then life is tough man.

    *********************************************** Misc ***********************************************
    This section will be dedicated to information on features or function that are independent and im just too lazy to figure out how to catageroize them

    Teleporter frames: Each door you see has a teleport function.  When walking into the door you will teleport to a pre determined spot.
    If you want to learn more look for the teleport link script.  Teleport_to script is all you need.  Just feed it a position you want to send the player to.  
    The player itself is atuomatically retrieved.

    Rock VR is a great way to make recordings in unity.  Point the Dedicatedcapture game object towards what you want to record and click start recording.  Make sure you click stop recording before closing the game.
    You will find a dedicatedCApture gameoject connected to the camera head under the VRcontroller prefab.  if you want to record the player view then disable the top level dedicated capture and enable this one.
    The videocaptureCTRL needs a dedicated capture to start recording. So make sure one of each of these is active.

    */
}
