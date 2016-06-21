﻿using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QuincyGameEnginePractice.EngineCode;
using QuincyGameEnginePractice.GameScripts;
using QuincyGameEnginePractice.EngineCode.Ui;

namespace QuincyGameEnginePractice.Scenes
{
	public class BallPitLevel : Scene
	{
		InputHandler input;

		/// <summary>
		/// this shit doesnt work properly
		/// </summary>
		TileMap tileMap;

		Wall[] walls;

		Floor floor;

		World world;

		FPSCounter fps;

		Label debugFps;

		SpriteFont orangeKid;

		/// <summary>
		/// 0.033333 = 30 times per second
		/// 0.016666 = 60 times per second
		/// 0.008888 = 120 times per second
		/// </summary>
		float fixedUpdate = 0.01666f;

		float toUpdate;

		public override void LoadContent()
		{
			BackgroundColor = Color.CornflowerBlue;
			orangeKid = Global.Ref.Content.Load<SpriteFont>(Global.pipeline + "Fonts/orangeKid");
		}

		public override void Start()
		{
			input = new InputHandler();
			tileMap = new TileMap(80, 45);
			world = new World(new Vector2(0f, 9.8f));
			walls = new Wall[2];
			floor = new Floor(world, ScreenArea);
			walls[0] = new Wall(world, ScreenArea, 0);
			walls[1] = new Wall(world, ScreenArea, 2);
			fps = new FPSCounter();
			debugFps = Label.CreateLabel(orangeKid);
			for(int i = 0; i < 10; i++)
			{
				var b = new Block(world);
			}
		}

		public override void Update(GameTime gameTime)
		{
			if(Global.Ref.IsActive)
			{
				debugFps.SetText($"FPS: {fps.GetCurrentFPS()}\nBlocks: {GetComponents.components.Count()}");
				var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
				if(toUpdate > fixedUpdate)
				{
					world.Step(fixedUpdate);
					toUpdate = 0;
				}
				toUpdate += delta;
				if(InputHandler.KeyPressed(Keys.Escape))
					Global.Ref.Exit();
				if(InputHandler.KeyPressed(Keys.D2))
					SceneManager.ChangeScene("MainMenu");
				if(InputHandler.KeyPressed(Keys.R))
					SceneManager.ResetScene();
				if(InputHandler.MouseLeftClicked())
				{
					new Block(world).Start();
				}
			}
		}

		public override void Draw()
		{
			Clear();
			spriteBatch.Begin(samplerState: SamplerState.PointWrap);
			DrawStuff();
			spriteBatch.End();
		}

		public override void DrawUi()
		{
			spriteBatch.Begin();
			DrawUiStuff();
			spriteBatch.End();
		}

		public override void UnloadContent()
		{
			UnloadStuff();
		}
	}
}
