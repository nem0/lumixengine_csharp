function linkMono()
	libdirs {[[c:/Program Files/Mono/lib/]]}
end

project "lumixengine_csharp"
	libType()
	files { 
		"src/**.c",
		"src/api.txt",
		"src/**.cpp",
		"src/**.h",
		"src/**.inl",
		"genie.lua"
	}
	includedirs { "../lumixengine_csharp/src", [[c:/Program Files/Mono/include/mono-2.0]] }
	buildoptions { "/wd4267", "/wd4244" }
	defines { "BUILDING_CSHARP" }
	links { "engine" }
	useLua()
	defaultConfigurations()

	configuration { "Debug" }
		postbuildcommands {
			"xcopy /Y \"c:\\Program Files\\Mono\\bin\\mono-2.0-sgen.dll\" \"$(SolutionDir)bin\\Debug\"",
			"xcopy /Y \"c:\\Program Files\\Mono\\lib\\mono\\4.5\\mscorlib.dll\" \"$(SolutionDir)bin\\Debug\"",
			"xcopy /Y \"c:\\Program Files\\Mono\\lib\\mono\\4.5\\system.dll\" \"$(SolutionDir)bin\\Debug\"",
			"xcopy /Y \"c:\\Program Files\\Mono\\lib\\mono\\4.5\\system.configuration.dll\" \"$(SolutionDir)bin\\Debug\"",
		}
	configuration { "RelWithDebInfo" }
		postbuildcommands {
			"xcopy /Y \"c:\\Program Files\\Mono\\bin\\mono-2.0-sgen.dll\" \"$(SolutionDir)bin\\RelWithDebInfo\"",
			"xcopy /Y \"c:\\Program Files\\Mono\\lib\\mono\\4.5\\mscorlib.dll\" \"$(SolutionDir)bin\\RelWithDebInfo\"",
			"xcopy /Y \"c:\\Program Files\\Mono\\lib\\mono\\4.5\\system.dll\" \"$(SolutionDir)bin\\RelWithDebInfo\"",
			"xcopy /Y \"c:\\Program Files\\Mono\\lib\\mono\\4.5\\system.configuration.dll\" \"$(SolutionDir)bin\\RelWithDebInfo\"",
		}

table.insert(build_app_callbacks, linkMono)
table.insert(build_studio_callbacks, linkMono)
