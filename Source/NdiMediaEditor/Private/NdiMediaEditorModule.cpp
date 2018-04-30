// Copyright 1998-2017 Epic Games, Inc. All Rights Reserved.

#include "Modules/ModuleInterface.h"
#include "Modules/ModuleManager.h"
#include "NdiMediaFinder.h"
#include "NdiMediaFinderCustomization.h"
#include "NdiMediaSource.h"
#include "NdiMediaSourceCustomization.h"
#include "PropertyEditorModule.h"


/**
 * Implements the NdiMediaEditor module.
 */
class FNdiMediaEditorModule
	: public IModuleInterface
{
public:

	//~ IModuleInterface interface

	virtual void StartupModule() override
	{
		RegisterCustomizations();
	}

	virtual void ShutdownModule() override
	{
		UnregisterCustomizations();
	}

protected:

	/** Register details view customizations. */
	void RegisterCustomizations()
	{
		NdiMediaFinderName = UNdiMediaFinder::StaticClass()->GetFName();
		NdiMediaSourceName = UNdiMediaSource::StaticClass()->GetFName();

		FPropertyEditorModule& PropertyModule = FModuleManager::LoadModuleChecked<FPropertyEditorModule>("PropertyEditor");
		{
			PropertyModule.RegisterCustomClassLayout(NdiMediaFinderName, FOnGetDetailCustomizationInstance::CreateStatic(&FNdiMediaFinderCustomization::MakeInstance));
			PropertyModule.RegisterCustomClassLayout(NdiMediaSourceName, FOnGetDetailCustomizationInstance::CreateStatic(&FNdiMediaSourceCustomization::MakeInstance));
		}
	}

	/** Unregister details view customizations. */
	void UnregisterCustomizations()
	{
		FPropertyEditorModule& PropertyModule = FModuleManager::LoadModuleChecked<FPropertyEditorModule>("PropertyEditor");
		{
			PropertyModule.UnregisterCustomClassLayout(NdiMediaSourceName);
			PropertyModule.UnregisterCustomClassLayout(NdiMediaFinderName);
		}
	}

private:

	/** Class names. */
	FName NdiMediaFinderName;
	FName NdiMediaSourceName;
};


IMPLEMENT_MODULE(FNdiMediaEditorModule, NdiMediaEditor);
