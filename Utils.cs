using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FEVSF
{
    internal class Utils
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

        /// <summary>
        /// Transforme un fichier EVS en un fichier FEVS et l'enregistre selon son path dans le dossier local
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="finishedBytes"></param>
        public static void transformEVStoFEVS(string fileName, string[][] finishedBytes)
        {
            // Transformation de hex en int vers finishedInts
            int[][] finishedInts = new int[32][];
            for (int i = 0; i < finishedBytes.Length; i++)
            {
                finishedInts[i] = new int[10];
                for (int j = 0; j < finishedBytes[i].Length; j++)
                {
                    finishedInts[i][j] = Convert.ToInt32(finishedBytes[i][j], 16);
                }
            }

            string[] finishedStrings = new string[32];
            string filename = fileName;
            string[] file = filename.Split('.');
            string path = file[0] + ".fevs";

            // Gère les tableaux et les transforme en pseudo-code lisible
            using (FileStream fs = File.Create(path))
            {
                for (int i = 0; i < finishedInts.Length; i++)
                {
                    switch (finishedInts[i][0])
                    {
                        case 0:
                            finishedStrings[i] = stop(); break;
                        case 1: // Player Action
                            switch (finishedInts[i][1])
                            {
                                case 2:
                                    finishedStrings[i] = turnP(finishedInts[i]); break;
                                case 5:
                                    finishedStrings[i] = emoteP(finishedInts[i]); break;
                                case 7:
                                    finishedStrings[i] = jumpP(finishedInts[i]); break;
                                case 13:
                                case 14:
                                case 16:
                                    finishedStrings[i] = moveP(finishedInts[i]); break;
                                default:
                                    finishedStrings[i] = all(finishedInts[i]); break;
                            }
                            break;
                        case 2: // Event Action
                            switch (finishedInts[i][1])
                            {
                                case 2:
                                    finishedStrings[i] = turnE(finishedInts[i]); break;
                                case 5:
                                    finishedStrings[i] = emoteE(finishedInts[i]); break;
                                case 7:
                                    finishedStrings[i] = jumpE(finishedInts[i]); break;
                                case 13:
                                case 14:
                                case 16:
                                    finishedStrings[i] = moveE(finishedInts[i]); break;
                                default:
                                    finishedStrings[i] = all(finishedInts[i]); break;
                            }
                            break;
                        case 3:
                            finishedStrings[i] = camera(finishedInts[i]); break;
                        case 4:
                            finishedStrings[i] = warp(finishedInts[i]); break;
                        case 5:
                            finishedStrings[i] = txt(finishedInts[i]); break;
                        case 6:
                            finishedStrings[i] = setFlag(finishedInts[i]); break;
                        case 7:
                            finishedStrings[i] = loadEvent(finishedInts[i]); break;
                        case 8:
                            finishedStrings[i] = EDObject(finishedInts[i]); break;
                        case 9:
                            finishedStrings[i] = checkFlag(finishedInts[i]); break;
                        case 10:
                            finishedStrings[i] = shaderA(finishedInts[i]); break;
                        case 11:
                            finishedStrings[i] = shaderB(finishedInts[i]); break;
                        case 12:
                            finishedStrings[i] = battle(finishedInts[i]); break;
                        case 13:
                            finishedStrings[i] = trainer(finishedInts[i]); break;
                        case 15:
                            finishedStrings[i] = eTrainer(finishedInts[i]); break;
                        case 16:
                            finishedStrings[i] = name(finishedInts[i]); break;
                        case 17:
                            finishedStrings[i] = gender(); break;
                        case 18:
                            finishedStrings[i] = choosePuppet(finishedInts[i]); break;
                        case 19:
                            finishedStrings[i] = givePuppet(finishedInts[i]); break;
                        case 20:
                            finishedStrings[i] = giveItem(finishedInts[i]); break;
                        case 21:
                            finishedStrings[i] = removeItem(finishedInts[i]); break;
                        case 22:
                            finishedStrings[i] = testItem(finishedInts[i]); break;
                        case 23:
                            finishedStrings[i] = music(finishedInts[i]); break;
                        case 24:
                            finishedStrings[i] = se(finishedInts[i]); break;
                        case 25:
                            finishedStrings[i] = box(); break;
                        case 28:
                            finishedStrings[i] = shop(finishedInts[i]); break;
                        case 29:
                            finishedStrings[i] = heal(finishedInts[i]); break;
                        case 31:
                            finishedStrings[i] = testBook(finishedInts[i]); break;
                        case 32:
                            finishedStrings[i] = warpNPC(finishedInts[i]); break;
                        case 33:
                            finishedStrings[i] = fullTxt(finishedInts[i]); break;
                        case 34:
                            finishedStrings[i] = pvp(); break;
                        case 41:
                            finishedStrings[i] = releasePuppet(); break;
                        case 43:
                            finishedStrings[i] = displayMoney(); break;
                        case 44:
                            finishedStrings[i] = testMoney(finishedInts[i]); break;
                        case 45:
                            finishedStrings[i] = removeMoney(finishedInts[i]); break;
                        case 47:
                            finishedStrings[i] = listGet(finishedInts[i]); break;
                        case 48:
                            finishedStrings[i] = lockPuppetMov(finishedInts[i]); break;
                        case 50:
                            finishedStrings[i] = steps(finishedInts[i]); break;
                        case 51:
                            finishedStrings[i] = cSteps(); break;
                        case 59:
                            finishedStrings[i] = weather(finishedInts[i]); break;
                        case 63:
                            finishedStrings[i] = getWinStreakTournament(); break;
                        case 65:
                            finishedStrings[i] = autoSave(finishedInts[i]); break;
                        case 257:
                            finishedStrings[i] = noWaitMoveP(finishedInts[i]); break;
                        case 258:
                            finishedStrings[i] = noWaitMoveE(finishedInts[i]); break;
                        case 259:
                            finishedStrings[i] = noWaitCamera(finishedInts[i]); break;
                        case 260:
                            finishedStrings[i] = noWaitWarp(finishedInts[i]); break;
                        case 261:
                            finishedStrings[i] = txt2(finishedInts[i]); break;
                        default:
                            finishedStrings[i] = all(finishedInts[i]); break;
                    }
                    if (i == finishedInts.Length - 1)
                    {
                        char[] value = $"{finishedStrings[i]}".ToCharArray();
                        fs.Write(Encoding.UTF8.GetBytes(value), 0, value.Length);
                    }
                    else
                    {
                        char[] value = $"{finishedStrings[i]}\n".ToCharArray();
                        fs.Write(Encoding.UTF8.GetBytes(value), 0, value.Length);
                    }
                }
            }
        }

        /// <summary>
        /// Code taken from the dev-tools editor program by PhantomPilot and edited it a bit to fit my needs.
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="inlines"></param>
        public static void transformSEVStoEVS(string filepath, string[] inlines)
        {
            var buf = new byte[640];
            var lines = inlines;
            int pos = 0;

            int inbase = 16;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] fields = line.Split(',');
                for (int i = 0; i < 10; ++i)
                {
                    int index = (pos * 20) + (i * 2);
                    var val = BitConverter.GetBytes(Convert.ToUInt16(fields[i].Trim(), inbase));
                    buf[index] = val[0];
                    buf[index + 1] = val[1];
                }
                ++pos;
            }
            File.WriteAllBytes(filepath, buf);
        }

        /*
         * 1th byte = 0
         * 3rd byte = 1
         * 5th byte = 2
         * 7th byte = 3
         * 9th byte = 4
         * 11th byte = 5
         * 13th byte = 6
         * 15th byte = 7
         * 17th byte = 8
         * 19th byte = 9
         */

        private static string all(int[] ints)
        {
            string res;
            res = "all(";
            for (int i = 0; i < ints.Length; i++)
            {
                if (i == ints.Length - 1)
                {
                    res += $"{ints[i]})";
                }
                else
                {
                    res += $"{ints[i]}, ";
                }
            }
            return res;
        }
        private static string stop() => $"stop()";
        private static string turnP(int[] ints) => $"turnP({(Direction)ints[2]}, {ints[3]})";
        private static string emoteP(int[] ints) => $"emoteP({ints[3]}, {(Bubble)ints[5]})";
        private static string jumpP(int[] ints) => $"jumpP({ints[3]})";
        private static string moveP(int[] ints) => $"moveP({(MovementMode)ints[1]}, {(Direction)ints[2]}, {ints[7]})";
        private static string turnE(int[] ints) => $"turnE({(Direction)ints[2]}, {ints[3]}, {ints[4]})";
        private static string emoteE(int[] ints) => $"emoteE({ints[3]}, {(Bubble)ints[5]}, {ints[4]})";
        private static string jumpE(int[] ints) => $"jumpE({ints[3]}, {ints[4]})";
        private static string moveE(int[] ints) => $"moveE({(MovementMode)ints[1]}, {(Direction)ints[2]}, {ints[7]}, {ints[4]})";
        private static string camera(int[] ints) => $"camera({(Direction)ints[1]}, {ints[3]})";
        private static string warp(int[] ints) => $"warp({ints[1]}, {ints[2]}, {(Direction)ints[3]})";
        private static string txt(int[] ints) => $"txt({ints[1]}, {ints[2] != 0}, {ints[3]})";
        private static string setFlag(int[] ints) => $"setFlag({ints[1]}, {ints[2] != 0})";
        private static string loadEvent(int[] ints) => $"loadEvent({ints[1]}, {ints[2]})";
        private static string EDObject(int[] ints) => $"EDObject({ints[1]}, {ints[2]}, {ints[3] != 0})";
        private static string checkFlag(int[] ints) => $"checkFlag({ints[1]}, {ints[2]})";
        private static string shaderA(int[] ints) => $"shaderA({ints[1]}, {ints[2]}, {ints[3]})";
        private static string shaderB(int[] ints) => $"shaderB({ints[1]}, {ints[2]}, {ints[3]})";
        private static string battle(int[] ints) => $"battle({ints[1]}, {ints[4]}, {ints[5]})";
        private static string trainer(int[] ints) => $"trainer({ints[1]})";
        private static string eTrainer(int[] ints) => $"eTrainer({ints[1]})";
        private static string name(int[] ints) => $"name({ints[1] != 0})";
        private static string gender() => $"gender()";
        private static string choosePuppet(int[] ints) => $"choosePuppet({ints[1]})";
        private static string givePuppet(int[] ints) => $"givePuppet({ints[1]}, {ints[2]}, {ints[3]}, {ints[4]})";
        private static string giveItem(int[] ints) => $"giveItem({ints[1]}, {ints[2]})";
        private static string removeItem(int[] ints) => $"removeItem({ints[1]}, {ints[2]})";
        private static string testItem(int[] ints) => $"testItem({ints[1]}, {ints[2]}, {ints[3]})";
        private static string music(int[] ints) => $"music({ints[1]})";
        private static string se(int[] ints) => $"se({ints[1]})";
        private static string box() => $"box()";
        private static string shop(int[] ints) => $"shop({ints[1]})";
        private static string heal(int[] ints) => $"heal({ints[1] != 0})";
        private static string testBook(int[] ints) => $"testBook({ints[1]}, {ints[2]})";
        private static string warpNPC(int[] ints) => $"warpNPC({ints[1]}, {ints[2]}, {ints[3]})";
        private static string fullTxt(int[] ints) => $"fullTxt({ints[1]}, {ints[2]})";
        private static string pvp() => $"pvp()";
        private static string releasePuppet() => $"releasePuppet()";
        private static string displayMoney() => $"displayMoney()";
        private static string testMoney(int[] ints) => $"textMoney({ints[1]}, {ints[2]})";
        private static string removeMoney(int[] ints) => $"removeMoney({ints[1]})";
        private static string listGet(int[] ints) => $"listGet({ints[1]}, {ints[2]})";
        private static string lockPuppetMov(int[] ints) => $"lockPuppetMov({ints[1] != 0})";
        private static string steps(int[] ints) => $"steps({ints[1]}, {ints[2]})";
        private static string cSteps() => $"cSteps()";
        private static string weather(int[] ints) => $"weather({ints[1]}, {(Weather)ints[2]})";
        private static string getWinStreakTournament() => $"getWinStreakTournament()";
        private static string autoSave(int[] ints) => $"autoSave({ints[1]})";
        private static string noWaitMoveP(int[] ints) => $"noWaitMoveP({(MovementMode)ints[1]}, {(Direction)ints[2]}, {ints[7]})";
        private static string noWaitMoveE(int[] ints) => $"noWaitMoveE({(MovementMode)ints[1]}, {(Direction)ints[2]}, {ints[7]}, {ints[4]})";
        private static string noWaitCamera(int[] ints) => $"noWaitCamera({(Direction)ints[1]}, {ints[3]})";
        private static string noWaitWarp(int[] ints) => $"noWaitWarp({ints[1]}, {ints[2]}, {(Direction)ints[3]})";
        private static string txt2(int[] ints) => $"txt2({ints[1]}, {ints[2] != 0}, {ints[3]})";
    }
}