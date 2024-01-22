# metamorphic_test_ar
This is my ongoing project about finding metamorphic relation in augmented reality application. 
Here I have implemented the follwing MR.
1. Detected MR(Raycast within boundary)
     1. Test Input: Place a spawn object
     2. Expected output: Spawn object should be inside the boundary of the plane
     3. Transformation: While changing the camera AR camera increases and adds multiple plane. Spawn object in these newly added plane
     4. Expected Transform output: The position of the placed objects should be consistent within the boundary of the newly detected plan
        Implementation-
          1. Use ARPLanemanager’s trackables to get all the plane
          2. Then for each of the plane’s transform, it calculates the dot product
          3. Then check whether the product value is within tolerance.(tolerance set to .1F)
          4. For each of the plane it iterates the process.

2. Detected MR(Check View)-
    1. Collision with gameobject- Simulate touch event where there is existing gameobject
    2. Test Input: Touch and gameobject should be initiated
    3. Transformation: After spawning gameobject,any new gameobject should looking for existing object
    4. Expected Transformed Output: If there is existing gameobject it should not initiated and existing gameobject should change its position.
    5. The implementation provide information from one camera position which objects are visible and which are not
   Implementation-
    1. Used layer masking technique of unity
    2. Define a tolerance level and apply raycast
    3. If it does not hit the layer of the plane rather hit the layer of other spawn object then overlapping
3. Detected MR(Virtual physics and interaction)
   1. Varying rotation in constant-
   2. Test Input: Simulate rotation to evaluate whether its moving constant
   3. Expected Output: The rotation angle should be same
   4. Transformation: change the initial angle and speed
   5. Expected Transformed Output: If the rotation is moving in constant, the rotation angle should be within specific threshold.

   


