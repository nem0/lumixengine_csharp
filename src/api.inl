{
	using T = Lumix::EntityPtr(Lumix::GUIScene::*)(const Vec2&) const;
	auto f = &CSharpMethodProxy<T>::call<(T)&GUIScene::getRectAt>;
	mono_add_internal_call("Lumix.GuiScene::getRectAt", f);
}


{
	using T = Lumix::EntityPtr(Lumix::GUIScene::*)(const Vec2&) const;
	auto f = &CSharpMethodProxy<T>::call<(T)&GUIScene::getRectAt>;
	mono_add_internal_call("Lumix.GuiScene::GetRectAt", f);
}
{
	using T = bool(Lumix::GUIScene::*)(const Vec2&, EntityRef);
	auto f = &CSharpMethodProxy<T>::call<(T)&GUIScene::isOver>;
	mono_add_internal_call("Lumix.GuiScene::isOver", f);
}


{
	using T = bool(Lumix::GUIScene::*)(const Vec2&, EntityRef);
	auto f = &CSharpMethodProxy<T>::call<(T)&GUIScene::isOver>;
	mono_add_internal_call("Lumix.GuiScene::IsOver", f);
}
{
	using T = Lumix::GUISystem *(Lumix::GUIScene::*)();
	auto f = &CSharpMethodProxy<T>::call<(T)&GUIScene::getSystem>;
	mono_add_internal_call("Lumix.GuiScene::getSystem", f);
}


{
	using T = Lumix::GUISystem *(Lumix::GUIScene::*)();
	auto f = &CSharpMethodProxy<T>::call<(T)&GUIScene::getSystem>;
	mono_add_internal_call("Lumix.GuiScene::GetSystem", f);
}
{
	using T = void(Lumix::AudioScene::*)(float);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::setMasterVolume>;
	mono_add_internal_call("Lumix.AudioScene::setMasterVolume", f);
}


{
	using T = void(Lumix::AudioScene::*)(float);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::setMasterVolume>;
	mono_add_internal_call("Lumix.AudioScene::SetMasterVolume", f);
}
{
	using T = int(Lumix::AudioScene::*)(EntityRef, const Path&, bool);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::play>;
	mono_add_internal_call("Lumix.AudioScene::play", f);
}


{
	using T = int(Lumix::AudioScene::*)(EntityRef, const Path&, bool);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::play>;
	mono_add_internal_call("Lumix.AudioScene::Play", f);
}
{
	using T = void(Lumix::AudioScene::*)(int);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::stop>;
	mono_add_internal_call("Lumix.AudioScene::stop", f);
}


{
	using T = void(Lumix::AudioScene::*)(int);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::stop>;
	mono_add_internal_call("Lumix.AudioScene::Stop", f);
}
{
	using T = bool(Lumix::AudioScene::*)(int);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::isEnd>;
	mono_add_internal_call("Lumix.AudioScene::isEnd", f);
}


{
	using T = bool(Lumix::AudioScene::*)(int);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::isEnd>;
	mono_add_internal_call("Lumix.AudioScene::IsEnd", f);
}
{
	using T = void(Lumix::AudioScene::*)(int, u32);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::setFrequency>;
	mono_add_internal_call("Lumix.AudioScene::setFrequency", f);
}


{
	using T = void(Lumix::AudioScene::*)(int, u32);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::setFrequency>;
	mono_add_internal_call("Lumix.AudioScene::SetFrequency", f);
}
{
	using T = void(Lumix::AudioScene::*)(int, float);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::setVolume>;
	mono_add_internal_call("Lumix.AudioScene::setVolume", f);
}


{
	using T = void(Lumix::AudioScene::*)(int, float);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::setVolume>;
	mono_add_internal_call("Lumix.AudioScene::SetVolume", f);
}
{
	using T = void(Lumix::AudioScene::*)(int, float, float, float, float);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::setEcho>;
	mono_add_internal_call("Lumix.AudioScene::setEcho", f);
}


{
	using T = void(Lumix::AudioScene::*)(int, float, float, float, float);
	auto f = &CSharpMethodProxy<T>::call<(T)&AudioScene::setEcho>;
	mono_add_internal_call("Lumix.AudioScene::SetEcho", f);
}
{
	using T = void(Lumix::RenderScene::*)(const DVec3&, float, Color);
	auto f = &CSharpMethodProxy<T>::call<(T)&RenderScene::addDebugCross>;
	mono_add_internal_call("Lumix.RendererScene::addDebugCross", f);
}


{
	using T = void(Lumix::RenderScene::*)(const DVec3&, float, Color);
	auto f = &CSharpMethodProxy<T>::call<(T)&RenderScene::addDebugCross>;
	mono_add_internal_call("Lumix.RendererScene::AddDebugCross", f);
}
{
	using T = void(Lumix::RenderScene::*)(const DVec3&, const DVec3&, Color);
	auto f = &CSharpMethodProxy<T>::call<(T)&RenderScene::addDebugLine>;
	mono_add_internal_call("Lumix.RendererScene::addDebugLine", f);
}


{
	using T = void(Lumix::RenderScene::*)(const DVec3&, const DVec3&, Color);
	auto f = &CSharpMethodProxy<T>::call<(T)&RenderScene::addDebugLine>;
	mono_add_internal_call("Lumix.RendererScene::AddDebugLine", f);
}
{
	using T = void(Lumix::RenderScene::*)(const DVec3&, const DVec3&, const DVec3&, Color);
	auto f = &CSharpMethodProxy<T>::call<(T)&RenderScene::addDebugTriangle>;
	mono_add_internal_call("Lumix.RendererScene::addDebugTriangle", f);
}


{
	using T = void(Lumix::RenderScene::*)(const DVec3&, const DVec3&, const DVec3&, Color);
	auto f = &CSharpMethodProxy<T>::call<(T)&RenderScene::addDebugTriangle>;
	mono_add_internal_call("Lumix.RendererScene::AddDebugTriangle", f);
}
{
	using T = void(Lumix::RenderScene::*)(EntityRef);
	auto f = &CSharpMethodProxy<T>::call<(T)&RenderScene::setActiveCamera>;
	mono_add_internal_call("Lumix.RendererScene::setActiveCamera", f);
}


{
	using T = void(Lumix::RenderScene::*)(EntityRef);
	auto f = &CSharpMethodProxy<T>::call<(T)&RenderScene::setActiveCamera>;
	mono_add_internal_call("Lumix.RendererScene::SetActiveCamera", f);
}
{
	using T = Lumix::EntityPtr(Lumix::PhysicsScene::*)(const Vec3&, const Vec3&, EntityRef);
	auto f = &CSharpMethodProxy<T>::call<(T)&PhysicsScene::raycast>;
	mono_add_internal_call("Lumix.PhysicsScene::raycast", f);
}


{
	using T = Lumix::EntityPtr(Lumix::PhysicsScene::*)(const Vec3&, const Vec3&, EntityRef);
	auto f = &CSharpMethodProxy<T>::call<(T)&PhysicsScene::raycast>;
	mono_add_internal_call("Lumix.PhysicsScene::Raycast", f);
}
{
	const reflection::Property<bool>* prop = getProperty<bool>("gui_rect", "Enabled");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getEnabled", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setEnabled", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("gui_rect", "Clip content");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getClipContent", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setClipContent", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("gui_rect", "Top Points");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getTopPoints", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setTopPoints", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("gui_rect", "Top Relative");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getTopRelative", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setTopRelative", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("gui_rect", "Right Points");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getRightPoints", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setRightPoints", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("gui_rect", "Right Relative");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getRightRelative", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setRightRelative", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("gui_rect", "Bottom Points");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getBottomPoints", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setBottomPoints", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("gui_rect", "Bottom Relative");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getBottomRelative", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setBottomRelative", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("gui_rect", "Left Points");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getLeftPoints", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setLeftPoints", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("gui_rect", "Left Relative");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiRect::getLeftRelative", prop->getter);
	mono_add_internal_call("Lumix.GuiRect::setLeftRelative", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("gui_canvas", "Is 3D");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiCanvas::getIs3D", prop->getter);
	mono_add_internal_call("Lumix.GuiCanvas::setIs3D", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("gui_canvas", "Orient to camera");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiCanvas::getOrientToCamera", prop->getter);
	mono_add_internal_call("Lumix.GuiCanvas::setOrientToCamera", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("gui_canvas", "Virtual size");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiCanvas::getVirtualSize", prop->getter);
	mono_add_internal_call("Lumix.GuiCanvas::setVirtualSize", prop->setter);
}

{
	const reflection::Property<u32>* prop = getProperty<u32>("particle_emitter", "Emit rate");
	ASSERT(prop);
	mono_add_internal_call("Lumix.ParticleEmitter::getEmitRate", prop->getter);
	mono_add_internal_call("Lumix.ParticleEmitter::setEmitRate", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("particle_emitter", "Autodestroy");
	ASSERT(prop);
	mono_add_internal_call("Lumix.ParticleEmitter::getAutodestroy", prop->getter);
	mono_add_internal_call("Lumix.ParticleEmitter::setAutodestroy", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("particle_emitter", "Source");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.ParticleEmitter::getSource", &S::cs_getter);
	mono_add_internal_call("Lumix.ParticleEmitter::setSource", &S::cs_setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("terrain", "Material");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.Terrain::getMaterial", &S::cs_getter);
	mono_add_internal_call("Lumix.Terrain::setMaterial", &S::cs_setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("terrain", "XZ scale");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Terrain::getXZScale", prop->getter);
	mono_add_internal_call("Lumix.Terrain::setXZScale", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("terrain", "Height scale");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Terrain::getHeightScale", prop->getter);
	mono_add_internal_call("Lumix.Terrain::setHeightScale", prop->setter);
}

{
	const reflection::Property<u32>* prop = getProperty<u32>("terrain", "Tesselation");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Terrain::getTesselation", prop->getter);
	mono_add_internal_call("Lumix.Terrain::setTesselation", prop->setter);
}

{
	const reflection::Property<u32>* prop = getProperty<u32>("terrain", "Grid resolution");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Terrain::getGridResolution", prop->getter);
	mono_add_internal_call("Lumix.Terrain::setGridResolution", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("camera", "FOV");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Camera::getFOV", prop->getter);
	mono_add_internal_call("Lumix.Camera::setFOV", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("camera", "Near");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Camera::getNear", prop->getter);
	mono_add_internal_call("Lumix.Camera::setNear", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("camera", "Far");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Camera::getFar", prop->getter);
	mono_add_internal_call("Lumix.Camera::setFar", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("camera", "Orthographic");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Camera::getOrthographic", prop->getter);
	mono_add_internal_call("Lumix.Camera::setOrthographic", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("camera", "Orthographic size");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Camera::getOrthographicSize", prop->getter);
	mono_add_internal_call("Lumix.Camera::setOrthographicSize", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("decal", "Material");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.Decal::getMaterial", &S::cs_getter);
	mono_add_internal_call("Lumix.Decal::setMaterial", &S::cs_setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("decal", "Half extents");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Decal::getHalfExtents", prop->getter);
	mono_add_internal_call("Lumix.Decal::setHalfExtents", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("decal", "UV scale");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Decal::getUVScale", prop->getter);
	mono_add_internal_call("Lumix.Decal::setUVScale", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("curve_decal", "Material");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.CurveDecal::getMaterial", &S::cs_getter);
	mono_add_internal_call("Lumix.CurveDecal::setMaterial", &S::cs_setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("curve_decal", "Half extents");
	ASSERT(prop);
	mono_add_internal_call("Lumix.CurveDecal::getHalfExtents", prop->getter);
	mono_add_internal_call("Lumix.CurveDecal::setHalfExtents", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("curve_decal", "UV scale");
	ASSERT(prop);
	mono_add_internal_call("Lumix.CurveDecal::getUVScale", prop->getter);
	mono_add_internal_call("Lumix.CurveDecal::setUVScale", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("curve_decal", "Bezier P0");
	ASSERT(prop);
	mono_add_internal_call("Lumix.CurveDecal::getBezierP0", prop->getter);
	mono_add_internal_call("Lumix.CurveDecal::setBezierP0", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("curve_decal", "Bezier P2");
	ASSERT(prop);
	mono_add_internal_call("Lumix.CurveDecal::getBezierP2", prop->getter);
	mono_add_internal_call("Lumix.CurveDecal::setBezierP2", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("point_light", "Cast shadows");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PointLight::getCastShadows", prop->getter);
	mono_add_internal_call("Lumix.PointLight::setCastShadows", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("point_light", "Dynamic");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PointLight::getDynamic", prop->getter);
	mono_add_internal_call("Lumix.PointLight::setDynamic", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("point_light", "Intensity");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PointLight::getIntensity", prop->getter);
	mono_add_internal_call("Lumix.PointLight::setIntensity", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("point_light", "FOV");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PointLight::getFOV", prop->getter);
	mono_add_internal_call("Lumix.PointLight::setFOV", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("point_light", "Attenuation");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PointLight::getAttenuation", prop->getter);
	mono_add_internal_call("Lumix.PointLight::setAttenuation", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("point_light", "Color");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PointLight::getColor", prop->getter);
	mono_add_internal_call("Lumix.PointLight::setColor", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("point_light", "Range");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PointLight::getRange", prop->getter);
	mono_add_internal_call("Lumix.PointLight::setRange", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("environment", "Color");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Environment::getColor", prop->getter);
	mono_add_internal_call("Lumix.Environment::setColor", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("environment", "Intensity");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Environment::getIntensity", prop->getter);
	mono_add_internal_call("Lumix.Environment::setIntensity", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("environment", "Indirect intensity");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Environment::getIndirectIntensity", prop->getter);
	mono_add_internal_call("Lumix.Environment::setIndirectIntensity", prop->setter);
}

{
	const reflection::Property<Vec4>* prop = getProperty<Vec4>("environment", "Shadow cascades");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Environment::getShadowCascades", prop->getter);
	mono_add_internal_call("Lumix.Environment::setShadowCascades", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("environment", "Cast shadows");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Environment::getCastShadows", prop->getter);
	mono_add_internal_call("Lumix.Environment::setCastShadows", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("instanced_model", "Model");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.InstancedModel::getModel", &S::cs_getter);
	mono_add_internal_call("Lumix.InstancedModel::setModel", &S::cs_setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("model_instance", "Enabled");
	ASSERT(prop);
	mono_add_internal_call("Lumix.ModelInstance::getEnabled", prop->getter);
	mono_add_internal_call("Lumix.ModelInstance::setEnabled", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("model_instance", "Material");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.ModelInstance::getMaterial", &S::cs_getter);
	mono_add_internal_call("Lumix.ModelInstance::setMaterial", &S::cs_setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("model_instance", "Source");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.ModelInstance::getSource", &S::cs_getter);
	mono_add_internal_call("Lumix.ModelInstance::setSource", &S::cs_setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("environment_probe", "Enabled");
	ASSERT(prop);
	mono_add_internal_call("Lumix.EnvironmentProbe::getEnabled", prop->getter);
	mono_add_internal_call("Lumix.EnvironmentProbe::setEnabled", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("environment_probe", "Inner range");
	ASSERT(prop);
	mono_add_internal_call("Lumix.EnvironmentProbe::getInnerRange", prop->getter);
	mono_add_internal_call("Lumix.EnvironmentProbe::setInnerRange", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("environment_probe", "Outer range");
	ASSERT(prop);
	mono_add_internal_call("Lumix.EnvironmentProbe::getOuterRange", prop->getter);
	mono_add_internal_call("Lumix.EnvironmentProbe::setOuterRange", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("reflection_probe", "Enabled");
	ASSERT(prop);
	mono_add_internal_call("Lumix.ReflectionProbe::getEnabled", prop->getter);
	mono_add_internal_call("Lumix.ReflectionProbe::setEnabled", prop->setter);
}

{
	const reflection::Property<u32>* prop = getProperty<u32>("reflection_probe", "size");
	ASSERT(prop);
	mono_add_internal_call("Lumix.ReflectionProbe::getSize", prop->getter);
	mono_add_internal_call("Lumix.ReflectionProbe::setSize", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("reflection_probe", "half_extents");
	ASSERT(prop);
	mono_add_internal_call("Lumix.ReflectionProbe::getHalfExtents", prop->getter);
	mono_add_internal_call("Lumix.ReflectionProbe::setHalfExtents", prop->setter);
}

{
	const reflection::Property<u32>* prop = getProperty<u32>("fur", "Layers");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Fur::getLayers", prop->getter);
	mono_add_internal_call("Lumix.Fur::setLayers", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("fur", "Scale");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Fur::getScale", prop->getter);
	mono_add_internal_call("Lumix.Fur::setScale", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("fur", "Gravity");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Fur::getGravity", prop->getter);
	mono_add_internal_call("Lumix.Fur::setGravity", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("fur", "Enabled");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Fur::getEnabled", prop->getter);
	mono_add_internal_call("Lumix.Fur::setEnabled", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("procedural_geom", "Material");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.ProceduralGeom::getMaterial", &S::cs_getter);
	mono_add_internal_call("Lumix.ProceduralGeom::setMaterial", &S::cs_setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("bone_attachment", "Relative position");
	ASSERT(prop);
	mono_add_internal_call("Lumix.BoneAttachment::getRelativePosition", prop->getter);
	mono_add_internal_call("Lumix.BoneAttachment::setRelativePosition", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("bone_attachment", "Relative rotation");
	ASSERT(prop);
	mono_add_internal_call("Lumix.BoneAttachment::getRelativeRotation", prop->getter);
	mono_add_internal_call("Lumix.BoneAttachment::setRelativeRotation", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("bone_attachment", "Bone");
	ASSERT(prop);
	mono_add_internal_call("Lumix.BoneAttachment::getBone", prop->getter);
	mono_add_internal_call("Lumix.BoneAttachment::setBone", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("rigid_actor", "Layer");
	ASSERT(prop);
	mono_add_internal_call("Lumix.RigidActor::getLayer", prop->getter);
	mono_add_internal_call("Lumix.RigidActor::setLayer", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("rigid_actor", "Dynamic");
	ASSERT(prop);
	mono_add_internal_call("Lumix.RigidActor::getDynamic", prop->getter);
	mono_add_internal_call("Lumix.RigidActor::setDynamic", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("rigid_actor", "Trigger");
	ASSERT(prop);
	mono_add_internal_call("Lumix.RigidActor::getTrigger", prop->getter);
	mono_add_internal_call("Lumix.RigidActor::setTrigger", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("rigid_actor", "Mesh");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.RigidActor::getMesh", &S::cs_getter);
	mono_add_internal_call("Lumix.RigidActor::setMesh", &S::cs_setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("rigid_actor", "Material");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.RigidActor::getMaterial", &S::cs_getter);
	mono_add_internal_call("Lumix.RigidActor::setMaterial", &S::cs_setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("physical_heightfield", "Layer");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalHeightfield::getLayer", prop->getter);
	mono_add_internal_call("Lumix.PhysicalHeightfield::setLayer", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("physical_heightfield", "Heightmap");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.PhysicalHeightfield::getHeightmap", &S::cs_getter);
	mono_add_internal_call("Lumix.PhysicalHeightfield::setHeightmap", &S::cs_setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("physical_heightfield", "Y scale");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalHeightfield::getYScale", prop->getter);
	mono_add_internal_call("Lumix.PhysicalHeightfield::setYScale", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("physical_heightfield", "XZ scale");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalHeightfield::getXZScale", prop->getter);
	mono_add_internal_call("Lumix.PhysicalHeightfield::setXZScale", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("physical_controller", "Radius");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalController::getRadius", prop->getter);
	mono_add_internal_call("Lumix.PhysicalController::setRadius", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("physical_controller", "Height");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalController::getHeight", prop->getter);
	mono_add_internal_call("Lumix.PhysicalController::setHeight", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("physical_controller", "Layer");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalController::getLayer", prop->getter);
	mono_add_internal_call("Lumix.PhysicalController::setLayer", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("physical_controller", "Use root motion");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalController::getUseRootMotion", prop->getter);
	mono_add_internal_call("Lumix.PhysicalController::setUseRootMotion", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("physical_controller", "Use custom gravity");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalController::getUseCustomGravity", prop->getter);
	mono_add_internal_call("Lumix.PhysicalController::setUseCustomGravity", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("physical_controller", "Custom gravity acceleration");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalController::getCustomGravityAcceleration", prop->getter);
	mono_add_internal_call("Lumix.PhysicalController::setCustomGravityAcceleration", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("gui_image", "Enabled");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiImage::getEnabled", prop->getter);
	mono_add_internal_call("Lumix.GuiImage::setEnabled", prop->setter);
}

{
	const reflection::Property<Vec4>* prop = getProperty<Vec4>("gui_image", "Color");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiImage::getColor", prop->getter);
	mono_add_internal_call("Lumix.GuiImage::setColor", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("gui_image", "Sprite");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.GuiImage::getSprite", &S::cs_getter);
	mono_add_internal_call("Lumix.GuiImage::setSprite", &S::cs_setter);
}

{
	const reflection::Property<const char*>* prop = getProperty<const char*>("gui_text", "Text");
	ASSERT(prop);
	struct S : CSPropertyWrapper<const char*> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.GuiText::getText", &S::cs_getter);
	mono_add_internal_call("Lumix.GuiText::setText", &S::cs_setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("gui_text", "Font");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.GuiText::getFont", &S::cs_getter);
	mono_add_internal_call("Lumix.GuiText::setFont", &S::cs_setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("gui_text", "Font Size");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiText::getFontSize", prop->getter);
	mono_add_internal_call("Lumix.GuiText::setFontSize", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("gui_text", "Horizontal align");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiText::getHorizontalAlign", prop->getter);
	mono_add_internal_call("Lumix.GuiText::setHorizontalAlign", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("gui_text", "Vertical align");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiText::getVerticalAlign", prop->getter);
	mono_add_internal_call("Lumix.GuiText::setVerticalAlign", prop->setter);
}

{
	const reflection::Property<Vec4>* prop = getProperty<Vec4>("gui_text", "Color");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiText::getColor", prop->getter);
	mono_add_internal_call("Lumix.GuiText::setColor", prop->setter);
}

{
	const reflection::Property<Vec4>* prop = getProperty<Vec4>("gui_button", "Hovered color");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiButton::getHoveredColor", prop->getter);
	mono_add_internal_call("Lumix.GuiButton::setHoveredColor", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("gui_button", "Cursor");
	ASSERT(prop);
	mono_add_internal_call("Lumix.GuiButton::getCursor", prop->getter);
	mono_add_internal_call("Lumix.GuiButton::setCursor", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("animable", "Animation");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.Animable::getAnimation", &S::cs_getter);
	mono_add_internal_call("Lumix.Animable::setAnimation", &S::cs_setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("property_animator", "Animation");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.PropertyAnimator::getAnimation", &S::cs_getter);
	mono_add_internal_call("Lumix.PropertyAnimator::setAnimation", &S::cs_setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("property_animator", "Enabled");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PropertyAnimator::getEnabled", prop->getter);
	mono_add_internal_call("Lumix.PropertyAnimator::setEnabled", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("animator", "Source");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.Animator::getSource", &S::cs_getter);
	mono_add_internal_call("Lumix.Animator::setSource", &S::cs_setter);
}

{
	const reflection::Property<u32>* prop = getProperty<u32>("animator", "Default set");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Animator::getDefaultSet", prop->getter);
	mono_add_internal_call("Lumix.Animator::setDefaultSet", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("distance_joint", "Axis position");
	ASSERT(prop);
	mono_add_internal_call("Lumix.DistanceJoint::getAxisPosition", prop->getter);
	mono_add_internal_call("Lumix.DistanceJoint::setAxisPosition", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("distance_joint", "Damping");
	ASSERT(prop);
	mono_add_internal_call("Lumix.DistanceJoint::getDamping", prop->getter);
	mono_add_internal_call("Lumix.DistanceJoint::setDamping", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("distance_joint", "Stiffness");
	ASSERT(prop);
	mono_add_internal_call("Lumix.DistanceJoint::getStiffness", prop->getter);
	mono_add_internal_call("Lumix.DistanceJoint::setStiffness", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("distance_joint", "Tolerance");
	ASSERT(prop);
	mono_add_internal_call("Lumix.DistanceJoint::getTolerance", prop->getter);
	mono_add_internal_call("Lumix.DistanceJoint::setTolerance", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("distance_joint", "Limits");
	ASSERT(prop);
	mono_add_internal_call("Lumix.DistanceJoint::getLimits", prop->getter);
	mono_add_internal_call("Lumix.DistanceJoint::setLimits", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("hinge_joint", "Axis position");
	ASSERT(prop);
	mono_add_internal_call("Lumix.HingeJoint::getAxisPosition", prop->getter);
	mono_add_internal_call("Lumix.HingeJoint::setAxisPosition", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("hinge_joint", "Axis direction");
	ASSERT(prop);
	mono_add_internal_call("Lumix.HingeJoint::getAxisDirection", prop->getter);
	mono_add_internal_call("Lumix.HingeJoint::setAxisDirection", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("hinge_joint", "Damping");
	ASSERT(prop);
	mono_add_internal_call("Lumix.HingeJoint::getDamping", prop->getter);
	mono_add_internal_call("Lumix.HingeJoint::setDamping", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("hinge_joint", "Stiffness");
	ASSERT(prop);
	mono_add_internal_call("Lumix.HingeJoint::getStiffness", prop->getter);
	mono_add_internal_call("Lumix.HingeJoint::setStiffness", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("hinge_joint", "Use limit");
	ASSERT(prop);
	mono_add_internal_call("Lumix.HingeJoint::getUseLimit", prop->getter);
	mono_add_internal_call("Lumix.HingeJoint::setUseLimit", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("hinge_joint", "Limit");
	ASSERT(prop);
	mono_add_internal_call("Lumix.HingeJoint::getLimit", prop->getter);
	mono_add_internal_call("Lumix.HingeJoint::setLimit", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("spherical_joint", "Axis position");
	ASSERT(prop);
	mono_add_internal_call("Lumix.SphericalJoint::getAxisPosition", prop->getter);
	mono_add_internal_call("Lumix.SphericalJoint::setAxisPosition", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("spherical_joint", "Axis direction");
	ASSERT(prop);
	mono_add_internal_call("Lumix.SphericalJoint::getAxisDirection", prop->getter);
	mono_add_internal_call("Lumix.SphericalJoint::setAxisDirection", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("spherical_joint", "Use limit");
	ASSERT(prop);
	mono_add_internal_call("Lumix.SphericalJoint::getUseLimit", prop->getter);
	mono_add_internal_call("Lumix.SphericalJoint::setUseLimit", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("spherical_joint", "Limit");
	ASSERT(prop);
	mono_add_internal_call("Lumix.SphericalJoint::getLimit", prop->getter);
	mono_add_internal_call("Lumix.SphericalJoint::setLimit", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("d6_joint", "Axis position");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getAxisPosition", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setAxisPosition", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("d6_joint", "Axis direction");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getAxisDirection", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setAxisDirection", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("d6_joint", "X motion");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getXMotion", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setXMotion", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("d6_joint", "Y motion");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getYMotion", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setYMotion", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("d6_joint", "Z motion");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getZMotion", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setZMotion", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("d6_joint", "Swing 1");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getSwing1", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setSwing1", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("d6_joint", "Swing 2");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getSwing2", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setSwing2", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("d6_joint", "Twist");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getTwist", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setTwist", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("d6_joint", "Linear limit");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getLinearLimit", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setLinearLimit", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("d6_joint", "Swing limit");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getSwingLimit", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setSwingLimit", prop->setter);
}

{
	const reflection::Property<Vec2>* prop = getProperty<Vec2>("d6_joint", "Twist limit");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getTwistLimit", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setTwistLimit", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("d6_joint", "Damping");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getDamping", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setDamping", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("d6_joint", "Stiffness");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getStiffness", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setStiffness", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("d6_joint", "Restitution");
	ASSERT(prop);
	mono_add_internal_call("Lumix.D6Joint::getRestitution", prop->getter);
	mono_add_internal_call("Lumix.D6Joint::setRestitution", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("vehicle", "Speed");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Vehicle::getSpeed", prop->getter);
	mono_add_internal_call("Lumix.Vehicle::setSpeed", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("vehicle", "Current gear");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Vehicle::getCurrentGear", prop->getter);
	mono_add_internal_call("Lumix.Vehicle::setCurrentGear", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("vehicle", "RPM");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Vehicle::getRPM", prop->getter);
	mono_add_internal_call("Lumix.Vehicle::setRPM", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("vehicle", "Mass");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Vehicle::getMass", prop->getter);
	mono_add_internal_call("Lumix.Vehicle::setMass", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("vehicle", "Center of mass");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Vehicle::getCenterOfMass", prop->getter);
	mono_add_internal_call("Lumix.Vehicle::setCenterOfMass", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("vehicle", "MOI multiplier");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Vehicle::getMOIMultiplier", prop->getter);
	mono_add_internal_call("Lumix.Vehicle::setMOIMultiplier", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("vehicle", "Chassis");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.Vehicle::getChassis", &S::cs_getter);
	mono_add_internal_call("Lumix.Vehicle::setChassis", &S::cs_setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("vehicle", "Chassis layer");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Vehicle::getChassisLayer", prop->getter);
	mono_add_internal_call("Lumix.Vehicle::setChassisLayer", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("vehicle", "Wheels layer");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Vehicle::getWheelsLayer", prop->getter);
	mono_add_internal_call("Lumix.Vehicle::setWheelsLayer", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "Radius");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getRadius", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setRadius", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "Width");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getWidth", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setWidth", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "Mass");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getMass", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setMass", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "MOI");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getMOI", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setMOI", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "Max compression");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getMaxCompression", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setMaxCompression", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "Max droop");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getMaxDroop", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setMaxDroop", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "Spring strength");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getSpringStrength", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setSpringStrength", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "Spring damper rate");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getSpringDamperRate", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setSpringDamperRate", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("wheel", "Slot");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getSlot", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setSlot", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("wheel", "RPM");
	ASSERT(prop);
	mono_add_internal_call("Lumix.Wheel::getRPM", prop->getter);
	mono_add_internal_call("Lumix.Wheel::setRPM", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_agent", "Radius");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshAgent::getRadius", prop->getter);
	mono_add_internal_call("Lumix.NavmeshAgent::setRadius", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_agent", "Height");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshAgent::getHeight", prop->getter);
	mono_add_internal_call("Lumix.NavmeshAgent::setHeight", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("navmesh_agent", "Move entity");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshAgent::getMoveEntity", prop->getter);
	mono_add_internal_call("Lumix.NavmeshAgent::setMoveEntity", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_agent", "Speed");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshAgent::getSpeed", prop->getter);
	mono_add_internal_call("Lumix.NavmeshAgent::setSpeed", prop->setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("navmesh_zone", "Extents");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getExtents", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setExtents", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_zone", "Agent height");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getAgentHeight", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setAgentHeight", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_zone", "Agent radius");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getAgentRadius", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setAgentRadius", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_zone", "Cell size");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getCellSize", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setCellSize", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_zone", "Cell height");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getCellHeight", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setCellHeight", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_zone", "Walkable slope angle");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getWalkableSlopeAngle", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setWalkableSlopeAngle", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("navmesh_zone", "Max climb");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getMaxClimb", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setMaxClimb", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("navmesh_zone", "Autoload");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getAutoload", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setAutoload", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("navmesh_zone", "Detailed");
	ASSERT(prop);
	mono_add_internal_call("Lumix.NavmeshZone::getDetailed", prop->getter);
	mono_add_internal_call("Lumix.NavmeshZone::setDetailed", prop->setter);
}

{
	const reflection::Property<const char*>* prop = getProperty<const char*>("lua_script_inline", "Code");
	ASSERT(prop);
	struct S : CSPropertyWrapper<const char*> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.LuaScriptInline::getCode", &S::cs_getter);
	mono_add_internal_call("Lumix.LuaScriptInline::setCode", &S::cs_setter);
}

{
	const reflection::Property<Vec3>* prop = getProperty<Vec3>("physical_instanced_cube", "Half extents");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalInstancedCube::getHalfExtents", prop->getter);
	mono_add_internal_call("Lumix.PhysicalInstancedCube::setHalfExtents", prop->setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("physical_instanced_cube", "Layer");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalInstancedCube::getLayer", prop->getter);
	mono_add_internal_call("Lumix.PhysicalInstancedCube::setLayer", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("physical_instanced_mesh", "Mesh");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.PhysicalInstancedMesh::getMesh", &S::cs_getter);
	mono_add_internal_call("Lumix.PhysicalInstancedMesh::setMesh", &S::cs_setter);
}

{
	const reflection::Property<int>* prop = getProperty<int>("physical_instanced_mesh", "Layer");
	ASSERT(prop);
	mono_add_internal_call("Lumix.PhysicalInstancedMesh::getLayer", prop->getter);
	mono_add_internal_call("Lumix.PhysicalInstancedMesh::setLayer", prop->setter);
}

{
	const reflection::Property<bool>* prop = getProperty<bool>("ambient_sound", "3D");
	ASSERT(prop);
	mono_add_internal_call("Lumix.AmbientSound::get3D", prop->getter);
	mono_add_internal_call("Lumix.AmbientSound::set3D", prop->setter);
}

{
	const reflection::Property<Path>* prop = getProperty<Path>("ambient_sound", "Sound");
	ASSERT(prop);
	struct S : CSPropertyWrapper<Path> {};
	S::getter = prop->getter;
	S::setter = prop->setter;
	mono_add_internal_call("Lumix.AmbientSound::getSound", &S::cs_getter);
	mono_add_internal_call("Lumix.AmbientSound::setSound", &S::cs_setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("echo_zone", "Radius");
	ASSERT(prop);
	mono_add_internal_call("Lumix.EchoZone::getRadius", prop->getter);
	mono_add_internal_call("Lumix.EchoZone::setRadius", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("echo_zone", "Delay (ms)");
	ASSERT(prop);
	mono_add_internal_call("Lumix.EchoZone::getDelay", prop->getter);
	mono_add_internal_call("Lumix.EchoZone::setDelay", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("chorus_zone", "Radius");
	ASSERT(prop);
	mono_add_internal_call("Lumix.ChorusZone::getRadius", prop->getter);
	mono_add_internal_call("Lumix.ChorusZone::setRadius", prop->setter);
}

{
	const reflection::Property<float>* prop = getProperty<float>("chorus_zone", "Delay (ms)");
	ASSERT(prop);
	mono_add_internal_call("Lumix.ChorusZone::getDelay", prop->getter);
	mono_add_internal_call("Lumix.ChorusZone::setDelay", prop->setter);
}

