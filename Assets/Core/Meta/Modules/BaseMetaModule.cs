﻿using Core.App;
using Core.Meta.App;
using Core.Meta.UI.App;
using UnityEngine;

namespace Core.Meta.Modules {
	[CreateAssetMenu(menuName = "App/Modules/BaseMetaModule", fileName = "BaseMetaModule")]
	public class BaseMetaModule : AppModule {
		[SerializeField]
		private UIInitializerService.Settings _uiInitSettings;
		
		public override void OnInitialize (AppComponentRegistry componentRegistry) {
			componentRegistry
				.Instantiate<UIInitializerService>(_uiInitSettings)
				.Initialize(componentRegistry);
			
			componentRegistry.Instantiate<UIScreenService>();
		}
	}
}
