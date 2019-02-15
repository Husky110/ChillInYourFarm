﻿using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace ChillInYourFarmHouse
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {

        private string location = "";

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Player.Warped += PlayerEvents_Warped;
            helper.Events.GameLoop.TimeChanged += TimeEvents_TimeChanged;
            
        }

        /*********
        ** Private methods
        *********/


        private void PlayerEvents_Warped(object sender, WarpedEventArgs e)
        {
            if (Context.IsWorldReady)
            {
                this.location = e.NewLocation.Name;
            }
        }

        private void TimeEvents_TimeChanged(object sender, TimeChangedEventArgs e)
        {

            StardewValley.Farmer player = Game1.player;

            bool allowAccess = this.location == "FarmHouse" || this.location == "Cabin";

            if (Context.IsWorldReady == false || allowAccess == false || player.Stamina >= (float)player.MaxStamina)
            {
                return;
            }
            
            float currentPercentage = (player.Stamina / player.MaxStamina) * 100;
            float multiplicator = 0;

            
            if(currentPercentage < 20 || currentPercentage >= 80)
            {
                multiplicator = 0.05f;
            }
            else
            {
                multiplicator = 0.1f;
            }

            int staminaToGive = (int)Math.Round(player.MaxStamina * multiplicator, MidpointRounding.AwayFromZero);
            if((player.Stamina + staminaToGive) > player.MaxStamina)
            {
                staminaToGive = (int)Math.Floor(player.MaxStamina - player.stamina);
            }

            player.Stamina += staminaToGive;

        }


    }
}