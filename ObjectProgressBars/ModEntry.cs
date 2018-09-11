using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System.Collections.Generic;

namespace ObjectProgressBars
{
	public class ModEntry : Mod
	{

		private static readonly string STONE = "Stone";
		private static readonly string INCUBATOR = "Incubator";
		private static readonly int COOPMASTER = 2;

		private Configuration config;
		private bool showing = true;

		private bool debug_printed = false;

		private static readonly Dictionary<string, int> MACHINE_TIMES = new Dictionary<string, int>();
		static ModEntry() {
			MACHINE_TIMES.Add("Bee House", 6100);
			MACHINE_TIMES.Add("Cheese Press", 200);
			MACHINE_TIMES.Add("Loom", 240);
			MACHINE_TIMES.Add("Mayonnaise Machine", 180);
			MACHINE_TIMES.Add("Preserves Jar", 4000);
			MACHINE_TIMES.Add("Charcoal Kiln", 30);
			MACHINE_TIMES.Add("Recycling Machine", 60);
			MACHINE_TIMES.Add("Seed Maker", 20);
			MACHINE_TIMES.Add("Slime Egg-Press", 1260);
			MACHINE_TIMES.Add("Lightning Rod ", 24 * 60);

			MACHINE_TIMES.Add("Keg_Beer", 2250);
			MACHINE_TIMES.Add("Keg_Pale Ale", 2360);
			MACHINE_TIMES.Add("Keg_Wine", 10000);
			MACHINE_TIMES.Add("Keg_Juice", 6000);
			MACHINE_TIMES.Add("Keg_Mead", 600);
			MACHINE_TIMES.Add("Keg_Coffee", 120);

			MACHINE_TIMES.Add("Crystalarium_Diamond", 5 * 24 * 60);
			MACHINE_TIMES.Add("Crystalarium_Star Shards", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Helvite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Neptunite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Fire Opal", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Bixite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Dolomite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Tigerseye", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Emerald", 2 * 24 * 60 + 2 * 60);
			MACHINE_TIMES.Add("Crystalarium_Ruby", 2 * 24 * 60 + 2 * 60);
			MACHINE_TIMES.Add("Crystalarium_Kyanite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Fairy Stone", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Ocean Stone", 3 * 24 * 60 + 11 * 60 + 20);         
			MACHINE_TIMES.Add("Crystalarium_Jade", 1 * 24 * 60 + 16 * 60    );
			MACHINE_TIMES.Add("Crystalarium_Fluorapatite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Lunarite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Lemon Stone", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Ghost Crystal", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Obsidian", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Aquamarine", 1 * 24 * 60 + 13 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Basalt", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Opal", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Alamite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Geminite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Jamborite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Jasper", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Hematite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Aerinite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Celestine", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Petrified Slime", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Pyrite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Soapstone", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Jagoite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Marble", 3 * 24 * 60 + 11 * 60 + 20);         
			MACHINE_TIMES.Add("Crystalarium_Amethyst", 22 * 60 + 40);
			MACHINE_TIMES.Add("Crystalarium_Fire Quartz", 21 * 60 + 40);
			MACHINE_TIMES.Add("Crystalarium_Esperite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Malachite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Thunder Egg", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Slate", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Topaz", 18 * 60 + 40);
			MACHINE_TIMES.Add("Crystalarium_Nekoite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Orpiment", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Frozen Tear", 18 * 60 + 40);
			MACHINE_TIMES.Add("Crystalarium_Calcite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Granite", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Sandstone", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Earth Crystal", 13 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Baryte", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Quartz", 7 * 60);
			MACHINE_TIMES.Add("Crystalarium_Mudstone", 3 * 24 * 60 + 11 * 60 + 20);
			MACHINE_TIMES.Add("Crystalarium_Limestone", 3 * 24 * 60 + 11 * 60 + 20);
			         
			MACHINE_TIMES.Add("Tapper_Maple Syrup", 8 * 24 * 60 + 12);
			MACHINE_TIMES.Add("Tapper_Oak Resin", 7 * 24 * 60 + 12);
			MACHINE_TIMES.Add("Tapper_Pine Tar", 5 * 24 * 60 + 12);

			MACHINE_TIMES.Add("Furnace_Copper Bar", 30);
			MACHINE_TIMES.Add("Furnace_Iron Bar", 120);
			MACHINE_TIMES.Add("Furnace_Gold Bar", 300);
			MACHINE_TIMES.Add("Furnace_Refined Quartz", 90);
			MACHINE_TIMES.Add("Furnace_Iridium Bar", 480);

			MACHINE_TIMES.Add("Incubator_Large Egg", 9000);
			MACHINE_TIMES.Add("Incubator_Void Egg", 9000);
			MACHINE_TIMES.Add("Incubator_Duck Egg", 9000);
			MACHINE_TIMES.Add("Incubator_Dinosaur Egg", 18000);
			MACHINE_TIMES.Add("Slime Incubator_Slime Egg", 4000);

			MACHINE_TIMES.Add("Machine_", 20);

		}

		private static Dictionary<string, int> GUESSED_MACHINE_TIMES = new Dictionary<string, int>();

  
        public override void Entry(IModHelper helper)
        {
			this.config = this.Helper.ReadConfig<Configuration>();
			InputEvents.ButtonPressed += this.InputEvents_ButtonPressed;
			GraphicsEvents.OnPreRenderHudEvent += this.Draw;
        }


        private void InputEvents_ButtonPressed(object sender, EventArgsInput e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

			if (e.Button.Equals(config.ToggleDisplay.key.ToSButton())) {
				this.showing = !this.showing;
				debug_printed = false;
			}
        }

		private void Draw(object sender, EventArgs args)
        {
			if (this.showing && Context.IsWorldReady && Context.IsPlayerFree)
            {
                SpriteBatch spriteBatch = Game1.spriteBatch;
				foreach (Vector2 item in (IEnumerable<Vector2>)(Game1.currentLocation.netObjects).Keys) {
					StardewValley.Object gameObject = Game1.currentLocation.netObjects[item];
					if (gameObject.MinutesUntilReady > 0 && gameObject.MinutesUntilReady != 999999) {

						if (gameObject.Name.Equals (STONE)) {
							continue;
						}

						if (!debug_printed) {
							
						}
                        
                        float x = item.X;
                        float y = item.Y;
                        Vector2 val2 = Game1.GlobalToLocal(Game1.viewport, new Vector2(x * 64f, y * 64f));
                        x = val2.X;
                        y = val2.Y;

						x += 12; // adjusting the position a bit

						float percentage = 0;

						if (MACHINE_TIMES.ContainsKey(gameObject.Name)) {   // check if a predetermined time is stored for this machine
							float totalMachineTime = MACHINE_TIMES[gameObject.Name] / 10.0f;
							percentage = (totalMachineTime - gameObject.MinutesUntilReady / 10.0f) / totalMachineTime;
                        
						} else if (MACHINE_TIMES.ContainsKeyPattern(gameObject.Name + "_" + ((StardewValley.Object)gameObject.heldObject).Name)) {  // check if a predetermined time is stored for this machine and the item it holds
							float totalMachineTime = MACHINE_TIMES.GetItemByKeyPattern(gameObject.Name 
                            + "_" + ((StardewValley.Object)gameObject.heldObject).Name) / 10.0f;

							if (gameObject.Name.Contains(INCUBATOR) && Game1.player.professions.Contains(COOPMASTER)) {
								totalMachineTime = totalMachineTime / 2.0f;
							}

							percentage = (totalMachineTime - gameObject.MinutesUntilReady / 10.0f) / totalMachineTime;

						} else if (GUESSED_MACHINE_TIMES.ContainsKey(gameObject.Name + "_" + gameObject.TileLocation)) {    // check if a guessed time is stored for this machine at the current tile location 
							// (location used as a unique identifier, maybe problemtic if the same location is occupied in a different map)

							float totalMachineTime = GUESSED_MACHINE_TIMES[gameObject.Name + "_" + gameObject.TileLocation] / 10.0f;
       
							percentage = (totalMachineTime - gameObject.MinutesUntilReady / 10.0f) / totalMachineTime;

						} else {    // save a guessed time for this machine at this location
							GUESSED_MACHINE_TIMES.Add (gameObject.Name + "_" + 
							                           gameObject.TileLocation, gameObject.MinutesUntilReady);
							//this.Monitor.Log(gameObject.Name + "_" + gameObject.TileLocation);
						}

						if (percentage > 1) {   // don't show progress bar for 100 or more percentage
							continue;
						}

                        ((SpriteBatch)spriteBatch).Draw(Game1.staminaRect, new Rectangle((int)x - 6, (int)y - 6, 52, 20), (Rectangle)((Texture2D)Game1.staminaRect).Bounds, new Color(0.357f, 0.169f, 0.165f), 0f, Vector2.Zero, (SpriteEffects)0, 0.887f);
						((SpriteBatch)spriteBatch).Draw(Game1.staminaRect, new Rectangle((int)x - 4, (int)y - 4, 48, 16), (Rectangle)((Texture2D)Game1.staminaRect).Bounds, new Color(0.863f, 0.482f, 0.02f), 0f, Vector2.Zero, (SpriteEffects)0, 0.887f);
						((SpriteBatch)spriteBatch).Draw(Game1.staminaRect, new Rectangle((int)x - 2, (int)y - 2, 44, 12), (Rectangle)((Texture2D)Game1.staminaRect).Bounds, new Color(0.694f, 0.306f, 0.02f), 0f, Vector2.Zero, (SpriteEffects)0, 0.887f);
						((SpriteBatch)spriteBatch).Draw(Game1.staminaRect, new Rectangle((int)x, (int)y, 40, 8), (Rectangle)((Texture2D)Game1.staminaRect).Bounds, new Color (1.0f, 0.843f, 0.537f), 0f, Vector2.Zero, (SpriteEffects)0, 0.887f);
						((SpriteBatch)spriteBatch).Draw(Game1.staminaRect, new Rectangle((int) x, (int) y, (int)(40f * percentage), 8), (Rectangle)((Texture2D) Game1.staminaRect).Bounds, Utility.getRedToGreenLerpColor(percentage), 0f, Vector2.Zero, (SpriteEffects) 0, 0.887f);
						((SpriteBatch)spriteBatch).Draw(Game1.staminaRect, new Rectangle((int)x + (int)(36f * percentage), (int)y, 4, 8), (Rectangle)((Texture2D)Game1.staminaRect).Bounds, Utility.getRedToGreenLerpColor(percentage), 0f, Vector2.Zero, (SpriteEffects)0, 0.887f);
						((SpriteBatch)spriteBatch).Draw(Game1.staminaRect, new Rectangle((int)x + (int)(36f * percentage), (int)y, 4, 8), (Rectangle)((Texture2D)Game1.staminaRect).Bounds, new Color(Color.Black, 0.2f), 0f, Vector2.Zero, SpriteEffects.None, 0.887f);
                                          
					} else if (gameObject.MinutesUntilReady == 0) { // remove from guessed times
						if (!string.Equals($"{gameObject.heldObject}", "null")) {
							GUESSED_MACHINE_TIMES.Remove(gameObject.Name + "_" + gameObject.TileLocation);
						}
					}
                }
            }

			debug_printed = true;
        }

    }
}