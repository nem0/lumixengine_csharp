{
auto fAnimationScene_getControllerSource = &csharp_getProperty<Path, AnimationScene, &AnimationScene::getControllerSource>;
mono_add_internal_call("Lumix.AnimController::getControllerSource", fAnimationScene_getControllerSource);
auto fAnimationScene_setControllerSource = &csharp_setProperty<Path, AnimationScene, &AnimationScene::setControllerSource>;
mono_add_internal_call("Lumix.AnimController::setControllerSource", fAnimationScene_setControllerSource);

}
{
auto fAnimationScene_getAnimation = &csharp_getProperty<Path, AnimationScene, &AnimationScene::getAnimation>;
mono_add_internal_call("Lumix.Animable::getAnimation", fAnimationScene_getAnimation);
auto fAnimationScene_setAnimation = &csharp_setProperty<Path, AnimationScene, &AnimationScene::setAnimation>;
mono_add_internal_call("Lumix.Animable::setAnimation", fAnimationScene_setAnimation);

}
{
auto fAnimationScene_getAnimableStartTime = &csharp_getProperty<float, AnimationScene, &AnimationScene::getAnimableStartTime>;
mono_add_internal_call("Lumix.Animable::getAnimableStartTime", fAnimationScene_getAnimableStartTime);
auto fAnimationScene_setAnimableStartTime = &csharp_setProperty<float, AnimationScene, &AnimationScene::setAnimableStartTime>;
mono_add_internal_call("Lumix.Animable::setAnimableStartTime", fAnimationScene_setAnimableStartTime);

}
{
auto fAnimationScene_getAnimableTimeScale = &csharp_getProperty<float, AnimationScene, &AnimationScene::getAnimableTimeScale>;
mono_add_internal_call("Lumix.Animable::getAnimableTimeScale", fAnimationScene_getAnimableTimeScale);
auto fAnimationScene_setAnimableTimeScale = &csharp_setProperty<float, AnimationScene, &AnimationScene::setAnimableTimeScale>;
mono_add_internal_call("Lumix.Animable::setAnimableTimeScale", fAnimationScene_setAnimableTimeScale);

}
{
auto fAnimationScene_getSharedControllerParent = &csharp_getProperty<Entity, AnimationScene, &AnimationScene::getSharedControllerParent>;
mono_add_internal_call("Lumix.SharedAnimController::getSharedControllerParent", fAnimationScene_getSharedControllerParent);
auto fAnimationScene_setSharedControllerParent = &csharp_setProperty<Entity, AnimationScene, &AnimationScene::setSharedControllerParent>;
mono_add_internal_call("Lumix.SharedAnimController::setSharedControllerParent", fAnimationScene_setSharedControllerParent);

}
{
auto fAudioScene_getAmbientSoundClipIndex = &csharp_getProperty<int, AudioScene, &AudioScene::getAmbientSoundClipIndex>;
mono_add_internal_call("Lumix.AmbientSound::getAmbientSoundClipIndex", fAudioScene_getAmbientSoundClipIndex);
auto fAudioScene_setAmbientSoundClipIndex = &csharp_setProperty<int, AudioScene, &AudioScene::setAmbientSoundClipIndex>;
mono_add_internal_call("Lumix.AmbientSound::setAmbientSoundClipIndex", fAudioScene_setAmbientSoundClipIndex);

}
{
auto fAudioScene_isAmbientSound3D = &csharp_getProperty<bool, AudioScene, &AudioScene::isAmbientSound3D>;
mono_add_internal_call("Lumix.AmbientSound::isAmbientSound3D", fAudioScene_isAmbientSound3D);
auto fAudioScene_setAmbientSound3D = &csharp_setProperty<bool, AudioScene, &AudioScene::setAmbientSound3D>;
mono_add_internal_call("Lumix.AmbientSound::setAmbientSound3D", fAudioScene_setAmbientSound3D);

}
{
auto fAudioScene_getEchoZoneRadius = &csharp_getProperty<float, AudioScene, &AudioScene::getEchoZoneRadius>;
mono_add_internal_call("Lumix.EchoZone::getEchoZoneRadius", fAudioScene_getEchoZoneRadius);
auto fAudioScene_setEchoZoneRadius = &csharp_setProperty<float, AudioScene, &AudioScene::setEchoZoneRadius>;
mono_add_internal_call("Lumix.EchoZone::setEchoZoneRadius", fAudioScene_setEchoZoneRadius);

}
{
auto fAudioScene_getEchoZoneDelay = &csharp_getProperty<float, AudioScene, &AudioScene::getEchoZoneDelay>;
mono_add_internal_call("Lumix.EchoZone::getEchoZoneDelay", fAudioScene_getEchoZoneDelay);
auto fAudioScene_setEchoZoneDelay = &csharp_setProperty<float, AudioScene, &AudioScene::setEchoZoneDelay>;
mono_add_internal_call("Lumix.EchoZone::setEchoZoneDelay", fAudioScene_setEchoZoneDelay);

}
{
auto fNavigationScene_getAgentRadius = &csharp_getProperty<float, NavigationScene, &NavigationScene::getAgentRadius>;
mono_add_internal_call("Lumix.NavmeshAgent::getAgentRadius", fNavigationScene_getAgentRadius);
auto fNavigationScene_setAgentRadius = &csharp_setProperty<float, NavigationScene, &NavigationScene::setAgentRadius>;
mono_add_internal_call("Lumix.NavmeshAgent::setAgentRadius", fNavigationScene_setAgentRadius);

}
{
auto fNavigationScene_getAgentHeight = &csharp_getProperty<float, NavigationScene, &NavigationScene::getAgentHeight>;
mono_add_internal_call("Lumix.NavmeshAgent::getAgentHeight", fNavigationScene_getAgentHeight);
auto fNavigationScene_setAgentHeight = &csharp_setProperty<float, NavigationScene, &NavigationScene::setAgentHeight>;
mono_add_internal_call("Lumix.NavmeshAgent::setAgentHeight", fNavigationScene_setAgentHeight);

}
{
auto fNavigationScene_useAgentRootMotion = &csharp_getProperty<bool, NavigationScene, &NavigationScene::useAgentRootMotion>;
mono_add_internal_call("Lumix.NavmeshAgent::useAgentRootMotion", fNavigationScene_useAgentRootMotion);
auto fNavigationScene_setUseAgentRootMotion = &csharp_setProperty<bool, NavigationScene, &NavigationScene::setUseAgentRootMotion>;
mono_add_internal_call("Lumix.NavmeshAgent::setUseAgentRootMotion", fNavigationScene_setUseAgentRootMotion);

}
{
auto fNavigationScene_isGettingRootMotionFromAnim = &csharp_getProperty<bool, NavigationScene, &NavigationScene::isGettingRootMotionFromAnim>;
mono_add_internal_call("Lumix.NavmeshAgent::isGettingRootMotionFromAnim", fNavigationScene_isGettingRootMotionFromAnim);
auto fNavigationScene_setIsGettingRootMotionFromAnim = &csharp_setProperty<bool, NavigationScene, &NavigationScene::setIsGettingRootMotionFromAnim>;
mono_add_internal_call("Lumix.NavmeshAgent::setIsGettingRootMotionFromAnim", fNavigationScene_setIsGettingRootMotionFromAnim);

}
{
auto fPhysicsScene_getRagdollLayer = &csharp_getProperty<int, PhysicsScene, &PhysicsScene::getRagdollLayer>;
mono_add_internal_call("Lumix.Ragdoll::getRagdollLayer", fPhysicsScene_getRagdollLayer);
auto fPhysicsScene_setRagdollLayer = &csharp_setProperty<int, PhysicsScene, &PhysicsScene::setRagdollLayer>;
mono_add_internal_call("Lumix.Ragdoll::setRagdollLayer", fPhysicsScene_setRagdollLayer);

}
{
auto fPhysicsScene_getSphereRadius = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getSphereRadius>;
mono_add_internal_call("Lumix.SphereRigidActor::getSphereRadius", fPhysicsScene_getSphereRadius);
auto fPhysicsScene_setSphereRadius = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setSphereRadius>;
mono_add_internal_call("Lumix.SphereRigidActor::setSphereRadius", fPhysicsScene_setSphereRadius);

}
{
auto fPhysicsScene_getDynamicType = &csharp_getProperty<Lumix::PhysicsScene::DynamicType, PhysicsScene, &PhysicsScene::getDynamicType>;
mono_add_internal_call("Lumix.SphereRigidActor::getDynamicType", fPhysicsScene_getDynamicType);
auto fPhysicsScene_setDynamicType = &csharp_setProperty<Lumix::PhysicsScene::DynamicType, PhysicsScene, &PhysicsScene::setDynamicType>;
mono_add_internal_call("Lumix.SphereRigidActor::setDynamicType", fPhysicsScene_setDynamicType);

}
{
auto fPhysicsScene_isTrigger = &csharp_getProperty<bool, PhysicsScene, &PhysicsScene::isTrigger>;
mono_add_internal_call("Lumix.SphereRigidActor::isTrigger", fPhysicsScene_isTrigger);
auto fPhysicsScene_setIsTrigger = &csharp_setProperty<bool, PhysicsScene, &PhysicsScene::setIsTrigger>;
mono_add_internal_call("Lumix.SphereRigidActor::setIsTrigger", fPhysicsScene_setIsTrigger);

}
{
auto fPhysicsScene_getCapsuleRadius = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getCapsuleRadius>;
mono_add_internal_call("Lumix.CapsuleRigidActor::getCapsuleRadius", fPhysicsScene_getCapsuleRadius);
auto fPhysicsScene_setCapsuleRadius = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setCapsuleRadius>;
mono_add_internal_call("Lumix.CapsuleRigidActor::setCapsuleRadius", fPhysicsScene_setCapsuleRadius);

}
{
auto fPhysicsScene_getCapsuleHeight = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getCapsuleHeight>;
mono_add_internal_call("Lumix.CapsuleRigidActor::getCapsuleHeight", fPhysicsScene_getCapsuleHeight);
auto fPhysicsScene_setCapsuleHeight = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setCapsuleHeight>;
mono_add_internal_call("Lumix.CapsuleRigidActor::setCapsuleHeight", fPhysicsScene_setCapsuleHeight);

}
{
auto fPhysicsScene_getDynamicType = &csharp_getProperty<Lumix::PhysicsScene::DynamicType, PhysicsScene, &PhysicsScene::getDynamicType>;
mono_add_internal_call("Lumix.CapsuleRigidActor::getDynamicType", fPhysicsScene_getDynamicType);
auto fPhysicsScene_setDynamicType = &csharp_setProperty<Lumix::PhysicsScene::DynamicType, PhysicsScene, &PhysicsScene::setDynamicType>;
mono_add_internal_call("Lumix.CapsuleRigidActor::setDynamicType", fPhysicsScene_setDynamicType);

}
{
auto fPhysicsScene_isTrigger = &csharp_getProperty<bool, PhysicsScene, &PhysicsScene::isTrigger>;
mono_add_internal_call("Lumix.CapsuleRigidActor::isTrigger", fPhysicsScene_isTrigger);
auto fPhysicsScene_setIsTrigger = &csharp_setProperty<bool, PhysicsScene, &PhysicsScene::setIsTrigger>;
mono_add_internal_call("Lumix.CapsuleRigidActor::setIsTrigger", fPhysicsScene_setIsTrigger);

}
{
auto fPhysicsScene_getJointConnectedBody = &csharp_getProperty<Entity, PhysicsScene, &PhysicsScene::getJointConnectedBody>;
mono_add_internal_call("Lumix.D6Joint::getJointConnectedBody", fPhysicsScene_getJointConnectedBody);
auto fPhysicsScene_setJointConnectedBody = &csharp_setProperty<Entity, PhysicsScene, &PhysicsScene::setJointConnectedBody>;
mono_add_internal_call("Lumix.D6Joint::setJointConnectedBody", fPhysicsScene_setJointConnectedBody);

}
{
auto fPhysicsScene_getJointAxisPosition = &csharp_getProperty<Vec3, PhysicsScene, &PhysicsScene::getJointAxisPosition>;
mono_add_internal_call("Lumix.D6Joint::getJointAxisPosition", fPhysicsScene_getJointAxisPosition);
auto fPhysicsScene_setJointAxisPosition = &csharp_setProperty<Vec3, PhysicsScene, &PhysicsScene::setJointAxisPosition>;
mono_add_internal_call("Lumix.D6Joint::setJointAxisPosition", fPhysicsScene_setJointAxisPosition);

}
{
auto fPhysicsScene_getJointAxisDirection = &csharp_getProperty<Vec3, PhysicsScene, &PhysicsScene::getJointAxisDirection>;
mono_add_internal_call("Lumix.D6Joint::getJointAxisDirection", fPhysicsScene_getJointAxisDirection);
auto fPhysicsScene_setJointAxisDirection = &csharp_setProperty<Vec3, PhysicsScene, &PhysicsScene::setJointAxisDirection>;
mono_add_internal_call("Lumix.D6Joint::setJointAxisDirection", fPhysicsScene_setJointAxisDirection);

}
{
auto fPhysicsScene_getD6JointXMotion = &csharp_getProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::getD6JointXMotion>;
mono_add_internal_call("Lumix.D6Joint::getD6JointXMotion", fPhysicsScene_getD6JointXMotion);
auto fPhysicsScene_setD6JointXMotion = &csharp_setProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::setD6JointXMotion>;
mono_add_internal_call("Lumix.D6Joint::setD6JointXMotion", fPhysicsScene_setD6JointXMotion);

}
{
auto fPhysicsScene_getD6JointYMotion = &csharp_getProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::getD6JointYMotion>;
mono_add_internal_call("Lumix.D6Joint::getD6JointYMotion", fPhysicsScene_getD6JointYMotion);
auto fPhysicsScene_setD6JointYMotion = &csharp_setProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::setD6JointYMotion>;
mono_add_internal_call("Lumix.D6Joint::setD6JointYMotion", fPhysicsScene_setD6JointYMotion);

}
{
auto fPhysicsScene_getD6JointZMotion = &csharp_getProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::getD6JointZMotion>;
mono_add_internal_call("Lumix.D6Joint::getD6JointZMotion", fPhysicsScene_getD6JointZMotion);
auto fPhysicsScene_setD6JointZMotion = &csharp_setProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::setD6JointZMotion>;
mono_add_internal_call("Lumix.D6Joint::setD6JointZMotion", fPhysicsScene_setD6JointZMotion);

}
{
auto fPhysicsScene_getD6JointSwing1Motion = &csharp_getProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::getD6JointSwing1Motion>;
mono_add_internal_call("Lumix.D6Joint::getD6JointSwing1Motion", fPhysicsScene_getD6JointSwing1Motion);
auto fPhysicsScene_setD6JointSwing1Motion = &csharp_setProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::setD6JointSwing1Motion>;
mono_add_internal_call("Lumix.D6Joint::setD6JointSwing1Motion", fPhysicsScene_setD6JointSwing1Motion);

}
{
auto fPhysicsScene_getD6JointSwing2Motion = &csharp_getProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::getD6JointSwing2Motion>;
mono_add_internal_call("Lumix.D6Joint::getD6JointSwing2Motion", fPhysicsScene_getD6JointSwing2Motion);
auto fPhysicsScene_setD6JointSwing2Motion = &csharp_setProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::setD6JointSwing2Motion>;
mono_add_internal_call("Lumix.D6Joint::setD6JointSwing2Motion", fPhysicsScene_setD6JointSwing2Motion);

}
{
auto fPhysicsScene_getD6JointTwistMotion = &csharp_getProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::getD6JointTwistMotion>;
mono_add_internal_call("Lumix.D6Joint::getD6JointTwistMotion", fPhysicsScene_getD6JointTwistMotion);
auto fPhysicsScene_setD6JointTwistMotion = &csharp_setProperty<Lumix::PhysicsScene::D6Motion, PhysicsScene, &PhysicsScene::setD6JointTwistMotion>;
mono_add_internal_call("Lumix.D6Joint::setD6JointTwistMotion", fPhysicsScene_setD6JointTwistMotion);

}
{
auto fPhysicsScene_getD6JointLinearLimit = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getD6JointLinearLimit>;
mono_add_internal_call("Lumix.D6Joint::getD6JointLinearLimit", fPhysicsScene_getD6JointLinearLimit);
auto fPhysicsScene_setD6JointLinearLimit = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setD6JointLinearLimit>;
mono_add_internal_call("Lumix.D6Joint::setD6JointLinearLimit", fPhysicsScene_setD6JointLinearLimit);

}
{
auto fPhysicsScene_getJointConnectedBody = &csharp_getProperty<Entity, PhysicsScene, &PhysicsScene::getJointConnectedBody>;
mono_add_internal_call("Lumix.SphericalJoint::getJointConnectedBody", fPhysicsScene_getJointConnectedBody);
auto fPhysicsScene_setJointConnectedBody = &csharp_setProperty<Entity, PhysicsScene, &PhysicsScene::setJointConnectedBody>;
mono_add_internal_call("Lumix.SphericalJoint::setJointConnectedBody", fPhysicsScene_setJointConnectedBody);

}
{
auto fPhysicsScene_getJointAxisPosition = &csharp_getProperty<Vec3, PhysicsScene, &PhysicsScene::getJointAxisPosition>;
mono_add_internal_call("Lumix.SphericalJoint::getJointAxisPosition", fPhysicsScene_getJointAxisPosition);
auto fPhysicsScene_setJointAxisPosition = &csharp_setProperty<Vec3, PhysicsScene, &PhysicsScene::setJointAxisPosition>;
mono_add_internal_call("Lumix.SphericalJoint::setJointAxisPosition", fPhysicsScene_setJointAxisPosition);

}
{
auto fPhysicsScene_getJointAxisDirection = &csharp_getProperty<Vec3, PhysicsScene, &PhysicsScene::getJointAxisDirection>;
mono_add_internal_call("Lumix.SphericalJoint::getJointAxisDirection", fPhysicsScene_getJointAxisDirection);
auto fPhysicsScene_setJointAxisDirection = &csharp_setProperty<Vec3, PhysicsScene, &PhysicsScene::setJointAxisDirection>;
mono_add_internal_call("Lumix.SphericalJoint::setJointAxisDirection", fPhysicsScene_setJointAxisDirection);

}
{
auto fPhysicsScene_getSphericalJointUseLimit = &csharp_getProperty<bool, PhysicsScene, &PhysicsScene::getSphericalJointUseLimit>;
mono_add_internal_call("Lumix.SphericalJoint::getSphericalJointUseLimit", fPhysicsScene_getSphericalJointUseLimit);
auto fPhysicsScene_setSphericalJointUseLimit = &csharp_setProperty<bool, PhysicsScene, &PhysicsScene::setSphericalJointUseLimit>;
mono_add_internal_call("Lumix.SphericalJoint::setSphericalJointUseLimit", fPhysicsScene_setSphericalJointUseLimit);

}
{
auto fPhysicsScene_getDistanceJointDamping = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getDistanceJointDamping>;
mono_add_internal_call("Lumix.DistanceJoint::getDistanceJointDamping", fPhysicsScene_getDistanceJointDamping);
auto fPhysicsScene_setDistanceJointDamping = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setDistanceJointDamping>;
mono_add_internal_call("Lumix.DistanceJoint::setDistanceJointDamping", fPhysicsScene_setDistanceJointDamping);

}
{
auto fPhysicsScene_getDistanceJointStiffness = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getDistanceJointStiffness>;
mono_add_internal_call("Lumix.DistanceJoint::getDistanceJointStiffness", fPhysicsScene_getDistanceJointStiffness);
auto fPhysicsScene_setDistanceJointStiffness = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setDistanceJointStiffness>;
mono_add_internal_call("Lumix.DistanceJoint::setDistanceJointStiffness", fPhysicsScene_setDistanceJointStiffness);

}
{
auto fPhysicsScene_getDistanceJointTolerance = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getDistanceJointTolerance>;
mono_add_internal_call("Lumix.DistanceJoint::getDistanceJointTolerance", fPhysicsScene_getDistanceJointTolerance);
auto fPhysicsScene_setDistanceJointTolerance = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setDistanceJointTolerance>;
mono_add_internal_call("Lumix.DistanceJoint::setDistanceJointTolerance", fPhysicsScene_setDistanceJointTolerance);

}
{
auto fPhysicsScene_getJointConnectedBody = &csharp_getProperty<Entity, PhysicsScene, &PhysicsScene::getJointConnectedBody>;
mono_add_internal_call("Lumix.DistanceJoint::getJointConnectedBody", fPhysicsScene_getJointConnectedBody);
auto fPhysicsScene_setJointConnectedBody = &csharp_setProperty<Entity, PhysicsScene, &PhysicsScene::setJointConnectedBody>;
mono_add_internal_call("Lumix.DistanceJoint::setJointConnectedBody", fPhysicsScene_setJointConnectedBody);

}
{
auto fPhysicsScene_getDistanceJointLimits = &csharp_getProperty<Vec2, PhysicsScene, &PhysicsScene::getDistanceJointLimits>;
mono_add_internal_call("Lumix.DistanceJoint::getDistanceJointLimits", fPhysicsScene_getDistanceJointLimits);
auto fPhysicsScene_setDistanceJointLimits = &csharp_setProperty<Vec2, PhysicsScene, &PhysicsScene::setDistanceJointLimits>;
mono_add_internal_call("Lumix.DistanceJoint::setDistanceJointLimits", fPhysicsScene_setDistanceJointLimits);

}
{
auto fPhysicsScene_getJointConnectedBody = &csharp_getProperty<Entity, PhysicsScene, &PhysicsScene::getJointConnectedBody>;
mono_add_internal_call("Lumix.HingeJoint::getJointConnectedBody", fPhysicsScene_getJointConnectedBody);
auto fPhysicsScene_setJointConnectedBody = &csharp_setProperty<Entity, PhysicsScene, &PhysicsScene::setJointConnectedBody>;
mono_add_internal_call("Lumix.HingeJoint::setJointConnectedBody", fPhysicsScene_setJointConnectedBody);

}
{
auto fPhysicsScene_getHingeJointDamping = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getHingeJointDamping>;
mono_add_internal_call("Lumix.HingeJoint::getHingeJointDamping", fPhysicsScene_getHingeJointDamping);
auto fPhysicsScene_setHingeJointDamping = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setHingeJointDamping>;
mono_add_internal_call("Lumix.HingeJoint::setHingeJointDamping", fPhysicsScene_setHingeJointDamping);

}
{
auto fPhysicsScene_getHingeJointStiffness = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getHingeJointStiffness>;
mono_add_internal_call("Lumix.HingeJoint::getHingeJointStiffness", fPhysicsScene_getHingeJointStiffness);
auto fPhysicsScene_setHingeJointStiffness = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setHingeJointStiffness>;
mono_add_internal_call("Lumix.HingeJoint::setHingeJointStiffness", fPhysicsScene_setHingeJointStiffness);

}
{
auto fPhysicsScene_getJointAxisPosition = &csharp_getProperty<Vec3, PhysicsScene, &PhysicsScene::getJointAxisPosition>;
mono_add_internal_call("Lumix.HingeJoint::getJointAxisPosition", fPhysicsScene_getJointAxisPosition);
auto fPhysicsScene_setJointAxisPosition = &csharp_setProperty<Vec3, PhysicsScene, &PhysicsScene::setJointAxisPosition>;
mono_add_internal_call("Lumix.HingeJoint::setJointAxisPosition", fPhysicsScene_setJointAxisPosition);

}
{
auto fPhysicsScene_getJointAxisDirection = &csharp_getProperty<Vec3, PhysicsScene, &PhysicsScene::getJointAxisDirection>;
mono_add_internal_call("Lumix.HingeJoint::getJointAxisDirection", fPhysicsScene_getJointAxisDirection);
auto fPhysicsScene_setJointAxisDirection = &csharp_setProperty<Vec3, PhysicsScene, &PhysicsScene::setJointAxisDirection>;
mono_add_internal_call("Lumix.HingeJoint::setJointAxisDirection", fPhysicsScene_setJointAxisDirection);

}
{
auto fPhysicsScene_getHingeJointUseLimit = &csharp_getProperty<bool, PhysicsScene, &PhysicsScene::getHingeJointUseLimit>;
mono_add_internal_call("Lumix.HingeJoint::getHingeJointUseLimit", fPhysicsScene_getHingeJointUseLimit);
auto fPhysicsScene_setHingeJointUseLimit = &csharp_setProperty<bool, PhysicsScene, &PhysicsScene::setHingeJointUseLimit>;
mono_add_internal_call("Lumix.HingeJoint::setHingeJointUseLimit", fPhysicsScene_setHingeJointUseLimit);

}
{
auto fPhysicsScene_getControllerLayer = &csharp_getProperty<int, PhysicsScene, &PhysicsScene::getControllerLayer>;
mono_add_internal_call("Lumix.PhysicalController::getControllerLayer", fPhysicsScene_getControllerLayer);
auto fPhysicsScene_setControllerLayer = &csharp_setProperty<int, PhysicsScene, &PhysicsScene::setControllerLayer>;
mono_add_internal_call("Lumix.PhysicalController::setControllerLayer", fPhysicsScene_setControllerLayer);

}
{
auto fPhysicsScene_getDynamicType = &csharp_getProperty<Lumix::PhysicsScene::DynamicType, PhysicsScene, &PhysicsScene::getDynamicType>;
mono_add_internal_call("Lumix.BoxRigidActor::getDynamicType", fPhysicsScene_getDynamicType);
auto fPhysicsScene_setDynamicType = &csharp_setProperty<Lumix::PhysicsScene::DynamicType, PhysicsScene, &PhysicsScene::setDynamicType>;
mono_add_internal_call("Lumix.BoxRigidActor::setDynamicType", fPhysicsScene_setDynamicType);

}
{
auto fPhysicsScene_isTrigger = &csharp_getProperty<bool, PhysicsScene, &PhysicsScene::isTrigger>;
mono_add_internal_call("Lumix.BoxRigidActor::isTrigger", fPhysicsScene_isTrigger);
auto fPhysicsScene_setIsTrigger = &csharp_setProperty<bool, PhysicsScene, &PhysicsScene::setIsTrigger>;
mono_add_internal_call("Lumix.BoxRigidActor::setIsTrigger", fPhysicsScene_setIsTrigger);

}
{
auto fPhysicsScene_getHalfExtents = &csharp_getProperty<Vec3, PhysicsScene, &PhysicsScene::getHalfExtents>;
mono_add_internal_call("Lumix.BoxRigidActor::getHalfExtents", fPhysicsScene_getHalfExtents);
auto fPhysicsScene_setHalfExtents = &csharp_setProperty<Vec3, PhysicsScene, &PhysicsScene::setHalfExtents>;
mono_add_internal_call("Lumix.BoxRigidActor::setHalfExtents", fPhysicsScene_setHalfExtents);

}
{
auto fPhysicsScene_getActorLayer = &csharp_getProperty<int, PhysicsScene, &PhysicsScene::getActorLayer>;
mono_add_internal_call("Lumix.BoxRigidActor::getActorLayer", fPhysicsScene_getActorLayer);
auto fPhysicsScene_setActorLayer = &csharp_setProperty<int, PhysicsScene, &PhysicsScene::setActorLayer>;
mono_add_internal_call("Lumix.BoxRigidActor::setActorLayer", fPhysicsScene_setActorLayer);

}
{
auto fPhysicsScene_getShapeSource = &csharp_getProperty<Path, PhysicsScene, &PhysicsScene::getShapeSource>;
mono_add_internal_call("Lumix.MeshRigidActor::getShapeSource", fPhysicsScene_getShapeSource);
auto fPhysicsScene_setShapeSource = &csharp_setProperty<Path, PhysicsScene, &PhysicsScene::setShapeSource>;
mono_add_internal_call("Lumix.MeshRigidActor::setShapeSource", fPhysicsScene_setShapeSource);

}
{
auto fPhysicsScene_getDynamicType = &csharp_getProperty<Lumix::PhysicsScene::DynamicType, PhysicsScene, &PhysicsScene::getDynamicType>;
mono_add_internal_call("Lumix.MeshRigidActor::getDynamicType", fPhysicsScene_getDynamicType);
auto fPhysicsScene_setDynamicType = &csharp_setProperty<Lumix::PhysicsScene::DynamicType, PhysicsScene, &PhysicsScene::setDynamicType>;
mono_add_internal_call("Lumix.MeshRigidActor::setDynamicType", fPhysicsScene_setDynamicType);

}
{
auto fPhysicsScene_getActorLayer = &csharp_getProperty<int, PhysicsScene, &PhysicsScene::getActorLayer>;
mono_add_internal_call("Lumix.MeshRigidActor::getActorLayer", fPhysicsScene_getActorLayer);
auto fPhysicsScene_setActorLayer = &csharp_setProperty<int, PhysicsScene, &PhysicsScene::setActorLayer>;
mono_add_internal_call("Lumix.MeshRigidActor::setActorLayer", fPhysicsScene_setActorLayer);

}
{
auto fPhysicsScene_getHeightmapSource = &csharp_getProperty<Path, PhysicsScene, &PhysicsScene::getHeightmapSource>;
mono_add_internal_call("Lumix.PhysicalHeightfield::getHeightmapSource", fPhysicsScene_getHeightmapSource);
auto fPhysicsScene_setHeightmapSource = &csharp_setProperty<Path, PhysicsScene, &PhysicsScene::setHeightmapSource>;
mono_add_internal_call("Lumix.PhysicalHeightfield::setHeightmapSource", fPhysicsScene_setHeightmapSource);

}
{
auto fPhysicsScene_getHeightmapXZScale = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getHeightmapXZScale>;
mono_add_internal_call("Lumix.PhysicalHeightfield::getHeightmapXZScale", fPhysicsScene_getHeightmapXZScale);
auto fPhysicsScene_setHeightmapXZScale = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setHeightmapXZScale>;
mono_add_internal_call("Lumix.PhysicalHeightfield::setHeightmapXZScale", fPhysicsScene_setHeightmapXZScale);

}
{
auto fPhysicsScene_getHeightmapYScale = &csharp_getProperty<float, PhysicsScene, &PhysicsScene::getHeightmapYScale>;
mono_add_internal_call("Lumix.PhysicalHeightfield::getHeightmapYScale", fPhysicsScene_getHeightmapYScale);
auto fPhysicsScene_setHeightmapYScale = &csharp_setProperty<float, PhysicsScene, &PhysicsScene::setHeightmapYScale>;
mono_add_internal_call("Lumix.PhysicalHeightfield::setHeightmapYScale", fPhysicsScene_setHeightmapYScale);

}
{
auto fPhysicsScene_getHeightfieldLayer = &csharp_getProperty<int, PhysicsScene, &PhysicsScene::getHeightfieldLayer>;
mono_add_internal_call("Lumix.PhysicalHeightfield::getHeightfieldLayer", fPhysicsScene_getHeightfieldLayer);
auto fPhysicsScene_setHeightfieldLayer = &csharp_setProperty<int, PhysicsScene, &PhysicsScene::setHeightfieldLayer>;
mono_add_internal_call("Lumix.PhysicalHeightfield::setHeightfieldLayer", fPhysicsScene_setHeightfieldLayer);

}
{
auto fRenderScene_getBoneAttachmentParent = &csharp_getProperty<Entity, RenderScene, &RenderScene::getBoneAttachmentParent>;
mono_add_internal_call("Lumix.BoneAttachment::getBoneAttachmentParent", fRenderScene_getBoneAttachmentParent);
auto fRenderScene_setBoneAttachmentParent = &csharp_setProperty<Entity, RenderScene, &RenderScene::setBoneAttachmentParent>;
mono_add_internal_call("Lumix.BoneAttachment::setBoneAttachmentParent", fRenderScene_setBoneAttachmentParent);

}
{
auto fRenderScene_getBoneAttachmentPosition = &csharp_getProperty<Vec3, RenderScene, &RenderScene::getBoneAttachmentPosition>;
mono_add_internal_call("Lumix.BoneAttachment::getBoneAttachmentPosition", fRenderScene_getBoneAttachmentPosition);
auto fRenderScene_setBoneAttachmentPosition = &csharp_setProperty<Vec3, RenderScene, &RenderScene::setBoneAttachmentPosition>;
mono_add_internal_call("Lumix.BoneAttachment::setBoneAttachmentPosition", fRenderScene_setBoneAttachmentPosition);

}
{
auto fRenderScene_getParticleEmitterShapeRadius = &csharp_getProperty<float, RenderScene, &RenderScene::getParticleEmitterShapeRadius>;
mono_add_internal_call("Lumix.ParticleEmitterSpawnShape::getParticleEmitterShapeRadius", fRenderScene_getParticleEmitterShapeRadius);
auto fRenderScene_setParticleEmitterShapeRadius = &csharp_setProperty<float, RenderScene, &RenderScene::setParticleEmitterShapeRadius>;
mono_add_internal_call("Lumix.ParticleEmitterSpawnShape::setParticleEmitterShapeRadius", fRenderScene_setParticleEmitterShapeRadius);

}
{
auto fRenderScene_getParticleEmitterPlaneBounce = &csharp_getProperty<float, RenderScene, &RenderScene::getParticleEmitterPlaneBounce>;
mono_add_internal_call("Lumix.ParticleEmitterPlane::getParticleEmitterPlaneBounce", fRenderScene_getParticleEmitterPlaneBounce);
auto fRenderScene_setParticleEmitterPlaneBounce = &csharp_setProperty<float, RenderScene, &RenderScene::setParticleEmitterPlaneBounce>;
mono_add_internal_call("Lumix.ParticleEmitterPlane::setParticleEmitterPlaneBounce", fRenderScene_setParticleEmitterPlaneBounce);

}
{
auto fRenderScene_getParticleEmitterAttractorForce = &csharp_getProperty<float, RenderScene, &RenderScene::getParticleEmitterAttractorForce>;
mono_add_internal_call("Lumix.ParticleEmitterAttractor::getParticleEmitterAttractorForce", fRenderScene_getParticleEmitterAttractorForce);
auto fRenderScene_setParticleEmitterAttractorForce = &csharp_setProperty<float, RenderScene, &RenderScene::setParticleEmitterAttractorForce>;
mono_add_internal_call("Lumix.ParticleEmitterAttractor::setParticleEmitterAttractorForce", fRenderScene_setParticleEmitterAttractorForce);

}
{
auto fRenderScene_getParticleEmitterAcceleration = &csharp_getProperty<Vec3, RenderScene, &RenderScene::getParticleEmitterAcceleration>;
mono_add_internal_call("Lumix.ParticleEmitterForce::getParticleEmitterAcceleration", fRenderScene_getParticleEmitterAcceleration);
auto fRenderScene_setParticleEmitterAcceleration = &csharp_setProperty<Vec3, RenderScene, &RenderScene::setParticleEmitterAcceleration>;
mono_add_internal_call("Lumix.ParticleEmitterForce::setParticleEmitterAcceleration", fRenderScene_setParticleEmitterAcceleration);

}
{
auto fRenderScene_getParticleEmitterSubimageRows = &csharp_getProperty<int, RenderScene, &RenderScene::getParticleEmitterSubimageRows>;
mono_add_internal_call("Lumix.ParticleEmitterSubimage::getParticleEmitterSubimageRows", fRenderScene_getParticleEmitterSubimageRows);
auto fRenderScene_setParticleEmitterSubimageRows = &csharp_setProperty<int, RenderScene, &RenderScene::setParticleEmitterSubimageRows>;
mono_add_internal_call("Lumix.ParticleEmitterSubimage::setParticleEmitterSubimageRows", fRenderScene_setParticleEmitterSubimageRows);

}
{
auto fRenderScene_getParticleEmitterSubimageCols = &csharp_getProperty<int, RenderScene, &RenderScene::getParticleEmitterSubimageCols>;
mono_add_internal_call("Lumix.ParticleEmitterSubimage::getParticleEmitterSubimageCols", fRenderScene_getParticleEmitterSubimageCols);
auto fRenderScene_setParticleEmitterSubimageCols = &csharp_setProperty<int, RenderScene, &RenderScene::setParticleEmitterSubimageCols>;
mono_add_internal_call("Lumix.ParticleEmitterSubimage::setParticleEmitterSubimageCols", fRenderScene_setParticleEmitterSubimageCols);

}
{
auto fRenderScene_getParticleEmitterLinearMovementX = &csharp_getProperty<Vec2, RenderScene, &RenderScene::getParticleEmitterLinearMovementX>;
mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::getParticleEmitterLinearMovementX", fRenderScene_getParticleEmitterLinearMovementX);
auto fRenderScene_setParticleEmitterLinearMovementX = &csharp_setProperty<Vec2, RenderScene, &RenderScene::setParticleEmitterLinearMovementX>;
mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::setParticleEmitterLinearMovementX", fRenderScene_setParticleEmitterLinearMovementX);

}
{
auto fRenderScene_getParticleEmitterLinearMovementY = &csharp_getProperty<Vec2, RenderScene, &RenderScene::getParticleEmitterLinearMovementY>;
mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::getParticleEmitterLinearMovementY", fRenderScene_getParticleEmitterLinearMovementY);
auto fRenderScene_setParticleEmitterLinearMovementY = &csharp_setProperty<Vec2, RenderScene, &RenderScene::setParticleEmitterLinearMovementY>;
mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::setParticleEmitterLinearMovementY", fRenderScene_setParticleEmitterLinearMovementY);

}
{
auto fRenderScene_getParticleEmitterLinearMovementZ = &csharp_getProperty<Vec2, RenderScene, &RenderScene::getParticleEmitterLinearMovementZ>;
mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::getParticleEmitterLinearMovementZ", fRenderScene_getParticleEmitterLinearMovementZ);
auto fRenderScene_setParticleEmitterLinearMovementZ = &csharp_setProperty<Vec2, RenderScene, &RenderScene::setParticleEmitterLinearMovementZ>;
mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::setParticleEmitterLinearMovementZ", fRenderScene_setParticleEmitterLinearMovementZ);

}
{
auto fRenderScene_getParticleEmitterInitialLife = &csharp_getProperty<Vec2, RenderScene, &RenderScene::getParticleEmitterInitialLife>;
mono_add_internal_call("Lumix.ParticleEmitter::getParticleEmitterInitialLife", fRenderScene_getParticleEmitterInitialLife);
auto fRenderScene_setParticleEmitterInitialLife = &csharp_setProperty<Vec2, RenderScene, &RenderScene::setParticleEmitterInitialLife>;
mono_add_internal_call("Lumix.ParticleEmitter::setParticleEmitterInitialLife", fRenderScene_setParticleEmitterInitialLife);

}
{
auto fRenderScene_getParticleEmitterInitialSize = &csharp_getProperty<Vec2, RenderScene, &RenderScene::getParticleEmitterInitialSize>;
mono_add_internal_call("Lumix.ParticleEmitter::getParticleEmitterInitialSize", fRenderScene_getParticleEmitterInitialSize);
auto fRenderScene_setParticleEmitterInitialSize = &csharp_setProperty<Vec2, RenderScene, &RenderScene::setParticleEmitterInitialSize>;
mono_add_internal_call("Lumix.ParticleEmitter::setParticleEmitterInitialSize", fRenderScene_setParticleEmitterInitialSize);

}
{
auto fRenderScene_getParticleEmitterSpawnPeriod = &csharp_getProperty<Vec2, RenderScene, &RenderScene::getParticleEmitterSpawnPeriod>;
mono_add_internal_call("Lumix.ParticleEmitter::getParticleEmitterSpawnPeriod", fRenderScene_getParticleEmitterSpawnPeriod);
auto fRenderScene_setParticleEmitterSpawnPeriod = &csharp_setProperty<Vec2, RenderScene, &RenderScene::setParticleEmitterSpawnPeriod>;
mono_add_internal_call("Lumix.ParticleEmitter::setParticleEmitterSpawnPeriod", fRenderScene_setParticleEmitterSpawnPeriod);

}
{
auto fRenderScene_getParticleEmitterSpawnCount = &csharp_getProperty<Int2, RenderScene, &RenderScene::getParticleEmitterSpawnCount>;
mono_add_internal_call("Lumix.ParticleEmitter::getParticleEmitterSpawnCount", fRenderScene_getParticleEmitterSpawnCount);
auto fRenderScene_setParticleEmitterSpawnCount = &csharp_setProperty<Int2, RenderScene, &RenderScene::setParticleEmitterSpawnCount>;
mono_add_internal_call("Lumix.ParticleEmitter::setParticleEmitterSpawnCount", fRenderScene_setParticleEmitterSpawnCount);

}
{
auto fRenderScene_getParticleEmitterAutoemit = &csharp_getProperty<bool, RenderScene, &RenderScene::getParticleEmitterAutoemit>;
mono_add_internal_call("Lumix.ParticleEmitter::getParticleEmitterAutoemit", fRenderScene_getParticleEmitterAutoemit);
auto fRenderScene_setParticleEmitterAutoemit = &csharp_setProperty<bool, RenderScene, &RenderScene::setParticleEmitterAutoemit>;
mono_add_internal_call("Lumix.ParticleEmitter::setParticleEmitterAutoemit", fRenderScene_setParticleEmitterAutoemit);

}
{
auto fRenderScene_getParticleEmitterLocalSpace = &csharp_getProperty<bool, RenderScene, &RenderScene::getParticleEmitterLocalSpace>;
mono_add_internal_call("Lumix.ParticleEmitter::getParticleEmitterLocalSpace", fRenderScene_getParticleEmitterLocalSpace);
auto fRenderScene_setParticleEmitterLocalSpace = &csharp_setProperty<bool, RenderScene, &RenderScene::setParticleEmitterLocalSpace>;
mono_add_internal_call("Lumix.ParticleEmitter::setParticleEmitterLocalSpace", fRenderScene_setParticleEmitterLocalSpace);

}
{
auto fRenderScene_getParticleEmitterMaterialPath = &csharp_getProperty<Path, RenderScene, &RenderScene::getParticleEmitterMaterialPath>;
mono_add_internal_call("Lumix.ParticleEmitter::getParticleEmitterMaterialPath", fRenderScene_getParticleEmitterMaterialPath);
auto fRenderScene_setParticleEmitterMaterialPath = &csharp_setProperty<Path, RenderScene, &RenderScene::setParticleEmitterMaterialPath>;
mono_add_internal_call("Lumix.ParticleEmitter::setParticleEmitterMaterialPath", fRenderScene_setParticleEmitterMaterialPath);

}
{
auto fRenderScene_getCameraSlot = &csharp_getProperty<const char*, RenderScene, &RenderScene::getCameraSlot>;
mono_add_internal_call("Lumix.Camera::getCameraSlot", fRenderScene_getCameraSlot);
auto fRenderScene_setCameraSlot = &csharp_setProperty<const char*, RenderScene, &RenderScene::setCameraSlot>;
mono_add_internal_call("Lumix.Camera::setCameraSlot", fRenderScene_setCameraSlot);

}
{
auto fRenderScene_getCameraOrthoSize = &csharp_getProperty<float, RenderScene, &RenderScene::getCameraOrthoSize>;
mono_add_internal_call("Lumix.Camera::getCameraOrthoSize", fRenderScene_getCameraOrthoSize);
auto fRenderScene_setCameraOrthoSize = &csharp_setProperty<float, RenderScene, &RenderScene::setCameraOrthoSize>;
mono_add_internal_call("Lumix.Camera::setCameraOrthoSize", fRenderScene_setCameraOrthoSize);

}
{
auto fRenderScene_isCameraOrtho = &csharp_getProperty<bool, RenderScene, &RenderScene::isCameraOrtho>;
mono_add_internal_call("Lumix.Camera::isCameraOrtho", fRenderScene_isCameraOrtho);
auto fRenderScene_setCameraOrtho = &csharp_setProperty<bool, RenderScene, &RenderScene::setCameraOrtho>;
mono_add_internal_call("Lumix.Camera::setCameraOrtho", fRenderScene_setCameraOrtho);

}
{
auto fRenderScene_getCameraNearPlane = &csharp_getProperty<float, RenderScene, &RenderScene::getCameraNearPlane>;
mono_add_internal_call("Lumix.Camera::getCameraNearPlane", fRenderScene_getCameraNearPlane);
auto fRenderScene_setCameraNearPlane = &csharp_setProperty<float, RenderScene, &RenderScene::setCameraNearPlane>;
mono_add_internal_call("Lumix.Camera::setCameraNearPlane", fRenderScene_setCameraNearPlane);

}
{
auto fRenderScene_getCameraFarPlane = &csharp_getProperty<float, RenderScene, &RenderScene::getCameraFarPlane>;
mono_add_internal_call("Lumix.Camera::getCameraFarPlane", fRenderScene_getCameraFarPlane);
auto fRenderScene_setCameraFarPlane = &csharp_setProperty<float, RenderScene, &RenderScene::setCameraFarPlane>;
mono_add_internal_call("Lumix.Camera::setCameraFarPlane", fRenderScene_setCameraFarPlane);

}
{
auto fRenderScene_getModelInstancePath = &csharp_getProperty<Path, RenderScene, &RenderScene::getModelInstancePath>;
mono_add_internal_call("Lumix.ModelInstance::getModelInstancePath", fRenderScene_getModelInstancePath);
auto fRenderScene_setModelInstancePath = &csharp_setProperty<Path, RenderScene, &RenderScene::setModelInstancePath>;
mono_add_internal_call("Lumix.ModelInstance::setModelInstancePath", fRenderScene_setModelInstancePath);

}
{
auto fRenderScene_getModelInstanceKeepSkin = &csharp_getProperty<bool, RenderScene, &RenderScene::getModelInstanceKeepSkin>;
mono_add_internal_call("Lumix.ModelInstance::getModelInstanceKeepSkin", fRenderScene_getModelInstanceKeepSkin);
auto fRenderScene_setModelInstanceKeepSkin = &csharp_setProperty<bool, RenderScene, &RenderScene::setModelInstanceKeepSkin>;
mono_add_internal_call("Lumix.ModelInstance::setModelInstanceKeepSkin", fRenderScene_setModelInstanceKeepSkin);

}
{
auto fRenderScene_getGlobalLightColor = &csharp_getProperty<Vec3, RenderScene, &RenderScene::getGlobalLightColor>;
mono_add_internal_call("Lumix.GlobalLight::getGlobalLightColor", fRenderScene_getGlobalLightColor);
auto fRenderScene_setGlobalLightColor = &csharp_setProperty<Vec3, RenderScene, &RenderScene::setGlobalLightColor>;
mono_add_internal_call("Lumix.GlobalLight::setGlobalLightColor", fRenderScene_setGlobalLightColor);

}
{
auto fRenderScene_getGlobalLightIntensity = &csharp_getProperty<float, RenderScene, &RenderScene::getGlobalLightIntensity>;
mono_add_internal_call("Lumix.GlobalLight::getGlobalLightIntensity", fRenderScene_getGlobalLightIntensity);
auto fRenderScene_setGlobalLightIntensity = &csharp_setProperty<float, RenderScene, &RenderScene::setGlobalLightIntensity>;
mono_add_internal_call("Lumix.GlobalLight::setGlobalLightIntensity", fRenderScene_setGlobalLightIntensity);

}
{
auto fRenderScene_getGlobalLightIndirectIntensity = &csharp_getProperty<float, RenderScene, &RenderScene::getGlobalLightIndirectIntensity>;
mono_add_internal_call("Lumix.GlobalLight::getGlobalLightIndirectIntensity", fRenderScene_getGlobalLightIndirectIntensity);
auto fRenderScene_setGlobalLightIndirectIntensity = &csharp_setProperty<float, RenderScene, &RenderScene::setGlobalLightIndirectIntensity>;
mono_add_internal_call("Lumix.GlobalLight::setGlobalLightIndirectIntensity", fRenderScene_setGlobalLightIndirectIntensity);

}
{
auto fRenderScene_getShadowmapCascades = &csharp_getProperty<Vec4, RenderScene, &RenderScene::getShadowmapCascades>;
mono_add_internal_call("Lumix.GlobalLight::getShadowmapCascades", fRenderScene_getShadowmapCascades);
auto fRenderScene_setShadowmapCascades = &csharp_setProperty<Vec4, RenderScene, &RenderScene::setShadowmapCascades>;
mono_add_internal_call("Lumix.GlobalLight::setShadowmapCascades", fRenderScene_setShadowmapCascades);

}
{
auto fRenderScene_getFogDensity = &csharp_getProperty<float, RenderScene, &RenderScene::getFogDensity>;
mono_add_internal_call("Lumix.GlobalLight::getFogDensity", fRenderScene_getFogDensity);
auto fRenderScene_setFogDensity = &csharp_setProperty<float, RenderScene, &RenderScene::setFogDensity>;
mono_add_internal_call("Lumix.GlobalLight::setFogDensity", fRenderScene_setFogDensity);

}
{
auto fRenderScene_getFogBottom = &csharp_getProperty<float, RenderScene, &RenderScene::getFogBottom>;
mono_add_internal_call("Lumix.GlobalLight::getFogBottom", fRenderScene_getFogBottom);
auto fRenderScene_setFogBottom = &csharp_setProperty<float, RenderScene, &RenderScene::setFogBottom>;
mono_add_internal_call("Lumix.GlobalLight::setFogBottom", fRenderScene_setFogBottom);

}
{
auto fRenderScene_getFogHeight = &csharp_getProperty<float, RenderScene, &RenderScene::getFogHeight>;
mono_add_internal_call("Lumix.GlobalLight::getFogHeight", fRenderScene_getFogHeight);
auto fRenderScene_setFogHeight = &csharp_setProperty<float, RenderScene, &RenderScene::setFogHeight>;
mono_add_internal_call("Lumix.GlobalLight::setFogHeight", fRenderScene_setFogHeight);

}
{
auto fRenderScene_getFogColor = &csharp_getProperty<Vec3, RenderScene, &RenderScene::getFogColor>;
mono_add_internal_call("Lumix.GlobalLight::getFogColor", fRenderScene_getFogColor);
auto fRenderScene_setFogColor = &csharp_setProperty<Vec3, RenderScene, &RenderScene::setFogColor>;
mono_add_internal_call("Lumix.GlobalLight::setFogColor", fRenderScene_setFogColor);

}
{
auto fRenderScene_getPointLightColor = &csharp_getProperty<Vec3, RenderScene, &RenderScene::getPointLightColor>;
mono_add_internal_call("Lumix.PointLight::getPointLightColor", fRenderScene_getPointLightColor);
auto fRenderScene_setPointLightColor = &csharp_setProperty<Vec3, RenderScene, &RenderScene::setPointLightColor>;
mono_add_internal_call("Lumix.PointLight::setPointLightColor", fRenderScene_setPointLightColor);

}
{
auto fRenderScene_getPointLightSpecularColor = &csharp_getProperty<Vec3, RenderScene, &RenderScene::getPointLightSpecularColor>;
mono_add_internal_call("Lumix.PointLight::getPointLightSpecularColor", fRenderScene_getPointLightSpecularColor);
auto fRenderScene_setPointLightSpecularColor = &csharp_setProperty<Vec3, RenderScene, &RenderScene::setPointLightSpecularColor>;
mono_add_internal_call("Lumix.PointLight::setPointLightSpecularColor", fRenderScene_setPointLightSpecularColor);

}
{
auto fRenderScene_getPointLightIntensity = &csharp_getProperty<float, RenderScene, &RenderScene::getPointLightIntensity>;
mono_add_internal_call("Lumix.PointLight::getPointLightIntensity", fRenderScene_getPointLightIntensity);
auto fRenderScene_setPointLightIntensity = &csharp_setProperty<float, RenderScene, &RenderScene::setPointLightIntensity>;
mono_add_internal_call("Lumix.PointLight::setPointLightIntensity", fRenderScene_setPointLightIntensity);

}
{
auto fRenderScene_getPointLightSpecularIntensity = &csharp_getProperty<float, RenderScene, &RenderScene::getPointLightSpecularIntensity>;
mono_add_internal_call("Lumix.PointLight::getPointLightSpecularIntensity", fRenderScene_getPointLightSpecularIntensity);
auto fRenderScene_setPointLightSpecularIntensity = &csharp_setProperty<float, RenderScene, &RenderScene::setPointLightSpecularIntensity>;
mono_add_internal_call("Lumix.PointLight::setPointLightSpecularIntensity", fRenderScene_setPointLightSpecularIntensity);

}
{
auto fRenderScene_getLightAttenuation = &csharp_getProperty<float, RenderScene, &RenderScene::getLightAttenuation>;
mono_add_internal_call("Lumix.PointLight::getLightAttenuation", fRenderScene_getLightAttenuation);
auto fRenderScene_setLightAttenuation = &csharp_setProperty<float, RenderScene, &RenderScene::setLightAttenuation>;
mono_add_internal_call("Lumix.PointLight::setLightAttenuation", fRenderScene_setLightAttenuation);

}
{
auto fRenderScene_getLightRange = &csharp_getProperty<float, RenderScene, &RenderScene::getLightRange>;
mono_add_internal_call("Lumix.PointLight::getLightRange", fRenderScene_getLightRange);
auto fRenderScene_setLightRange = &csharp_setProperty<float, RenderScene, &RenderScene::setLightRange>;
mono_add_internal_call("Lumix.PointLight::setLightRange", fRenderScene_setLightRange);

}
{
auto fRenderScene_getLightCastShadows = &csharp_getProperty<bool, RenderScene, &RenderScene::getLightCastShadows>;
mono_add_internal_call("Lumix.PointLight::getLightCastShadows", fRenderScene_getLightCastShadows);
auto fRenderScene_setLightCastShadows = &csharp_setProperty<bool, RenderScene, &RenderScene::setLightCastShadows>;
mono_add_internal_call("Lumix.PointLight::setLightCastShadows", fRenderScene_setLightCastShadows);

}
{
auto fRenderScene_getDecalMaterialPath = &csharp_getProperty<Path, RenderScene, &RenderScene::getDecalMaterialPath>;
mono_add_internal_call("Lumix.Decal::getDecalMaterialPath", fRenderScene_getDecalMaterialPath);
auto fRenderScene_setDecalMaterialPath = &csharp_setProperty<Path, RenderScene, &RenderScene::setDecalMaterialPath>;
mono_add_internal_call("Lumix.Decal::setDecalMaterialPath", fRenderScene_setDecalMaterialPath);

}
{
auto fRenderScene_getDecalScale = &csharp_getProperty<Vec3, RenderScene, &RenderScene::getDecalScale>;
mono_add_internal_call("Lumix.Decal::getDecalScale", fRenderScene_getDecalScale);
auto fRenderScene_setDecalScale = &csharp_setProperty<Vec3, RenderScene, &RenderScene::setDecalScale>;
mono_add_internal_call("Lumix.Decal::setDecalScale", fRenderScene_setDecalScale);

}
{
auto fRenderScene_getTerrainMaterialPath = &csharp_getProperty<Path, RenderScene, &RenderScene::getTerrainMaterialPath>;
mono_add_internal_call("Lumix.Terrain::getTerrainMaterialPath", fRenderScene_getTerrainMaterialPath);
auto fRenderScene_setTerrainMaterialPath = &csharp_setProperty<Path, RenderScene, &RenderScene::setTerrainMaterialPath>;
mono_add_internal_call("Lumix.Terrain::setTerrainMaterialPath", fRenderScene_setTerrainMaterialPath);

}
{
auto fRenderScene_getTerrainXZScale = &csharp_getProperty<float, RenderScene, &RenderScene::getTerrainXZScale>;
mono_add_internal_call("Lumix.Terrain::getTerrainXZScale", fRenderScene_getTerrainXZScale);
auto fRenderScene_setTerrainXZScale = &csharp_setProperty<float, RenderScene, &RenderScene::setTerrainXZScale>;
mono_add_internal_call("Lumix.Terrain::setTerrainXZScale", fRenderScene_setTerrainXZScale);

}
{
auto fRenderScene_getTerrainYScale = &csharp_getProperty<float, RenderScene, &RenderScene::getTerrainYScale>;
mono_add_internal_call("Lumix.Terrain::getTerrainYScale", fRenderScene_getTerrainYScale);
auto fRenderScene_setTerrainYScale = &csharp_setProperty<float, RenderScene, &RenderScene::setTerrainYScale>;
mono_add_internal_call("Lumix.Terrain::setTerrainYScale", fRenderScene_setTerrainYScale);

}
	{
		typedef int (AnimationScene::*MethodType)(Lumix::ComponentHandle, const char *) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerInputIndex>;
		mono_add_internal_call("Lumix.AnimController::getControllerInputIndex", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, int, int);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setControllerInput>;
		mono_add_internal_call("Lumix.AnimController::setControllerInput(intptr,int,int,int)", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setControllerInput>;
		mono_add_internal_call("Lumix.AnimController::setControllerInput(intptr,int,int,single)", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, int, bool);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setControllerInput>;
		mono_add_internal_call("Lumix.AnimController::setControllerInput(intptr,int,int,bool)", f);
	}
	{
		typedef void (NavigationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&NavigationScene::cancelNavigation>;
		mono_add_internal_call("Lumix.NavmeshAgent::cancelNavigation", f);
	}
	{
		typedef bool (NavigationScene::*MethodType)(Lumix::ComponentHandle, const Lumix::Vec3 &, float, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&NavigationScene::navigate>;
		mono_add_internal_call("Lumix.NavmeshAgent::navigate", f);
	}
	{
		typedef float (NavigationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&NavigationScene::getAgentSpeed>;
		mono_add_internal_call("Lumix.NavmeshAgent::getAgentSpeed", f);
	}
	{
		typedef bool (NavigationScene::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&NavigationScene::isNavmeshReady>;
		mono_add_internal_call("Lumix.NavigationScene::isNavmeshReady", f);
	}
	{
		typedef Lumix::Entity (Universe::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getFirstEntity>;
		mono_add_internal_call("Lumix.Entity::getFirstEntity", f);
	}
	{
		typedef Lumix::Entity (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getNextEntity>;
		mono_add_internal_call("Lumix.Entity::getNextEntity", f);
	}
	{
		typedef const char* (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getEntityName>;
		mono_add_internal_call("Lumix.Entity::getEntityName", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const char *);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setEntityName>;
		mono_add_internal_call("Lumix.Entity::setEntityName", f);
	}
	{
		typedef bool (Universe::*MethodType)(Lumix::Entity, Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::isDescendant>;
		mono_add_internal_call("Lumix.Entity::isDescendant", f);
	}
	{
		typedef Lumix::Entity (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getParent>;
		mono_add_internal_call("Lumix.Entity::getParent", f);
	}
	{
		typedef Lumix::Entity (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getFirstChild>;
		mono_add_internal_call("Lumix.Entity::getFirstChild", f);
	}
	{
		typedef Lumix::Entity (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getNextSibling>;
		mono_add_internal_call("Lumix.Entity::getNextSibling", f);
	}
	{
		typedef Lumix::Transform (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getLocalTransform>;
		mono_add_internal_call("Lumix.Entity::getLocalTransform", f);
	}
	{
		typedef float (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getLocalScale>;
		mono_add_internal_call("Lumix.Entity::getLocalScale", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, Lumix::Entity);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setParent>;
		mono_add_internal_call("Lumix.Entity::setParent", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Vec3 &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setLocalPosition>;
		mono_add_internal_call("Lumix.Entity::setLocalPosition", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Quat &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setLocalRotation>;
		mono_add_internal_call("Lumix.Entity::setLocalRotation", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Transform &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setLocalTransform>;
		mono_add_internal_call("Lumix.Entity::setLocalTransform", f);
	}
	{
		typedef Lumix::Transform (Universe::*MethodType)(Lumix::Entity, const Lumix::Transform &) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::computeLocalTransform>;
		mono_add_internal_call("Lumix.Entity::computeLocalTransform", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Matrix &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setMatrix>;
		mono_add_internal_call("Lumix.Entity::setMatrix", f);
	}
	{
		typedef Lumix::Matrix (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getPositionAndRotation>;
		mono_add_internal_call("Lumix.Entity::getPositionAndRotation", f);
	}
	{
		typedef Lumix::Matrix (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getMatrix>;
		mono_add_internal_call("Lumix.Entity::getMatrix", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Transform &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setTransform>;
		mono_add_internal_call("Lumix.Entity::setTransform(intptr,Entity,intptr)", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Vec3 &, const Lumix::Quat &, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setTransform>;
		mono_add_internal_call("Lumix.Entity::setTransform(intptr,Entity,Vec3,Quat,single)", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Transform &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setTransformKeepChildren>;
		mono_add_internal_call("Lumix.Entity::setTransformKeepChildren", f);
	}
	{
		typedef Lumix::Transform (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getTransform>;
		mono_add_internal_call("Lumix.Entity::getTransform", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, float, float, float, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setRotation>;
		mono_add_internal_call("Lumix.Entity::setRotation(intptr,Entity,single,single,single,single)", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Quat &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setRotation>;
		mono_add_internal_call("Lumix.Entity::setRotation(intptr,Entity,Quat)", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, float, float, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setPosition>;
		mono_add_internal_call("Lumix.Entity::setPosition(intptr,Entity,single,single,single)", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, const Lumix::Vec3 &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setPosition>;
		mono_add_internal_call("Lumix.Entity::setPosition(intptr,Entity,Vec3)", f);
	}
	{
		typedef void (Universe::*MethodType)(Lumix::Entity, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setScale>;
		mono_add_internal_call("Lumix.Entity::setScale", f);
	}
	{
		typedef float (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getScale>;
		mono_add_internal_call("Lumix.Entity::getScale", f);
	}
	{
		typedef const Lumix::Vec3& (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getPosition>;
		mono_add_internal_call("Lumix.Entity::getPosition", f);
	}
	{
		typedef const Lumix::Quat& (Universe::*MethodType)(Lumix::Entity) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getRotation>;
		mono_add_internal_call("Lumix.Entity::getRotation", f);
	}
	{
		typedef const char* (Universe::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getName>;
		mono_add_internal_call("Lumix.Entity::getName", f);
	}
	{
		typedef void (Universe::*MethodType)(const char *);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::setName>;
		mono_add_internal_call("Lumix.Entity::setName", f);
	}
	{
		typedef Lumix::IScene* (Universe::*MethodType)(Lumix::ComponentType) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getScene>;
		mono_add_internal_call("Lumix.Entity::getScene(intptr,int)", f);
	}
	{
		typedef Lumix::IScene* (Universe::*MethodType)(unsigned int) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getScene>;
		mono_add_internal_call("Lumix.Entity::getScene(intptr,uint)", f);
	}
	{
		typedef Lumix::Entity (Universe::*MethodType)(const char *);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::getEntityByName>;
		mono_add_internal_call("Lumix.Universe::getEntityByName", f);
	}
	{
		typedef Lumix::Entity (Universe::*MethodType)(const Lumix::PrefabResource &, const Lumix::Vec3 &, const Lumix::Quat &, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Universe::instantiatePrefab>;
		mono_add_internal_call("Lumix.Universe::instantiatePrefab", f);
	}
	{
		typedef void (AudioScene::*MethodType)(int, float, float, float, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AudioScene::setEcho>;
		mono_add_internal_call("Lumix.AudioScene::setEcho", f);
	}
	{
		typedef int (AudioScene::*MethodType)(Lumix::Entity, Lumix::AudioScene::ClipInfo *, bool);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AudioScene::play>;
		mono_add_internal_call("Lumix.AudioScene::play", f);
	}
	{
		typedef void (AudioScene::*MethodType)(int);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AudioScene::stop>;
		mono_add_internal_call("Lumix.AudioScene::stop", f);
	}
	{
		typedef void (AudioScene::*MethodType)(int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AudioScene::setVolume>;
		mono_add_internal_call("Lumix.AudioScene::setVolume", f);
	}
	{
		typedef bool (InputSystem::*MethodType)(Lumix::InputSystem::MouseButton);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&InputSystem::isMouseDown>;
		mono_add_internal_call("Lumix.Input::isMouseDown", f);
	}
	{
		typedef Lumix::Entity (PhysicsScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, Lumix::Entity);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::raycast>;
		mono_add_internal_call("Lumix.PhysicsScene::raycast", f);
	}
	{
		typedef bool (PhysicsScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, float, Lumix::RaycastHit &, Lumix::Entity);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::raycastEx>;
		mono_add_internal_call("Lumix.PhysicsScene::raycastEx", f);
	}
	{
		typedef const char* (PhysicsScene::*MethodType)(int);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::getCollisionLayerName>;
		mono_add_internal_call("Lumix.PhysicsScene::getCollisionLayerName", f);
	}
	{
		typedef void (PhysicsScene::*MethodType)(int, const char *);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::setCollisionLayerName>;
		mono_add_internal_call("Lumix.PhysicsScene::setCollisionLayerName", f);
	}
	{
		typedef bool (PhysicsScene::*MethodType)(int, int);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::canLayersCollide>;
		mono_add_internal_call("Lumix.PhysicsScene::canLayersCollide", f);
	}
	{
		typedef void (PhysicsScene::*MethodType)(int, int, bool);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::setLayersCanCollide>;
		mono_add_internal_call("Lumix.PhysicsScene::setLayersCanCollide", f);
	}
	{
		typedef int (PhysicsScene::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::getCollisionsLayersCount>;
		mono_add_internal_call("Lumix.PhysicsScene::getCollisionsLayersCount", f);
	}
	{
		typedef void (PhysicsScene::*MethodType)();
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::addCollisionLayer>;
		mono_add_internal_call("Lumix.PhysicsScene::addCollisionLayer", f);
	}
	{
		typedef void (PhysicsScene::*MethodType)();
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PhysicsScene::removeCollisionLayer>;
		mono_add_internal_call("Lumix.PhysicsScene::removeCollisionLayer", f);
	}
	{
		typedef void (Renderer::*MethodType)(const Lumix::Path &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Renderer::makeScreenshot>;
		mono_add_internal_call("Lumix.Renderer::makeScreenshot", f);
	}
	{
		typedef bool (Renderer::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Renderer::isOpenGL>;
		mono_add_internal_call("Lumix.Renderer::isOpenGL", f);
	}
	{
		typedef int (Renderer::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Renderer::getLayersCount>;
		mono_add_internal_call("Lumix.Renderer::getLayersCount", f);
	}
	{
		typedef int (Renderer::*MethodType)(const char *);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Renderer::getLayer>;
		mono_add_internal_call("Lumix.Renderer::getLayer", f);
	}
	{
		typedef const char* (Renderer::*MethodType)(int) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Renderer::getLayerName>;
		mono_add_internal_call("Lumix.Renderer::getLayerName", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, const Lumix::Vec3 &, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugTriangle>;
		mono_add_internal_call("Lumix.RenderScene::addDebugTriangle", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugPoint>;
		mono_add_internal_call("Lumix.RenderScene::addDebugPoint", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, const Lumix::Vec3 &, const Lumix::Vec3 &, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCone>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCone", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugLine>;
		mono_add_internal_call("Lumix.RenderScene::addDebugLine", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, float, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCross>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCross", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, const Lumix::Vec3 &, const Lumix::Vec3 &, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCube>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCube(intptr,Vec3,Vec3,Vec3,Vec3,uint,single)", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCube>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCube(intptr,Vec3,Vec3,uint,single)", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCubeSolid>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCubeSolid", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, float, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCircle>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCircle", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, float, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugSphere>;
		mono_add_internal_call("Lumix.RenderScene::addDebugSphere", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, const Lumix::Vec3 &, float, float, float, float, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugFrustum>;
		mono_add_internal_call("Lumix.RenderScene::addDebugFrustum(intptr,Vec3,Vec3,Vec3,single,single,single,single,uint,single)", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Frustum &, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugFrustum>;
		mono_add_internal_call("Lumix.RenderScene::addDebugFrustum(intptr,intptr,uint,single)", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, float, float, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCapsule>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCapsule(intptr,Vec3,single,single,uint,single)", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Matrix &, float, float, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCapsule>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCapsule(intptr,intptr,single,single,uint,single)", f);
	}
	{
		typedef void (RenderScene::*MethodType)(const Lumix::Vec3 &, const Lumix::Vec3 &, float, unsigned int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&RenderScene::addDebugCylinder>;
		mono_add_internal_call("Lumix.RenderScene::addDebugCylinder", f);
	}
	{
		typedef Lumix::Entity (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerEntity>;
		mono_add_internal_call("Lumix.AnimController::getControllerEntity", f);
	}
	{
		typedef Lumix::ResourceManager& (Engine::*MethodType)();
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Engine::getResourceManager>;
		mono_add_internal_call("Lumix.Engine::getResourceManager", f);
	}
	{
		typedef Lumix::ResourceManagerBase* (ResourceManager::*MethodType)(Lumix::ResourceType);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManager::get>;
		mono_add_internal_call("Lumix.ResourceManager::get", f);
	}
	{
		typedef void (PrefabResource::*MethodType)();
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&PrefabResource::unload>;
		mono_add_internal_call("Lumix.PrefabResource::unload", f);
	}
	{
		typedef void (ResourceManagerBase::*MethodType)(bool);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManagerBase::enableUnload>;
		mono_add_internal_call("Lumix.ResourceManagerBase::enableUnload", f);
	}
	{
		typedef Lumix::Resource* (ResourceManagerBase::*MethodType)(const Lumix::Path &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManagerBase::load>;
		mono_add_internal_call("Lumix.ResourceManagerBase::load(intptr,string)", f);
	}
	{
		typedef void (ResourceManagerBase::*MethodType)(Lumix::Resource &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManagerBase::load>;
		mono_add_internal_call("Lumix.ResourceManagerBase::load(intptr,intptr)", f);
	}
	{
		typedef void (ResourceManagerBase::*MethodType)();
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManagerBase::removeUnreferenced>;
		mono_add_internal_call("Lumix.ResourceManagerBase::removeUnreferenced", f);
	}
	{
		typedef void (ResourceManagerBase::*MethodType)(const Lumix::Path &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManagerBase::unload>;
		mono_add_internal_call("Lumix.ResourceManagerBase::unload(intptr,string)", f);
	}
	{
		typedef void (ResourceManagerBase::*MethodType)(Lumix::Resource &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManagerBase::unload>;
		mono_add_internal_call("Lumix.ResourceManagerBase::unload(intptr,intptr)", f);
	}
	{
		typedef void (ResourceManagerBase::*MethodType)(const Lumix::Path &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManagerBase::reload>;
		mono_add_internal_call("Lumix.ResourceManagerBase::reload(intptr,string)", f);
	}
	{
		typedef void (ResourceManagerBase::*MethodType)(Lumix::Resource &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&ResourceManagerBase::reload>;
		mono_add_internal_call("Lumix.ResourceManagerBase::reload(intptr,intptr)", f);
	}
	{
		typedef Lumix::Resource::State (Resource::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Resource::getState>;
		mono_add_internal_call("Lumix.Resource::getState", f);
	}
	{
		typedef bool (Resource::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Resource::isEmpty>;
		mono_add_internal_call("Lumix.Resource::isEmpty", f);
	}
	{
		typedef bool (Resource::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Resource::isReady>;
		mono_add_internal_call("Lumix.Resource::isReady", f);
	}
	{
		typedef bool (Resource::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Resource::isFailure>;
		mono_add_internal_call("Lumix.Resource::isFailure", f);
	}
	{
		typedef unsigned (Resource::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Resource::getRefCount>;
		mono_add_internal_call("Lumix.Resource::getRefCount", f);
	}
	{
		typedef unsigned (Resource::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Resource::size>;
		mono_add_internal_call("Lumix.Resource::size", f);
	}
	{
		typedef const Lumix::Path& (Resource::*MethodType)() const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Resource::getPath>;
		mono_add_internal_call("Lumix.Resource::getPath", f);
	}
	{
		typedef Lumix::ResourceManagerBase& (Resource::*MethodType)();
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&Resource::getResourceManager>;
		mono_add_internal_call("Lumix.Resource::getResourceManager", f);
	}
	{
		typedef Lumix::Animation* (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getAnimableAnimation>;
		mono_add_internal_call("Lumix.AnimationScene::getAnimableAnimation", f);
	}
	{
		typedef Lumix::Path (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getAnimation>;
		mono_add_internal_call("Lumix.AnimationScene::getAnimation", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, const Lumix::Path &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setAnimation>;
		mono_add_internal_call("Lumix.AnimationScene::setAnimation", f);
	}
	{
		typedef float (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getAnimableTime>;
		mono_add_internal_call("Lumix.AnimationScene::getAnimableTime", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setAnimableTime>;
		mono_add_internal_call("Lumix.AnimationScene::setAnimableTime", f);
	}
	{
		typedef float (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getAnimableTimeScale>;
		mono_add_internal_call("Lumix.AnimationScene::getAnimableTimeScale", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setAnimableTimeScale>;
		mono_add_internal_call("Lumix.AnimationScene::setAnimableTimeScale", f);
	}
	{
		typedef float (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getAnimableStartTime>;
		mono_add_internal_call("Lumix.AnimationScene::getAnimableStartTime", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setAnimableStartTime>;
		mono_add_internal_call("Lumix.AnimationScene::setAnimableStartTime", f);
	}
	{
		typedef unsigned* (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerInput>;
		mono_add_internal_call("Lumix.AnimationScene::getControllerInput", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, int, int);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setControllerInput>;
		mono_add_internal_call("Lumix.AnimationScene::setControllerInput(intptr,int,int,int)", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, int, float);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setControllerInput>;
		mono_add_internal_call("Lumix.AnimationScene::setControllerInput(intptr,int,int,single)", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, int, bool);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setControllerInput>;
		mono_add_internal_call("Lumix.AnimationScene::setControllerInput(intptr,int,int,bool)", f);
	}
	{
		typedef Lumix::RigidTransform (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerRootMotion>;
		mono_add_internal_call("Lumix.AnimationScene::getControllerRootMotion", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, const Lumix::Path &);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setControllerSource>;
		mono_add_internal_call("Lumix.AnimationScene::setControllerSource", f);
	}
	{
		typedef Lumix::Path (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerSource>;
		mono_add_internal_call("Lumix.AnimationScene::getControllerSource", f);
	}
	{
		typedef Lumix::Anim::ComponentInstance* (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerRoot>;
		mono_add_internal_call("Lumix.AnimationScene::getControllerRoot", f);
	}
	{
		typedef int (AnimationScene::*MethodType)(Lumix::ComponentHandle, const char *) const;
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerInputIndex>;
		mono_add_internal_call("Lumix.AnimationScene::getControllerInputIndex", f);
	}
	{
		typedef Lumix::Entity (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getSharedControllerParent>;
		mono_add_internal_call("Lumix.AnimationScene::getSharedControllerParent", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, Lumix::Entity);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setSharedControllerParent>;
		mono_add_internal_call("Lumix.AnimationScene::setSharedControllerParent", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, const char *);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::applyControllerSet>;
		mono_add_internal_call("Lumix.AnimationScene::applyControllerSet", f);
	}
	{
		typedef void (AnimationScene::*MethodType)(Lumix::ComponentHandle, int);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::setControllerDefaultSet>;
		mono_add_internal_call("Lumix.AnimationScene::setControllerDefaultSet", f);
	}
	{
		typedef int (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerDefaultSet>;
		mono_add_internal_call("Lumix.AnimationScene::getControllerDefaultSet", f);
	}
	{
		typedef Lumix::Anim::ControllerResource* (AnimationScene::*MethodType)(Lumix::ComponentHandle);
		auto f = &CSharpMethodProxy<MethodType>::call<(MethodType)&AnimationScene::getControllerResource>;
		mono_add_internal_call("Lumix.AnimationScene::getControllerResource", f);
	}
