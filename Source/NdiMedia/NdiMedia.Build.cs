// Copyright 1998-2017 Epic Games, Inc. All Rights Reserved.

using System.IO;

namespace UnrealBuildTool.Rules
{
	using System.IO;

	public class NdiMedia : ModuleRules
	{
		public NdiMedia(ReadOnlyTargetRules Target) : base(Target)
		{
			PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

			DynamicallyLoadedModuleNames.AddRange(
				new string[] {
					"Media",
				});

			PrivateDependencyModuleNames.AddRange(
				new string[] {
					"Core",
					"CoreUObject",
					"MediaUtils",
					"NdiMediaFactory",
					"Networking",
					"Projects",
					"RenderCore",
				});

			PrivateIncludePathModuleNames.AddRange(
				new string[] {
					"Media",
				});

			PrivateIncludePaths.AddRange(
				new string[] {
					"NdiMedia/Private",
					"NdiMedia/Private/Assets",
					"NdiMedia/Private/Ndi",
					"NdiMedia/Private/Player",
					"NdiMedia/Private/Shared",
				});

			PublicDependencyModuleNames.AddRange(
				new string[] {
					"MediaAssets",
				});

			// add NDI libraries
			string NdiDir = Path.GetFullPath(Path.Combine(ModuleDirectory, "..", "..", "ThirdParty"));

			PrivateIncludePaths.Add(Path.Combine(NdiDir, "include"));

			if (Target.Platform == UnrealTargetPlatform.IOS)
			{
				string LibDir = Path.Combine(NdiDir, "lib", "apple", "iOS");
				string LibPath = Path.Combine(LibDir, "libndi_ios.a");

				PublicAdditionalLibraries.Add(LibPath);
			}
			else if (Target.Platform == UnrealTargetPlatform.Linux)
			{
				string LibDir = Path.Combine(NdiDir, "lib", "linux", "x86_64-linux-gnu");
				string DllPath = Path.Combine(LibDir, "libndi.so.3.0.11");

				PublicAdditionalLibraries.Add("stdc++");
				RuntimeDependencies.Add(DllPath);
			}
			else if (Target.Platform == UnrealTargetPlatform.Mac)
			{
				string LibDir = Path.Combine(NdiDir, "lib", "apple", "x64");
				string DllPath = Path.Combine(LibDir, "libndi.3.dylib");

				PublicLibraryPaths.Add(LibDir);
				PublicAdditionalLibraries.Add(DllPath);
				PublicDelayLoadDLLs.Add(DllPath);
				RuntimeDependencies.Add(DllPath);
			}
			else if (Target.Platform == UnrealTargetPlatform.Win32)
			{
				string LibDir = Path.Combine(NdiDir, "lib", "windows", "x86");
				string DllPath = Path.Combine(LibDir, "Processing.NDI.Lib.x86.dll");

				PublicLibraryPaths.Add(LibDir);
				PublicAdditionalLibraries.Add("Processing.NDI.Lib.x86.lib");
				PublicDelayLoadDLLs.Add("Processing.NDI.Lib.x86.dll");
				RuntimeDependencies.Add(DllPath);
			}
			else if (Target.Platform == UnrealTargetPlatform.Win64)
			{
				string LibDir = Path.Combine(NdiDir, "lib", "windows", "x64");
				string DllPath = Path.Combine(LibDir, "Processing.NDI.Lib.x64.dll");

				PublicLibraryPaths.Add(LibDir);
				PublicAdditionalLibraries.Add("Processing.NDI.Lib.x64.lib");
				PublicDelayLoadDLLs.Add("Processing.NDI.Lib.x64.dll");
				RuntimeDependencies.Add(DllPath);
			}
			else
			{
				System.Console.WriteLine("NDI SDK does not supported this platform");
			}
		}
	}
}
