# metamorphic_test_ar
This is my ongoing project about finding metamorphic relation in augmented reality application. 
Here, I have implemented the following MR.
1. Raycast within boundary
     1. Test Input: Place a spawn object
     2. Expected output: Spawn object should be inside the boundary of the plane
     3. Transformation: While changing the camera AR camera increases and adds multiple plane. Spawn object in these newly added plane
     4. Expected Transform output: The position of the placed objects should be consistent within the boundary of the newly detected plan
        Implementation-
          1. Use ARPLanemanager’s trackables to get all the plane
          2. Then for each of the plane’s transform, it calculates the dot product
          3. Then check whether the product value is within tolerance.(tolerance set to .1F)
          4. For each of the plane it iterates the process.

2. Check Other object-
    1. Collision with game object- Simulate touch event where there is an existing gameobject
    2. Test Input: Touch and gameobject should be initiated
    3. Transformation: After spawning gameobject,any new gameobject should looking for existing object
    4. Expected Transformed Output: If there is existing gameobject it should not initiated.
   Implementation-
    1. Used layer masking technique of unity
    2. Define a tolerance level and apply raycast
    3. If it does not hit the layer of the plane rather hit the layer of other spawn object then overlapping


3. Visibility and Occlusion:
     1. Test Input: Place an object partially behind a real-world object.
     2. Expected Output: Part of the virtual object is occluded by the real-world object.
     3. Transformation: Move the camera to view the object from different angles.
     4. Expected Transformed Output: The occlusion should adjust correctly as the perspective changes.
     5. Implementation: Use depth sensing and spatial mapping to understand the environment and manage occlusion realistically.

4. Object Scaling with Distance:
     Test Input: Place a spawn object at a known distance.
     Expected Output: Object appears at a size proportional to its distance.
     Transformation: Change the position of the camera, moving closer to or further from the object.
     Expected Transformed Output: The object should scale up or down depending on the camera's distance, maintaining a realistic appearance.
     Implementation: Calculate the distance between the camera and the object and adjust the scale of the object accordingly.

5. Orientation Consistency Relative to Gravity:
     1. Test Input: Place an object with a specific orientation relative to gravity.
     2. Expected Output: Object maintains its orientation (e.g., upright) relative to gravity.
     3. Transformation: Rotate or tilt the AR device.
     4. Expected Transformed Output: The object should adjust its orientation to maintain consistency relative to gravity.

6. Detected MR(Virtual physics and interaction)
   1. Varying rotation in constant-
   2. Test Input: Simulate rotation to evaluate whether its moving constant
   3. Expected Output: The rotation angle should be same
   4. Transformation: change the initial angle and speed
   5. Expected Transformed Output: If the rotation is moving in constant, the rotation angle should be within specific threshold.
7. Check correct object instantiation-
   1. Input- Touch on screen
   2. Expected output- Touch object and instantiate object should be same


