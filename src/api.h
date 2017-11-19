{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getModelInstancePath), &RenderScene::getModelInstancePath>;
	mono_add_internal_call("Lumix.Renderable::getSource", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setModelInstancePath), &RenderScene::setModelInstancePath>;
	mono_add_internal_call("Lumix.Renderable::setSource", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getModelInstanceKeepSkin), &RenderScene::getModelInstanceKeepSkin>;
	mono_add_internal_call("Lumix.Renderable::getKeepSkin", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setModelInstanceKeepSkin), &RenderScene::setModelInstanceKeepSkin>;
	mono_add_internal_call("Lumix.Renderable::setKeepSkin", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getCameraSlot), &RenderScene::getCameraSlot>;
	mono_add_internal_call("Lumix.Camera::getSlot", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setCameraSlot), &RenderScene::setCameraSlot>;
	mono_add_internal_call("Lumix.Camera::setSlot", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getCameraOrthoSize), &RenderScene::getCameraOrthoSize>;
	mono_add_internal_call("Lumix.Camera::getOrthographicSize", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setCameraOrthoSize), &RenderScene::setCameraOrthoSize>;
	mono_add_internal_call("Lumix.Camera::setOrthographicSize", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::isCameraOrtho), &RenderScene::isCameraOrtho>;
	mono_add_internal_call("Lumix.Camera::getOrthographic", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setCameraOrtho), &RenderScene::setCameraOrtho>;
	mono_add_internal_call("Lumix.Camera::setOrthographic", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getCameraFOV), &RenderScene::getCameraFOV>;
	mono_add_internal_call("Lumix.Camera::getFOV", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setCameraFOV), &RenderScene::setCameraFOV>;
	mono_add_internal_call("Lumix.Camera::setFOV", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getCameraNearPlane), &RenderScene::getCameraNearPlane>;
	mono_add_internal_call("Lumix.Camera::getNear", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setCameraNearPlane), &RenderScene::setCameraNearPlane>;
	mono_add_internal_call("Lumix.Camera::setNear", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getCameraFarPlane), &RenderScene::getCameraFarPlane>;
	mono_add_internal_call("Lumix.Camera::getFar", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setCameraFarPlane), &RenderScene::setCameraFarPlane>;
	mono_add_internal_call("Lumix.Camera::setFar", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getControllerLayer), &PhysicsScene::getControllerLayer>;
	mono_add_internal_call("Lumix.PhysicalController::getLayer", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setControllerLayer), &PhysicsScene::setControllerLayer>;
	mono_add_internal_call("Lumix.PhysicalController::setLayer", setter);
}

{
	auto f = &CSharpMethodProxy<decltype(&PhysicsScene::moveController)>::call<&PhysicsScene::moveController>;
	mono_add_internal_call("Lumix.PhysicalController::moveController", f);
}


{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getActorLayer), &PhysicsScene::getActorLayer>;
	mono_add_internal_call("Lumix.BoxRigidActor::getLayer", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setActorLayer), &PhysicsScene::setActorLayer>;
	mono_add_internal_call("Lumix.BoxRigidActor::setLayer", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getIsTrigger), &PhysicsScene::getIsTrigger>;
	mono_add_internal_call("Lumix.BoxRigidActor::getTrigger", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setIsTrigger), &PhysicsScene::setIsTrigger>;
	mono_add_internal_call("Lumix.BoxRigidActor::setTrigger", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHalfExtents), &PhysicsScene::getHalfExtents>;
	mono_add_internal_call("Lumix.BoxRigidActor::getSize", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHalfExtents), &PhysicsScene::setHalfExtents>;
	mono_add_internal_call("Lumix.BoxRigidActor::setSize", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getGlobalLightColor), &RenderScene::getGlobalLightColor>;
	mono_add_internal_call("Lumix.GlobalLight::getColor", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setGlobalLightColor), &RenderScene::setGlobalLightColor>;
	mono_add_internal_call("Lumix.GlobalLight::setColor", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getGlobalLightIntensity), &RenderScene::getGlobalLightIntensity>;
	mono_add_internal_call("Lumix.GlobalLight::getIntensity", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setGlobalLightIntensity), &RenderScene::setGlobalLightIntensity>;
	mono_add_internal_call("Lumix.GlobalLight::setIntensity", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getGlobalLightIndirectIntensity), &RenderScene::getGlobalLightIndirectIntensity>;
	mono_add_internal_call("Lumix.GlobalLight::getIndirectIntensity", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setGlobalLightIndirectIntensity), &RenderScene::setGlobalLightIndirectIntensity>;
	mono_add_internal_call("Lumix.GlobalLight::setIndirectIntensity", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getFogDensity), &RenderScene::getFogDensity>;
	mono_add_internal_call("Lumix.GlobalLight::getFogDensity", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setFogDensity), &RenderScene::setFogDensity>;
	mono_add_internal_call("Lumix.GlobalLight::setFogDensity", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getFogBottom), &RenderScene::getFogBottom>;
	mono_add_internal_call("Lumix.GlobalLight::getFogBottom", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setFogBottom), &RenderScene::setFogBottom>;
	mono_add_internal_call("Lumix.GlobalLight::setFogBottom", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getFogHeight), &RenderScene::getFogHeight>;
	mono_add_internal_call("Lumix.GlobalLight::getFogHeight", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setFogHeight), &RenderScene::setFogHeight>;
	mono_add_internal_call("Lumix.GlobalLight::setFogHeight", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getFogColor), &RenderScene::getFogColor>;
	mono_add_internal_call("Lumix.GlobalLight::getFogColor", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setFogColor), &RenderScene::setFogColor>;
	mono_add_internal_call("Lumix.GlobalLight::setFogColor", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getShadowmapCascades), &RenderScene::getShadowmapCascades>;
	mono_add_internal_call("Lumix.GlobalLight::getShadowCascades", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setShadowmapCascades), &RenderScene::setShadowmapCascades>;
	mono_add_internal_call("Lumix.GlobalLight::setShadowCascades", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getPointLightColor), &RenderScene::getPointLightColor>;
	mono_add_internal_call("Lumix.PointLight::getDiffuseColor", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setPointLightColor), &RenderScene::setPointLightColor>;
	mono_add_internal_call("Lumix.PointLight::setDiffuseColor", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getPointLightSpecularColor), &RenderScene::getPointLightSpecularColor>;
	mono_add_internal_call("Lumix.PointLight::getSpecularColor", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setPointLightSpecularColor), &RenderScene::setPointLightSpecularColor>;
	mono_add_internal_call("Lumix.PointLight::setSpecularColor", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getPointLightIntensity), &RenderScene::getPointLightIntensity>;
	mono_add_internal_call("Lumix.PointLight::getDiffuseIntensity", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setPointLightIntensity), &RenderScene::setPointLightIntensity>;
	mono_add_internal_call("Lumix.PointLight::setDiffuseIntensity", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getPointLightSpecularIntensity), &RenderScene::getPointLightSpecularIntensity>;
	mono_add_internal_call("Lumix.PointLight::getSpecularIntensity", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setPointLightSpecularIntensity), &RenderScene::setPointLightSpecularIntensity>;
	mono_add_internal_call("Lumix.PointLight::setSpecularIntensity", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getLightFOV), &RenderScene::getLightFOV>;
	mono_add_internal_call("Lumix.PointLight::getFOV", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setLightFOV), &RenderScene::setLightFOV>;
	mono_add_internal_call("Lumix.PointLight::setFOV", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getLightAttenuation), &RenderScene::getLightAttenuation>;
	mono_add_internal_call("Lumix.PointLight::getAttenuation", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setLightAttenuation), &RenderScene::setLightAttenuation>;
	mono_add_internal_call("Lumix.PointLight::setAttenuation", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getLightRange), &RenderScene::getLightRange>;
	mono_add_internal_call("Lumix.PointLight::getRange", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setLightRange), &RenderScene::setLightRange>;
	mono_add_internal_call("Lumix.PointLight::setRange", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getLightCastShadows), &RenderScene::getLightCastShadows>;
	mono_add_internal_call("Lumix.PointLight::getCastShadows", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setLightCastShadows), &RenderScene::setLightCastShadows>;
	mono_add_internal_call("Lumix.PointLight::setCastShadows", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getTerrainMaterialPath), &RenderScene::getTerrainMaterialPath>;
	mono_add_internal_call("Lumix.Terrain::getMaterial", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setTerrainMaterialPath), &RenderScene::setTerrainMaterialPath>;
	mono_add_internal_call("Lumix.Terrain::setMaterial", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getTerrainXZScale), &RenderScene::getTerrainXZScale>;
	mono_add_internal_call("Lumix.Terrain::getXZScale", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setTerrainXZScale), &RenderScene::setTerrainXZScale>;
	mono_add_internal_call("Lumix.Terrain::setXZScale", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getTerrainYScale), &RenderScene::getTerrainYScale>;
	mono_add_internal_call("Lumix.Terrain::getHeightScale", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setTerrainYScale), &RenderScene::setTerrainYScale>;
	mono_add_internal_call("Lumix.Terrain::setHeightScale", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterInitialLife), &RenderScene::getParticleEmitterInitialLife>;
	mono_add_internal_call("Lumix.ParticleEmitter::getLife", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterInitialLife), &RenderScene::setParticleEmitterInitialLife>;
	mono_add_internal_call("Lumix.ParticleEmitter::setLife", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterInitialSize), &RenderScene::getParticleEmitterInitialSize>;
	mono_add_internal_call("Lumix.ParticleEmitter::getInitialSize", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterInitialSize), &RenderScene::setParticleEmitterInitialSize>;
	mono_add_internal_call("Lumix.ParticleEmitter::setInitialSize", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterSpawnPeriod), &RenderScene::getParticleEmitterSpawnPeriod>;
	mono_add_internal_call("Lumix.ParticleEmitter::getSpawnPeriod", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterSpawnPeriod), &RenderScene::setParticleEmitterSpawnPeriod>;
	mono_add_internal_call("Lumix.ParticleEmitter::setSpawnPeriod", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterAutoemit), &RenderScene::getParticleEmitterAutoemit>;
	mono_add_internal_call("Lumix.ParticleEmitter::getAutoemit", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterAutoemit), &RenderScene::setParticleEmitterAutoemit>;
	mono_add_internal_call("Lumix.ParticleEmitter::setAutoemit", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterLocalSpace), &RenderScene::getParticleEmitterLocalSpace>;
	mono_add_internal_call("Lumix.ParticleEmitter::getLocalSpace", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterLocalSpace), &RenderScene::setParticleEmitterLocalSpace>;
	mono_add_internal_call("Lumix.ParticleEmitter::setLocalSpace", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterMaterialPath), &RenderScene::getParticleEmitterMaterialPath>;
	mono_add_internal_call("Lumix.ParticleEmitter::getMaterial", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterMaterialPath), &RenderScene::setParticleEmitterMaterialPath>;
	mono_add_internal_call("Lumix.ParticleEmitter::setMaterial", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterSpawnCount), &RenderScene::getParticleEmitterSpawnCount>;
	mono_add_internal_call("Lumix.ParticleEmitter::getSpawnCount", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterSpawnCount), &RenderScene::setParticleEmitterSpawnCount>;
	mono_add_internal_call("Lumix.ParticleEmitter::setSpawnCount", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getScriptedParticleEmitterMaterialPath), &RenderScene::getScriptedParticleEmitterMaterialPath>;
	mono_add_internal_call("Lumix.ScriptedParticleEmitter::getMaterial", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setScriptedParticleEmitterMaterialPath), &RenderScene::setScriptedParticleEmitterMaterialPath>;
	mono_add_internal_call("Lumix.ScriptedParticleEmitter::setMaterial", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getDecalMaterialPath), &RenderScene::getDecalMaterialPath>;
	mono_add_internal_call("Lumix.Decal::getMaterial", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setDecalMaterialPath), &RenderScene::setDecalMaterialPath>;
	mono_add_internal_call("Lumix.Decal::setMaterial", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getDecalScale), &RenderScene::getDecalScale>;
	mono_add_internal_call("Lumix.Decal::getScale", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setDecalScale), &RenderScene::setDecalScale>;
	mono_add_internal_call("Lumix.Decal::setScale", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterSubimageRows), &RenderScene::getParticleEmitterSubimageRows>;
	mono_add_internal_call("Lumix.ParticleEmitterSubimage::getRows", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterSubimageRows), &RenderScene::setParticleEmitterSubimageRows>;
	mono_add_internal_call("Lumix.ParticleEmitterSubimage::setRows", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterSubimageCols), &RenderScene::getParticleEmitterSubimageCols>;
	mono_add_internal_call("Lumix.ParticleEmitterSubimage::getColumns", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterSubimageCols), &RenderScene::setParticleEmitterSubimageCols>;
	mono_add_internal_call("Lumix.ParticleEmitterSubimage::setColumns", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterAcceleration), &RenderScene::getParticleEmitterAcceleration>;
	mono_add_internal_call("Lumix.ParticleEmitterForce::getAcceleration", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterAcceleration), &RenderScene::setParticleEmitterAcceleration>;
	mono_add_internal_call("Lumix.ParticleEmitterForce::setAcceleration", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterAttractorForce), &RenderScene::getParticleEmitterAttractorForce>;
	mono_add_internal_call("Lumix.ParticleEmitterAttractor::getForce", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterAttractorForce), &RenderScene::setParticleEmitterAttractorForce>;
	mono_add_internal_call("Lumix.ParticleEmitterAttractor::setForce", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterPlaneBounce), &RenderScene::getParticleEmitterPlaneBounce>;
	mono_add_internal_call("Lumix.ParticleEmitterPlane::getBounce", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterPlaneBounce), &RenderScene::setParticleEmitterPlaneBounce>;
	mono_add_internal_call("Lumix.ParticleEmitterPlane::setBounce", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterShapeRadius), &RenderScene::getParticleEmitterShapeRadius>;
	mono_add_internal_call("Lumix.ParticleEmitterSpawnShape::getRadius", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterShapeRadius), &RenderScene::setParticleEmitterShapeRadius>;
	mono_add_internal_call("Lumix.ParticleEmitterSpawnShape::setRadius", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterLinearMovementX), &RenderScene::getParticleEmitterLinearMovementX>;
	mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::getX", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterLinearMovementX), &RenderScene::setParticleEmitterLinearMovementX>;
	mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::setX", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterLinearMovementY), &RenderScene::getParticleEmitterLinearMovementY>;
	mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::getY", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterLinearMovementY), &RenderScene::setParticleEmitterLinearMovementY>;
	mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::setY", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getParticleEmitterLinearMovementZ), &RenderScene::getParticleEmitterLinearMovementZ>;
	mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::getZ", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setParticleEmitterLinearMovementZ), &RenderScene::setParticleEmitterLinearMovementZ>;
	mono_add_internal_call("Lumix.ParticleEmitterLinearMovement::setZ", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHeightfieldLayer), &PhysicsScene::getHeightfieldLayer>;
	mono_add_internal_call("Lumix.PhysicalHeightfield::getLayer", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHeightfieldLayer), &PhysicsScene::setHeightfieldLayer>;
	mono_add_internal_call("Lumix.PhysicalHeightfield::setLayer", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHeightmapSource), &PhysicsScene::getHeightmapSource>;
	mono_add_internal_call("Lumix.PhysicalHeightfield::getHeightmap", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHeightmapSource), &PhysicsScene::setHeightmapSource>;
	mono_add_internal_call("Lumix.PhysicalHeightfield::setHeightmap", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHeightmapYScale), &PhysicsScene::getHeightmapYScale>;
	mono_add_internal_call("Lumix.PhysicalHeightfield::getYScale", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHeightmapYScale), &PhysicsScene::setHeightmapYScale>;
	mono_add_internal_call("Lumix.PhysicalHeightfield::setYScale", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHeightmapXZScale), &PhysicsScene::getHeightmapXZScale>;
	mono_add_internal_call("Lumix.PhysicalHeightfield::getXZScale", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHeightmapXZScale), &PhysicsScene::setHeightmapXZScale>;
	mono_add_internal_call("Lumix.PhysicalHeightfield::setXZScale", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getBoneAttachmentPosition), &RenderScene::getBoneAttachmentPosition>;
	mono_add_internal_call("Lumix.BoneAttachment::getRelativePosition", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setBoneAttachmentPosition), &RenderScene::setBoneAttachmentPosition>;
	mono_add_internal_call("Lumix.BoneAttachment::setRelativePosition", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&RenderScene::getBoneAttachmentRotation), &RenderScene::getBoneAttachmentRotation>;
	mono_add_internal_call("Lumix.BoneAttachment::getRelativeRotation", getter);
	auto setter = &csharp_setProperty<decltype(&RenderScene::setBoneAttachmentRotation), &RenderScene::setBoneAttachmentRotation>;
	mono_add_internal_call("Lumix.BoneAttachment::setRelativeRotation", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getRagdollLayer), &PhysicsScene::getRagdollLayer>;
	mono_add_internal_call("Lumix.Ragdoll::getLayer", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setRagdollLayer), &PhysicsScene::setRagdollLayer>;
	mono_add_internal_call("Lumix.Ragdoll::setLayer", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getSphereRadius), &PhysicsScene::getSphereRadius>;
	mono_add_internal_call("Lumix.SphereRigidActor::getRadius", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setSphereRadius), &PhysicsScene::setSphereRadius>;
	mono_add_internal_call("Lumix.SphereRigidActor::setRadius", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getActorLayer), &PhysicsScene::getActorLayer>;
	mono_add_internal_call("Lumix.SphereRigidActor::getLayer", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setActorLayer), &PhysicsScene::setActorLayer>;
	mono_add_internal_call("Lumix.SphereRigidActor::setLayer", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getIsTrigger), &PhysicsScene::getIsTrigger>;
	mono_add_internal_call("Lumix.SphereRigidActor::getTrigger", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setIsTrigger), &PhysicsScene::setIsTrigger>;
	mono_add_internal_call("Lumix.SphereRigidActor::setTrigger", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getCapsuleRadius), &PhysicsScene::getCapsuleRadius>;
	mono_add_internal_call("Lumix.CapsuleRigidActor::getRadius", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setCapsuleRadius), &PhysicsScene::setCapsuleRadius>;
	mono_add_internal_call("Lumix.CapsuleRigidActor::setRadius", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getCapsuleHeight), &PhysicsScene::getCapsuleHeight>;
	mono_add_internal_call("Lumix.CapsuleRigidActor::getHeight", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setCapsuleHeight), &PhysicsScene::setCapsuleHeight>;
	mono_add_internal_call("Lumix.CapsuleRigidActor::setHeight", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getIsTrigger), &PhysicsScene::getIsTrigger>;
	mono_add_internal_call("Lumix.CapsuleRigidActor::getTrigger", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setIsTrigger), &PhysicsScene::setIsTrigger>;
	mono_add_internal_call("Lumix.CapsuleRigidActor::setTrigger", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getDistanceJointDamping), &PhysicsScene::getDistanceJointDamping>;
	mono_add_internal_call("Lumix.DistanceJoint::getDamping", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setDistanceJointDamping), &PhysicsScene::setDistanceJointDamping>;
	mono_add_internal_call("Lumix.DistanceJoint::setDamping", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getDistanceJointStiffness), &PhysicsScene::getDistanceJointStiffness>;
	mono_add_internal_call("Lumix.DistanceJoint::getStiffness", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setDistanceJointStiffness), &PhysicsScene::setDistanceJointStiffness>;
	mono_add_internal_call("Lumix.DistanceJoint::setStiffness", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getDistanceJointTolerance), &PhysicsScene::getDistanceJointTolerance>;
	mono_add_internal_call("Lumix.DistanceJoint::getTolerance", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setDistanceJointTolerance), &PhysicsScene::setDistanceJointTolerance>;
	mono_add_internal_call("Lumix.DistanceJoint::setTolerance", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getDistanceJointLimits), &PhysicsScene::getDistanceJointLimits>;
	mono_add_internal_call("Lumix.DistanceJoint::getLimits", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setDistanceJointLimits), &PhysicsScene::setDistanceJointLimits>;
	mono_add_internal_call("Lumix.DistanceJoint::setLimits", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHingeJointDamping), &PhysicsScene::getHingeJointDamping>;
	mono_add_internal_call("Lumix.HingeJoint::getDamping", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHingeJointDamping), &PhysicsScene::setHingeJointDamping>;
	mono_add_internal_call("Lumix.HingeJoint::setDamping", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHingeJointStiffness), &PhysicsScene::getHingeJointStiffness>;
	mono_add_internal_call("Lumix.HingeJoint::getStiffness", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHingeJointStiffness), &PhysicsScene::setHingeJointStiffness>;
	mono_add_internal_call("Lumix.HingeJoint::setStiffness", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getJointAxisPosition), &PhysicsScene::getJointAxisPosition>;
	mono_add_internal_call("Lumix.HingeJoint::getAxisPosition", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setJointAxisPosition), &PhysicsScene::setJointAxisPosition>;
	mono_add_internal_call("Lumix.HingeJoint::setAxisPosition", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getJointAxisDirection), &PhysicsScene::getJointAxisDirection>;
	mono_add_internal_call("Lumix.HingeJoint::getAxisDirection", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setJointAxisDirection), &PhysicsScene::setJointAxisDirection>;
	mono_add_internal_call("Lumix.HingeJoint::setAxisDirection", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHingeJointUseLimit), &PhysicsScene::getHingeJointUseLimit>;
	mono_add_internal_call("Lumix.HingeJoint::getUseLimit", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHingeJointUseLimit), &PhysicsScene::setHingeJointUseLimit>;
	mono_add_internal_call("Lumix.HingeJoint::setUseLimit", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getHingeJointLimit), &PhysicsScene::getHingeJointLimit>;
	mono_add_internal_call("Lumix.HingeJoint::getLimit", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setHingeJointLimit), &PhysicsScene::setHingeJointLimit>;
	mono_add_internal_call("Lumix.HingeJoint::setLimit", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getJointAxisPosition), &PhysicsScene::getJointAxisPosition>;
	mono_add_internal_call("Lumix.SphericalJoint::getAxisPosition", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setJointAxisPosition), &PhysicsScene::setJointAxisPosition>;
	mono_add_internal_call("Lumix.SphericalJoint::setAxisPosition", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getJointAxisDirection), &PhysicsScene::getJointAxisDirection>;
	mono_add_internal_call("Lumix.SphericalJoint::getAxisDirection", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setJointAxisDirection), &PhysicsScene::setJointAxisDirection>;
	mono_add_internal_call("Lumix.SphericalJoint::setAxisDirection", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getSphericalJointUseLimit), &PhysicsScene::getSphericalJointUseLimit>;
	mono_add_internal_call("Lumix.SphericalJoint::getUseLimit", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setSphericalJointUseLimit), &PhysicsScene::setSphericalJointUseLimit>;
	mono_add_internal_call("Lumix.SphericalJoint::setUseLimit", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getSphericalJointLimit), &PhysicsScene::getSphericalJointLimit>;
	mono_add_internal_call("Lumix.SphericalJoint::getLimit", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setSphericalJointLimit), &PhysicsScene::setSphericalJointLimit>;
	mono_add_internal_call("Lumix.SphericalJoint::setLimit", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getJointAxisPosition), &PhysicsScene::getJointAxisPosition>;
	mono_add_internal_call("Lumix.D6Joint::getAxisPosition", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setJointAxisPosition), &PhysicsScene::setJointAxisPosition>;
	mono_add_internal_call("Lumix.D6Joint::setAxisPosition", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getJointAxisDirection), &PhysicsScene::getJointAxisDirection>;
	mono_add_internal_call("Lumix.D6Joint::getAxisDirection", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setJointAxisDirection), &PhysicsScene::setJointAxisDirection>;
	mono_add_internal_call("Lumix.D6Joint::setAxisDirection", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getD6JointLinearLimit), &PhysicsScene::getD6JointLinearLimit>;
	mono_add_internal_call("Lumix.D6Joint::getLinearLimit", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setD6JointLinearLimit), &PhysicsScene::setD6JointLinearLimit>;
	mono_add_internal_call("Lumix.D6Joint::setLinearLimit", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getD6JointSwingLimit), &PhysicsScene::getD6JointSwingLimit>;
	mono_add_internal_call("Lumix.D6Joint::getSwingLimit", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setD6JointSwingLimit), &PhysicsScene::setD6JointSwingLimit>;
	mono_add_internal_call("Lumix.D6Joint::setSwingLimit", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getD6JointTwistLimit), &PhysicsScene::getD6JointTwistLimit>;
	mono_add_internal_call("Lumix.D6Joint::getTwistLimit", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setD6JointTwistLimit), &PhysicsScene::setD6JointTwistLimit>;
	mono_add_internal_call("Lumix.D6Joint::setTwistLimit", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getActorLayer), &PhysicsScene::getActorLayer>;
	mono_add_internal_call("Lumix.RigidActor::getLayer", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setActorLayer), &PhysicsScene::setActorLayer>;
	mono_add_internal_call("Lumix.RigidActor::setLayer", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getIsTrigger), &PhysicsScene::getIsTrigger>;
	mono_add_internal_call("Lumix.RigidActor::getTrigger", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setIsTrigger), &PhysicsScene::setIsTrigger>;
	mono_add_internal_call("Lumix.RigidActor::setTrigger", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getActorLayer), &PhysicsScene::getActorLayer>;
	mono_add_internal_call("Lumix.MeshRigidActor::getLayer", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setActorLayer), &PhysicsScene::setActorLayer>;
	mono_add_internal_call("Lumix.MeshRigidActor::setLayer", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&PhysicsScene::getShapeSource), &PhysicsScene::getShapeSource>;
	mono_add_internal_call("Lumix.MeshRigidActor::getSource", getter);
	auto setter = &csharp_setProperty<decltype(&PhysicsScene::setShapeSource), &PhysicsScene::setShapeSource>;
	mono_add_internal_call("Lumix.MeshRigidActor::setSource", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&AudioScene::isAmbientSound3D), &AudioScene::isAmbientSound3D>;
	mono_add_internal_call("Lumix.AmbientSound::get3D", getter);
	auto setter = &csharp_setProperty<decltype(&AudioScene::setAmbientSound3D), &AudioScene::setAmbientSound3D>;
	mono_add_internal_call("Lumix.AmbientSound::set3D", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&AudioScene::getEchoZoneRadius), &AudioScene::getEchoZoneRadius>;
	mono_add_internal_call("Lumix.EchoZone::getRadius", getter);
	auto setter = &csharp_setProperty<decltype(&AudioScene::setEchoZoneRadius), &AudioScene::setEchoZoneRadius>;
	mono_add_internal_call("Lumix.EchoZone::setRadius", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&AudioScene::getEchoZoneDelay), &AudioScene::getEchoZoneDelay>;
	mono_add_internal_call("Lumix.EchoZone::getDelay", getter);
	auto setter = &csharp_setProperty<decltype(&AudioScene::setEchoZoneDelay), &AudioScene::setEchoZoneDelay>;
	mono_add_internal_call("Lumix.EchoZone::setDelay", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&AnimationScene::getAnimation), &AnimationScene::getAnimation>;
	mono_add_internal_call("Lumix.Animable::getAnimation", getter);
	auto setter = &csharp_setProperty<decltype(&AnimationScene::setAnimation), &AnimationScene::setAnimation>;
	mono_add_internal_call("Lumix.Animable::setAnimation", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&AnimationScene::getAnimableStartTime), &AnimationScene::getAnimableStartTime>;
	mono_add_internal_call("Lumix.Animable::getStartTime", getter);
	auto setter = &csharp_setProperty<decltype(&AnimationScene::setAnimableStartTime), &AnimationScene::setAnimableStartTime>;
	mono_add_internal_call("Lumix.Animable::setStartTime", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&AnimationScene::getAnimableTimeScale), &AnimationScene::getAnimableTimeScale>;
	mono_add_internal_call("Lumix.Animable::getTimeScale", getter);
	auto setter = &csharp_setProperty<decltype(&AnimationScene::setAnimableTimeScale), &AnimationScene::setAnimableTimeScale>;
	mono_add_internal_call("Lumix.Animable::setTimeScale", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&AnimationScene::getControllerSource), &AnimationScene::getControllerSource>;
	mono_add_internal_call("Lumix.AnimController::getSource", getter);
	auto setter = &csharp_setProperty<decltype(&AnimationScene::setControllerSource), &AnimationScene::setControllerSource>;
	mono_add_internal_call("Lumix.AnimController::setSource", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&NavigationScene::getAgentRadius), &NavigationScene::getAgentRadius>;
	mono_add_internal_call("Lumix.NavmeshAgent::getRadius", getter);
	auto setter = &csharp_setProperty<decltype(&NavigationScene::setAgentRadius), &NavigationScene::setAgentRadius>;
	mono_add_internal_call("Lumix.NavmeshAgent::setRadius", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&NavigationScene::getAgentHeight), &NavigationScene::getAgentHeight>;
	mono_add_internal_call("Lumix.NavmeshAgent::getHeight", getter);
	auto setter = &csharp_setProperty<decltype(&NavigationScene::setAgentHeight), &NavigationScene::setAgentHeight>;
	mono_add_internal_call("Lumix.NavmeshAgent::setHeight", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&NavigationScene::useAgentRootMotion), &NavigationScene::useAgentRootMotion>;
	mono_add_internal_call("Lumix.NavmeshAgent::getUseRootMotion", getter);
	auto setter = &csharp_setProperty<decltype(&NavigationScene::setUseAgentRootMotion), &NavigationScene::setUseAgentRootMotion>;
	mono_add_internal_call("Lumix.NavmeshAgent::setUseRootMotion", setter);
}

{
	auto getter = &csharp_getProperty<decltype(&NavigationScene::isGettingRootMotionFromAnim), &NavigationScene::isGettingRootMotionFromAnim>;
	mono_add_internal_call("Lumix.NavmeshAgent::getGetRootMotionFromAnimation", getter);
	auto setter = &csharp_setProperty<decltype(&NavigationScene::setIsGettingRootMotionFromAnim), &NavigationScene::setIsGettingRootMotionFromAnim>;
	mono_add_internal_call("Lumix.NavmeshAgent::setGetRootMotionFromAnimation", setter);
}

