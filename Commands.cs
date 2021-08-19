using System;

namespace FEVSF
{
    public class Commands
    {
        public enum Direction
        {
            UP = 4,
            DOWN = 1,
            LEFT = 2,
            RIGHT = 3,
            REVERSED = 16
        }

        public enum MovementMode
        {
            WALK = 13,
            RUN = 14,
            BACKWARD = 16
        }

        public enum Bubble
        {
            EXCLAMATION = 1,
            QUESTION = 2,
            SMILE = 3,
            SWEAT = 4,
            IDEA = 5,
            MUSIC = 6,
            HEART = 7,
            BLUSH = 8,
            SLEEP = 9,
            THINK = 10,
            CONFUSED = 11,
            ANGER = 12
        }

        public enum Weather
        {
            NONE = 0,
            DARKNESS = 1,
            FOG = 2,
            REDFOG = 3,
            SNOW = 4,
            CHERRY = 5,
            HEAT = 6,
            SPIRITS = 7,
            ORBS = 8,
            RAIN = 9
        }

        public static int[] all(string arg0, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6,
            string arg7, string arg8, string arg9)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = int.Parse(arg0);
            command[1] = int.Parse(arg1);
            command[2] = int.Parse(arg2);
            command[3] = int.Parse(arg3);
            command[4] = int.Parse(arg4);
            command[5] = int.Parse(arg5);
            command[6] = int.Parse(arg6);
            command[7] = int.Parse(arg7);
            command[8] = int.Parse(arg8);
            command[9] = int.Parse(arg9);
            return command;
        }

        public static int[] stop(string argVide)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            return command;
        }

        public static int[] turnP(string direction, string num)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Direction arg1 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 1; // 1
            command[1] = 2; // 3
            command[2] = (int)arg1; // 5
            command[3] = int.Parse(num); // 7
            return command;
        }

        public static int[] emoteP(string num, string bubble)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Bubble arg2 = (Bubble)Enum.Parse(typeof(Bubble), bubble); // Get the enum type from string
            command[0] = 1; // 1
            command[1] = 5; // 3
            command[3] = int.Parse(num); // 7
            command[5] = (int)arg2; // 11
            return command;
        }

        public static int[] jumpP(string num)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 1; // 1
            command[1] = 7; // 3
            command[3] = int.Parse(num); // 7
            return command;
        }

        public static int[] moveP(string movementMode, string direction, string distance)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            MovementMode arg1 = (MovementMode)Enum.Parse(typeof(MovementMode), movementMode); // Get the enum type from string
            Direction arg2 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 1; // 1
            command[1] = (int)arg1; // 3
            command[2] = (int)arg2; // 5
            command[3] = int.Parse(distance); // 7
            return command;
        }

        public static int[] turnE(string direction, string num, string eventID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Direction arg1 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 2; // 1
            command[1] = 2; // 3
            command[2] = (int)arg1; // 5
            command[3] = int.Parse(num); // 7
            command[4] = int.Parse(eventID); // 9
            return command;
        }

        public static int[] emoteE(string num, string bubble, string eventID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Bubble arg2 = (Bubble)Enum.Parse(typeof(Bubble), bubble); // Get the enum type from string
            command[0] = 2; // 1
            command[1] = 5; // 3
            command[3] = int.Parse(num); // 7
            command[4] = int.Parse(eventID); // 9
            command[5] = (int)arg2; // 11
            return command;
        }

        public static int[] jumpE(string num, string eventID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 2; // 1
            command[1] = 7; // 3
            command[3] = int.Parse(num); // 7
            command[4] = int.Parse(eventID); // 9
            return command;
        }
        public static int[] moveE(string movementMode, string direction, string distance, string eventID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            MovementMode arg1 = (MovementMode)Enum.Parse(typeof(MovementMode), movementMode); // Get the enum type from string
            Direction arg2 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 2; // 1
            command[1] = (int)arg1; // 3
            command[2] = (int)arg2; // 5
            command[3] = int.Parse(distance); // 7
            command[4] = int.Parse(eventID); // 9
            return command;
        }

        public static int[] camera(string direction, string distance)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Direction arg1 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 3; // 1
            command[1] = (int)arg1; // 3
            command[3] = int.Parse(distance); // 7
            return command;
        }

        public static int[] warp(string mapID, string eventID, string direction)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Direction arg1 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 4; // 1
            command[1] = int.Parse(mapID); // 3
            command[2] = int.Parse(eventID); // 5
            command[3] = (int)arg1; // 7
            return command;
        }

        public static int[] txt(string ID, string choice, string jump)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 5; // 1
            command[1] = int.Parse(ID); // 3
            command[2] = Convert.ToInt32(bool.Parse(choice)); // 5
            command[3] = int.Parse(jump); // 7
            return command;
        }

        public static int[] setFlag(string flag, string IO)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 6; // 1
            command[1] = int.Parse(flag); // 3
            command[2] = Convert.ToInt32(bool.Parse(IO)); // 5
            return command;
        }

        public static int[] loadEvent(string flag, string ID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 7; // 1
            command[1] = int.Parse(flag); // 3
            command[2] = int.Parse(ID); // 5
            return command;
        }

        public static int[] EDObject(string mapID, string eventID, string IO)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 8; // 1
            command[1] = int.Parse(mapID); // 3
            command[2] = int.Parse(eventID); // 5
            command[3] = Convert.ToInt32(bool.Parse(IO)); // 7
            return command;
        }

        public static int[] checkFlag(string flag, string skip)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 9; // 1
            command[1] = int.Parse(flag); // 3
            command[2] = int.Parse(skip); // 5
            return command;
        }

        public static int[] shaderA(string color, string opacity, string time)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 10; // 1
            command[1] = int.Parse(color); // 3
            command[2] = int.Parse(opacity); // 5
            command[3] = int.Parse(time); // 7
            return command;
        }

        public static int[] shaderB(string image, string time, string opacity)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 11; // 1
            command[1] = int.Parse(image); // 3
            command[2] = int.Parse(time); // 5
            command[3] = int.Parse(opacity); // 7
            return command;
        }

        public static int[] battle(string puppetID, string style, string level)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 12; // 1
            command[1] = int.Parse(puppetID); // 3
            command[2] = 33; // 5
            command[3] = 33; // 7
            command[4] = int.Parse(style); // 9
            command[5] = int.Parse(level); // 11
            return command;
        }

        public static int[] trainer(string trainerID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 13; // 1
            command[1] = int.Parse(trainerID); // 3
            command[2] = 33; // 5
            command[3] = 33; // 7
            return command;
        }

        public static int[] eTrainer(string trainerID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 15; // 1
            command[1] = int.Parse(trainerID); // 3
            return command;
        }

        public static int[] name(string IO)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 16; // 1
            command[1] = Convert.ToInt32(bool.Parse(IO)); // 3
            return command;
        }

        public static int[] gender(string argVide)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 17; // 1
            return command;
        }

        public static int[] choosePuppet(string arg1)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 18; // 1
            command[1] = int.Parse(arg1); // 3
            return command;
        }

        public static int[] givePuppet(string puppetID, string style, string level, string num)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 19; // 1
            command[1] = int.Parse(puppetID); // 3
            command[2] = int.Parse(style); // 5
            command[3] = int.Parse(level); // 7
            command[4] = int.Parse(num); // 9
            command[5] = 33; // 11
            return command;
        }

        public static int[] giveItem(string itemID, string num)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 20; // 1
            command[1] = int.Parse(itemID); // 3
            command[2] = int.Parse(num); // 5
            command[3] = 34; // 7
            return command;
        }

        public static int[] removeItem(string itemID, string num)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 21; // 1
            command[1] = int.Parse(itemID); // 3
            command[2] = int.Parse(num); // 5
            return command;
        }

        public static int[] testItem(string itemID, string num, string jump)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 22; // 1
            command[1] = int.Parse(itemID); // 3
            command[2] = int.Parse(num); // 5
            command[3] = int.Parse(jump); // 7
            return command;
        }

        public static int[] music(string musicID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 23; // 1
            command[1] = int.Parse(musicID); // 3
            return command;
        }

        public static int[] se(string seID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 24; // 1
            command[1] = int.Parse(seID); // 3
            return command;
        }

        public static int[] box(string argVide)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 25; // 1
            return command;
        }

        public static int[] shop(string shopID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 28; // 1
            command[1] = int.Parse(shopID); // 3
            return command;
        }

        public static int[] heal(string mode)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 29; // 1
            command[1] = Convert.ToInt32(bool.Parse(mode) ? 2 : 1); // 3
            return command;
        }

        public static int[] testBook(string num, string jump)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 31; // 1
            command[1] = int.Parse(num); // 3
            command[2] = int.Parse(jump); // 5
            return command;
        }

        public static int[] warpNPC(string eventID, string x, string y)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 32; // 1
            command[1] = int.Parse(eventID); // 3
            command[2] = int.Parse(x); // 5
            command[3] = int.Parse(y); // 7
            command[4] = 2; // 9
            return command;
        }

        public static int[] fullTxt(string txtID, string arg2)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 33; // 1
            command[1] = int.Parse(txtID); // 3
            command[2] = int.Parse(arg2); // 5
            return command;
        }

        public static int[] pvp(string argVide)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 34; // 1
            return command;
        }

        public static int[] releasePuppet(string argVide)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 41; // 1
            return command;
        }

        public static int[] displayMoney(string argVide)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 43; // 1
            return command;
        }

        public static int[] testMoney(string num, string jump)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 44; // 1
            command[1] = int.Parse(num); // 3
            command[2] = int.Parse(jump); // 5
            return command;
        }

        public static int[] removeMoney(string num)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 45; // 1
            command[1] = int.Parse(num); // 3
            return command;
        }

        public static int[] listGet(string txtID, string listID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 47; // 1
            command[1] = int.Parse(txtID); // 3
            command[2] = int.Parse(listID); // 5
            command[4] = 1; // 9
            return command;
        }

        public static int[] lockPuppetMov(string IO)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 48; // 1
            command[1] = Convert.ToInt32(bool.Parse(IO)); // 3
            return command;
        }

        public static int[] steps(string num, string eventID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 50; // 1
            command[1] = int.Parse(num); // 3
            command[2] = int.Parse(eventID); // 5
            return command;
        }

        public static int[] cSteps(string argVide)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 51; // 1
            return command;
        }

        public static int[] weather(string mapID, string weatherID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Weather arg1 = (Weather)Enum.Parse(typeof(Weather), weatherID); // Get the enum type from string
            command[0] = 59; // 1
            command[1] = int.Parse(mapID); // 3
            command[2] = (int)arg1; // 5
            return command;
        }

        public static int[] getWinStreakTournament(string argVide)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 63; // 1
            return command;
        }

        public static int[] autoSave(string arg1)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 65; // 1
            command[1] = int.Parse(arg1); // 3
            return command;
        }

        public static int[] noWaitMoveP(string movementMode, string direction, string distance)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            MovementMode arg1 = (MovementMode)Enum.Parse(typeof(MovementMode), movementMode); // Get the enum type from string
            Direction arg2 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 257; // 1
            command[1] = (int)arg1; // 3
            command[2] = (int)arg2; // 5
            command[3] = int.Parse(distance); // 7
            return command;
        }

        public static int[] noWaitMoveE(string movementMode, string direction, string distance, string eventID)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            MovementMode arg1 = (MovementMode)Enum.Parse(typeof(MovementMode), movementMode); // Get the enum type from string
            Direction arg2 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 258; // 1
            command[1] = (int)arg1; // 3
            command[2] = (int)arg2; // 5
            command[3] = int.Parse(distance); // 7
            command[4] = int.Parse(eventID); // 9
            return command;
        }

        public static int[] noWaitCamera(string direction, string distance)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Direction arg1 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 259; // 1
            command[1] = (int)arg1; // 3
            command[3] = int.Parse(distance); // 7
            return command;
        }

        public static int[] noWaitWarp(string mapID, string eventID, string direction)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Direction arg1 = (Direction)Enum.Parse(typeof(Direction), direction); // Get the enum type from string
            command[0] = 260; // 1
            command[1] = int.Parse(mapID); // 3
            command[2] = int.Parse(eventID); // 5
            command[3] = (int)arg1; // 7
            return command;
        }

        public static int[] txt2(string ID, string choice, string jump)
        {
            int[] command = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            command[0] = 261; // 1
            command[1] = int.Parse(ID); // 3
            command[2] = Convert.ToInt32(bool.Parse(choice)); // 5
            command[3] = int.Parse(jump); // 7
            return command;
        }
    }
}