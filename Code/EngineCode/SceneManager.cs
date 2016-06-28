﻿using Microsoft.Xna.Framework;
using QEngine.EngineCode.Interfaces;
using QEngine.Scenes.PhysicsGame;
using QEngine.Scenes.TestScenes;

namespace QEngine.EngineCode
{
	/// <summary>
	/// static class that handles all the scenes in the game, right now you have to statically add everything in here for it to be seen as a gamescreen outside of here,
	///  but I want to make this a lot better in the future
	/// </summary>
	static class SceneManager
	{
		static QDictionary Scenes;

		static IScene CurrentScene;

		/// <summary>
		/// call this once to setup a new scenemanager for your instance of the game
		/// call again to remove all the scenes and restart all of them
		/// </summary>
		/// <returns>The scene manager.</returns>
		public static void NewSceneManager()
		{
			Scenes = new QDictionary();
			QDictionary.Add("MainMenu", new PhizzleLevelOne());
			QDictionary.Add("Options", new Options());
			QDictionary.Add("Test", new BallPitLevel());
			ChangeScene("MainMenu");
		}

		/// <summary>
		/// get the current scene from anywhere
		/// </summary>
		/// <returns>The scene.</returns>
		public static IScene GetScene()
		{
			return CurrentScene;
		}

		/// <summary>
		/// change the current scene to another scene that you have for you levels
		/// </summary>
		/// <returns>The scene.</returns>
		/// <param name="scene">Scene.</param>
		public static void ChangeScene(string scene)
		{
			if(CurrentScene == null)
			{
				CurrentScene = QDictionary.ChangeScene(scene);
				LoadContent();
				Start();
			}
			else
			{
				if(GetScene().SceneName != scene)
				{
					CurrentScene.UnloadContent();
					//Reload the scene
					//http://stackoverflow.com/questions/840261/passing-arguments-to-c-sharp-generic-new-of-templated-type
					//REEEEEFLECTION
					CurrentScene = QDictionary.ChangeScene(scene);// = (T)Activator.CreateInstance(typeof(T));
					LoadContent();
					Start();
				}
				else
					ResetScene();
			}
		}

		/// <summary>
		/// resets the current scene
		/// </summary>
		/// <returns>The scene.</returns>
		public static void ResetScene()
		{
			UnloadContent();
			LoadContent();
			Start();
		}

		/// <summary>
		/// calls the scenes loadcontent method that inits componentManager and a spriteBatch
		/// </summary>
		/// <returns>The content.</returns>
		static void LoadContent()
		{
			CurrentScene.OnLoadContent();
		}

		/// <summary>
		/// calls the start function in the scene that starts all the objects in the scene
		/// </summary>
		static void Start()
		{
			CurrentScene.OnStart();
		}

		/// <summary>
		/// gets called by xna so you dont need to call this
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		public static void Update(GameTime gameTime)
		{
			CurrentScene.OnUpdate(gameTime);
		}

		/// <summary>
		/// gets called by xna so you also dont need to call this
		/// </summary>
		public static void Draw()
		{
			CurrentScene.OnDraw();
			CurrentScene.OnDrawUi();
		}

		/// <summary>
		/// unload all the content in the scene and then can load a new scene into memory
		/// </summary>
		/// <returns>The content.</returns>
		static void UnloadContent()
		{
			CurrentScene.OnUnloadContent();
		}

		/// <summary>
		/// this gets called automatically by xna after everything else is disposed then the game closes
		/// </summary>
		/// <returns>The unload content.</returns>
		public static void GlobalUnloadContent()
		{
			UnloadContent();
			Global.Ref.Content.Dispose();
		}
	}
}
