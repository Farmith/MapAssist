using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapAssist.Types;
using MapAssist.Helpers;
using MapAssist.Structs;
using System.Configuration;

namespace MapAssist
{
    public partial class DebugWindow : Form
    {
        private GameData _currentGameData;
        public char DebugKey = Convert.ToChar(ConfigurationManager.AppSettings["DebugKey"]);
        private string appendItems;
        public DebugWindow()
        {
            InitializeComponent();
            appendItems = "";
        }
        public void DoSomething()
        {
            if(_currentGameData.PlayerName.CompareTo("Apocamancer") != 0) // TODO: Move this to config?
            {
                appendItems += "\r\nFollowing Apocamancer!";
                foreach(Players player in _currentGameData.Players)
                {
                    if(player.PlayerName.CompareTo("Apocamancer") == 0)
                    {
                        appendItems += "\r\nMoving closer to: " + player.PlayerPath.DynamicX + ":" + player.PlayerPath.DynamicY;
                    }
                }
            }
        }
        public void UpdateGameData(GameData gameData)
        {
            _currentGameData = gameData;
            try
            {
                playerPosX.Text = gameData.PlayerPosition.X.ToString();
                playerPosY.Text = gameData.PlayerPosition.Y.ToString();
                playerName.Text = gameData.PlayerName;
                playerArea.Text = gameData.Area.ToString();
                playerDifficulty.Text = gameData.Difficulty.ToString();
                // Clear entities
                entityList.Items.Clear();
                foreach(Players player in gameData.Players)
                {
                    var item = "Name: " + player.PlayerName + " UnitId: " + player.PlayerUnit.UnitId + " X:" + player.PlayerPath.DynamicX + " Y: " + player.PlayerPath.DynamicY;
                    entityList.Items.Add(item);
                }
                entityList.Items.Add(appendItems);

            } catch (Exception ex)
            {
                // Something didn't work, meh, who cares.
                entityList.Items.Add(ex.ToString());
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
