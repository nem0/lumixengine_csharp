function linkMono()
	libdirs {"../../lumixengine_csharp/external/lib/"}
end

project "lumixengine_csharp"
	libType()
	files { 
		"src/**.c",
		"src/**.cpp",
		"src/**.h",
		"genie.lua"
	}
	includedirs { "../lumixengine_csharp/src", [[external/include/mono-2.0]] }
	buildoptions { "/wd4267", "/wd4244" }
	defines { "BUILDING_CSHARP" }
	links { "engine" }
	useLua()
	defaultConfigurations()

project "lumixengine_csharp_binder"
	kind "ConsoleApp"

	files {
			"binder/**.cpp",
			"genie.lua"
	}

	defines { "_CRT_SECURE_NO_WARNINGS" }
	
	defaultConfigurations()
	
table.insert(build_app_callbacks, linkMono)
table.insert(build_studio_callbacks, linkMono)
